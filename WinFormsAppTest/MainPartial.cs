﻿using Newtonsoft.Json;
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


        struct GuiData
        {
            public string Type;
            public string Visibility;
            public string Key;
            public string Value;
        }
        List<GuiData> guiDataList = new List<GuiData>();


        /// <summary>
        /// json 읽는 함수 삭제해도 됨
        /// </summary>
        private void Initialize_Params()
        {
            /*
            // 파일에서 JSON 데이터 읽기
            string filePath = "..\\bin\\config.json";
            string json = File.ReadAllText(filePath);
            // JSON 데이터를 객체로 변환
            var jsonArray = JsonConvert.DeserializeObject<dynamic>(json);

            dynamic JObject = jsonArray[0].GUI;

            gui.radius = JObject.circle.radius;
            gui.centerX = JObject.circle.cx;
            gui.centerY = JObject.circle.cy;
            gui.xMin = JObject.rectangle.xmin;
            gui.yMin = JObject.rectangle.ymin;
            gui.xMax = JObject.rectangle.xmax;
            gui.yMax = JObject.rectangle.ymax;
            gui.resultPath = JObject.result_path;

            JObject = jsonArray[1].Crop;
            crop.buffer = JObject.buffer;

            JObject = jsonArray[2].Sub;
            subsampling.cellSize = JObject.Sub_cell;

            JObject = jsonArray[3].Outlier;
            outlier.method = JObject.method;
            outlier.mean_k = JObject.mean_k;
            outlier.Multiplier = JObject.multiplier;

            JObject = jsonArray[4].Ground;
            groundseg.scalar = JObject.scalar;
            groundseg.slope = JObject.slope;
            groundseg.cellSize = JObject.Ground_cell;
            groundseg.windowSize = JObject.window;
            groundseg.threshold = JObject.threshold;

            JObject = jsonArray[5].TSlice;
            tSlice.minHeight = JObject.T_minheight;
            tSlice.maxHeight = JObject.T_maxheight;

            JObject = jsonArray[6].CSlice;
            cSlice.minHeight = JObject.C_minheight;
            cSlice.maxHeight = JObject.C_maxheight;

            JObject = jsonArray[7].Crownseg;
            crownSeg.CrownNN = JObject.Crown_nnearest;

            JObject = jsonArray[8].Measure;
            measure.MeasureNN = JObject.Measure_nnearest;
            measure.minRad = JObject.minRad;
            measure.maxRad = JObject.maxRad;
            measure.iterations = JObject.iterations;

            JObject = jsonArray[9].SegmentStem;
            segmentStem.smoothness = JObject.smoothness;
            segmentStem.minDBH = JObject.mindbh;
            segmentStem.maxDBH = JObject.maxdbh;
            segmentStem.HeightThreshold = JObject.heightThreshold;*/
        }
        /// <summary>
        /// csv 읽는 함수
        /// </summary>
        private void read_csv()
        {
            //csv 읽기
            string csvFilePath = @"..\bin\config.csv";

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
                                Value = values[3]
                            };
                            if (guiData.Key == "circle")
                            {
                                ExtractCircleValues(guiData.Value);
                            }
                            if (guiData.Key == "rectangle")
                            {
                                ExtractRectangleValues( guiData.Value);
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
                            case "nnearest":
                                csp_crown.CrownNN = int.Parse(data.Value);
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
                            case "height_threshold":
                                csp_stem.HeightThreshold = double.Parse(data.Value);
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

        public void write_csv()
        {
            // CSV 파일 경로
            string filePath = @"..\bin\config.csv";

            // CSV 내용 생성
            StringBuilder csvContent = new StringBuilder();
            csvContent.AppendLine("gui,public,circle,cx=0.0 cy=0.0 radius=100.0,AAA");
            csvContent.AppendLine("gui,public,rectangle,xmin=-10.0 ymin=-10.0 xmax=10.0 ymax=10.0,AAA");
            csvContent.AppendLine("gui,public,result_path,..\\\\result,AAA");
            csvContent.AppendLine("filters.crop,private,buffer,120,AAA");
            csvContent.AppendLine("filters.sample,public,cell,"+subsampling.cellSize+",AAA");
            csvContent.AppendLine("filters.outlier,private,method,statistical,AAA");
            csvContent.AppendLine("filters.outlier,private,mean_k,12,AAA");
            csvContent.AppendLine("filters.outlier,private,multiplier,2.2,AAA");
            csvContent.AppendLine("filters.smrf,public,cell," + groundseg.cellSize + ",AAA");
            csvContent.AppendLine("filters.smrf,public,window," + groundseg.windowSize + ",AAA");
            csvContent.AppendLine("filters.smrf,public,slope," + groundseg.slope + ",AAA");
            csvContent.AppendLine("filters.smrf,public,scalar," + groundseg.scalar + ",AAA");
            csvContent.AppendLine("filters.smrf,public,threshold," + groundseg.threshold +",AAA");
            csvContent.AppendLine("filters.range.trunk,public,minheight,"+tSlice.minHeight+",AAA");
            csvContent.AppendLine("filters.range.trunk,public,maxheight," + tSlice.maxHeight + ",AAA");
            csvContent.AppendLine("filters.range.crown,public,minheight," + cSlice.minHeight + ",AAA");
            csvContent.AppendLine("filters.range.crown,public,maxheight," + cSlice.maxHeight + ",AAA");
            csvContent.AppendLine("csp_segmentcrown,private,nnearest,16,AAA");
            csvContent.AppendLine("csp_segmentstem,private,smoothness,8,AAA");
            csvContent.AppendLine("csp_segmentstem,private,mindbh,0.01,AAA");
            csvContent.AppendLine("csp_segmentstem,private,maxdbh,1,AAA");
            csvContent.AppendLine("csp_segmentstem,private,height_threshold,1,AAA");
            csvContent.AppendLine("measure,private,nnearest,16,AAA");
            csvContent.AppendLine("measure,private,minrad,0.03,AAA");
            csvContent.AppendLine("measure,private,maxrad,0.5,AAA");
            csvContent.AppendLine("measure,private,iterations,10000,AAA");
            csvContent.AppendLine("measure,private,zmin_check,0.2,AAA");
            csvContent.AppendLine("measure,private,zmax_check,0.7,AAA");

            // CSV 파일 생성 및 내용 기록
            try
            {
                File.WriteAllText(filePath, csvContent.ToString());
                MessageBox.Show("CSV 파일이 수정되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSV 파일 생성 중 오류 발생: " + ex.Message);
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
                    switch(key)
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
        private void UpdateParams()
        {
            //subsamplng_textbox
            subsampling.cellSize = double.Parse(tbSubCellSize.Text);

            //outlierRemoving_textboxes
            outlier.mean_k = int.Parse(tbOutlierMeank.Text);
            outlier.Multiplier = double.Parse(tbOutlierMul.Text);

            groundseg.scalar = tbNorScalar.Text;
            groundseg.cellSize = tbNorCellSize.Text;
            groundseg.slope = tbNorSlope.Text;
            groundseg.windowSize = tbNorWinSize.Text;
            groundseg.threshold = tbNorThres.Text;

            //trunkSlice_textboxes
            tSlice.minHeight = double.Parse(tbTrunkMinHeight.Text);
            tSlice.maxHeight = double.Parse(tbTrunkMaxHeight.Text);

            //CrownSlice_textboxes
            cSlice.minHeight = double.Parse(tbCrownMinHeight.Text);
            cSlice.maxHeight = double.Parse(tbCrownMaxHeight.Text);

            ////treeSegment_textbox
            csp_crown.CrownNN = int.Parse(tbTreeSegNN.Text);

            //MeasureAttribute_textbox
            measure.MeasureNN = int.Parse(tbMeasureNN.Text);

            ////trunkSegment_textboxes
            csp_stem.smoothness = double.Parse(tbTreeSegSmooth.Text);
            csp_stem.minDBH = double.Parse(tbTreeSegMinDBH.Text);
            csp_stem.HeightThreshold = double.Parse(tbTreeSegHeightThres.Text);
        }

        private void FillTextboxes()
        {
            //subsamplng_textboxes
            tbSubCellSize.Text = subsampling.cellSize.ToString();

            //outlierRemoving_textboxes
            tbOutlierMeank.Text = outlier.mean_k.ToString();
            tbOutlierMul.Text = outlier.Multiplier.ToString();

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

            ////treeSegment_textbox
            tbTreeSegNN.Text = csp_crown.CrownNN.ToString();

            ////trunkSegment_textboxes
            tbTreeSegSmooth.Text = csp_stem.smoothness.ToString();
            tbTreeSegMinDBH.Text = csp_stem.minDBH.ToString();
            tbTreeSegHeightThres.Text = csp_stem.HeightThreshold.ToString();

            //measure_textbox
            tbMeasureNN.Text = measure.MeasureNN.ToString();
            ///
        }

        private void RegistTextBoxHandler()
        {
            //double형만 입력가능하도록 키 등록
            tbSubCellSize.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbOutlierMeank.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbOutlierMul.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbNorScalar.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbNorSlope.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbNorThres.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbTrunkMinHeight.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbTrunkMaxHeight.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbCrownMinHeight.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbCrownMaxHeight.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbTreeSegSmooth.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbTreeSegMinDBH.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbTreeSegHeightThres.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbTreeSegNN.KeyPress += TextBox_KeyPressOnlyNumbers;
            tbMeasureNN.KeyPress += TextBox_KeyPressOnlyNumbers;
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
            tbTreeSegNN.KeyPress += TextBox_KeyPress_OnlyInt;
            tbTreeSegNN.KeyPress += TextBox_KeyPress_OnlyInt;
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
        private void setAllparams(ref List<object> data)
        {
            Dictionary<string, double> plotData = new Dictionary<string, double>();

            if (Application.OpenForms["PlotForm"] != null)
            {
                plotData = plotSender();
            }          
            else
            {
                plotData.Add("cx", gui.centerX);
                plotData.Add("cy", gui.centerY);
                plotData.Add("radius", gui.radius);

                plotData.Add("xmin", gui.xMin);
                plotData.Add("ymin", gui.yMin);
                plotData.Add("xmax", gui.xMax);
                plotData.Add("ymax", gui.yMax);
            }

            data = new List<object>
            {
                new
                {
                    GUI = new
                    {
                        circle = new
                        {
                            cx = plotData["cx"],
                            cy = plotData["cy"],
                            radius = plotData["radius"]
                        },
                        rectangle = new
                        {
                            xmin = plotData["xmin"],
                            ymin = plotData["ymin"],
                            xmax = plotData["xmax"],
                            ymax = plotData["ymax"]
                        },
                        result_path = "..\\result"
                    }
                },
                new
                {
                    Crop = new
                    {
                        buffer = 120.0
                    }
                },
                new
                {
                    Sub = new
                    {
                        Sub_cell = double.Parse(tbSubCellSize.Text)
                    }
                },
                new
                {
                    Outlier = new
                    {
                        method = "statistical",
                        mean_k = int.Parse(tbOutlierMeank.Text),
                        multiplier = double.Parse(tbOutlierMul.Text)
                    }
                },
                new
                {
                    Ground = new
                    {
                        Ground_cell = double.Parse(tbNorCellSize.Text),
                        window = double.Parse(tbNorWinSize.Text),
                        slope = double.Parse(tbNorSlope.Text),
                        scalar = double.Parse(tbNorScalar.Text),
                        threshold = double.Parse(tbNorThres.Text)
                    }
                },
                new
                {
                    TSlice = new
                    {
                        T_minheight = double.Parse(tbTrunkMinHeight.Text),
                        T_maxheight = double.Parse(tbTrunkMaxHeight.Text)
                    }
                },
                new
                {
                    CSlice = new
                    {
                        C_minheight = double.Parse(tbCrownMinHeight.Text),
                        C_maxheight = int.Parse(tbCrownMaxHeight.Text)
                    }
                },
                new
                {
                    Crownseg = new
                    {
                        Crown_nnearest = int.Parse(tbTreeSegNN.Text)
                    }
                },
                new
                {
                    Measure = new
                    {
                        Measure_nnearest = int.Parse(tbMeasureNN.Text),
                        minRad = 0.03,
                        maxRad = 0.5,
                        iterations = 10000,
                        zmin_check = 0.2,
                        zmax_check = 0.7
                    }
                },
                new
                {
                    SegmentStem = new
                    {
                        smoothness = int.Parse(tbTreeSegSmooth.Text),
                        mindbh = double.Parse(tbTreeSegMinDBH.Text),
                        maxdbh = 0.8,
                        heightThreshold = int.Parse(tbTreeSegHeightThres.Text)
                    }
                }
            };
        }

        private void MakeConfig(configFileType confType)
        {
            string filePath = configPath + reqDi[(int)confType];

            //해당 폴더 없을 시 만들기
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            //해당 폴더 내 config파일 갯수 확인
            string[] confCheck = Directory.GetFiles(Path.Combine(configPath, reqDi[(int)confType]), "*config*");

            List<object> data = new List<object>
            {
                new
                {
                    GUI = new
                    {
                        circle = new
                        {
                            cx = 0.0,
                            cy = 0.0,
                            radius = 0.0
                        },
                        rectangle = new
                        {
                            xmin = 0.0,
                            ymin = 0.0,
                            xmax = 0.0,
                            ymax = 0.0
                        },
                        result_path = "..\\result"
                    }
                },
                new
                {
                    Crop = new
                    {
                        buffer = 120.0
                    }
                },
                new
                {
                    Sub = new
                    {
                        Sub_cell = 0.03
                    }
                },
                new
                {
                    Outlier = new
                    {
                        method = "statistical",
                        mean_k = 12,
                        multiplier = 2.2
                    }
                },
                new
                {
                    Ground = new
                    {
                        Ground_cell = 4.0,
                        window = 16.0,
                        slope = 0.3,
                        scalar = 1.25,
                        threshold = 0.15
                    }
                },
                new
                {
                    TSlice = new
                    {
                        T_minheight = 0.0,
                        T_maxheight = 4.8
                    }
                },
                new
                {
                    CSlice = new
                    {
                        C_minheight = 4.8,
                        C_maxheight = 100.0
                    }
                },
                new
                {
                    Crownseg = new
                    {
                        Crown_nnearest = 16
                    }
                },
                new
                {
                    Measure = new
                    {
                        Measure_nnearest = 16,
                        minRad = 0.03,
                        maxRad = 0.5,
                        iterations = 10000,
                        zmin_check = 0.2,
                        zmax_check = 0.7
                    }
                },
                new
                {
                    SegmentStem = new
                    {
                        smoothness = 16.0,
                        mindbh = 0.06,
                        maxdbh = 0.8,
                        heightThreshold = 9.0
                    }
                }
            };

            

            //config 파일 종류별 전처리(종류별로 필요한 데이터 추가)
            switch (confType)
            {
                case configFileType.Default:
                    filePath += @"\config.json";
                    break;
                case configFileType.Recent:
                    Dictionary<string, double> plotData = plotSender();
                    setAllparams(ref data);
                    var infoRecent = new
                    {
                        FIleInfo = new
                        {
                            fileType = confType,
                            title = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            selection = plotData["selection"]
                        }
                    };
                    data.Insert(0, infoRecent);
                    filePath += @"\recentConfig0.json";
                    for (int i = confCheck.Length - 1; i >= 0; i--)
                    {
                        string oldPath = Path.Combine(Path.Combine(configPath, reqDi[(int)configFileType.Recent]) + @"\", $"recentConfig{i}.json");
                        string newPath = Path.Combine(Path.Combine(configPath, reqDi[(int)configFileType.Recent]), $"recentConfig{i + 1}.json");

                        if (i >= 4)
                        {
                            continue;
                        }
                        if (File.Exists(oldPath) && File.Exists(newPath))
                        {
                            File.Delete(newPath);
                        }
                        File.Move(oldPath, newPath);
                    }
                    break;
                case configFileType.Preset:
                    setAllparams(ref data);
                    var infoPreset = new
                    {
                        FIleInfo = new
                        {
                            fileType = confType,
                            title = "config" + confCheck.Length.ToString(),
                        }
                    };
                    data.Insert(0, infoPreset);
                    filePath += @"\presetConfig" + confCheck.Length.ToString() + ".json";
                    break;
                default:
                    throw new Exception("unknown error: configFileType.Exception");
            }

            string json = System.Text.Json.JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            
            File.WriteAllText(filePath, json);
        }
    }
}
