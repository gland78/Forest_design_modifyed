using Newtonsoft.Json;
using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WK.Libraries.BetterFolderBrowserNS;
using System.Security;
using System.Threading.Tasks.Dataflow;
using System.Text.RegularExpressions;
using System.Data.SQLite;

namespace WinFormsAppTest
{
    partial class MainForm
    {
        //변수 구조체   --> public 제외 나머지는 삭제예정
        public GUI gui = new GUI();

        //plotForm 열기 전 plot값을 저장할 임시 변수
        public GUI guiTemp = new GUI();

        //csv의 파라메터를 저장하기 위한 변수
        public List<List<string>> csv_data = new List<List<string>>();

        //exe파일이 위치한 기본 폴더와 그 내부 config 폴더들
        internal static string basePath = (Environment.CurrentDirectory).ToString();
        internal static string[] reqDi = { "", "RecentConfig", "PresetConfig" };
        public string csv_path = Path.Combine(basePath, "config.csv");

        //config 파일의 타입(preset recent default)
        public string fileType = "";

        //사용자 설정값 활성화 여부 판단 변수
        public int activatePreset = -1;

        //최근 작업 목록 활성화 여부 판단 변수
        public int activateRecent = -1;
        
        //db 파일 명
        public string bin_folder;
        public string databaseFileName;
        //테이블 이름 목록
        public string[] tablename = { "gui", "filters_crop", "filters_outlier", "filters_smrf", "filters_range_trunk", "filters_range_crown", "csp_segmentstem", "csp_segmentcrown", "measure" };

        //Circle형 plot값 csv->구조체로 뽑아내는 메서드
        void ExtractCircleValues(ref GUI guiStruct, string circleString)
        {
            string[] parts = circleString.Split(' ');

            foreach (string part in parts)
            {
                string[] keyValue = part.Split('=');

                if (keyValue.Length == 2)
                {
                    string key = keyValue[0];
                    string value = keyValue[1];
                    switch (key)
                    {
                        case "cx":
                            guiStruct.centerX = double.Parse(value);
                            break;
                        case "cy":
                            guiStruct.centerY = double.Parse(value);
                            break;
                        case "radius":
                            guiStruct.radius = double.Parse(value);
                            break;
                    }
                }
            }
        }
        //Rect형 plot값 csv->구조체로 뽑아내는 메서드
        void ExtractRectangleValues(ref GUI guiStruct,string rectangleString)
        {
            string[] parts = rectangleString.Split(' ');

            foreach (string part in parts)
            {
                string[] keyValue = part.Split('=');

                if (keyValue.Length == 2)
                {
                    string key = keyValue[0];
                    string value = keyValue[1];
                    switch (key)
                    {
                        case "xmin":
                            guiStruct.xMin = double.Parse(value);
                            break;
                        case "ymin":
                            guiStruct.yMin = double.Parse(value);
                            break;
                        case "xmax":
                            guiStruct.xMax = double.Parse(value);
                            break;
                        case "ymax":
                            guiStruct.yMax = double.Parse(value);
                            break;
                    }
                }
            }
        }
        
        

