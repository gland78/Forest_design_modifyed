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
        public Crop crop = new Crop();
        public Subsampling subsampling = new Subsampling();
        public Outlier outlier = new Outlier();
        public GroundSeg groundseg = new GroundSeg();
        public TSlice tSlice = new TSlice();
        public CSlice cSlice = new CSlice();
        public csp_segmentcrown csp_crown = new csp_segmentcrown();
        public csp_segmentstem csp_stem = new csp_segmentstem();
        public Measure measure = new Measure();

        /// <summary>
        ///  csv 관련 변수
        /// </summary>
        struct GuiData
        {
            public string Type;
            public string Visibility;
            public string Key;
            public string Value;
            public string explain;
        }
        List<GuiData> guiDataList = new List<GuiData>();

        public string csv_path = Path.Combine(basePath, "config.csv");

        /// <summary>
        /// csv 읽는 함수 
        /// </summary>
        private void read_csv(string filepath)
        {
            guiDataList.Clear();
            //csv 읽기
            string csvFilePath = filepath;

            try
            {
                using (StreamReader reader = new StreamReader(csvFilePath,Encoding.UTF8))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();                        
                        //string[] values = ParseString(line);
                        string[] values = line.Split(',');
                        if (values.Length >= 4)
                        {                           
                            GuiData guiData = new GuiData
                            {
                                Type = values[0],
                                Visibility = values[1],
                                Key = values[2],
                                Value = values[3],
                                explain = values[4]
                            };
                            //MessageBox.Show(guiData.explain);
                            if (guiData.Key == "circle")
                            {
                                ExtractCircleValues(guiData.Value);
                            }
                            if (guiData.Key == "rectangle")
                            {
                                ExtractRectangleValues(guiData.Value);
                            }
                            guiDataList.Add(guiData);                            
                        }
                    }
                }

                // 벡터에 저장된 데이터를 사용하거나 처리하는 부분
                foreach (GuiData data in guiDataList)
                {
                    if (data.Type == "gui" && data.Key == "result_path")
                    {
                        gui.resultPath = data.Value;
                    }
                    else if (data.Type == "filters.crop" && data.Key == "buffer")
                    {
                        crop.buffer = double.Parse(data.Value);
                    }
                    else if (data.Type == "filters.sample" && data.Key == "cell")
                    {
                        subsampling.cellSize = double.Parse(data.Value);
                    }
                    else if (data.Type == "filters.outlier")
                    {
                        switch (data.Key)
                        {
                            case "method":
                                outlier.method = data.Value;
                                break;
                            case "mean_k":
                                outlier.mean_k = double.Parse(data.Value);
                                break;
                            case "multiplier":
                                outlier.Multiplier = double.Parse(data.Value);
                                break;
                        }
                    }
                    else if (data.Type == "filters.smrf")
                    {
                        switch (data.Key)
                        {
                            case "cell":
                                groundseg.cellSize = data.Value;
                                break;
                            case "window":
                                groundseg.windowSize = data.Value;
                                break;
                            case "slope":
                                groundseg.slope = data.Value;
                                break;
                            case "scalar":
                                groundseg.scalar = data.Value;
                                break;
                            case "threshold":
                                groundseg.threshold = data.Value;
                                break;
                        }
                    }
                    else if (data.Type == "filters.range.trunk")
                    {
                        switch (data.Key)
                        {
                            case "minheight":
                                tSlice.minHeight = double.Parse(data.Value);
                                break;
                            case "maxheight":
                                tSlice.maxHeight = double.Parse(data.Value);
                                break;
                        }
                    }
                    else if (data.Type == "filters.range.crown")
                    {
                        switch (data.Key)
                        {
                            case "minheight":
                                cSlice.minHeight = double.Parse(data.Value);
                                break;
                            case "maxheight":
                                cSlice.maxHeight = double.Parse(data.Value);
                                break;
                        }
                    }
                    else if (data.Type == "csp_segmentcrown")
                    {
                        switch (data.Key)
                        {
                            case "num_nn_samples":
                                csp_crown.num_nn_samples = int.Parse(data.Value);
                                break;
                        }
                    }
                    else if (data.Type == "csp_segmentstem")
                    {
                        switch (data.Key)
                        {
                            case "smoothness":
                                csp_stem.smoothness = double.Parse(data.Value);
                                break;
                            case "mindbh":
                                csp_stem.minDBH = double.Parse(data.Value);
                                break;
                            case "maxdbh":
                                csp_stem.maxDBH = double.Parse(data.Value);
                                break;
                            /*case "height_threshold":
                                csp_stem.HeightThreshold = double.Parse(data.Value);
                                break;*/
                            case "nnearest":
                                csp_stem.nnearest = double.Parse(data.Value);
                                break;
                            case "nmin":
                                csp_stem.nmin = double.Parse(data.Value);
                                break;
                            case "num_neighbours":
                                csp_stem.num_neighbours = double.Parse(data.Value);
                                break;
                            case "anglemax":
                                csp_stem.anglemax = double.Parse(data.Value);
                                break;
                        }
                    }
                    else if (data.Type == "measure")
                    {
                        switch (data.Key)
                        {
                            case "nnearest":
                                measure.MeasureNN = int.Parse(data.Value);
                                break;
                            case "minrad":
                                measure.minRad = double.Parse(data.Value);
                                break;
                            case "maxrad":
                                measure.maxRad = double.Parse(data.Value);
                                break;
                            case "iterations":
                                measure.iterations = int.Parse(data.Value);
                                break;
                            case "zmin_check":
                                measure.zmin_check = double.Parse(data.Value);
                                break;
                            case "zmax_check":
                                measure.zmax_check = double.Parse(data.Value);
                                break;
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
        static string[] ParseString(string input)
        {
            // 쉼표로 문자열을 분할
            string[] splitParts = input.Split(',');

            // 마지막 부분에 따옴표로 끝나는 부분을 제거
            splitParts[splitParts.Length - 1] = splitParts[splitParts.Length - 1].TrimEnd('\"');

            return splitParts;
        }
        //csv 쓰는 함수
        public void write_csv(string filepath)
        {
            // CSV 파일 경로
            string filePath = filepath;

            // CSV 내용 생성
            StringBuilder csvContent = new StringBuilder();
            /*foreach (var item in guiDataList)
            {
                string str = item.Type.ToString() + "," + item.Visibility.ToString() + "," + item.Key + "," + item.Value + ","+item.explain;
                csvContent.AppendLine(str);
            }*/
            csvContent.AppendLine($"gui,public,circle,cx={gui.centerX} cy={gui.centerY} radius={gui.radius},{guiDataList[0].explain}");
            csvContent.AppendLine($"gui,public,rectangle,xmin={gui.xMin} ymin={gui.yMin} xmax={gui.xMax} ymax={gui.yMax},{guiDataList[1].explain}");
            csvContent.AppendLine($"gui,public,result_path,{gui.resultPath},{guiDataList[2].explain}");
            csvContent.AppendLine($"filters.crop,private,buffer,{crop.buffer},{guiDataList[3].explain}");
            csvContent.AppendLine($"filters.sample,public,cell,{tbSubCellSize.Text},{guiDataList[4].explain}");
            csvContent.AppendLine($"filters.outlier,private,method,{outlier.method},{guiDataList[5].explain}");
            csvContent.AppendLine($"filters.outlier,private,mean_k,{outlier.mean_k},{guiDataList[6].explain}");
            csvContent.AppendLine($"filters.outlier,private,multiplier,{outlier.Multiplier},{guiDataList[7].explain}");
            csvContent.AppendLine($"filters.smrf,public,cell,{tbNorCellSize.Text},{guiDataList[8].explain}");
            csvContent.AppendLine($"filters.smrf,public,window,{tbNorWinSize.Text},{guiDataList[9].explain}");
            csvContent.AppendLine($"filters.smrf,public,slope,{tbNorSlope.Text},{guiDataList[10].explain}");
            csvContent.AppendLine($"filters.smrf,public,scalar,{tbNorScalar.Text},{guiDataList[11].explain}");
            csvContent.AppendLine($"filters.smrf,public,threshold,{tbNorThres.Text},{guiDataList[12].explain}");
            csvContent.AppendLine($"filters.range.trunk,public,minheight,{tbTrunkMinHeight.Text},{guiDataList[13].explain}");
            csvContent.AppendLine($"filters.range.trunk,public,maxheight,{tbTrunkMaxHeight.Text},{guiDataList[14].explain}");
            csvContent.AppendLine($"filters.range.crown,public,minheight,{tbCrownMinHeight.Text},{guiDataList[15].explain}");
            csvContent.AppendLine($"filters.range.crown,public,maxheight,{tbCrownMaxHeight.Text},{guiDataList[16].explain}");
            csvContent.AppendLine($"csp_segmentcrown,private,num_nn_samples,{csp_crown.num_nn_samples},{guiDataList[17].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,smoothness,{csp_stem.smoothness},{guiDataList[18].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,mindbh,{csp_stem.minDBH},{guiDataList[19].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,maxdbh,{csp_stem.maxDBH},{guiDataList[20].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,nnearest,{csp_stem.nnearest},{guiDataList[21].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,nmin,{csp_stem.nmin},{guiDataList[22].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,num_neighbours,{csp_stem.num_neighbours},{guiDataList[23].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,anglemax,{csp_stem.anglemax},{guiDataList[24].explain}");
            csvContent.AppendLine($"measure,private,nnearest,{measure.MeasureNN},{guiDataList[25].explain}");
            csvContent.AppendLine($"measure,private,minrad,{measure.minRad},{guiDataList[26].explain}");
            csvContent.AppendLine($"measure,private,maxrad,{measure.maxRad},{guiDataList[27].explain}");
            csvContent.AppendLine($"measure,private,iterations,{measure.iterations},{guiDataList[28].explain}");
            csvContent.AppendLine($"measure,private,zmin_check,{measure.zmin_check},{guiDataList[29].explain}");
            csvContent.Append($"measure,private,zmax_check,{measure.zmax_check},{guiDataList[30].explain}");

            // CSV 파일 생성 및 내용 기록
            try
            {
                File.WriteAllText(filePath, csvContent.ToString(),Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSV 파일 작성 중 오류 발생: " + ex.Message);
            }
        }

        //textbox 값 -> 구조체로 대입 
        private void UpdateParams()
        {
            //subsamplng_textbox
            subsampling.cellSize = double.Parse(tbSubCellSize.Text.Trim());

            //Normalize_textboxes
            groundseg.scalar = tbNorScalar.Text.Trim();
            groundseg.cellSize = tbNorCellSize.Text.Trim();
            groundseg.slope = tbNorSlope.Text.Trim();
            groundseg.windowSize = tbNorWinSize.Text.Trim();
            groundseg.threshold = tbNorThres.Text.Trim();

            //trunkSlice_textboxes
            tSlice.minHeight = double.Parse(tbTrunkMinHeight.Text.Trim());
            tSlice.maxHeight = double.Parse(tbTrunkMaxHeight.Text.Trim());

            //CrownSlice_textboxes
            cSlice.minHeight = double.Parse(tbCrownMinHeight.Text.Trim());
            cSlice.maxHeight = double.Parse(tbCrownMaxHeight.Text.Trim());

            //csp_stem.HeightThreshold = double.Parse(tbTreeSegHeightThres.Text);
        }

        //구조체 값 -> textbox 대입
        private void FillTextboxes()
        {
            //subsamplng_textboxes
            tbSubCellSize.Text = subsampling.cellSize.ToString();

            //normalize_textboxes
            tbNorCellSize.Text = groundseg.cellSize;
            tbNorScalar.Text = groundseg.scalar;
            tbNorSlope.Text = groundseg.slope;
            tbNorThres.Text = groundseg.threshold;
            tbNorWinSize.Text = groundseg.windowSize;

            //trunkSlice_textboxes
            tbTrunkMinHeight.Text = tSlice.minHeight.ToString();
            tbTrunkMaxHeight.Text = tSlice.maxHeight.ToString();

            //CrownSlice_textboxes
            tbCrownMinHeight.Text = cSlice.minHeight.ToString();
            tbCrownMaxHeight.Text = cSlice.maxHeight.ToString();
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
            /*
            ground_threshold_textbox.KeyPress += TextBox_KeyPressOnlyNumbers;
            TSlice_minHeight_textbox.KeyPress += TextBox_KeyPressOnlyNumbers;
            TSlice_maxHeight_textbox.KeyPress += TextBox_KeyPressOnlyNumbers;
            CSlice_minHeight_textbox.KeyPress += TextBox_KeyPressOnlyNumbers;
            CSlice_maxHeight_textbox.KeyPress += TextBox_KeyPressOnlyNumbers;
            CSP_nnearest_textbox.KeyPress += TextBox_KeyPressOnlyNumbers;
            measure_nnearest_textbox.KeyPress += TextBox_KeyPressOnlyNumbers;
            segStem_maxDBH_textbox.KeyPress += TextBox_KeyPressOnlyNumbers;
            segStem_minDBH_textbox.KeyPress += TextBox_KeyPressOnlyNumbers;
            segStem_smoothness_textbox.KeyPress += TextBox_KeyPressOnlyNumbers;
            threshold_height_textbox.KeyPress += TextBox_KeyPressOnlyNumbers;
            measure_maxRad_textbox.KeyPress += TextBox_KeyPressOnlyNumbers;
            measure_minRad_textbox.KeyPress += TextBox_KeyPressOnlyNumbers;
            */


            //정수만 입력하도록 하는 핸들러
            tbNorCellSize.KeyPress += TextBox_KeyPress_OnlyInt;
            tbNorWinSize.KeyPress += TextBox_KeyPress_OnlyInt;
            //CSP_nnearest_textbox.KeyPress += TextBox_KeyPress_OnlyInt;
            //measure_iterations_textbox.KeyPress += TextBox_KeyPress_OnlyInt;


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
        /// <summary>
        /// preset,recent 설정파일 저장용코드
        /// </summary>
        /// <param name="csvContent"></param>
        private void setAllparams(ref StringBuilder csvContent)
        {
            csvContent = new StringBuilder();
            //일단 고정값 받게 해놓고 추후 plot 값 넣어줄 예정(Recent일 때만)
            csvContent.AppendLine($"gui,public,circle,cx={gui.centerX} cy={gui.centerY} radius={gui.radius},{guiDataList[0].explain}");
            csvContent.AppendLine($"gui,public,rectangle,xmin={gui.xMin} ymin={gui.yMin} xmax={gui.xMax} ymax={gui.yMax},{guiDataList[1].explain}");
            csvContent.AppendLine($"gui,public,result_path,{gui.resultPath},{guiDataList[2].explain}");
            csvContent.AppendLine($"filters.crop,private,buffer,{crop.buffer},{guiDataList[3].explain}");
            csvContent.AppendLine($"filters.sample,public,cell,{tbSubCellSize.Text},{guiDataList[4].explain}");
            csvContent.AppendLine($"filters.outlier,private,method,{outlier.method},{guiDataList[5].explain}");
            csvContent.AppendLine($"filters.outlier,private,mean_k,{outlier.mean_k},{guiDataList[6].explain}");
            csvContent.AppendLine($"filters.outlier,private,multiplier,{outlier.Multiplier},{guiDataList[7].explain}");
            csvContent.AppendLine($"filters.smrf,public,cell,{tbNorCellSize.Text},{guiDataList[8].explain}");
            csvContent.AppendLine($"filters.smrf,public,window,{tbNorWinSize.Text},{guiDataList[9].explain}");
            csvContent.AppendLine($"filters.smrf,public,slope,{tbNorSlope.Text},{guiDataList[10].explain}");
            csvContent.AppendLine($"filters.smrf,public,scalar,{tbNorScalar.Text},{guiDataList[11].explain}");
            csvContent.AppendLine($"filters.smrf,public,threshold,{tbNorThres.Text},{guiDataList[12].explain}");
            csvContent.AppendLine($"filters.range.trunk,public,minheight,{tbTrunkMinHeight.Text},{guiDataList[13].explain}");
            csvContent.AppendLine($"filters.range.trunk,public,maxheight,{tbTrunkMaxHeight.Text},{guiDataList[14].explain}");
            csvContent.AppendLine($"filters.range.crown,public,minheight,{tbCrownMinHeight.Text},{guiDataList[15].explain}");
            csvContent.AppendLine($"filters.range.crown,public,maxheight,{tbCrownMaxHeight.Text},{guiDataList[16].explain}");
            csvContent.AppendLine($"csp_segmentcrown,private,num_nn_samples,{csp_crown.num_nn_samples},{guiDataList[17].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,smoothness,{csp_stem.smoothness},{guiDataList[18].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,mindbh,{csp_stem.minDBH},{guiDataList[19].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,maxdbh,{csp_stem.maxDBH},{guiDataList[20].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,nnearest,{csp_stem.nnearest},{guiDataList[21].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,nmin,{csp_stem.nmin},{guiDataList[22].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,num_neighbours,{csp_stem.num_neighbours},{guiDataList[23].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,anglemax,{csp_stem.anglemax},{guiDataList[24].explain}");
            csvContent.AppendLine($"measure,private,nnearest,{measure.MeasureNN},{guiDataList[25].explain}");
            csvContent.AppendLine($"measure,private,minrad,{measure.minRad},{guiDataList[26].explain}");
            csvContent.AppendLine($"measure,private,maxrad,{measure.maxRad},{guiDataList[27].explain}");
            csvContent.AppendLine($"measure,private,iterations,{measure.iterations},{guiDataList[28].explain}");
            csvContent.AppendLine($"measure,private,zmin_check,{measure.zmin_check},{guiDataList[29].explain}");
            csvContent.Append($"measure,private,zmax_check,{measure.zmax_check},{guiDataList[30].explain}");
        }

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
                    break;
                case configFileType.Preset:
                    setAllparams(ref csvContent);
                    infoRecent = new string[] {
                        $"FileInfo,public,fileType,{confType},AAA",
                        $"FileInfo,public,title,{"config" + confCheck.Length.ToString()},AAA"
                    };
                    for (int i = infoRecent.Length - 1; i >= 0; i--)
                    {
                        csvContent.Insert(0, infoRecent[i] + Environment.NewLine);
                    }
                    filePath = Path.Combine(filePath, $"presetConfig{confCheck.Length.ToString()}.csv");
                    break;
                default:
                    throw new Exception("unknown error: configFileType.Exception");
            }

            // CSV 파일 생성 및 내용 기록
            try
            {
                File.WriteAllText(filePath, csvContent.ToString(),Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSV 파일 생성 중 오류 발생: " + ex.Message);
            }
        }

        //공장초기화 함수
        private void FactoryReset(string filepath)
        {
            // CSV 파일 경로
            string filePath = filepath;

            // CSV 내용 생성
            StringBuilder csvContent = new StringBuilder();

            //설명 변경해야함
            csvContent.AppendLine("gui,public,circle,cx=0 cy=0 radius=100,cx cy radius는 중심점 좌표와 반지름입니다.");
            csvContent.AppendLine("gui,public,rectangle,xmin=-10 ymin=-10 xmax=10 ymax=10,xmin xmax ymin ymax는 범위 자료입니다. ");
            csvContent.AppendLine("gui,public,result_path,..\\result,결과를 저장하는 폴더입니다. ");
            csvContent.AppendLine("filters.crop,private,buffer,120,plot 영역보다 120% 큰 영역을 의미한다.");
            csvContent.AppendLine("filters.sample,public,cell,0.01,복셀 크기 (예: 0.03m)");
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
            csvContent.Append("measure,private,zmax_check,0.7,나무가 표준지의 속하는지 판단하기 위해 사용 된. 기준포인트들 중 최상점의 높이다");

            // CSV 파일 생성 및 내용 기록
            try
            {
                File.WriteAllText(filePath, csvContent.ToString(),Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSV 파일 작성 중 오류 발생: " + ex.Message);
            }
        }
    }
}
