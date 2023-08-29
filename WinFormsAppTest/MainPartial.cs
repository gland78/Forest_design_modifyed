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

namespace WinFormsAppTest
{
    partial class MainForm
    {
        //변수 구조체   --> public 제외 나머지는 삭제예정
        public GUI gui = new GUI();

        /// <summary>
        ///  csv 관련 변수
        /// </summary>
        public List<List<string>> csv_data = new List<List<string>>();

        //exe파일이 위치한 기본 폴더와 그 내부 config 폴더들
        internal static string basePath = (Environment.CurrentDirectory).ToString();
        internal static string[] reqDi = { "", "RecentConfig", "PresetConfig" };
        public string csv_path = Path.Combine(basePath, "config.csv");

        public string fileType = "";

        /// <summary>
        /// csv 읽는 함수 
        /// </summary>
        private void read_csv(string filepath)
        {
            csv_data.Clear();
            //csv 읽기
            string csvFilePath = filepath;

            try
            {
                using (StreamReader reader = new StreamReader(csvFilePath, Encoding.UTF8))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] values = line.Split(',');
                        List<string> string_data = new List<string>();

                        if (values.Length >= 4)
                        {
                            foreach (var v in values)
                            {
                                string_data.Add(v);
                                //MessageBox.Show(v);
                            }
                            
                            csv_data.Add(string_data);
                        }
                        else
                        {
                            MessageBox.Show("config.csv 형식에 문제가 있습니다.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("CSV파일 읽는 중 오류 발생: " + ex.Message);
            }
        }
        void ExtractCircleValues(string circleString)
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
                            gui.centerX = double.Parse(value);
                            break;
                        case "cy":
                            gui.centerY = double.Parse(value);
                            break;
                        case "radius":
                            gui.radius = double.Parse(value);
                            break;
                    }
                }
            }
        }
        void ExtractRectangleValues(string rectangleString)
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
                            gui.xMin = double.Parse(value);
                            break;
                        case "ymin":
                            gui.xMax = double.Parse(value);
                            break;
                        case "xmax":
                            gui.yMin = double.Parse(value);
                            break;
                        case "ymax":
                            gui.yMax = double.Parse(value);
                            break;
                    }
                }
            }
        }
        //csv 쓰는 함수
        public void write_csv(string filepath)
        {
            // CSV 파일 경로
            string filePath = filepath;

            // CSV 내용 생성
            StringBuilder csvContent = new StringBuilder();

            for (int i = 0; i < csv_data.Count; i++)
            {
                string str = "";
                for (int j = 0; j < csv_data[i].Count; j++)
                {
                    if(j== csv_data[i].Count - 1)
                    {
                        str += csv_data[i][j];
                        break;
                    }
                    str += csv_data[i][j] + ",";

                }
                //MessageBox.Show(str);
                csvContent.AppendLine(str);
            }

            // CSV 파일 생성 및 내용 기록
            try
            {
                File.WriteAllText(filePath, csvContent.ToString(), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSV 파일 작성 중 오류 발생: " + ex.Message);
            }
        }

        //textbox 값 -> List 테이블로 대입 
        private void UpdateParams()
        {
            //subsamplng_textbox
            setParam("filters.sample", "cell", tbSubCellSize.Text.Trim());

            //Normalize_textboxes
            setParam("filters.smrf", "cell", tbNorCellSize.Text.Trim());
            setParam("filters.smrf", "scalar", tbNorScalar.Text.Trim());
            setParam("filters.smrf", "slope", tbNorSlope.Text.Trim());
            setParam("filters.smrf", "window", tbNorWinSize.Text.Trim()); 
            setParam("filters.smrf", "threshold", tbNorThres.Text.Trim());

            //trunkSlice_textboxes
            setParam("filters.range.trunk", "minheight", tbTrunkMinHeight.Text.Trim());
            setParam("filters.range.trunk", "maxheight", tbTrunkMaxHeight.Text.Trim());

            //CrownSlice_textboxes
            setParam("filters.range.crown", "minheight", tbCrownMinHeight.Text.Trim());
            setParam("filters.range.crown", "maxheight", tbCrownMaxHeight.Text.Trim());
        }

        //List 테이블 -> textbox 대입
        private void FillTextboxes()
        {
            ExtractCircleValues(csv_data[0][3]);
            ExtractRectangleValues(csv_data[1][3]);
            //subsamplng_textboxes
            tbSubCellSize.Text = getParam("filters.sample","cell");

            //normalize_textboxes
            tbNorCellSize.Text = getParam("filters.smrf","cell");
            tbNorScalar.Text = getParam("filters.smrf", "scalar");
            tbNorSlope.Text = getParam("filters.smrf", "slope");
            tbNorThres.Text = getParam("filters.smrf", "threshold");
            tbNorWinSize.Text = getParam("filters.smrf", "window");

            //trunkSlice_textboxes
            tbTrunkMinHeight.Text = getParam("filters.range.trunk","minheight");
            tbTrunkMaxHeight.Text = getParam("filters.range.trunk", "maxheight");

            //CrownSlice_textboxes
            tbCrownMinHeight.Text = getParam("filters.range.crown", "minheight");
            tbCrownMaxHeight.Text = getParam("filters.range.crown", "maxheight");
        }
        /// <summary>
        /// 무결성 검사를 위한 코드
        /// </summary>
        private void RegistTextBoxHandler()
        {
            //double형만 입력가능하도록 키 등록
            tbSubCellSize.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbNorScalar.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbNorSlope.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbNorThres.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbTrunkMinHeight.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbTrunkMaxHeight.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbCrownMinHeight.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbCrownMaxHeight.KeyPress += TextBox_KeyPressOnlyNumbers;


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
        /// preset,recent 설정파일 저장용코드
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
        private void MakeConfig(configFileType confType)
        {
            read_csv(csv_path);
            string filePath = Path.Combine(basePath, reqDi[(int)confType]);

            //해당 폴더 없을 시 만들기
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            //해당 폴더 내 config파일 갯수 확인
            string[] confCheck = Directory.GetFiles(Path.Combine(basePath, reqDi[(int)confType]), "*config*");

            string temp1 = csv_data[0][3], temp2 = csv_data[0][3];

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
                    infoRecent = new string[] {
                        $"FileInfo,public,fileType,{confType},파일의 타입을 구분 (preset default recent).",
                        $"FileInfo,public,title,{"config" + confCheck.Length.ToString()},gui에 표시될 파일의 제목",
                        $"FileInfo,public,date,{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")},해당 파일이 만들어진 날짜 및 시간",
                        $"FileInfo,public,info, ,사용자가 입력할 해당 설정값의 설명",
                        $"FileInfo,public,Lasfilename,{gui.loadPath},설정을 저장할 LAS파일 경로"
                    };
                    for (int i = infoRecent.Length - 1; i >= 0; i--)
                    {
                        csvContent.Insert(0, infoRecent[i] + Environment.NewLine);
                    }
                    filePath = Path.Combine(filePath, $"presetConfig{confCheck.Length.ToString()}.csv");
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
            string filePath = filepath;

            // CSV 내용 생성
            StringBuilder csvContent = new StringBuilder();

            //설명 변경해야함
            csvContent.AppendLine("gui,private,circle,cx=0 cy=0 radius=100,cx,cy,radius는 중심점 좌표와 반지름입니다.");
            csvContent.AppendLine("gui,private,rectangle,xmin=-10 ymin=-10 xmax=10 ymax=10,xmin,xmax,ymin,ymax는 범위 자료입니다. ");
            csvContent.AppendLine("gui,private,result_path,..\\result,결과를 저장하는 폴더입니다. ");
            csvContent.AppendLine("filters.crop,private,buffer,120,plot 영역보다 120% 큰 영역을 의미한다.");
            csvContent.AppendLine("filters.sample,public,cell,0.03,복셀 크기 (예: 0.03m)");
            csvContent.AppendLine("filters.outlier,private,method,statistical,통계 기반으로 이상치(이상점) 제거한다.");
            csvContent.AppendLine("filters.outlier,private,mean_k,12,최근접 이웃의 개수를 지정한다. ");
            csvContent.AppendLine("filters.outlier,private,multiplier,2.2,거리의 표준편차의 계수를 지정한다. ");
            csvContent.AppendLine("filters.smrf,public,cell,4,셀 크기를 지정한다. PDAL 기본값이 1.0m인데, ForestLi는 4m를 사용하는데, 확인이 필요하다.");
            csvContent.AppendLine("filters.smrf,public,window,16,max window size를 지정한다. PDAL 기본값이 18m인데, ForestLi는 16m를 사용하는데, 확인이 필요하다.");
            csvContent.AppendLine("filters.smrf,public,slope,0.3,slope(rise over run)을 지정한다. PDAL 기본값이 0.15인데, ForestLi는 0.3를 사용하는데, 확인이 필요하다. 단위가 무엇일까? Radian인가?");
            csvContent.AppendLine("filters.smrf,public,scalar,1.25,Elevation scalar를 지정한다. 단위가 무엇일까? meter인가?");
            csvContent.AppendLine("filters.smrf,public,threshold,1,Elevation threshold를 지정한다. 단위가 무엇일까? meter인가?");
            csvContent.AppendLine("filters.range.trunk,public,minheight,0,수간(trunk)으로 조사하는 영역의 높이의 최솟값을 지정한다. 보통은 0m으로 지정한다. Public인지 private인지 고민해 보자.");
            csvContent.AppendLine("filters.range.trunk,public,maxheight,5,수간(trunk)으로 조사하는 영역의 높이의 최댓값을 지정한다. 보통은 5m으로 지정한다. Public인지 private인지 고민해 보자.");
            csvContent.AppendLine("filters.range.crown,public,minheight,3,수관(crown)으로 조사하는 영역의 높이의 최솟값을 지정한다. ");
            csvContent.AppendLine("filters.range.crown,public,maxheight,100,수관(crown)으로 조사하는 영역의 높이의 최댓값을 지정한다. ");
            csvContent.AppendLine("csp_segmentcrown,private,num_nn_samples,16,최근접 이웃 점들의 개수를 지정하는 값이다. 지정된 최근접 이웃 점들까지 거리를 계산하여 거리의 평균과 표준편차를 결정한다.");
            csvContent.AppendLine("csp_segmentstem,private,smoothness,10,smoothness (degrees)는 ??이다. 영역성장(regiongrowing) 알고리즘의 smoothness를 설정하는 값이다. 영역성장 알고리즘 파악이 우선이다. ");
            csvContent.AppendLine("csp_segmentstem,private,mindbh,0.01,수간(trunk)에서 흉고직경의 최솟값이다. 기본적으로 0.01m를 사용한다. ");
            csvContent.AppendLine("csp_segmentstem,private,maxdbh,1,수간(trunk)에서 흉고직경의 최댓값이다. 기본적으로 1m를 사용한다. ");
            csvContent.AppendLine("csp_segmentstem,private,nnearest,10,최근접 이웃의 개수를 지정한다. 시스템 인자이다. 사용자가 알 필요가 없다.");
            csvContent.AppendLine("csp_segmentstem,private,nmin,50,유클리디어 군집화에서 클러스터를 이루는 점들의 최소 개수이다. 시스템 인자이다. 사용자가 알 필요가 없다.");
            csvContent.AppendLine("csp_segmentstem,private,num_neighbours,50,영역성장(regiongrowing) 알고리즘의 최근접 이웃 점들의 개수를 설정하는 값이다. 영역성장 알고리즘 파악이 우선이다. ");
            csvContent.AppendLine("csp_segmentstem,private,anglemax,20,주성분 분석에서 수간부의 실린더가 기울기를 설정하는 값이다. 예를 들면, 20 일 때, 수간부(trunk)의 기울기는 70도와 110도 사이에 존재한다.");
            csvContent.AppendLine("measure,private,nnearest,16,DBH(흉고직경) 측정할 때 stem에서 원형 모델을 찾을 경우, 이웃한 점들의 거리의 표준편차를 사용하는데, 표준편차를 구할때 사용되는 이웃 점들의 개수를 의미한다");
            csvContent.AppendLine("measure,private,minrad,0.03,찾는 원형모델의 최소 반지름이다");
            csvContent.AppendLine("measure,private,maxrad,0.5,찾는 원형모델의 최대 반지름이다");
            csvContent.AppendLine("measure,private,iterations,10000,원형모델을 찾는 RANSAC 알고리즘의 최대 시도 횟수이다");
            csvContent.AppendLine("measure,private,zmin_check,0.2,나무가 표준지의 속하는지 판단하기 위해 사용 된다. 기준포인트들 중 최하점의 높이다");
            csvContent.Append("measure,private,zmax_check,0.7,나무가 표준지의 속하는지 판단하기 위해 사용 된다. 기준포인트들 중 최상점의 높이다");

            // CSV 파일 생성 및 내용 기록
            try
            {
                File.WriteAllText(filePath, csvContent.ToString(), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSV 파일 작성 중 오류 발생: " + ex.Message);
            }

            read_csv(filePath);
            FillTextboxes();
            MessageBox.Show("설정 파일이 초기화 되었습니다.");
        }

        //csv_data를 저장해놓은 List에서 값을 가져오는 함수
        public string getParam(string Type,string Key)
        {
            string ret = "";
            for (int i = 0; i < csv_data.Count; i++)
            {
                if (csv_data[i][0] != Type)
                {
                    continue;
                }
                else if (csv_data[i][2] != Key)
                {
                    continue;
                }
                else
                {
                    ret = csv_data[i][3];
                    break;
                }
            }
            //MessageBox.Show(ret);
            return ret;
        }
        //csv_data를 저장해놓은 List에 값을 설정하는 함수
        public void setParam(string Type, string Key, string Value)
        {
            for (int i = 0; i < csv_data.Count; i++)
            {
                if (csv_data[i][0] != Type)
                {
                    continue;
                }
                else if (csv_data[i][2] != Key)
                {
                    continue;
                }
                else
                {
                    csv_data[i][3] = Value;
                   //MessageBox.Show(csv_data[i][3]); 
                    return;
                }
            }
        }
    }
}