        // 무결성 검사를 위한 코드
        private void RegistTextBoxHandler()
        {
            //double형만 입력가능하도록 키 등록
            tbNorScalar.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbNorSlope.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbNorThres.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbTrunkMinHeight.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbTrunkMaxHeight.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbTrunkSmooth.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbCrownMinHeight.KeyPress += TextBox_KeyPressOnlyNumbers;


            //정수만 입력하도록 하는 핸들러
            tbNorCellSize.KeyPress += TextBox_KeyPress_OnlyInt;
            tbNorWinSize.KeyPress += TextBox_KeyPress_OnlyInt;
        }
        private void TextBox_KeyPressOnlyNumbers(object sender, KeyPressEventArgs e)
        {
            // 입력된 키가 숫자가 아니면서 Backspace(백스페이스)도 아닌 경우
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '-')
            {
                // 해당 키 입력 무시
                e.Handled = true;
            }
        }
        private void TextBox_KeyPress_OnlyInt(object sender, KeyPressEventArgs e)
        {
            // Allow only digits, backspace, and delete keys
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("정수만 입력 가능합니다!");
                e.Handled = true;
            }
        }

        //기본 config.csv 제외 나머지 config 파일을 만들기 위한 메서드
        //gui구조체의 plot값과 plot 리스트 값을 모두 모아 stringbuilder로 전달
        private void setAllparams(ref StringBuilder csvContent)
        {
            UpdateParams();
            csv_data[0][3] = $"cx={gui.centerX} cy={gui.centerY} radius={gui.radius}";
            csv_data[1][3] = $"xmin={gui.xMin} ymin={gui.yMin} xmax={gui.xMax} ymax={gui.yMax}";
            for (int i = 0; i < csv_data.Count; i++)
            {
                string str = "";
                for (int j = 0; j < csv_data[i].Count; j++)
                {
                    if (j == csv_data[i].Count - 1)
                    {
                        str += csv_data[i][j];
                        break;
                    }
                    str += csv_data[i][j] + ",";

                }
                csvContent.AppendLine(str);
            }
        }
        //기본 config.csv 제외 나머지 config 파일을 만드는 메서드(추후 default 부분 삭제 요망)
        private void MakeConfig(configFileType confType)
        {
            string filePath = Path.Combine(basePath, reqDi[(int)confType]);

            //해당 폴더 없을 시 만들기
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            //해당 폴더 내 config파일 갯수 확인
            string[] confCheck = Directory.GetFiles(Path.Combine(basePath, reqDi[(int)confType]), "*config*");

            string temp1 = csv_data[0][3], temp2 = csv_data[1][3];

            // CSV 내용 생성
            StringBuilder csvContent = new StringBuilder();

            string[] infoRecent = new string[] { };
            //config 파일 종류별 전처리(종류별로 필요한 데이터 추가)
            switch (confType)
            {
                case configFileType.Default:
                    filePath += @"\config.csv";
                    break;
                case configFileType.Recent:
                    Dictionary<string, double> plotData = plotSender();
                    
                    setAllparams(ref csvContent);
                    infoRecent = new string[] {
                        $"FileInfo,public,fileType,{confType},파일의 타입을 구분 (preset default recent).",
                        $"FileInfo,public,title,{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")},gui에 표시될 파일의 제목",
                        $"FileInfo,public,Lasfilename,{gui.loadPath},최근 사용한 LAS파일 경로",
                        $"FileInfo,public,selection,{plotData["selection"]},최근 설정한 좌표 정보"
                    };
                    for (int i = infoRecent.Length - 1; i >= 0; i--)
                    {
                        csvContent.Insert(0, infoRecent[i] + Environment.NewLine);
                    }
                    //만들어질 가장 최근 작업 파일을 recentConfig0.csv로, 나머지 번호 1씩 밀기
                    for (int i = confCheck.Length - 1; i >= 0; i--)
                    {
                        if (i >= 4)
                        {
                            continue;
                        }

                        string oldPath = Path.Combine(filePath, $"recentConfig{i}.csv");
                        string newPath = Path.Combine(filePath, $"recentConfig{i + 1}.csv");

                        if (File.Exists(oldPath) && File.Exists(newPath))
                        {
                            File.Delete(newPath);
                        }
                        File.Move(oldPath, newPath);
                    }

                    filePath = Path.Combine(filePath, "recentConfig0.csv");
                    csv_data[0][3] = temp1;
                    csv_data[1][3] = temp2;
                    break;
                case configFileType.Preset:
                    setAllparams(ref csvContent);
                    string title = "config";
                    string info = "";
                    string lasPath = gui.loadPath;
                    int fileNum = confCheck.Length;

                    //default 파일명 정하기(config + 존재하지 않는 config번호 파일 중 가장 작은 수)
                    string titleNum = "0";
                    bool[] fileNumCheck = Enumerable.Repeat(false, 10).ToArray();
                    for (int i = confCheck.Length - 1; i >= 0; i--)
                    {
                        if (Regex.IsMatch(pnSidePreset.Controls[i].Text, @"config\d"))
                        {
                            int numCheck = int.Parse(Regex.Replace(pnSidePreset.Controls[i].Text, @"\D", ""));
                            fileNumCheck[numCheck] = true;
                        }
                    }
                    bool foundTrue = false;
                    for (int i = 0; i < fileNumCheck.Length; i++)
                    {
                        if (fileNumCheck[i])
                        {
                            foundTrue = true;
                        }
                        else if (foundTrue)
                        {
                            titleNum = i.ToString();
                            break;
                        }
                    }
                    title += titleNum;

                    //사용자 설정값 선택 상태일 시 해당 파일 fileInfo 읽어오기
                    if (activatePreset != -1)
                    {
                        List<List<string>> csvLoad = new List<List<string>>();
                        fileNum = activatePreset;
                    }
                    infoRecent = new string[] {
                        $"FileInfo,public,fileType,{confType},파일의 타입을 구분 (preset default recent).",
                        $"FileInfo,public,title,{title},gui에 표시될 파일의 제목",
                        $"FileInfo,public,date,{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")},해당 파일이 만들어진 날짜 및 시간",
                        $"FileInfo,public,info,{info},사용자가 입력할 해당 설정값의 설명",
                        $"FileInfo,public,Lasfilename,{lasPath},설정을 저장할 LAS파일 경로"
                    };
                    for (int i = infoRecent.Length - 1; i >= 0; i--)
                    {
                        csvContent.Insert(0, infoRecent[i] + Environment.NewLine);
                    }
                    filePath = Path.Combine(filePath, $"presetConfig{fileNum}.csv");
                    csv_data[0][3] = temp1;
                    csv_data[1][3] = temp2;
                    break;
                default:
                    throw new Exception("unknown error: configFileType.Exception");
            }

            // CSV 파일 생성 및 내용 기록
            try
            {
                File.WriteAllText(filePath, csvContent.ToString(), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSV 파일 생성 중 오류 발생: " + ex.Message);
            }
        }

        //공장초기화 함수
        private void FactoryReset(string filepath)
        {

            DialogResult dialogResult = MessageBox.Show("기본 설정값을 초기로 되돌립니다.\n현재 설정된 기본값은 저장되지 않습니다.", 
                "공장 초기화", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Cancel)
            {
                return;
            }

            // CSV 파일 경로
            FillTextboxes();
            MessageBox.Show("설정 파일이 초기화 되었습니다.");
        }

        //텍스트에 반영만 하고, 실제 gui 구조체나 csv_data 리스트에 저장하진 않는다
        private void reflectConfs(string fileDi, string fileName)
        {
            string? csvLines;
            using (StreamReader sr = new StreamReader(Path.Combine(fileDi, fileName + ".csv")))
            {
                while ((csvLines = sr.ReadLine()) != null)
                {
                    if (csvLines.Contains("FileInfo,public,fileType"))
                    {
                        fileType = csvLines.Split(',')[3];
                    }
                    if (csvLines.Contains("FileInfo,public,Lasfilename"))
                    {
                        guiTemp.loadPath = csvLines.Split(',')[3];
                    }
                    if (csvLines.Contains("gui,private,circle"))
                    {
                        ExtractCircleValues(ref guiTemp, csvLines.Split(",")[3]);
                    }
                    if (csvLines.Contains("gui,private,rectangle"))
                    {
                        ExtractRectangleValues(ref guiTemp, csvLines.Split(",")[3]);
                    }
                    else if (csvLines.Contains("filters.smrf,public,cell"))
                    {
                        tbNorCellSize.Text = csvLines.Split(",")[3];
                    }
                    else if (csvLines.Contains("filters.smrf,public,window"))
                    {
                        tbNorWinSize.Text = csvLines.Split(",")[3];
                    }

                    else if (csvLines.Contains("filters.smrf,public,slope"))
                    {
                        tbNorSlope.Text = csvLines.Split(",")[3];
                    }

                    else if (csvLines.Contains("filters.smrf,public,scalar"))
                    {
                        tbNorScalar.Text = csvLines.Split(",")[3];
                    }

                    else if (csvLines.Contains("filters.smrf,public,threshold"))
                    {
                        tbNorThres.Text = csvLines.Split(",")[3];
                    }

                    else if (csvLines.Contains("filters.range.trunk,public,minheight"))
                    {
                        tbTrunkMinHeight.Text = csvLines.Split(",")[3];
                    }

                    else if (csvLines.Contains("filters.range.trunk,public,maxheight"))
                    {
                        tbTrunkMaxHeight.Text = csvLines.Split(",")[3];
                    }
                    else if (csvLines.Contains("csp_segmentstem,public,smoothness"))
                    {
                        tbTrunkSmooth.Text = csvLines.Split(",")[3];
                    }
                    else if (csvLines.Contains("filters.range.crown,public,minheight"))
                    {
                        tbCrownMinHeight.Text = csvLines.Split(",")[3];
                    }
                }
            }
            tcMainHome.SelectedIndex = 1;
        }
        //임시로 저장된 plot값을 적용하기 버튼을 누를 시
        //gui 구조체로 옮겨담는 역할
        private void apply_temp()
        {
            gui.loadPath = guiTemp.loadPath;
            gui.centerX = guiTemp.centerX;
            gui.centerY = guiTemp.centerY;
            gui.radius = guiTemp.radius;
            gui.xMin = guiTemp.xMin;
            gui.xMax = guiTemp.xMax;
            gui.yMin = guiTemp.yMin;
            gui.yMax = guiTemp.yMax;
        }



        //db파일 생성 및 테이블 생성
        private void create_dbFile_dbtable()
        {
            //bin_path 변수 초기화
            string filePath = "bin_path.txt";
            if (File.Exists(Path.Combine(basePath, filePath)))
            {
                string content = File.ReadAllText(filePath);
                bin_folder = content;               
                //MessageBox.Show(content);
            }
            else
            {
                Console.WriteLine("파일이 존재하지 않습니다.");
            }

            databaseFileName = Path.Combine(bin_folder, "config.db");

            //DB 파일 생성, 테이블 삭제 및 재생성, 초기값 insert
            if (!File.Exists(databaseFileName))
            {
                CreateDatabaseFile(bin_folder, databaseFileName);
                DeleteAllTables(databaseFileName, tablename);
                CreateTable(databaseFileName, tablename);
                insert_initial_data();
                //MessageBox.Show(SelectValueFromTable(databaseFileName, "gui", "circle"));
            }
            else { return; }
        }

        //db 파일 생성
        private void CreateDatabaseFile(string bin_folder, string databaseFileName)
        {
            if (!Directory.Exists(bin_folder))
            {
                Directory.CreateDirectory(bin_folder);
            }
            if (!System.IO.File.Exists(databaseFileName))
            {
                SQLiteConnection.CreateFile(databaseFileName);
                //MessageBox.Show("데이터베이스 파일이 성공적으로 생성되었습니다.");
            }
            else
            {
                //MessageBox.Show("데이터베이스 파일이 이미 존재합니다.");
            }
        }

        //테이블 생성
        private void CreateTable(string databaseFileName, string[] tableNames)
        {
            string connectionString = $"Data Source={databaseFileName};Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    foreach (string tableName in tableNames)
                    {
                        string createTableQuery = $"CREATE TABLE IF NOT EXISTS {tableName} (ID VARCHAR(30) PRIMARY KEY, access_modifier VARCHAR(10), value VARCHAR(65535), explanation TEXT)";

                        using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }

                    //MessageBox.Show("테이블이 성공적으로 생성되었습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류: " + ex.Message);
            }
        }
        //전체 테이블 삭제
        public void DeleteAllTables(string databaseFileName, string[] tableNames)
        {
            string connectionString = $"Data Source={databaseFileName};Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    foreach (string tableName in tableNames)
                    {
                        string deleteTableQuery = $"DROP TABLE IF EXISTS {tableName}";

                        using (SQLiteCommand command = new SQLiteCommand(deleteTableQuery, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }

                    //MessageBox.Show("테이블이 성공적으로 삭제되었습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류: " + ex.Message);
            }
        }
        //db파일 삭제
        public void delete_dbFile()
        {
            string databaseFileName = "config.db"; // 데이터베이스 파일 이름

            try
            {
                if (File.Exists(databaseFileName))
                {
                    File.Delete(databaseFileName);
                    MessageBox.Show("데이터베이스 파일이 삭제되었습니다.");
                }
                else
                {
                    MessageBox.Show("데이터베이스 파일이 이미 존재하지 않습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("데이터베이스 파일 삭제 오류: " + ex.Message);
            }
        }

        //insert 쿼리 실행
        public bool InsertDataIntoTable(string tableName, string accessModifier, string ID, string value, string explanation)
        {
            bool flag = false;
            string connectionString = $"Data Source={databaseFileName};Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string insertDataQuery = $"INSERT INTO {tableName} (ID, access_modifier, value, explanation) VALUES (@ID, @access_modifier, @value, @explanation)";

                    using (SQLiteCommand command = new SQLiteCommand(insertDataQuery, connection))
                    {
                        command.Parameters.AddWithValue("@access_modifier", accessModifier);
                        command.Parameters.AddWithValue("@ID", ID);
                        command.Parameters.AddWithValue("@value", value);
                        command.Parameters.AddWithValue("@explanation", explanation);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류: " + ex.Message);
            }

            return flag;
        }

        //update 쿼리 실행
        public void UpdateDataInTable(string tableName, string ID, string newValue)
        {
            string connectionString = $"Data Source={databaseFileName};Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string updateDataQuery = $"UPDATE {tableName} SET value = @newValue WHERE ID = @ID";

                    using (SQLiteCommand command = new SQLiteCommand(updateDataQuery, connection))
                    {
                        command.Parameters.AddWithValue("@newValue", newValue);
                        command.Parameters.AddWithValue("@ID", ID);

                        int rowsAffected = command.ExecuteNonQuery();

                        //if (rowsAffected > 0)
                        //{
                        //    MessageBox.Show("데이터가 성공적으로 업데이트되었습니다.");
                        //}
                        //else
                        //{
                        //    MessageBox.Show("업데이트할 데이터를 찾을 수 없습니다.");
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류: " + ex.Message);
            }
        }

        //vlaue 값 추출
        public string SelectDataFromTable(string databaseFileName, string tableName, string ID)
        {
            string connectionString = $"Data Source={databaseFileName};Version=3;";
            string value = null;

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string selectValueQuery = $"SELECT value FROM {tableName} WHERE ID = @ID";

                    using (SQLiteCommand command = new SQLiteCommand(selectValueQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                value = reader["value"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류: " + ex.Message);
            }

            return value;
        }
        //db를 읽어서 하나의 테이블을 List<List<string>>형태로 반환
        public List<List<string>> SelectDataFromTable(string databaseFileName, string tableName)
        {
            List<List<string>> result = new List<List<string>>();
            string connectionString = $"Data Source={databaseFileName};Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string selectDataQuery = $"SELECT * FROM {tableName}";

                    using (SQLiteCommand command = new SQLiteCommand(selectDataQuery, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                List<string> rowData = new List<string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    rowData.Add(reader[i].ToString());
                                }
                                result.Add(rowData);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류: " + ex.Message);
            }

            return result;
        }

        //db를 읽어서 List<List<string>>형태의 table로 반환
        public List<List<string>> SelectDataFromTable(string databaseFileName)
        {
            List<List<string>> result = new List<List<string>>();
            string connectionString = $"Data Source={databaseFileName};Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string num_of_tables= "SELECT count(*) as num_of_tables FROM sqlite_master WHERE type=\"table\";";
                    int num = 0; ;

                    using (SQLiteCommand command = new SQLiteCommand(num_of_tables, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                num = int.Parse(reader["num_of_tables"].ToString());
                                MessageBox.Show(num.ToString());
                            }
                        }
                    }

                    for(int i=0; i<num; i++)
                    {
                        string selectDataQuery = $"SELECT * FROM {tablename[i]}";

                        using (SQLiteCommand command = new SQLiteCommand(selectDataQuery, connection))
                        {
                            using (SQLiteDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    List<string> rowData = new List<string>();
                                    for (int j = 0; j < reader.FieldCount; j++)
                                    {
                                        rowData.Add(reader[j].ToString());
                                    }
                                    result.Add(rowData);
                                }
                            }
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류: " + ex.Message);
            }

            return result;
        }

        //초기값 insert
        public void insert_initial_data()
        {
            InsertDataIntoTable("gui", "private", "circle", "cx=0 cy=0 radius=100", "cx,cy,radius는 중심점 좌표와 반지름입니다.");
            InsertDataIntoTable("gui", "private", "rectangle", "xmin=-10 xmax=10 ymin=-10 ymax=10", "xmin,xmax,ymin,ymax는 범위 자료입니다.");
            InsertDataIntoTable("gui", "private", "result_path", "..\\result", "결과를 저장하는 폴더입니다.");
            InsertDataIntoTable("gui", "private", "intermediate_dir", "", "중간산출물을 저장하는 폴더입니다.");
            InsertDataIntoTable("gui", "private", "tree_dir", "", "개체목 저장하는 폴더입니다.");
            InsertDataIntoTable("filters_crop", "private", "buffer", "120", "plot 영역보다 120% 큰 영역을 의미한다.");
            InsertDataIntoTable("filters_crop", "private", "bufferd_dat", "xmin=-12.35 xmax=12.43 ymin=-6.22 ymax=22.14", "bufferd_plot 영역의 정보를 저장하는 파일 이름이다.");
            InsertDataIntoTable("filters_crop", "private", "origin_dat", "xmin=-14.738 xmax=5.531 ymin=-2.147 ymax=15.436 cx=0 cy=0 radius=100", "origin_plot 영역의 정보를 저장하는 파일 이름이다.");
            InsertDataIntoTable("filters_outlier", "private", "method", "statistical", "통계 기반으로 이상치(이상점) 제거한다.");
            InsertDataIntoTable("filters_outlier", "private", "mean_k", "12", "최근접 이웃의 개수를 지정한다.");
            InsertDataIntoTable("filters_outlier", "private", "multiplier", "2.2", "거리의 표준편차의 계수를 지정한다.");
            InsertDataIntoTable("filters_smrf", "public", "cell", "4", "셀 크기를 지정한다. PDAL 기본값이 1.0m인데, ForestLi는 4m를 사용하는데, 확인이 필요하다.");
            InsertDataIntoTable("filters_smrf", "public", "window", "16", "max window size를 지정한다.PDAL 기본값이 18m인데, ForestLi는 16m를 사용하는데, 확인이 필요하다.");
            InsertDataIntoTable("filters_smrf", "public", "slope", "0.3", "slope(rise over run)을 지정한다.PDAL 기본값이 0.15인데, ForestLi는 0.3를 사용하는데, 확인이 필요하다. 단위가 무엇일까? Radian인가?");
            InsertDataIntoTable("filters_smrf", "public", "scalar", "1.25", "Elevation scalar를 지정한다. 단위가 무엇일까? meter인가?");
            InsertDataIntoTable("filters_smrf", "public", "threshold", "1", "Elevation threshold를 지정한다. 단위가 무엇일까? meter인가?");
            InsertDataIntoTable("filters_range_trunk", "public", "minheight", "0", "수간(trunk)으로 조사하는 영역의 높이의 최솟값을 지정한다.보통은 0m으로 지정한다.");
            InsertDataIntoTable("filters_range_trunk", "public", "maxheight", "5", "수간(trunk)으로 조사하는 영역의 높이의 최댓값을 지정한다.보통은 5m으로 지정한다.");
            InsertDataIntoTable("filters_range_crown", "public", "minheight", "3", "수관(crown)으로 조사하는 영역의 높이의 최솟값을 지정한다.");
            InsertDataIntoTable("filters_range_crown", "public", "maxheight", "100", "수관(crown)으로 조사하는 영역의 높이의 최댓값을 지정한다.");
            InsertDataIntoTable("csp_segmentstem", "private", "smoothness", "10", "smoothness (degrees)는 ??이다. 영역성장(regiongrowing) 알고리즘의 smoothness를 설정하는 값이다. 영역성장 알고리즘 파악이 우선이다");
            InsertDataIntoTable("csp_segmentstem", "private", "mindbh", "0.01", "수간(trunk)에서 흉고직경의 최솟값이다. 기본적으로 0.01m를 사용한다.");
            InsertDataIntoTable("csp_segmentstem", "private", "maxdbh", "1", "수간(trunk)에서 흉고직경의 최댓값이다. 기본적으로 1m를 사용한다.");
            InsertDataIntoTable("csp_segmentstem", "private", "nnearest", "10", "최근접 이웃의 개수를 지정한다. 시스템 인자이다. 사용자가 알 필요가 없다.");
            InsertDataIntoTable("csp_segmentstem", "private", "nmin", "50", "유클리디어 군집화에서 클러스터를 이루는 점들의 최소 개수이다. 시스템 인자이다. 사용자가 알 필요가 없다.");
            InsertDataIntoTable("csp_segmentstem", "private", "num_neighbours", "50", "영역성장(regiongrowing) 알고리즘의 최근접 이웃 점들의 개수를 설정하는 값이다. 영역성장 알고리즘 파악이 우선이다.");
            InsertDataIntoTable("csp_segmentstem", "private", "anglemax", "20", "주성분 분석에서 수간부의 실린더가 기울기를 설정하는 값이다.예를 들면, 20 일 때, 수간부(trunk)의 기울기는 70도와 110도 사이에 존재한다.");
            InsertDataIntoTable("csp_segmentstem", "private", "trunk_slice_file", "", "수간부(trunk) 슬라이스에 해당하는 점들을 저장하는 파일 이름이다.");
            InsertDataIntoTable("csp_segmentcrown", "private", "num_nn_samples", "16", "최근접 이웃 점들의 개수를 지정하는 값이다. 지정된 최근접 이웃 점들까지 거리를 계산하여 거리의 평균과 표준편차를 결정한다.");
            InsertDataIntoTable("csp_segmentcrown", "private", "trunk_files", "", "csp_segmentstem 단계에서 생성된 수간부(trunk) 파일들을 저장한다.");
            InsertDataIntoTable("csp_segmentcrown", "private", "crown_slice_file", "", "수관부(crown) 슬라이스에 해당하는 점들을 저장하는 파일 이름이다.");
            InsertDataIntoTable("measure", "private", "nnearest", "16", "DBH(흉고직경) 측정할 때 stem에서 원형 모델을 찾을 경우, 이웃한 점들의 거리의 표준편차를 사용하는데, 표준편차를 구할때 사용되는 이웃 점들의 개수를 의미한다");
            InsertDataIntoTable("measure", "private", "minrad", "0.03", "찾는 원형모델의 최소 반지름이다");
            InsertDataIntoTable("measure", "private", "maxrad", "0.5", "찾는 원형모델의 최대 반지름이다");
            InsertDataIntoTable("measure", "private", "iterations", "10000", "원형모델을 찾는 RANSAC 알고리즘의 최대 시도 횟수이다");
            InsertDataIntoTable("measure", "private", "zmin_check", "0.2", "나무가 표준지의 속하는지 판단하기 위해 사용 된다. 기준포인트들 중 최하점의 높이다");
            InsertDataIntoTable("measure", "private", "zmax_check", "0.7", "나무가 표준지의 속하는지 판단하기 위해 사용 된다. 기준포인트들 중 최상점의 높이다");
            InsertDataIntoTable("measure", "private", "tree_files", "", "추출된 개체목 파일입니다.");
        }


        //textbox 값 -> List 테이블로 대입 
        private void UpdateParams()
        {
            //Normalize_textboxes
            UpdateDataInTable("filters_smrf", "cell", tbNorCellSize.Text.Trim());
            UpdateDataInTable("filters_smrf", "scalar", tbNorScalar.Text.Trim());
            UpdateDataInTable("filters_smrf", "slope", tbNorSlope.Text.Trim());
            UpdateDataInTable("filters_smrf", "window", tbNorWinSize.Text.Trim());
            UpdateDataInTable("filters_smrf", "threshold", tbNorThres.Text.Trim());

            //setParam(dataList, "filters.smrf", "cell", tbNorCellSize.Text.Trim());
            //setParam(dataList, "filters.smrf", "scalar", tbNorScalar.Text.Trim());
            //setParam(dataList, "filters.smrf", "slope", tbNorSlope.Text.Trim());
            //setParam(dataList, "filters.smrf", "window", tbNorWinSize.Text.Trim()); 
            //setParam(dataList, "filters.smrf", "threshold", tbNorThres.Text.Trim());

            //trunkSlice_textboxes
            UpdateDataInTable("filters_range_trunk", "minheight", tbTrunkMinHeight.Text.Trim());
            UpdateDataInTable("filters_range_trunk", "maxheight", tbTrunkMaxHeight.Text.Trim());
            UpdateDataInTable("csp_segmentstem", "smoothness", tbTrunkSmooth.Text.Trim());

            //setParam(dataList, "filters.range.trunk", "minheight", tbTrunkMinHeight.Text.Trim());
            //setParam(dataList, "filters.range.trunk", "maxheight", tbTrunkMaxHeight.Text.Trim());
            //setParam(dataList, "csp_segmentstem", "smoothness", tbTrunkSmooth.Text.Trim());

            //CrownSlice_textboxes
            UpdateDataInTable("filters_range_crown", "minheight", tbCrownMinHeight.Text.Trim());
            //setParam(dataList, "filters.range.crown", "minheight", tbCrownMinHeight.Text.Trim());
        }
        //List 테이블 -> textbox 대입
        private void FillTextboxes()
        {
            ExtractCircleValues(ref gui, SelectDataFromTable(databaseFileName, "gui", "circle"));
            ExtractRectangleValues(ref gui, SelectDataFromTable(databaseFileName, "gui", "rectangle"));

            //normalize_textboxes
            tbNorCellSize.Text = SelectDataFromTable(databaseFileName, "filters_smrf", "cell");
            tbNorScalar.Text = SelectDataFromTable(databaseFileName, "filters_smrf", "scalar");
            tbNorSlope.Text = SelectDataFromTable(databaseFileName, "filters_smrf", "slope");
            tbNorThres.Text = SelectDataFromTable(databaseFileName, "filters_smrf", "threshold");
            tbNorWinSize.Text = SelectDataFromTable(databaseFileName, "filters_smrf", "window");

            //trunkSlice_textboxes
            tbTrunkMinHeight.Text = SelectDataFromTable(databaseFileName, "filters_range_trunk", "minheight");
            tbTrunkMaxHeight.Text = SelectDataFromTable(databaseFileName, "filters_range_trunk", "maxheight");
            tbTrunkSmooth.Text = SelectDataFromTable(databaseFileName, "csp_segmentstem", "smoothness");

            //CrownSlice_textboxes
            tbCrownMinHeight.Text = SelectDataFromTable(databaseFileName, "filters_range_crown", "minheight");
        }
    }
}
