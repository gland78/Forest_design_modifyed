using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WinFormsAppTest
{
    public partial class PlotForm : Form
    {
        //파일이 존재하는 디렉토리
        public GUI gui = new GUI();
        public GroundSeg groundseg = new GroundSeg();
        public Subsampling subsampling = new Subsampling();
        public Outlier outlier = new Outlier();
        public TSlice tSlice = new TSlice();
        public CSlice cSlice = new CSlice();
        public Crop crop = new Crop();
        //임시변수 삭제
        public double smoothness;
        public double mindbh;
        public double maxdbh;

        /// 원본 las파일의 경로(path)
        string originLasPath = "";
        /// 결과 파일들을 저장하는 폴더 디렉토리
        string resultPath = "";
        /// 결과를 저장하는 폴더 디렉토리(intermediate,stem,tree등등이 저장되는 폴더)
        string resultSavedDirectory = "";
        string originLasName = "";
        string originLasDirectory = "";
        //자주쓰는 변수 왜 있는지 모르겠음
        string shape;

        //progess표시 변수
        int progress = 0;

        Coords bufferedPolycords = new Coords();

        private Point relativePformPos = new Point();

        public PlotForm()
        {
            InitializeComponent();
            c_pnPlotSelection.isFill = true;
            c_pnPlotSelection.isBorder = false;
            c_pnPlotSelection.borderColor = Color.Honeydew;
            c_pnPlotData.isFill = true;
            c_pnPlotData.isBorder = false;
            c_pnPlotData.borderColor = Color.Honeydew;
        }

        private void PlotForm_Load(object sender, EventArgs e)
        {
            Point screenSize = ((Point)Screen.PrimaryScreen.Bounds.Size);

            this.Location = new Point((screenSize.X - this.Width) / 2, (screenSize.Y - this.Height) / 2);
        }

        private void btnPlotCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPlotOK_Click(object sender, EventArgs e)
        {
            preProAndExcuteStep();

            if (progress == 10)
            {
                MessageBox.Show("실행 완료");
            }
            else
            {
                MessageBox.Show(progress + "단계 에러");
            }
        }

        private void c_btnPlotData_Click(object sender, EventArgs e)
        {
            String fileContent = string.Empty;
            String filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = @"..\LIDAR_PROJECT";
                openFileDialog.Filter = "las files (*.las)|*.las";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;

                    tbPlotData.Text = filePath;
                }
            }
        }











        private void InitializeMemberParam()
        {
            ReadConfig();
            //파일경로 관련 파라미터 초기화

            shape = "_circle";

            resultPath = gui.resultPath;

            /*
            center_x_textbox.Text = gui.centerX.ToString();
            center_y_textbox.Text = gui.centerY.ToString();
            radius_textbox.Text = gui.radius.ToString();
            xmin_textbox.Text = gui.xMin.ToString();
            ymin_textbox.Text = gui.yMin.ToString();
            xmax_textbox.Text = gui.xMax.ToString();
            ymax_textbox.Text = gui.yMax.ToString();
            LoadPath_Box.Text = gui.loadPath;

            if (saved_cx != "") center_x_textbox.Text = saved_cx;
            if (saved_cy != "") center_y_textbox.Text = saved_cy;
            if (saved_ra != "") radius_textbox.Text = saved_ra;
            if (saved_mx != "") xmin_textbox.Text = saved_mx;
            if (saved_my != "") ymin_textbox.Text = saved_my;
            if (saved_Mx != "") xmax_textbox.Text = saved_Mx;
            if (saved_My != "") ymax_textbox.Text = saved_My;
            */
        }
        //PLOT
        private void MakeResultDirectory_PLOT()
        {

            string resultSavedDirectory = this.resultSavedDirectory + shape;
            DirectoryInfo di1 = new DirectoryInfo(resultSavedDirectory);
            if (di1.Exists == true)
            {
                return;
            }
            string resultSavedDirectory2 = resultSavedDirectory + @"\" + "tree";
            DirectoryInfo di2 = new DirectoryInfo(resultSavedDirectory2);

            if (di2.Exists == false)
            {
                di2.Create();
            }
            string resultSavedDirectory3 = resultSavedDirectory + @"\" + "intermediate";
            DirectoryInfo di3 = new DirectoryInfo(resultSavedDirectory3);
            if (di3.Exists == false)
            {
                di3.Create();
            }


            if (shape == "_circle")
            {
                MakeCirclePlot();
            }
            else if (shape == "_rectangle")
            {
                MakeSquarePlot();
            }
            else
            {
                MakePolygonPlot();
            }
            RunBatchMakeDat();
        }

        /// cropping step, 배치파일 생성 및 실행하는 함수입니다.
        private void RunBatchMakeDat()
        {
            string one = "level1_cropped_";
            string resultSavedDirectory = this.resultSavedDirectory + shape;
            DirectoryInfo di = new DirectoryInfo(resultSavedDirectory);
            if (di.Exists == true)
            {
                {

                    //배치 파일 생성
                    string batFilePath = resultSavedDirectory + @"\intermediate\" + one + originLasName + ".bat";
                    if (!File.Exists(batFilePath))
                    {
                        using (FileStream fs = File.Create(batFilePath))
                        {
                            fs.Close();
                        }
                    }
                    //MessageBox.Show("batFileCreated");
                    using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
                    {
                        sw.WriteLine("@ECHO OFF");
                        sw.WriteLine("echo Buffer 표준지 분류 중...");
                        sw.WriteLine("pdal pipeline " + one + originLasName + ".json");
                        sw.WriteLine("echo>" + one + originLasName + "_B.json");
                        sw.WriteLine("pdal info " + one + originLasName + "_B.las > " + one + originLasName + "_B.json");
                    }
                    LogWrite(resultSavedDirectory + @"\intermediate\" + originLasName + "1.crop" + ".bat 파일 생성");
                }
                {
                    Process proc = null;
                    string _batDir = resultSavedDirectory + @"\intermediate\";
                    proc = new Process();
                    proc.StartInfo.WorkingDirectory = _batDir;
                    proc.StartInfo.FileName = one + originLasName + ".bat";
                    proc.StartInfo.CreateNoWindow = false;
                    proc.Start();
                    proc.WaitForExit();
                    proc.Close();

                    string strFile1 = resultSavedDirectory + @"\intermediate\" + one + originLasName + "_B.las";
                    FileInfo fileInfo1 = new FileInfo(strFile1);//파일 있는지 확인 있을때(true), 없으면(false)
                    if (fileInfo1.Exists)
                    {
                        try
                        {
                            string JsonText1 = System.IO.File.ReadAllText(resultSavedDirectory + @"\intermediate\" + one + originLasName + "_B.json");
                            JObject JsonData1 = JObject.Parse(JsonText1);
                            JToken minx = JsonData1["stats"]["bbox"]["native"]["bbox"]["minx"];
                            JToken maxx = JsonData1["stats"]["bbox"]["native"]["bbox"]["maxx"];
                            JToken miny = JsonData1["stats"]["bbox"]["native"]["bbox"]["miny"];
                            JToken maxy = JsonData1["stats"]["bbox"]["native"]["bbox"]["maxy"];
                            //str을 만들었으니 이제 그 데이터를 dat 파일에 넣는다.
                            File.WriteAllText(resultSavedDirectory + @"\intermediate\" + one + originLasName + "_B.dat", minx + " " + maxx + " " + miny + " " + maxy);
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.ToString());
                            LogWrite(resultSavedDirectory + @"\intermediate\" + one + originLasName + "_B.dat 파일을 생성 오류.");
                            //LogWrite(ex.ToString());
                            return;
                        }
                        LogWrite(resultSavedDirectory + @"\intermediate\" + one + originLasName + "_B.dat 파일을 생성했습니다.");
                    }
                }
            }
        }
        /// Cropping step, 원형으로 표준지를 자릅니다. 
        private void MakeCirclePlot()
        {
            string one = "level1_cropped_";
            string resultSavedDirectory = this.resultSavedDirectory + shape;
            double centerX = gui.centerX;
            double centerY = gui.centerY;
            double radius = gui.radius;
            double buffer = crop.buffer;
            {
                JObject Readers = new JObject(
                  new JProperty("type", "readers.las"),
                  new JProperty("filename", originLasDirectory + @"\" + originLasName + ".las")
              );
                JObject sonSpec = new JObject(
                   new JProperty("type", "filters.crop"),
                   new JProperty("point", "POINT(" + centerX + " " + centerY + ")"),
                   new JProperty("distance", (radius * buffer / 100))
               );
                JObject Writers = new JObject(
                   new JProperty("type", "writers.las"),
                   new JProperty("filename", one + originLasName + "_B.las")
               );
                File.WriteAllText(resultSavedDirectory + @"\intermediate\" + one + originLasName + ".json",
                    "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");
                LogWrite(resultSavedDirectory + @"\intermediate\" + one + originLasName + "_B.las 파일을 생성했습니다.");
            }
        }

        /// Cropping step, 정사각형으로 표준지를 자릅니다.
        private void MakeSquarePlot()
        {
            string one = "level1_cropped_";
            string resultSavedDirectory = this.resultSavedDirectory + shape;

            double xmin = gui.xMin;
            double ymin = gui.yMin;
            double xmax = gui.xMax;
            double ymax = gui.yMax;
            double buffer = crop.buffer;

            double width = Math.Abs(xmax - xmin);
            double height = Math.Abs(ymax - ymin);
            double buffer_width = width * buffer / 100;
            double buffer_height = height * buffer / 100;

            double dif_w = (buffer_width - width) / 2;
            double dif_h = (buffer_height - height) / 2;

            point new_LT;
            new_LT.x = xmin - dif_w;
            new_LT.y = ymax + dif_h;

            point new_RB;
            new_RB.x = xmax + dif_w;
            new_RB.y = ymin - dif_h;
            {
                JObject Readers = new JObject(
                  new JProperty("type", "readers.las"),
                  new JProperty("filename", originLasDirectory + @"\" + originLasName + ".las")
              );
                JObject sonSpec = new JObject(
                    new JProperty("type", "filters.crop"),
                   new JProperty("bounds", "([" + new_LT.x + "," + new_RB.x + "],[" + new_RB.y + "," + new_LT.y + "])")
                    );
                JObject Writers = new JObject(
                   new JProperty("type", "writers.las"),
                    new JProperty("filename", @".\" + one + originLasName + "_B.las")
               );
                File.WriteAllText(resultSavedDirectory + @"\intermediate\" + one + originLasName + ".json",
                     "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");

                LogWrite(resultSavedDirectory + @"\intermediate\" + one + originLasName + "_B.las 파일을 생성했습니다.");
            }
        }

        /// Cropping step, 사용자가 입력한 좌표를(다각형의 꼭짓점) 읽어온 후 표준지를 다각형으로 자릅니다.
        private void MakePolygonPlot()
        {
            double buffer = crop.buffer;
            string one = "level1_cropped_";
            string resultSavedDirectory = this.resultSavedDirectory + shape;


            double width = Math.Abs(bufferedPolycords.right.x - bufferedPolycords.left.x);
            double height = Math.Abs(bufferedPolycords.top.y - bufferedPolycords.bottom.y);
            double buffer_width = width * buffer / 100;
            double buffer_height = height * buffer / 100;

            double dif_w = (buffer_width - width) / 2;
            double dif_h = (buffer_height - height) / 2;

            point new_LT;
            new_LT.x = bufferedPolycords.left.x - dif_w;
            new_LT.y = bufferedPolycords.top.y + dif_h;

            point new_RB;
            new_RB.x = bufferedPolycords.right.x + dif_w;
            new_RB.y = bufferedPolycords.bottom.y - dif_h;
            {
                //default
                //버퍼 적용
                JObject Readers = new JObject(
                  new JProperty("type", "readers.las"),
                  new JProperty("filename", originLasDirectory + @"\" + originLasName + ".las")
              );
                JObject sonSpec = new JObject(
                    new JProperty("type", "filters.crop"),
                    new JProperty("bounds", "([" + new_LT.x + "," + new_RB.x + "],[" + new_RB.y + "," + new_LT.y + "])")
                    );
                JObject Writers = new JObject(
                   new JProperty("type", "writers.las"),
                   new JProperty("filename", @".\" + one + originLasName + "_B.las")
               );
                File.WriteAllText(resultSavedDirectory + @"\intermediate\" + one + originLasName + ".json",
                    "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");

                LogWrite(resultSavedDirectory + @"\intermediate\" + one + originLasName + "_B.las 파일을 생성했습니다.");
            }
        }
        //데이터 전처리 단계
        private void Subsampling()
        {
            string two = "level2_subsampled_";

            //MessageBox.Show("sun resultSD : " + resultSavedDirectory);
            {
                JObject Readers = new JObject(
                  new JProperty("type", "readers.las"),
                  new JProperty("filename", @".\" + "level1_cropped_" + originLasName + "_B.las")
              );
                JObject sonSpec = new JObject(
                   new JProperty("type", "filters.sample"),
                   new JProperty("cell", subsampling.cellSize)
               );
                JObject Writers = new JObject(
                   new JProperty("type", "writers.las"),
                   new JProperty("filename", @".\" + two + originLasName + ".las")
               );
                File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + two + originLasName + ".json", "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + two + originLasName + ".json 파일을 생성했습니다..");
            }
            /*
            if (radioButton2.Checked)
            {
                //voxeldown
                {
                    JObject Readers = new JObject(
                      new JProperty("type", "readers.las"),
                      new JProperty("filename", plot.LoadPath + shape + @"\intermediate\" + originLasName + "_B.las")
                  );
                    JObject sonSpec = new JObject(
                       new JProperty("type", "filters.voxeldownsize"),
                       new JProperty("cell", sub.cell),
                       new JProperty("mode", "center")
                   );
                    JObject Writers = new JObject(
                       new JProperty("type", "writers.las"),
                       new JProperty("filename", plot.LoadPath + shape + @"\intermediate\" + originLasName + "_Sub.las")
                   );
                    File.WriteAllText(plot.LoadPath + shape + @"\intermediate\" + originLasName + two + ".json", "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");
                    LogWrite(plot.LoadPath + shape + @"\intermediate\" + originLasName + two + ".json 파일을 생성했습니다.");
                }
            }*/
        }
        private void Outlier()
        {
            string three = "level3_outlierRemoved_";
            {
                JObject secondin = new JObject(
               new JProperty("type", "readers.las"),
               new JProperty("filename", @".\" + "level2_subsampled_" + originLasName + ".las")
             );
                JObject Outlier = new JObject(
                  new JProperty("type", "filters.outlier"),
                  new JProperty("method", "statistical"),
                   new JProperty("mean_k", outlier.mean_k),
                new JProperty("multiplier", outlier.Multiplier)
              );
                JObject secondout = new JObject(
                   new JProperty("type", "writers.las"),
                   new JProperty("filename", @".\" + "level3_outlierRemoved_" + originLasName + ".las")
               );
                File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + three + originLasName + ".json",
                    "[" + secondin.ToString() + ", " + Outlier.ToString() + ", " + secondout.ToString() + "]");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + three + originLasName + ".json 파일을 생성했습니다.");
            }
        }
        private void Normalization()
        {
            string four = "level4_nomarlized_";
            {
                JObject thirdin = new JObject(
              new JProperty("type", "readers.las"),
              new JProperty("filename", @".\" + "level3_outlierRemoved_" + originLasName + ".las")
                );
                JArray jarray = new JArray();
                jarray.Add("ReturnNumber = 1 WHERE ReturnNumber < 1");
                jarray.Add("NumberOfReturns = 1 WHERE NumberOfReturns < 1");
                JObject Assign = new JObject(
                  new JProperty("type", "filters.assign"),
                   new JProperty("value", jarray)
                   );
                JObject elm = new JObject(
                  new JProperty("type", "filters.elm")
              );
                JObject smrf = new JObject(
                   new JProperty("type", "filters.smrf"),
                   new JProperty("cell", groundseg.cellSize),
                   new JProperty("window", groundseg.windowSize),
                   new JProperty("slope", groundseg.slope),
                   new JProperty("threshold", groundseg.threshold),
                   new JProperty("scalar", groundseg.scalar)
               );
                JObject hagnn = new JObject(
                  new JProperty("type", "filters.hag_nn")
              );
                JObject ferry = new JObject(
                    new JProperty("type", "filters.ferry"),
                   new JProperty("dimensions", "HeightAboveGround=>Z")
              );
                JObject Writers = new JObject(
                   new JProperty("type", "writers.las"),
                   new JProperty("filename", @".\" + four + originLasName + ".las")
               );
                File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + four + originLasName + ".json", "[" + thirdin.ToString() + ", " + Assign.ToString() + ", " + elm.ToString() + ", " + smrf.ToString() + ", " + hagnn.ToString() + ", " + ferry.ToString() + ", " + Writers.ToString() + "]");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + four + originLasName + ".json 파일을 생성했습니다.");
            }
        }
        private void MakeJsonFile()
        {
            string fourone = "level4-1_LAStoPCDforBufferdPlot_";
            string fourtwo = "level4-2_croporiginPlot_";
            //LAStoPCD
            {
                JObject Readers = new JObject(
                  new JProperty("type", "readers.las"),
                new JProperty("filename", @".\" + "level4_nomarlized_" + originLasName + ".las")
               );
                JObject Writers = new JObject(
                   new JProperty("type", "writers.pcd"),
                   new JProperty("filename", @".\" + fourone + originLasName + ".pcd")
               );
                File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + fourone + originLasName + ".json", "[" + Readers.ToString() + ", " + Writers.ToString() + "]");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + fourone + originLasName + ".json 파일을 생성했습니다.");
            }
            //4.2.croporiginplot
            {
                double centerX = gui.centerX;
                double centerY = gui.centerY;

                if (shape == "_circle")
                {
                    double radius = gui.radius;
                    {
                        JObject Readers = new JObject(
                          new JProperty("type", "readers.las"),
                        new JProperty("filename", @".\" + "level4_nomarlized_" + originLasName + ".las")
                       );
                        JObject sonSpec = new JObject(
                           new JProperty("type", "filters.crop"),
                           new JProperty("point", "POINT(" + centerX + " " + centerY + ")"),
                        new JProperty("distance", radius)
                       );
                        JObject Writers = new JObject(
                           new JProperty("type", "writers.las"),
                           new JProperty("filename", @".\" + fourtwo + originLasName + "_O.las")
                       );
                        File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + fourtwo + originLasName + ".json", "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");
                        LogWrite(resultSavedDirectory + shape + @"\intermediate\" + fourtwo + originLasName + ".json 파일을 생성했습니다.");
                    }
                }
                //square
                else if (shape == "_rectangle")
                {
                    double xmin = gui.xMin;
                    double ymin = gui.yMin;
                    double xmax = gui.xMax;
                    double ymax = gui.yMax;
                    {
                        //default
                        JObject Readers = new JObject(
                          new JProperty("type", "readers.las"),
                          new JProperty("filename", @".\" + "level4_nomarlized_" + originLasName + ".las")
                        );
                        JObject sonSpec = new JObject(
                            new JProperty("type", "filters.crop"),
                            new JProperty("bounds", "([" + xmin + "," + xmax + "],[" + ymin + "," + ymax + "])")
                        );
                        JObject Writers = new JObject(
                           new JProperty("type", "writers.las"),
                           new JProperty("filename", @".\" + fourtwo + originLasName + "_O.las")
                        );
                        File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + fourtwo + originLasName + ".json", "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");
                        LogWrite(resultSavedDirectory + shape + @"\intermediate\" + fourtwo + originLasName + ".json 파일을 생성했습니다.");
                    }
                }
                //polygon
                else
                {
                    string points = gui.pointList;
                    {
                        JObject Readers = new JObject(
                          new JProperty("type", "readers.las"),
                          new JProperty("filename", @".\" + "level4_nomarlized_" + originLasName + ".las")
                        );
                        JObject sonSpec = new JObject(
                            new JProperty("type", "filters.crop"),
                            new JProperty("polygon", "POLYGON((" + points + "))")
                        );
                        JObject Writers = new JObject(
                           new JProperty("type", "writers.las"),
                           new JProperty("filename", @".\" + fourtwo + originLasName + "_O.las")
                        );
                        File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + fourtwo + originLasName + ".json", "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");
                        LogWrite(resultSavedDirectory + shape + @"\intermediate\" + fourtwo + originLasName + ".json 파일을 생성했습니다.");
                    }
                }
            }
            MakeJsonFileagain();
        }
        private void MakeJsonFileagain()
        {
            string five = "level5_trunkslice_";
            string six = "level6_crownslice_";
            //5.trunkslice
            {
                JObject Readers = new JObject(
                  new JProperty("type", "readers.pcd"),
                  new JProperty("filename", @".\" + "level4-1_LAStoPCDforBufferdPlot_" + originLasName + ".pcd")
              );
                JObject sonSpec = new JObject(
                   new JProperty("type", "filters.range"),
                   new JProperty("limits", @"Z[" + tSlice.minHeight + ":" + tSlice.maxHeight + "]")
               );
                JObject Writers = new JObject(
                   new JProperty("type", "writers.pcd"),
                   new JProperty("filename", @".\" + originLasName + "_TSlice.pcd")
               );
                File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + five + originLasName + ".json", "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + five + originLasName + ".json 파일을 생성했습니다.");
            }
            //6.crownslice
            {
                JObject Readers = new JObject(
                  new JProperty("type", "readers.pcd"),
                  new JProperty("filename", @".\" + "level4-1_LAStoPCDforBufferdPlot_" + originLasName + ".pcd")
              );
                JObject sonSpec = new JObject(
                   new JProperty("type", "filters.range"),
                   new JProperty("limits", @"Z[" + cSlice.minHeight + ":" + cSlice.maxHeight + "]")
               );
                JObject Writers = new JObject(
                   new JProperty("type", "writers.pcd"),
                   new JProperty("filename", @".\" + originLasName + "_CSlice.pcd")
               );
                File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + six + originLasName + ".json", "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + six + originLasName + ".json 파일을 생성했습니다.");
            }
        }
        private void ProcessBatch(string num)
        {
            //num(ex. "level1_~";
            Process proc = null;
            string _batDir = resultSavedDirectory + shape + @"\intermediate\";
            proc = new Process();
            proc.StartInfo.WorkingDirectory = _batDir;
            proc.StartInfo.FileName = num + originLasName + ".bat";
            proc.StartInfo.CreateNoWindow = false;
            proc.Start();
            proc.WaitForExit();
            proc.Close();
        }

        private void RunFileSecond()
        {
            string two = "level2_subsampled_";
            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + two + originLasName + ".bat";

                if (!File.Exists(batFilePath))
                {
                    using (FileStream fs = File.Create(batFilePath))
                    {
                        fs.Close();
                    }
                }
                using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
                {
                    sw.WriteLine("@ECHO OFF");
                    sw.WriteLine("echo 파일 크기 축소 중...");
                    sw.WriteLine("pdal pipeline " + two + originLasName + ".json");
                }
                ProcessBatch(two);
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + two + originLasName + ".bat 파일을 생성했습니다.");
            }
        }
        private void RunFileThird()
        {
            string three = "level3_outlierRemoved_";
            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + three + originLasName + ".bat";
                if (!File.Exists(batFilePath))
                {
                    using (FileStream fs = File.Create(batFilePath))
                    {
                        fs.Close();
                    }
                }
                using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
                {
                    sw.WriteLine("@ECHO OFF");
                    sw.WriteLine("echo 이상점 제거 중...");
                    sw.WriteLine("pdal pipeline " + three + originLasName + ".json");
                }
                ProcessBatch(three);
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + three + originLasName + ".bat 파일을 생성했습니다.");
            }
        }
        private void RunFileForth()
        {
            string four = "level4_nomarlized_";

            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + four + originLasName + ".bat";
                if (!File.Exists(batFilePath))
                {
                    using (FileStream fs = File.Create(batFilePath))
                    {
                        fs.Close();
                    }
                }
                using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))

                {
                    sw.WriteLine("@ECHO OFF");
                    sw.WriteLine("echo 지면 추출 및 평탄화 중...");
                    sw.WriteLine("pdal pipeline " + four + originLasName + ".json");
                }
                ProcessBatch(four);
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + four + originLasName + ".bat 파일을 생성했습니다.");
            }
        }
        private void RunFileForthOne()
        {
            string fourone = "level4-1_LAStoPCDforBufferdPlot_"; //lastopcd

            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + fourone + originLasName + ".bat";
                if (!File.Exists(batFilePath))
                {
                    using (FileStream fs = File.Create(batFilePath))
                    {
                        fs.Close();
                    }
                }
                using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
                {
                    sw.WriteLine("@ECHO OFF");
                    sw.WriteLine("echo las에서 pcd로 확장자 변환 중...");
                    sw.WriteLine("pdal pipeline " + fourone + originLasName + ".json");
                }
                ProcessBatch(fourone);
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + fourone + originLasName + ".bat 파일을 생성했습니다.");
            }
        }
        private void RunFileForthTwo()
        {
            string centerX = gui.centerX.ToString();
            string centerY = gui.centerY.ToString();

            string fourtwo = "level4-2_croporiginPlot_"; //crop 후 dat 파일 만들기
            {
                //crop origin plot
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + fourtwo + originLasName + ".bat";
                if (!File.Exists(batFilePath))
                {
                    using (FileStream fs = File.Create(batFilePath))
                    {
                        fs.Close();
                    }
                }
                using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
                {
                    sw.WriteLine("@ECHO OFF");
                    sw.WriteLine("echo 원본 표준지 분류 중...");
                    sw.WriteLine("pdal pipeline " + fourtwo + originLasName + ".json");
                    sw.WriteLine("echo>" + @".\" + fourtwo + originLasName + "_O.json");
                    sw.WriteLine("pdal info " + @".\" + fourtwo + originLasName + "_O.las > " + @".\" + fourtwo + originLasName + "_O.json");
                }
                ProcessBatch(fourtwo);
                //process batch file to make dat file
                string strFile1 = resultSavedDirectory + shape + @"\intermediate\" + fourtwo + originLasName + "_O.las";
                FileInfo fileInfo1 = new FileInfo(strFile1);//파일 있는지 확인 있을때(true), 없으면(false)
                if (fileInfo1.Exists)
                {
                    if (shape == "_polygon")
                    {
                        try
                        {
                            //MessageBox.Show(strFile1);
                            string JsonText1 = System.IO.File.ReadAllText(resultSavedDirectory + shape + @"\intermediate\" + fourtwo + originLasName + "_O.json");

                            JObject JsonData1 = JObject.Parse(JsonText1);

                            JArray coordinatesArray = (JArray)JsonData1["stats"]["bbox"]["native"]["boundary"]["coordinates"][0];

                            //double[] xCoordinates = new double[coordinatesArray.Count];
                            //double[] yCoordinates = new double[coordinatesArray.Count];

                            string points = "";
                            for (int i = 0; i < coordinatesArray.Count; i++)
                            {
                                JArray vertex = (JArray)coordinatesArray[i];
                                points += vertex[0] + " ";
                                points += vertex[1] + " ";
                                //xCoordinates[i] = (double)vertex[0];
                                //yCoordinates[i] = (double)vertex[1];
                            }



                            File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + fourtwo + originLasName + "_O.dat", points);

                        }
                        catch (Exception ex)
                        {
                            LogWrite(resultSavedDirectory + @"\intermediate\" + fourtwo + originLasName + "_O.dat 파일을 생성 오류.");
                            return;
                        }
                    }
                    else
                    {
                        try
                        {
                            //MessageBox.Show(strFile1);
                            string JsonText1 = System.IO.File.ReadAllText(resultSavedDirectory + shape + @"\intermediate\" + fourtwo + originLasName + "_O.json");

                            JObject JsonData1 = JObject.Parse(JsonText1);

                            JToken minx = JsonData1["stats"]["bbox"]["native"]["bbox"]["minx"];
                            JToken maxx = JsonData1["stats"]["bbox"]["native"]["bbox"]["maxx"];
                            JToken miny = JsonData1["stats"]["bbox"]["native"]["bbox"]["miny"];
                            JToken maxy = JsonData1["stats"]["bbox"]["native"]["bbox"]["maxy"];

                            //passvalue centerx, centery, radius를 추가하기
                            if (shape == "_circle")
                            {
                                double radius = gui.radius;

                                File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + fourtwo + originLasName + "_O.dat",
                                    minx + " " + maxx + " " + miny + " " + maxy + " " + centerX + " " + centerY + " " + radius);
                            }
                            else
                            {
                                File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + fourtwo + originLasName + "_O.dat", minx + " " + maxx + " " + miny + " " + maxy);
                            }
                        }
                        catch (Exception ex)
                        {
                            LogWrite(resultSavedDirectory + @"\intermediate\" + fourtwo + originLasName + "_O.dat 파일을 생성 오류.");
                            return;
                        }
                    }
                }
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + fourtwo + originLasName + "_O.dat 파일을 생성했습니다.");
            }

        }
        private void RunFileFifth()
        {
            string five = "level5_trunkslice_";
            //5단계
            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + five + originLasName + ".bat";
                if (!File.Exists(batFilePath))
                {
                    using (FileStream fs = File.Create(batFilePath))
                    {
                        fs.Close();
                    }
                }
                using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
                {
                    sw.WriteLine("@ECHO OFF");
                    sw.WriteLine("echo 수간 영역 잘라내는 중...");
                    sw.WriteLine("pdal pipeline " + five + originLasName + ".json");
                }
                ProcessBatch(five);
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + five + originLasName + ".bat 파일을 생성했습니다.");
            }
        }
        private void RunFileSixth()
        {
            string six = "level6_crownslice_";
            //6단계
            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + six + originLasName + ".bat";
                if (!File.Exists(batFilePath))
                {
                    using (FileStream fs = File.Create(batFilePath))
                    {
                        fs.Close();
                    }
                }
                using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
                {
                    sw.WriteLine("@ECHO OFF");
                    sw.WriteLine("echo 수관 영역 잘라내는 중...");
                    sw.WriteLine("pdal pipeline " + six + originLasName + ".json");
                }
                ProcessBatch(six);
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + six + originLasName + ".bat 파일을 생성했습니다.");
            }
        }
        private void RunFileSeventh()
        {
            string seven = "level7_segmentTrunk_";
            //7단계 treeseg_findstems 
            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + seven + originLasName + ".bat";
                if (!File.Exists(batFilePath))
                {
                    using (FileStream fs = File.Create(batFilePath))
                    {
                        fs.Close();
                    }
                }
                using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
                {
                    sw.WriteLine("@ECHO OFF");
                    sw.WriteLine("echo 수간 추출 및 하층식생 제거 중...");
                    //최종에서는 명령인수 삭제
                    sw.WriteLine("csp_segmentstem " + smoothness + " " + mindbh + " " + maxdbh + " level1_cropped_" + originLasName + "_B.dat "
                     + originLasName + "_Tslice.pcd");
                    sw.WriteLine();
                }
                ProcessBatch(seven);
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + originLasName + seven + ".bat 파일을 생성했습니다.");
            }
        }
        private void RunFileEighth()
        {
            string eight = "level8_segmentCrown_";
            //8단계 개별목 추출 
            {
                List<String> filenames_pcd = new List<String>();

                string stemlist = "";

                String FolderName = resultSavedDirectory + shape + @"\intermediate";
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(FolderName);
                foreach (System.IO.FileInfo File in di.GetFiles())
                {
                    if (File.Extension.ToLower().CompareTo(".pcd") == 0 && File.Name.Contains(originLasName) == true && File.Name.Contains("_TRUNK") == true)
                    {
                        String FileNameOnly = File.Name.Substring(0, File.Name.Length - 4);
                        String FullFileName = File.FullName;

                        stemlist += FullFileName + " ";
                    }
                }
                string batFilePath = resultSavedDirectory + shape + @"\\intermediate\" + eight + originLasName + ".bat";
                if (!File.Exists(batFilePath))
                {
                    using (FileStream fs = File.Create(batFilePath))
                    {
                        fs.Close();
                    }
                }
                using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
                {
                    string tree_name = "_TREE_";
                    string destination = @"..\tree";
                    sw.WriteLine("@ECHO OFF");
                    sw.WriteLine("echo 개별목 추출 중...");

                    sw.WriteLine("csp_segmentcrown " + stemlist + " " + originLasName + "_CSlice.pcd");
                    sw.WriteLine();
                    sw.WriteLine("set destination=\"{0}\"", destination);
                    sw.WriteLine();
                    sw.WriteLine("for /r %%i in (*{0}*.pcd) do (", tree_name);
                    sw.WriteLine("    move \"%%i\" \"%destination%\"");
                    sw.WriteLine(")");


                }
                ProcessBatch(eight);
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + eight + originLasName + ".bat 파일을 생성했습니다.");
            }
        }
        private void RunFileNinth()
        {
            string nine = "level9_measure_DBH_treeHeight_";
            //9단계 산림정보 속성 계산
            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + nine + originLasName + ".bat";
                if (!File.Exists(batFilePath))
                {
                    using (FileStream fs = File.Create(batFilePath))
                    {
                        fs.Close();
                    }
                }
                String FolderName = ".\\";
                String FolderName2 = "..\\tree";
                using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
                {
                    sw.WriteLine("@ECHO OFF");
                    sw.WriteLine("echo 산림 속성 정보 계산중...  ");
                    sw.WriteLine("measure " + FolderName + " " + FolderName2);
                }
                ProcessBatch(nine);
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + nine + originLasName + ".bat 파일을 생성했습니다.");
            }
        }

        private void RunFileTenth()
        {
            string ten = "level10_PCDtoLAS";
            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + ten + originLasName + ".bat";
                if (!File.Exists(batFilePath))
                {
                    using (FileStream fs = File.Create(batFilePath)) { }
                }
                String FolderName = ".\\";
                String FolderName2 = "..\\tree";
                using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
                {
                    sw.WriteLine("@ECHO OFF");
                    sw.WriteLine("echo pcd파일 las파일로 변환중");
                    sw.WriteLine("PCD2LAS " + FolderName);
                    sw.WriteLine("PCD2LAS " + FolderName2);

                    //pcd 지우는 코드
                    //sw.WriteLine("for /r \"..\\tree\" %%i in (*.pcd) do (");
                    //sw.WriteLine("    del \"%%i\"");
                    //sw.WriteLine(")");

                    //sw.WriteLine("for /r \".\" %%i in (*.pcd) do (");
                    //sw.WriteLine("    del \"%%i\"");
                    //sw.WriteLine(")");
                }

                ProcessBatch(ten);
                try
                {
                    ChangeLasName();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("10단계 오류 : " + ex.ToString());
                }

                MakeInfoFile();

                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + ten + originLasName + ".bat 파일을 생성했습니다.");
            }
        }

        private void ChangeLasName()
        {
            string destinationDirectory = resultSavedDirectory + shape + @"\intermediate";

            string[] lasFiles = {
                Path.Combine(destinationDirectory, (originLasName + "_TSlice.las")),
                Path.Combine(destinationDirectory, (originLasName + "_CSlice.las"))
            };

            // .las 파일들의 이름을 변경
            foreach (string lasFile in lasFiles)
            {
                // 파일의 확장자를 포함하지 않은 이름만 추출
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(lasFile);

                string newFileName;
                if (fileNameWithoutExtension.EndsWith("_TSlice"))
                {
                    newFileName = "level5_trunkslice_" + fileNameWithoutExtension + ".las";
                }
                else // fileNameWithoutExtension.EndsWith("_CSlice")
                {
                    newFileName = "level6_crownslice_" + fileNameWithoutExtension + ".las";
                }

                // 파일 이름 변경 및 이동
                string newFilePath = Path.Combine(destinationDirectory, newFileName);
                File.Move(lasFile, newFilePath);
            }
        }

        private void MakeInfoFile()
        {
            try
            {
                // 파일 경로와 이름 지정
                string filePath = resultSavedDirectory + shape + @"\tree\" + originLasName + shape + "_info.txt";

                // 파일이 존재하지 않으면 생성
                if (!File.Exists(filePath))
                {
                    using (File.Create(filePath)) { }
                }

                string info = "";
                if (shape == "_circle")
                {
                    string center = "중심 좌표(x,y) : " + gui.centerX + ", " + gui.centerY + "\n";
                    string radius = "반지름(m) : " + gui.radius;
                    info += center + radius;
                }
                else if (shape == "_rectangle")
                {
                    string LT_info = "좌상단 좌표(x,y) : " + gui.xMin + ", " + gui.yMax + "\n";
                    string RB_info = "우하단 좌표(x,y) : " + gui.xMax + ", " + gui.yMin;
                    info += LT_info + RB_info;
                }
                else
                {
                    string poly_info = "다각형 좌표(x1 y1, x2 y2, ...) : " + gui.pointList;
                    info += poly_info;
                }
                // StreamWriter를 사용하여 파일 생성 및 쓰기
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(info);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("10단계 오류 : " + ex.Message);
            }
        }
        private void LogWrite(string message)
        {

            string FilePath = resultSavedDirectory + shape + @"\Log\Log_" + DateTime.Today.ToString("yyyyMMdd") + ".log";
            string DirPath = resultSavedDirectory + shape + @"\Log"; string temp;
            DirectoryInfo di = new DirectoryInfo(DirPath);
            FileInfo fi = new FileInfo(FilePath);
            try
            {
                if (!di.Exists)
                    Directory.CreateDirectory(DirPath);
                if (!fi.Exists)
                {
                    using (StreamWriter sw = new StreamWriter(FilePath))
                    {
                        temp = string.Format("[{0}] {1}", DateTime.Now, message);
                        sw.WriteLine(temp); sw.Close();
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(FilePath))
                    {
                        temp = string.Format("[{0}] {1}", DateTime.Now, message);
                        sw.WriteLine(temp);
                        sw.Close();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void preProAndExcuteStep()
        {
            progress = 0;
            MakeResultDirectory_PLOT();

            string resultP = resultSavedDirectory + shape;
            //FileInfo fileInfo1 = new FileInfo("dummy");
            //try
            //{
            string strFile1 = resultP + @"\intermediate\" + "level1_cropped_" + originLasName + "_B.las";
            FileInfo fileInfo1 = new FileInfo(strFile1);//파일 있는지 확인 있을때(true), 없으면(false)
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show("1단계 산출물 에러");
            //}

            if (fileInfo1.Exists)
            {
                //전처리
                Subsampling();
                Outlier();
                Normalization();
                MakeJsonFile();
                progress++;
                //=====

                RunFileSecond();
                progress++;

                RunFileThird();
                progress++;

                RunFileForth();
                RunFileForthOne();
                RunFileForthTwo();
                progress++;

                RunFileFifth();
                progress++;

                RunFileSixth();
                progress++;

                RunFileSeventh();//trunk
                progress++;
                if (CatchError(resultP + @"\intermediate\", 7)) return;

                RunFileEighth();//tree
                progress++;
                if (CatchError(resultP + @"\tree\", 8)) return;

                RunFileNinth();//measure
                progress++;
                if (CatchError(resultP + @"\tree\", 9)) return;

                RunFileTenth();
                progress++;
            }
            else
            {
                MessageBox.Show("1단계 산출물 에러");
            }
        }
        bool CatchError(string path, int level)
        {
            bool isError = true;
            string find;
            if (level == 7)
            {
                find = "TRUNK";
            }
            else if (level == 8)
            {
                find = "TREE";
            }
            else
            {
                find = "DBH";
            }
            try
            {
                // 디렉토리에서 파일 이름들을 가져옵니다.
                string[] fileNames = Directory.GetFiles(path);
                foreach (var file in fileNames)
                {
                    if (file.Contains(find))
                    {
                        isError = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogWrite(level + "단계 파일을 가져오는 중에 오류가 발생했습니다 : " + ex.Message);
            }

            return isError;
        }

        private void tbPlotData_TextChanged(object sender, EventArgs e)
        {
            gui.loadPath = tbPlotData.Text;

            originLasPath = tbPlotData.Text;
            originLasName = Path.GetFileNameWithoutExtension(originLasPath);
            originLasDirectory = Path.GetDirectoryName(originLasPath);
        }


        public void FindExtremeCoordinates(point[] points)
        {
            // 초기화를 위해 첫 번째 점을 기준으로 설정
            bufferedPolycords.top = bufferedPolycords.bottom = bufferedPolycords.right = bufferedPolycords.left = points[0];

            foreach (point temppoint in points)
            {
                // 최상단 좌표 찾기
                if (temppoint.y > bufferedPolycords.top.y)
                    bufferedPolycords.top = temppoint;

                // 최하단 좌표 찾기
                if (temppoint.y < bufferedPolycords.bottom.y)
                    bufferedPolycords.bottom = temppoint;

                // 최우단 좌표 찾기
                if (temppoint.x > bufferedPolycords.right.x)
                    bufferedPolycords.right = temppoint;

                // 최좌단 좌표 찾기
                if (temppoint.x < bufferedPolycords.left.x)
                    bufferedPolycords.left = temppoint;
            }
        }

        public void ReadConfig()
        {
            // 파일에서 JSON 데이터 읽기
            string filePath = "..\\bin\\config.json";
            string json = File.ReadAllText(filePath);
            // JSON 데이터를 객체로 변환
            var jsonArray = JsonConvert.DeserializeObject<dynamic>(json);

            dynamic Jobj = jsonArray[0].GUI;

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
            smoothness = Jobj.smoothness;
            mindbh = Jobj.mindbh;
            maxdbh = Jobj.maxdbh;

        }

        private void PlotForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                relativePformPos = e.Location;
            }
        }

        private void PlotForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + (e.X - relativePformPos.X),
                    this.Location.Y + (e.Y - relativePformPos.Y));
            }
        }

        private void cbPlotShape_SelectedIndexChanged(object sender, EventArgs e)
        {
            tcPlot.SelectedIndex = cbPlotShape.SelectedIndex;
        }
    }
}
