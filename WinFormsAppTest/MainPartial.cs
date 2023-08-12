using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using WK.Libraries.BetterFolderBrowserNS;

namespace WinFormsAppTest
{
    partial class MainForm
    {
        //변수 구조체
        public GUI gui = new GUI();
        public Crop crop = new Crop();
        public Subsampling subsampling = new Subsampling();
        public Outlier outlier = new Outlier();
        public GroundSeg groundseg = new GroundSeg();
        public TSlice tSlice = new TSlice();
        public CSlice cSlice = new CSlice();

        //임시변수--> 삭제
        public CrownSeg crownSeg = new CrownSeg();
        public Measure measure = new Measure();
        public SegmentStem segmentStem = new SegmentStem();
        private void Initialize_Params()
        {
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
            segmentStem.HeightThreshold = JObject.heightThreshold;
        }

        private void UpdateParams()
        {
            //subsamplng_textbox
            subsampling.cellSize = double.Parse(tbSubCellSize.Text);

            //outlierRemoving_textboxes
            outlier.mean_k = int.Parse(tbOutlierMeank.Text);
            outlier.Multiplier = double.Parse(tbOutlierMul.Text);


            //trunkSlice_textboxes
            tSlice.minHeight = double.Parse(tbTrunkMinHeight.Text);
            tSlice.maxHeight = double.Parse(tbTrunkMaxHeight.Text);

            //CrownSlice_textboxes
            cSlice.minHeight = double.Parse(tbCrownMinHeight.Text);
            cSlice.maxHeight = double.Parse(tbCrownMaxHeight.Text);

            ////treeSegment_textbox
            crownSeg.CrownNN = int.Parse(tbTreeSegNN.Text);

            //MeasureAttribute_textbox
            measure.MeasureNN = int.Parse(tbMeasureNN.Text);

            ////trunkSegment_textboxes
            segmentStem.smoothness = double.Parse(tbTreeSegSmooth.Text);
            segmentStem.minDBH = double.Parse(tbTreeSegMinDBH.Text);
            segmentStem.HeightThreshold = double.Parse(tbTreeSegHeightThres.Text);
        }

