using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsAppTest
{
    public partial class PlotForm
    {
        /// 원본 las파일의 경로(path)
        string originLasPath = "";
        /// 결과 파일들을 저장하는 폴더 디렉토리
        string resultPath = "";
        /// 결과를 저장하는 폴더 디렉토리(intermediate,stem,tree등등이 저장되는 폴더)
        string resultSavedDirectory = "";
        string originLasName = "";
        string originLasDirectory = "";
        string internalLasName = "";
        //자주쓰는 변수 왜 있는지 모르겠음
        string shape;

        //progess표시 변수
        int progress = 0;

        //csv 작성용 Original data poiygon_points
        string poly_points="";

        //버퍼 계산 시 사용되는 좌표 구조체
        Coords bufferedPolycords = new Coords();

        //las파일 크기 저장 구조체
        struct LasSize
        {
            public double maxx;
            public double maxy;
            public double minx;
            public double miny;
            public double minz;
            public double maxz;
        }
        LasSize lasSize = new LasSize();

        public Form progressDialog;
        public TextBox progressTextBox;
        public ProgressBar pbLoadingBar;


        //db 파일 명
        string bin_folder="";
        string databaseFileName="";
        //테이블 이름 목록(현재 안쓰였음 - 삭제 보류 중)
        string[] tablename = { "gui", "filters_crop", "filters_outlier", "filters_smrf", "filters_range_trunk", "filters_range_crown", "csp_segmentstem", "csp_segmentcrown", "measure" };
        string inter;


        //PLOT
        private void MakeResultDirectory_PLOT()
        {
            //result 폴더 생성
            DirectoryInfo result_pth = new DirectoryInfo(resultPath);
            if (result_pth.Exists == false)
            {
                result_pth.Create();
            }

            //MessageBox.Show(this.resultSavedDirectory);
            //이 아래로 각 las파일 결과 폴더 생성(intermediate, Log, tree)
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


            string tree_dir = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(resultSavedDirectory + "\\tree"));
            string inter_dir= Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(resultSavedDirectory + "\\intermediate"));


            paramForm.UpdateDataInTable("gui", "tree_dir", tree_dir);
            paramForm.UpdateDataInTable("gui", "intermediate_dir", inter_dir);

            RunFileZero(originLasPath);

            internalLasName = paramForm.SelectDataFromTable(databaseFileName, "gui", "internal_las_file");

            inter = Path.GetFileNameWithoutExtension(internalLasName);

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
            string one = "lv1_cropped_";
            string resultSavedDirectory = this.resultSavedDirectory + shape;
            DirectoryInfo di = new DirectoryInfo(resultSavedDirectory);
            if (di.Exists == true)
            {
                {
                    //배치 파일 생성
                    string batFilePath = resultSavedDirectory + @"\intermediate\" + one + inter + ".bat";
                    if (!File.Exists(batFilePath))
                    {
                        using FileStream fs = File.Create(batFilePath);
                    }
                    //MessageBox.Show("batFileCreated");
                    using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
                    {
                        sw.WriteLine("@ECHO OFF");
                        sw.WriteLine("chcp 65001 > nul");
                        sw.WriteLine("cls");
                        sw.WriteLine("echo Buffer 표준지 분류 중...");
                        sw.WriteLine($"cd {this.resultSavedDirectory + shape + @"\intermediate"}");
                        sw.WriteLine("pdal pipeline \"" + one + inter + ".json\"");
                        sw.WriteLine("echo>" + "\"" + one + inter + "_B.json\"");
                        sw.WriteLine("pdal info \"" + one + inter + "_B.las\" > \"" + one + inter + "_B.json\"");
                    }

                    ProcessBatch(one + inter + ".bat");
                    LogWrite(resultSavedDirectory + @"\intermediate\" + inter + "1.crop" + ".bat 파일 생성");
                }

                {
                    string strFile1 = resultSavedDirectory + @"\intermediate\" + one + inter + "_B.las";
                    FileInfo fileInfo1 = new FileInfo(strFile1);//파일 있는지 확인 있을때(true), 없으면(false)
                    if (fileInfo1.Exists)
                    {
                        string dat_str = "";
                        try
                        {
                            string JsonText1 = System.IO.File.ReadAllText(resultSavedDirectory + @"\intermediate\" + one + inter + "_B.json");
                            JObject JsonData1 = JObject.Parse(JsonText1);
                            JToken minx = JsonData1["stats"]["bbox"]["native"]["bbox"]["minx"];
                            JToken maxx = JsonData1["stats"]["bbox"]["native"]["bbox"]["maxx"];
                            JToken miny = JsonData1["stats"]["bbox"]["native"]["bbox"]["miny"];
                            JToken maxy = JsonData1["stats"]["bbox"]["native"]["bbox"]["maxy"];
                            //str을 만들었으니 이제 그 데이터를 dat 파일에 넣는다.

                            dat_str = $"xmin={minx} xmax={maxx} ymin={miny} ymax={maxy}";
                            
                            //MessageBox.Show(dat_str);

                            paramForm.UpdateDataInTable("filters_crop", "bufferd_dat", dat_str);

                            //paramForm.setParam(paramForm.csv_data, "filters.crop", "bufferd_dat", dat_str);
                            //paramForm.write_csv(configpath);
                        }
                        catch
                        {
                            LogWrite(resultSavedDirectory + @"\intermediate\" + one + inter + "_B.dat 파일 생성 오류.");
                            return;
                        }
                        LogWrite(resultSavedDirectory + @"\intermediate\" + one + inter + "_B.dat 파일을 생성했습니다.");
                    }
                }
            }
        }
        /// Cropping step, 원형으로 표준지를 자릅니다. 
        private void MakeCirclePlot()
        {
            string one = "lv1_cropped_";
            string resultSavedDirectory = this.resultSavedDirectory + shape;

            string org_xmin = paramForm.SelectDataFromTable(databaseFileName, "gui", "org_xmin");
            string org_ymin = paramForm.SelectDataFromTable(databaseFileName, "gui", "org_ymin");

            double centerX = double.Parse(tbPlotCircleX.Text) - double.Parse(org_xmin);
            double centerY = double.Parse(tbPlotCircleY.Text) - double.Parse(org_ymin);

            double radius = double.Parse(tbPlotCircleR.Text);
            double buffer = double.Parse(paramForm.SelectDataFromTable(databaseFileName, "filters_crop", "buffer"));
            {
                JObject Readers = new JObject(
                  new JProperty("type", "readers.las"),
                  new JProperty("filename", internalLasName)
              );
                JObject sonSpec = new JObject(
                   new JProperty("type", "filters.crop"),
                   new JProperty("point", "POINT(" + centerX + " " + centerY + ")"),
                   new JProperty("distance", (radius * buffer / 100))
               );
                JObject Writers = new JObject(
                   new JProperty("type", "writers.las"),
                   new JProperty ("compression","laszip"),
                   new JProperty("filename",  one + inter + "_B.las")
               );
                File.WriteAllText(resultSavedDirectory + @"\intermediate\" + one + inter + ".json",
                    "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");
                LogWrite(resultSavedDirectory + @"\intermediate\" + one + inter + "_B.las 파일을 생성했습니다.");
            }
        }

        /// Cropping step, 정사각형으로 표준지를 자릅니다.
        private void MakeSquarePlot()
        {
            string one = "lv1_cropped_";
            string resultSavedDirectory = this.resultSavedDirectory + shape;

            double xmin = double.Parse(tbPlotRecXmin.Text) - double.Parse(paramForm.SelectDataFromTable(databaseFileName, "gui", "org_xmin")); 
            double ymin = double.Parse(tbPlotRecYmin.Text) - double.Parse(paramForm.SelectDataFromTable(databaseFileName, "gui", "org_ymin"));
            double xmax = double.Parse(tbPlotRecXmax.Text) - double.Parse(paramForm.SelectDataFromTable(databaseFileName, "gui", "org_xmin")); 
            double ymax = double.Parse(tbPlotRecYmax.Text) - double.Parse(paramForm.SelectDataFromTable(databaseFileName, "gui", "org_ymin")); 
            double buffer = double.Parse(paramForm.SelectDataFromTable(databaseFileName, "filters_crop", "buffer"));

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
                  new JProperty("filename", internalLasName)
              );
                JObject sonSpec = new JObject(
                    new JProperty("type", "filters.crop"),
                   new JProperty("bounds", "([" + new_LT.x + "," + new_RB.x + "],[" + new_RB.y + "," + new_LT.y + "])")
                    );
                JObject Writers = new JObject(
                   new JProperty("type", "writers.las"),
                   new JProperty("compression", "laszip"),
                    new JProperty("filename", one + inter + "_B.las")
               );
                File.WriteAllText(resultSavedDirectory + @"\intermediate\" + one + inter + ".json",
                     "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");

                LogWrite(resultSavedDirectory + @"\intermediate\" + one + inter + "_B.las 파일을 생성했습니다.");
            }
        }

        /// Cropping step, 사용자가 입력한 좌표를(다각형의 꼭짓점) 읽어온 후 표준지를 다각형으로 자릅니다.
        private void MakePolygonPlot()
        {
            double buffer = double.Parse(paramForm.SelectDataFromTable(databaseFileName, "filters_crop", "buffer"));
            string one = "lv1_cropped_";
            string resultSavedDirectory = this.resultSavedDirectory + shape;


            double width = Math.Abs(bufferedPolycords.right.x - bufferedPolycords.left.x);
            double height = Math.Abs(bufferedPolycords.top.y - bufferedPolycords.bottom.y);
            double buffer_width = width * buffer / 100;
            double buffer_height = height * buffer / 100;

            double dif_w = (buffer_width - width) / 2;
            double dif_h = (buffer_height - height) / 2;

            point new_LT;
            new_LT.x = bufferedPolycords.left.x - double.Parse(paramForm.SelectDataFromTable(databaseFileName, "gui", "org_xmin")) - dif_w;
            new_LT.y = bufferedPolycords.top.y - double.Parse(paramForm.SelectDataFromTable(databaseFileName, "gui", "org_ymin")) + dif_h;

            point new_RB;
            new_RB.x = bufferedPolycords.right.x - double.Parse(paramForm.SelectDataFromTable(databaseFileName, "gui", "org_xmin")) + dif_w;
            new_RB.y = bufferedPolycords.bottom.y - double.Parse(paramForm.SelectDataFromTable(databaseFileName, "gui", "org_ymin")) - dif_h;
            {
                //default
                //버퍼 적용
                JObject Readers = new JObject(
                  new JProperty("type", "readers.las"),
                  new JProperty("filename", internalLasName)
              );
                JObject sonSpec = new JObject(
                    new JProperty("type", "filters.crop"),
                    new JProperty("bounds", "([" + new_LT.x + "," + new_RB.x + "],[" + new_RB.y + "," + new_LT.y + "])")
                    );
                JObject Writers = new JObject(
                   new JProperty("type", "writers.las"),
                   new JProperty("compression", "laszip"),
                   new JProperty("filename", one + inter + "_B.las")
               );
                File.WriteAllText(resultSavedDirectory + @"\intermediate\" + one + inter + ".json",
                    "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");

                LogWrite(resultSavedDirectory + @"\intermediate\" + one + inter + "_B.las 파일을 생성했습니다.");
            }
        }


        //데이터 전처리 단계, 1~6단계 JSON 생성, 7~8단계 csv append line
        private void Outlier()
        {
            string second = "lv2_outlierRemoved_";
            {
                JObject secondin = new JObject(
               new JProperty("type", "readers.las"),
               new JProperty("filename", "lv1_cropped_" + inter + "_B.las")
             );
                JObject Outlier = new JObject(
                  new JProperty("type", "filters.outlier"),
                  new JProperty("method", "statistical"),
                  new JProperty("mean_k", paramForm.SelectDataFromTable(databaseFileName, "filters_outlier", "mean_k")),
                  new JProperty("multiplier", paramForm.SelectDataFromTable(databaseFileName, "filters_outlier", "multiplier"))
              );
                JObject secondout = new JObject(
                   new JProperty("type", "writers.las"),
                   new JProperty("compression", "laszip"),
                   new JProperty("filename", second + inter + ".las")
               );
                File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + second + inter + ".json",
                    "[" + secondin.ToString() + ", " + Outlier.ToString() + ", " + secondout.ToString() + "]");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + second + inter + ".json 파일을 생성했습니다.");
            }
        }
        private void Normalization()
        {
            string three = "lv3_nomarlized_";
            {
                JObject thirdin = new JObject(
              new JProperty("type", "readers.las"),
              new JProperty("filename", "lv2_outlierRemoved_" + inter + ".las")
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
                   new JProperty("cell", paramForm.SelectDataFromTable(databaseFileName, "filters_smrf", "cell")),
                   new JProperty("window", paramForm.SelectDataFromTable(databaseFileName, "filters_smrf", "window")),
                   new JProperty("slope", paramForm.SelectDataFromTable(databaseFileName, "filters_smrf", "slope")),
                   new JProperty("threshold", paramForm.SelectDataFromTable(databaseFileName, "filters_smrf", "threshold")),
                   new JProperty("scalar", paramForm.SelectDataFromTable(databaseFileName, "filters_smrf", "scalar"))
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
                   new JProperty("compression", "laszip"),
                   new JProperty("filename", three + inter + ".las")
               );
                File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + three + inter + ".json", "[" + thirdin.ToString() + ", " + Assign.ToString() + ", " + elm.ToString() + ", " + smrf.ToString() + ", " + hagnn.ToString() + ", " + ferry.ToString() + ", " + Writers.ToString() + "]");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + three + inter + ".json 파일을 생성했습니다.");
            }
        }

        private void ExtractOriginDat()
        {
            string six = "extractOriginDat_"; //crop 후 dat 파일 만들기
            string dat_str = "";
            {
                if (shape == "_polygon")
                {             
                    try
                    {
                        paramForm.UpdateDataInTable("filters_crop", "origin_dat", poly_points);

                    }
                    catch (Exception ex)
                    {
                        LogWrite(resultSavedDirectory + @"\intermediate\" + six + inter + "O.dat DB 기입 시 오류 발생.");
                        return;
                    }
                }
                else
                {                   
                    try
                    {
                        double minx = 0, maxx = 0, miny = 0, maxy = 0;

                        //passvalue centerx, centery, radius를 추가하기
                        if (shape == "_circle")
                        {
                            double centerX = paramForm.gui.centerX;
                            double centerY = paramForm.gui.centerY;
                            double radius = paramForm.gui.radius;

                            minx = lasSize.minx > (paramForm.gui.centerX - radius) ? lasSize.minx : (paramForm.gui.centerX - radius);
                            maxx = lasSize.maxx < (paramForm.gui.centerX + radius) ? lasSize.maxx : (paramForm.gui.centerX + radius);
                            miny = lasSize.miny > (paramForm.gui.centerX - radius) ? lasSize.miny : (paramForm.gui.centerX - radius);
                            maxy = lasSize.maxy < (paramForm.gui.centerX + radius) ? lasSize.maxy : (paramForm.gui.centerX + radius);

                            //상대좌표
                            //minx-= double.Parse(paramForm.SelectDataFromTable(databaseFileName, "gui", "org_xmin"));
                            //maxx-= double.Parse(paramForm.SelectDataFromTable(databaseFileName, "gui", "org_xmin"));
                            //miny-= double.Parse(paramForm.SelectDataFromTable(databaseFileName, "gui", "org_ymin"));
                            //maxy-= double.Parse(paramForm.SelectDataFromTable(databaseFileName, "gui", "org_ymin"));

                            dat_str = $"xmin={minx} xmax={maxx} ymin={miny} ymax={maxy} cx={centerX} cy={centerY} radius={radius}";
                            paramForm.UpdateDataInTable("filters_crop", "origin_dat", dat_str);
                        }
                        else
                        {
                            minx = lasSize.minx > (paramForm.gui.xMin) ? lasSize.minx : (paramForm.gui.xMin);
                            maxx = lasSize.maxx < (paramForm.gui.xMax) ? lasSize.maxx : (paramForm.gui.xMax);
                            miny = lasSize.miny > (paramForm.gui.yMin) ? lasSize.miny : (paramForm.gui.yMin);
                            maxy = lasSize.maxy < (paramForm.gui.yMax) ? lasSize.maxy : (paramForm.gui.yMax);


                            //상대좌표 
                            //minx-= double.Parse(paramForm.SelectDataFromTable(databaseFileName, "gui", "org_xmin"));
                            //maxx-= double.Parse(paramForm.SelectDataFromTable(databaseFileName, "gui", "org_xmin"));
                            //miny-= double.Parse(paramForm.SelectDataFromTable(databaseFileName, "gui", "org_ymin"));
                            //maxy-= double.Parse(paramForm.SelectDataFromTable(databaseFileName, "gui", "org_ymin"));

                            dat_str = $"xmin={minx} xmax={maxx} ymin={miny} ymax={maxy}";
                            paramForm.UpdateDataInTable("filters_crop", "origin_dat", dat_str);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogWrite(resultSavedDirectory + @"\intermediate\" + six + inter + "O.dat DB 기입 시 오류 발생.");
                        return;
                    }
                }
            }
            LogWrite(resultSavedDirectory + @"\intermediate\" + six + inter + "O.dat DB 기입 완료.");
        }

        private void Turn_Las_into_PCD()
        {
            string four = "lv4_LAStoPCD_";
            //LAStoPCD
            {
                JObject Readers = new JObject(
                  new JProperty("type", "readers.las"),
                new JProperty("filename", "lv3_nomarlized_" + inter + ".las")
               );
                JObject Writers = new JObject(
                   new JProperty("type", "writers.pcd"),
                   new JProperty("filename", four + inter + ".pcd"),
                   new JProperty("keep_unspecified", false),
                   new JProperty("order", "X=Float:8, Y=Float:8, Z=Float:8"),
                   new JProperty("compression", "ascii") // 바이너리 PCD 출력 설정
               );
                File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + four + inter + ".json", "[" + Readers.ToString() + ", " + Writers.ToString() + "]");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + four + inter + ".json 파일을 생성했습니다.");
            }
            
        }

        private void MakeSliceFile()
        {
            string sevenone = "lv5-1_trunkslice_";
            string seventwo = "lv5-2_crownslice_";
            //trunkslice
            {
                JObject Readers = new JObject(
                  new JProperty("type", "readers.pcd"),
                  new JProperty("filename", "lv4_LAStoPCD_" + inter + "_dup.pcd")
              );
                JObject sonSpec = new JObject(
                   new JProperty("type", "filters.range"),
                   new JProperty("limits", @"Z[" + paramForm.SelectDataFromTable(databaseFileName, "filters_range_trunk", "minheight") + ":"
                   + paramForm.SelectDataFromTable(databaseFileName, "filters_range_trunk", "maxheight") + "]")
               );
                JObject Writers = new JObject(
                   new JProperty("type", "writers.pcd"),
                   new JProperty("filename", sevenone + inter + ".pcd")
               );
                File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + sevenone + inter + ".json", "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + sevenone + inter + ".json 파일을 생성했습니다.");
            }
            //crownslice
            {
                JObject Readers = new JObject(
                  new JProperty("type", "readers.pcd"),
                  new JProperty("filename", "lv4_LAStoPCD_" + inter + "_dup.pcd")
              );
                JObject sonSpec = new JObject(
                   new JProperty("type", "filters.range"),
                   new JProperty("limits", @"Z[" + paramForm.SelectDataFromTable(databaseFileName, "filters_range_crown", "minheight") + ":"
                   + paramForm.SelectDataFromTable(databaseFileName, "filters_range_crown", "maxheight") + "]")
               );
                JObject Writers = new JObject(
                   new JProperty("type", "writers.pcd"),
                   new JProperty("filename", seventwo + inter + ".pcd")
               );
                File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + seventwo + inter + ".json", "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + seventwo + inter + ".json 파일을 생성했습니다.");
            }
        }

        private void AppendSeventhCSVFile()
        {
            string trunkslicefile = "";
            string crownslicefile = "";

            String FolderName = resultSavedDirectory + shape + @"\intermediate";
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(FolderName);
            foreach (System.IO.FileInfo fi in di.GetFiles())
            {
                if (fi.Extension.ToLower().CompareTo(".pcd") == 0 && fi.Name.Contains(inter) == true && fi.Name.ToLower().Contains("trunkslice") == true)
                {
                    trunkslicefile = fi.FullName;
                }
            }
            foreach (System.IO.FileInfo fi in di.GetFiles())
            {
                if (fi.Extension.ToLower().CompareTo(".pcd") == 0 && fi.Name.ToLower().Contains(inter.ToLower()) == true && fi.Name.ToLower().Contains("crownslice") == true)
                {
                    crownslicefile = fi.FullName;
                }
            }
            paramForm.UpdateDataInTable("csp_segmentstem", "trunk_slice_file", trunkslicefile);
            paramForm.UpdateDataInTable("csp_segmentcrown", "crown_slice_file", crownslicefile);
        }

        private void AppendEighthCSVFile()
        {
            List<String> filenames_pcd = new List<String>();

            string FolderName = resultSavedDirectory + shape + @"\intermediate";
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(FolderName);
            foreach (System.IO.FileInfo fi in di.GetFiles())
            {
                if (fi.Extension.ToLower().CompareTo(".pcd") == 0 && fi.Name.ToLower().Contains(inter.ToLower()) == true && fi.Name.ToLower().Contains("_trunk_") == true)
                {
                    filenames_pcd.Add(fi.FullName);
                }
            }

            paramForm.UpdateDataInTable("csp_segmentstem", "trunk_files", string.Join(" ", filenames_pcd));
        }

        //배치파일 실행 코드
        private void ProcessBatch(string batFile)
        {
            using (Process proc = new Process())
            {
                //proc.StartInfo.WorkingDirectory = resultSavedDirectory + shape + @"\intermediate\";
                proc.StartInfo.FileName = resultSavedDirectory + shape + @"\intermediate\" + batFile;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.OutputDataReceived += new DataReceivedEventHandler(OutputDataReceived);

                if (Application.OpenForms["progressDialog"] == null)
                {
                    MessageBox.Show("기능 실행이 정상적으로 수행되지 않았습니다.");
                    Process[] allProc = Process.GetProcesses();

                    foreach (Process procs in allProc)
                    {
                        try
                        {
                            if (procs.ProcessName == "ForestLi" || procs.ProcessName == "PlotForm")
                                procs.Kill();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    return;
                }

                progressTextBox.Invoke(new Action(() =>
                {
                    progressTextBox.AppendText("=================================" + Environment.NewLine);
                }));
                proc.Start();
                proc.BeginOutputReadLine();
                proc.WaitForExit();
            }
        }

        private void OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null && progressDialog != null)
            {
                progressTextBox.Invoke(new Action(() =>
                {
                    progressTextBox.AppendText(e.Data.Trim() + Environment.NewLine);
                    progressTextBox.Select(progressTextBox.Text.Length, 0);
                    progressTextBox.ScrollToCaret();
                }));
            }
        }

        //배치파일 작성 코드
        //배치파일 작성 부분 메서드로 빼서 정리 가능해보임

        //Dup_remove
        private void RunFileZero(string target_path)
        {
            string zero = "lv0_DuplicateRemove_";
            //7단계 treeseg_findstems 
            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + zero + inter + ".bat";
                if (!File.Exists(batFilePath))
                {
                    using FileStream fs = File.Create(batFilePath);
                }
                
                using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
                {
                    sw.WriteLine("@ECHO OFF");
                    sw.WriteLine("chcp 65001 > nul");
                    sw.WriteLine("cls");
                    sw.WriteLine("echo 원본 LAS 중복 점 제거중...");
                    sw.WriteLine($"cd {this.resultSavedDirectory + shape + @"\intermediate"}");
                    sw.WriteLine("laszip \"" + databaseFileName + "\"");
                    sw.WriteLine();
                }
                
                ProcessBatch(zero + inter + ".bat");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + inter + zero + ".bat 파일을 생성했습니다.");
            }
        }

        //OutlierRemove
        private void RunFileSecond()
        {
            string two = "lv2_outlierRemoved_";
            //2단계
            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + two + inter + ".bat";
                if (!File.Exists(batFilePath))
                {
                    using FileStream fs = File.Create(batFilePath);
                }
                
                using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
                {
                    sw.WriteLine("@ECHO OFF");
                    sw.WriteLine("chcp 65001 > nul");
                    sw.WriteLine("cls");
                    sw.WriteLine("echo 이상점 제거 중...");
                    sw.WriteLine($"cd {this.resultSavedDirectory + shape + @"\intermediate"}");
                    sw.WriteLine("pdal pipeline \"" + two + inter + ".json\"");
                }
                
                ProcessBatch(two + inter + ".bat");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + two + inter + ".bat 파일을 생성했습니다.");
            }
        }
        //Normalize
        private void RunFileThird()
        {
            string three = "lv3_nomarlized_";
            //3단계
            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + three + inter + ".bat";
                if (!File.Exists(batFilePath))
                {
                    using FileStream fs = File.Create(batFilePath);
                }
                
                using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
                {
                    sw.WriteLine("@ECHO OFF");
                    sw.WriteLine("chcp 65001 > nul");
                    sw.WriteLine("cls");
                    sw.WriteLine("echo 지면 추출 및 평탄화 중...");
                    sw.WriteLine($"cd {this.resultSavedDirectory + shape + @"\intermediate"}");
                    sw.WriteLine("pdal pipeline \"" + three + inter + ".json\"");
                }
                
                ProcessBatch(three + inter + ".bat");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + three + inter + ".bat 파일을 생성했습니다.");
            }
        }
       
        //LAS2PCD
        private void RunFileFourth()
        {
            string four = "lv4_LAStoPCD_";

            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + four + inter + ".bat";

                if (!File.Exists(batFilePath))
                {
                    using FileStream fs = File.Create(batFilePath);
                }
                using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
                {
                    sw.WriteLine("@ECHO OFF");
                    sw.WriteLine("chcp 65001 > nul");
                    sw.WriteLine("cls");
                    sw.WriteLine("echo LAS to PCD 변환 중...");
                    sw.WriteLine($"cd {this.resultSavedDirectory + shape + @"\intermediate"}");
                    sw.WriteLine("pdal pipeline \"" + four + inter + ".json\"");
                    sw.WriteLine("Delete_duplication \"" + four + inter + ".pcd\"");
                }
                ProcessBatch(four + inter + ".bat");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + four + inter + ".bat 파일을 생성했습니다.");
            }
        }
        //Slicing
        private void RunFileSeventh()
        {
            string seven = "lv5_Slicing_";
            string sevenone = "lv5-1_trunkslice_";
            string seventwo = "lv5-2_crownslice_";

            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + seven + inter + ".bat";
                if (!File.Exists(batFilePath))
                {
                    using FileStream fs = File.Create(batFilePath);
                }
                using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
                {
                    sw.WriteLine("@ECHO OFF");
                    sw.WriteLine("chcp 65001 > nul");
                    sw.WriteLine("cls");
                    sw.WriteLine("echo 수관 부분 수간 부분 영역 잘라내는 중...");
                    sw.WriteLine($"cd {this.resultSavedDirectory + shape + @"\intermediate"}");
                    sw.WriteLine("pdal pipeline \"" + sevenone + inter + ".json\"");
                    sw.WriteLine("pdal pipeline \"" + seventwo + inter + ".json\"");
                }
                ProcessBatch(seven + inter + ".bat");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + seven + inter + ".bat 파일을 생성했습니다.");
            }

            AppendSeventhCSVFile();
        }
        //SegTrunk
        private void RunFileEighth()
        {
            string eight = "lv6_segmentTrunk_";
            //7단계 treeseg_findstems 
            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + eight + inter + ".bat";
                if (!File.Exists(batFilePath))
                {
                    using FileStream fs = File.Create(batFilePath);
                }
                using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
                {
                    sw.WriteLine("@ECHO OFF");
                    sw.WriteLine("chcp 65001 > nul");
                    sw.WriteLine("cls");
                    sw.WriteLine("echo 수간 추출 및 하층식생 제거 중...");
                    sw.WriteLine($"cd {this.resultSavedDirectory + shape + @"\intermediate"}");
                    sw.WriteLine("csp_segmentstem \"" + databaseFileName + "\"");
                    sw.WriteLine();
                }
                ProcessBatch(eight + inter + ".bat");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + inter + eight + ".bat 파일을 생성했습니다.");
            }
            AppendEighthCSVFile();
        }
        //SegCrown
        private void RunFileNinth()
        {
            string nine = "lv7_segmentCrown_";
            //9단계 개별목 추출 
            {
                string batFilePath = resultSavedDirectory + shape + @"\\intermediate\" + nine + inter + ".bat";
                if (!File.Exists(batFilePath))
                {
                    using FileStream fs = File.Create(batFilePath);
                }
                using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
                {
                    string tree_name = "_tree_";
                    string destination = @"..\tree";
                    sw.WriteLine("@ECHO OFF");
                    sw.WriteLine("chcp 65001 > nul");
                    sw.WriteLine("cls");
                    sw.WriteLine("echo 개별목 추출 중...");
                    //sw.WriteLine($"cd {this.resultSavedDirectory + shape + @"\intermediate"}");
                    sw.WriteLine("csp_segmentcrown \"" + databaseFileName + "\"");
                    //sw.WriteLine();
                    //sw.WriteLine("set destination=\"{0}\"", destination);
                    //sw.WriteLine();
                    //sw.WriteLine("for /r %%i in (*{0}*.pcd) do (", tree_name);
                    //sw.WriteLine("    move \"%%i\" \"%destination%\"");
                    //sw.WriteLine(")");
                }
                ProcessBatch(nine + inter + ".bat");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + nine + inter + ".bat 파일을 생성했습니다.");
            }

        }
        //measure
        private void RunFileTenth()
        {
            string ten = "lv8_measure_DBH_treeHeight_";
            //10단계 산림정보 속성 계산
            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + ten + inter + ".bat";
                if (!File.Exists(batFilePath))
                {
                    using FileStream fs = File.Create(batFilePath);
                }

                //MessageBox.Show(treeDirectoryPath);
                using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
                {
                    sw.WriteLine("@ECHO OFF");
                    sw.WriteLine("chcp 65001 > nul");
                    sw.WriteLine("cls");
                    sw.WriteLine("echo 산림 속성 정보 계산중...  ");
                    sw.WriteLine($"cd {this.resultSavedDirectory + shape + @"\intermediate"}");
                    sw.WriteLine("measure " + @"../tree" + " \"" + databaseFileName + "\"");
                }
                ProcessBatch(ten + inter + ".bat");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + ten + inter + ".bat 파일을 생성했습니다.");
            }
        }
        //PCD2LAS
        private void RunFileEleventh()
        {
            string eleven = "lv9_PCDtoLAS";
            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + eleven + inter + ".bat";
                if (!File.Exists(batFilePath))
                {
                    using FileStream fs = File.Create(batFilePath);
                }
                string FolderName2 = "..\\tree";
                using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
                {
                    sw.WriteLine("@ECHO OFF");
                    sw.WriteLine("chcp 65001 > nul");
                    sw.WriteLine("cls");
                    sw.WriteLine("echo pcd파일 las파일로 변환중");
                    sw.WriteLine($"cd {this.resultSavedDirectory + shape + @"\intermediate"}");
                    sw.WriteLine("PCD2LAS " + FolderName2);

                    //pcd 지우는 코드  ---> 배포 시 주석 풀기
                    //sw.WriteLine("for /r \"..\\tree\" %%i in (*.pcd) do (");
                    //sw.WriteLine("    del \"%%i\"");
                    //sw.WriteLine(")");

                    //sw.WriteLine("for /r \".\" %%i in (*.pcd) do (");
                    //sw.WriteLine("    del \"%%i\"");
                    //sw.WriteLine(")");


                    //intermediate 폴더 숨김처리 코드
                    //sw.WriteLine("attrib +h ../intermediate");
                }

                ProcessBatch(eleven + inter + ".bat");

                MakeInfoFile();

                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + eleven + originLasName + ".bat 파일을 생성했습니다.");
            }
        }

        //intermediate 삭제 코드
        private void del_inter()
        {
            {
                // 실행할 명령 프롬프트 명령어 설정
                string resultSavedDirectory = this.resultSavedDirectory + shape;
                string resultSavedDirectory3 = resultSavedDirectory + @"\" + "intermediate";

                string command = @$"rmdir /s /q {resultSavedDirectory3}";

                // Process 시작
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe"; // 명령 프롬프트 실행
                process.StartInfo.Arguments = "/c " + command; // /c 옵션은 명령어 실행 후 프롬프트 종료

                // 출력을 읽기 위한 설정
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                // 프로세스 실행
                process.Start();

                // 출력을 읽어오기
                string output = process.StandardOutput.ReadToEnd();
                Console.WriteLine(output);

                // 프로세스가 종료될 때까지 대기
                process.WaitForExit();

                // 프로세스 종료
                process.Close();
            }
        }
        //info파일 생성코드
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
                    string center = "중심 좌표(x,y) : " + paramForm.gui.centerX + ", " + paramForm.gui.centerY + "\n";
                    string radius = "반지름(m) : " + paramForm.gui.radius;
                    info += center + radius;
                }
                else if (shape == "_rectangle")
                {
                    string LT_info = "좌상단 좌표(x,y) : " + paramForm.gui.xMin + ", " + paramForm.gui.yMax + "\n";
                    string RB_info = "우하단 좌표(x,y) : " + paramForm.gui.xMax + ", " + paramForm.gui.yMin;
                    info += LT_info + RB_info;
                }
                else
                {
                    string poly_info = "다각형 좌표(x1 y1, x2 y2, ...) : " + paramForm.gui.pointList;
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

        //로그 작성 코드
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

        //전체 process 실행 코드
        private void preProAndExcuteStep()
        {
            MakeResultDirectory_PLOT();

            string resultP = resultSavedDirectory + shape;
            string strFile1 = resultP + @"\intermediate\" + "lv1_cropped_" + inter + "_B.las";
            FileInfo fileInfo1 = new FileInfo(strFile1);//파일 있는지 확인 있을때(true), 없으면(false)
            if (fileInfo1.Exists)
            {
                //전처리

                Outlier();
                Normalization();
                MakeSliceFile();
                Turn_Las_into_PCD();
                progress++;
                ProgressBarSet(progress);
                //=====

                if (Application.OpenForms["progressDialog"] == null)
                {
                    MessageBox.Show("기능 실행이 정상적으로 수행되지 않았습니다.");
                    return;
                }

                RunFileSecond();
                progress++;
                ProgressBarSet(progress);

                if (Application.OpenForms["progressDialog"] == null)
                {
                    MessageBox.Show("기능 실행이 정상적으로 수행되지 않았습니다.");
                    return;
                }

                RunFileThird();
                progress++;
                ProgressBarSet(progress);

                if (Application.OpenForms["progressDialog"] == null)
                {
                    MessageBox.Show("기능 실행이 정상적으로 수행되지 않았습니다.");
                    return;
                }

                RunFileFourth();
                progress++;
                ProgressBarSet(progress);

                //RunFileFifth();
                progress++;
                ProgressBarSet(progress);

                if (Application.OpenForms["progressDialog"] == null)
                {
                    MessageBox.Show("기능 실행이 정상적으로 수행되지 않았습니다.");
                    return;
                }

                //RunFileSixth();
                ExtractOriginDat();
                progress++;
                ProgressBarSet(progress);

                if (Application.OpenForms["progressDialog"] == null)
                {
                    MessageBox.Show("기능 실행이 정상적으로 수행되지 않았습니다.");
                    return;
                }

                RunFileSeventh();
                progress++;
                ProgressBarSet(progress);

                if (Application.OpenForms["progressDialog"] == null)
                {
                    MessageBox.Show("기능 실행이 정상적으로 수행되지 않았습니다.");
                    return;
                }

                RunFileEighth();//trunk
                progress++;
                ProgressBarSet(progress);
                if (CatchError(resultP + @"\intermediate\", 8)) return;

                if (Application.OpenForms["progressDialog"] == null)
                {
                    MessageBox.Show("기능 실행이 정상적으로 수행되지 않았습니다.");
                    return;
                }

                RunFileNinth();//tree
                progress++;
                ProgressBarSet(progress);
                if (CatchError(resultP + @"\tree\", 9)) return;

                if (Application.OpenForms["progressDialog"] == null)
                {
                    MessageBox.Show("기능 실행이 정상적으로 수행되지 않았습니다.");
                    return;
                }

                RunFileTenth();//measure

                if (CatchError(resultP + @"\tree\", 10)) return;

                if (Application.OpenForms["progressDialog"] == null)
                {
                    MessageBox.Show("기능 실행이 정상적으로 수행되지 않았습니다.");
                    return;
                }

                RunFileEleventh();
                progress++;
                ProgressBarSet(progress);


                //del_inter();

                progressDialog.Invoke(new Action(() => progressDialog.Close()));
            }
            else
            {
                MessageBox.Show("1단계 산출물 에러");
            }
        }

        //파일 생성을 기준, 에러 확인 코드
        bool CatchError(string path, int lv)
        {
            bool isError = true;
            string find;
            if (lv == 8)
            {
                find = "_trunk";
            }
            else if (lv == 9)
            {
                find = "_tree";
            }
            else
            {
                find = "_dbh";
            }
            try
            {
                // 디렉토리에서 파일 이름들을 가져옵니다.
                string[] fileNames = Directory.GetFiles(path);
                foreach (var file in fileNames)
                {
                    if (file.ToLower().Contains(find))
                    {
                        isError = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogWrite(lv + "단계 파일을 가져오는 중에 오류가 발생했습니다 : " + ex.Message);
            }

            return isError;
        }

        //LAS파일 입력 시 각 경로 세팅(Las파일 경로, 이름 등)
        private void tbPlotData_TextChanged(object sender, EventArgs e)
        {
            paramForm.gui.loadPath = tbPlotData.Text;

            originLasPath = tbPlotData.Text;
            originLasName = Path.GetFileNameWithoutExtension(originLasPath).ToLower();
            originLasDirectory = Path.GetDirectoryName(originLasPath);
        }

        //폴리곤 plot 실행 시 buffer 적용을 위해 사각형 꼭지점 찾는 코드
        private void FindExtremeCoordinates(point[] points)
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

        //Las파일 크기 읽고 유효값인지 점검
        private bool IsLasSizeValid()
        {
            if (lasSize.minx == 0 && lasSize.miny == 0 && lasSize.maxx == 0 && lasSize.maxy == 0)
            {
                return false;
            }
            else if (lasSize.miny == lasSize.maxy || lasSize.minx == lasSize.maxx)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
