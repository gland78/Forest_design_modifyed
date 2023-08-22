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

        public string csv_path = Path.Combine(configPath, "config.csv");
        
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
                using (StreamReader reader = new StreamReader(csvFilePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] values = line.Split(',');
                        if (values.Length >= 4)
                        {
                            GuiData guiData = new GuiData
                            {
                                Type = values[0],
                                Visibility = values[1],
                                Key = values[2],
                                Value = values[3],
                                explain= values[4]
                            };
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
                Console.WriteLine("An error occurred: " + ex.Message);
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
            /*foreach (var item in guiDataList)
            {
                string str = item.Type.ToString() + "," + item.Visibility.ToString() + "," + item.Key + "," + item.Value + ","+item.explain;
                csvContent.AppendLine(str);
            }*/
            csvContent.AppendLine($"gui,public,circle,cx={gui.centerX} cy={gui.centerY} radius={gui.radius},{guiDataList[0].explain}");
            csvContent.AppendLine($"gui,public,rectangle,xmin= {gui.xMin} ymin={gui.yMin} xmax={gui.xMax} ymax={gui.yMax},{guiDataList[1].explain}");
            csvContent.AppendLine($"gui,public,result_path,{gui.resultPath},{guiDataList[2].explain}");
            csvContent.AppendLine($"filters.crop,private,buffer,{crop.buffer} ,{guiDataList[3].explain}");
            csvContent.AppendLine($"filters.sample,public,cell,{tbSubCellSize.Text},{guiDataList[4].explain}");
            csvContent.AppendLine($"filters.outlier,private,method,{outlier.method},{guiDataList[5].explain}");
            csvContent.AppendLine($"filters.outlier,private,mean_k,{outlier.mean_k},{guiDataList[6].explain}");
            csvContent.AppendLine($"filters.outlier,private,multiplier,{outlier.Multiplier} ,{guiDataList[7].explain}");
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
            csvContent.AppendLine($"csp_segmentstem,private,smoothness,{csp_stem.smoothness} ,{guiDataList[18].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,mindbh,{csp_stem.minDBH} ,{guiDataList[19].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,maxdbh,{csp_stem.maxDBH},{guiDataList[20].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,nnearest,{csp_stem.nnearest} ,{guiDataList[21].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,nmin,{csp_stem.nmin},{guiDataList[22].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,num_neighbours,{csp_stem.num_neighbours} ,{guiDataList[23].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,anglemax,{csp_stem.anglemax} ,{guiDataList[24].explain}");
            csvContent.AppendLine($"measure,private,nnearest,{measure.MeasureNN},{guiDataList[25].explain}");
            csvContent.AppendLine($"measure,private,minrad,{measure.minRad} ,{guiDataList[26].explain}");
            csvContent.AppendLine($"measure,private,maxrad,{measure.maxRad} ,{guiDataList[27].explain}");
            csvContent.AppendLine($"measure,private,iterations,{measure.iterations} ,{guiDataList[28].explain}");
            csvContent.AppendLine($"measure,private,zmin_check,{measure.zmin_check},{guiDataList[29].explain}");
            csvContent.Append($"measure,private,zmax_check,{measure.zmax_check},{guiDataList[30].explain}");

            // CSV 파일 생성 및 내용 기록
            try
            {
                File.WriteAllText(filePath, csvContent.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSV 파일 생성 중 오류 발생: " + ex.Message);
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
            csvContent.AppendLine($"gui,public,circle,cx={gui.centerX} cy={gui.centerY} radius={ gui.radius},{ guiDataList[0].explain}");
            csvContent.AppendLine($"gui,public,rectangle,xmin= {gui.xMin} ymin={gui.yMin} xmax={gui.xMax} ymax={gui.yMax},{guiDataList[1].explain}");
            csvContent.AppendLine($"gui,public,result_path,{ gui.resultPath},{guiDataList[2].explain}");
            csvContent.AppendLine($"filters.crop,private,buffer,{ crop.buffer} ,{guiDataList[3].explain}");
            csvContent.AppendLine($"filters.sample,public,cell,{tbSubCellSize.Text},{guiDataList[4].explain}");
            csvContent.AppendLine($"filters.outlier,private,method,{ outlier.method},{guiDataList[5].explain}");
            csvContent.AppendLine($"filters.outlier,private,mean_k,{ outlier.mean_k },{guiDataList[6].explain}");
            csvContent.AppendLine($"filters.outlier,private,multiplier,{ outlier.Multiplier} ,{guiDataList[7].explain}");
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
            csvContent.AppendLine($"csp_segmentstem,private,smoothness,{ csp_stem.smoothness} ,{guiDataList[18].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,mindbh,{ csp_stem.minDBH} ,{guiDataList[19].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,maxdbh,{ csp_stem.maxDBH },{guiDataList[20].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,nnearest,{ csp_stem.nnearest} ,{guiDataList[21].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,nmin,{ csp_stem.nmin },{guiDataList[22].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,num_neighbours,{ csp_stem.num_neighbours} ,{guiDataList[23].explain}");
            csvContent.AppendLine($"csp_segmentstem,private,anglemax,{ csp_stem.anglemax} ,{guiDataList[24].explain}");
            csvContent.AppendLine($"measure,private,nnearest,{ measure.MeasureNN },{guiDataList[25].explain}");
            csvContent.AppendLine($"measure,private,minrad,{ measure.minRad} ,{guiDataList[26].explain}");
            csvContent.AppendLine($"measure,private,maxrad,{ measure.maxRad} ,{guiDataList[27].explain}");
            csvContent.AppendLine($"measure,private,iterations,{ measure.iterations} ,{guiDataList[28].explain}");
            csvContent.AppendLine($"measure,private,zmin_check,{ measure.zmin_check },{guiDataList[29].explain}");
            csvContent.Append($"measure,private,zmax_check,{ measure.zmax_check },{guiDataList[30].explain}");
        }

        private void MakeConfig(configFileType confType)
        {
            string filePath = Path.Combine(configPath, reqDi[(int)confType]);

            //해당 폴더 없을 시 만들기
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            //해당 폴더 내 config파일 갯수 확인
            string[] confCheck = Directory.GetFiles(Path.Combine(configPath, reqDi[(int)confType]), "*config*");

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
                        $"FileInfo,public,fileType,{confType},AAA",
                        $"FileInfo,public,title,{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")},AAA",
                        $"FileInfo,public,selection,{plotData["selection"]},AAA" 
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
                File.WriteAllText(filePath, csvContent.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSV 파일 생성 중 오류 발생: " + ex.Message);
            }
        }
    }
}