        private void FillTextboxes()
        {
            //subsamplng_textboxes
            tbSubCellSize.Text = subsampling.cellSize.ToString();

            //outlierRemoving_textboxes
            tbOutlierMeank.Text = outlier.mean_k.ToString();
            tbOutlierMul.Text = outlier.Multiplier.ToString();


            //trunkSlice_textboxes
            tbTrunkMinHeight.Text = tSlice.minHeight.ToString();
            tbTrunkMaxHeight.Text = tSlice.maxHeight.ToString();

            //CrownSlice_textboxes
            tbCrownMinHeight.Text = cSlice.minHeight.ToString();
            tbCrownMaxHeight.Text = cSlice.maxHeight.ToString();

            ////treeSegment_textbox
            tbTreeSegNN.Text = crownSeg.CrownNN.ToString();

            ////trunkSegment_textboxes
            tbTreeSegSmooth.Text = segmentStem.smoothness.ToString();
            tbTreeSegMinDBH.Text = segmentStem.minDBH.ToString();
            tbTreeSegHeightThres.Text = segmentStem.HeightThreshold.ToString();

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

        private void customBtn3_Click(object sender, EventArgs e)
        {
            var betterFolderBrowser = new BetterFolderBrowser();
            string[] foldername;
            string myfoldername = "";
            betterFolderBrowser.Title = "폴더 찾아보기";
            betterFolderBrowser.RootFolder = "C:\\";

            // Allow multi-selection of folders.
            betterFolderBrowser.Multiselect = false;

            if (betterFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                foldername = betterFolderBrowser.SelectedFolders;
                myfoldername = foldername[0];
            }
            gui.resultPath = myfoldername;
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

        private void setJsonData<T>(List<object> data, string prop, string subProp, T value)
        {
            var subObject = data.FirstOrDefault(ob =>
            {
                var subTitle = ob.GetType().GetProperty(prop);
                return subTitle != null;
            });

            var param = subObject.GetType().GetProperty(subProp);
            param.SetValue(subObject, value);
        }

        private void setAllparams(List<object> data)
        {
            setJsonData(data, "Sub", "Sub_cell", double.Parse(tbSubCellSize.Text));
            setJsonData(data, "Outlier", "method", double.Parse(tbOutlierMethod.Text));
            setJsonData(data, "Outlier", "mean_k", double.Parse(tbOutlierMeank.Text));
            setJsonData(data, "Outlier", "multiplier", double.Parse(tbOutlierMul.Text));
            setJsonData(data, "TSlice", "T_minheight", double.Parse(tbTrunkMinHeight.Text));
            setJsonData(data, "TSlice", "T_maxheight", double.Parse(tbTrunkMaxHeight.Text));
            setJsonData(data, "CSlice", "C_minheight", double.Parse(tbCrownMinHeight.Text));
            setJsonData(data, "CSlice", "C_maxheight", double.Parse(tbCrownMaxHeight.Text));
            setJsonData(data, "Crownseg", "Crown_nnearest", double.Parse(tbTreeSegNN.Text));
            setJsonData(data, "SegmentStem", "smoothness", double.Parse(tbTreeSegSmooth.Text));
            setJsonData(data, "SegmentStem", "heightThreshold", double.Parse(tbTreeSegHeightThres.Text));
            setJsonData(data, "SegmentStem", "mindbh", double.Parse(tbTreeSegMinDBH.Text));
            setJsonData(data, "Measure", "Measure_nnearest", double.Parse(tbMeasureNN.Text));
        }

        private void MakeConfig(configFileType confType)
        {
            string filePath = configPath + reqDi[(int)confType];

            //해당 폴더 없을 시 만들기
            if(!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            //해당 폴더 내 config파일 갯수 확인
            string[] confCheck = Directory.GetFiles(Path.Combine(configPath, reqDi[(int)confType]), "config*");
            
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
                    setAllparams(data);
                    var infoRecent = new
                    {
                        FIleInfo = new
                        {
                            fileType = confType,
                            title = DateTime.Now
                        }
                    };
                    data.Insert(0, infoRecent);
                    filePath += @"\config" + confCheck.Length.ToString() + ".json";
                    break;
                case configFileType.Preset:
                    setAllparams(data);
                    var infoPreset = new
                    {
                        FIleInfo = new
                        {
                            fileType = confType,
                            title = "tmp"
                        }
                    };
                    data.Insert(0, infoPreset);
                    filePath += @"\config" + confCheck.Length.ToString() + ".json";
                    break;
                default:
                    throw new Exception("unknown error: configFileType.Exception");
            }

            string json = System.Text.Json.JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(filePath, json);

            /*
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                saveDialog.FilterIndex = 1;
                saveDialog.RestoreDirectory = true;

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveDialog.FileName;

                    string json = System.Text.Json.JsonSerializer.Serialize(data, new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

                    File.WriteAllText(filePath, json);

                    Console.WriteLine($"JSON file saved at: {filePath}");
                }
                else
                {
                    Console.WriteLine("File save canceled.");
                }
            }

            Console.WriteLine("preset.json 파일이 생성되었습니다.");*/
        }


        private void MakeConfig()
        {
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
                            radius = 11.3
                        },
                        rectangle = new
                        {
                            xmin = -5.0,
                            ymin = -5.0,
                            xmax = 5.0,
                            ymax = 5.0
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

            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                saveDialog.FilterIndex = 1;
                saveDialog.RestoreDirectory = true;

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveDialog.FileName;

                    string json = System.Text.Json.JsonSerializer.Serialize(data, new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

                    File.WriteAllText(filePath, json);

                    Console.WriteLine($"JSON file saved at: {filePath}");
                }
                else
                {
                    Console.WriteLine("File save canceled.");
                }
            }

            Console.WriteLine("preset.json 파일이 생성되었습니다.");
        }

        /* private bool CheckIntegrity()
        {
            //원형 표준지에 필요한 값들이 비어있는경우
            bool isEmptyVal_cir = x_center_textbox.Text == "" && y_center_textbox.Text == "" && circle_raidus_textbox.Text == "";
            //radius값이 0이하인지 확인
            bool isRadiusZero = (double.Parse(circle_raidus_textbox.Text) <= 0);

            //사각형 표준지에 필요한 값들이 비어있는경우
            bool isEmptyVal_rec = Xmin_textbox.Text == "" && Xmax_textbox.Text == "" && Ymin_textbox.Text == "" && Ymax_textbox.Text == "";

            //LT의 x, RB의 x || LT의 y, RB의 y값이 같은지 확인
            bool hasEqualXY = (double.Parse(Xmax_textbox.Text) <= double.Parse(Xmin_textbox.Text)) || (double.Parse(Ymax_textbox.Text) <= double.Parse(Ymin_textbox.Text));



            if (isRadiusZero || isEmptyVal_cir || isEmptyVal_rec || hasEqualXY)
            {
                return false;
            }
            else
            {
                return true;
            }

        }*/
        /*public void ReadConfig()
        {
            // 파일에서 JSON 데이터 읽기
            string filePath = "..\\bin\\config.json";
            string json = File.ReadAllText(filePath);
            // JSON 데이터를 객체로 변환
            var jsonArray = JsonConvert.DeserializeObject<dynamic>(json);

            dynamic Jobj = jsonArray[0].gui;

            gui.radius = Jobj.circle.radius;
            gui.centerX = Jobj.circle.cx;
            gui.centerY = Jobj.circle.cy;
            gui.xMin = Jobj.rectangle.xmin;
            gui.yMin = Jobj.rectangle.ymin;
            gui.xMax = Jobj.rectangle.xmax;
            gui.yMax = Jobj.rectangle.ymax;
            gui.resultPath = Jobj.result_path;

            Jobj = jsonArray[1].Crop;
            crop.buffer = Jobj.buffer;

            Jobj = jsonArray[2].Sub;
            subsampling.cellSize = Jobj.Sub_cell;

            Jobj = jsonArray[3].Outlier;
            outlier.method = Jobj.method;
            outlier.mean_k = Jobj.mean_k;
            outlier.Multiplier = Jobj.multiplier;

            Jobj = jsonArray[4].Ground;
            groundseg.scalar = Jobj.scalar;
            groundseg.slope = Jobj.slope;
            groundseg.cellSize = Jobj.Ground_cell;
            groundseg.windowSize = Jobj.window;
            groundseg.threshold = Jobj.threshold;

            Jobj = jsonArray[5].TSlice;
            tSlice.minHeight = Jobj.T_minheight;
            tSlice.maxHeight = Jobj.T_maxheight;

            Jobj = jsonArray[6].CSlice;
            cSlice.minHeight = Jobj.C_minheight;
            cSlice.maxHeight = Jobj.C_maxheight;


            //임시변수--> 삭제
            Jobj = jsonArray[9].SegmentStem;
            segmentStem.smoothness = Jobj.smoothness;
            segmentStem.minDBH = Jobj.mindbh;
            segmentStem.maxDBH = Jobj.maxdbh;

        }*/
    }
}

