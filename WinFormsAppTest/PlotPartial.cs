using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
        //자주쓰는 변수 왜 있는지 모르겠음
        string shape;

        //progess표시 변수
        int progress = 0;

        string configpath = "";

        //csv 작성용 Original data poiygon_points
        string poly_points="";

        //버퍼 계산 시 사용되는 좌표 구조체
        Coords bufferedPolycords = new Coords();

        //PlotForm 윈폼 이동 이벤트 관련 변수
        private Point relativePformPos = new Point();

        //las파일 크기 저장 구조체
        struct LasSize
        {
            public double maxx;
            public double maxy;
            public double minx;
            public double miny;
        }
        LasSize lasSize = new LasSize();

        //테스트용 변수(임시)
        string progressLog = "";

        public Form progressDialog;

        public System.Windows.Forms.TextBox progressTextBox;

        //PLOT
        private void MakeResultDirectory_PLOT()
        {
            //MessageBox.Show(this.resultSavedDirectory);
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

            paramForm.setParam(paramForm.csv_data, "gui", "tree_dir", tree_dir);
            paramForm.setParam(paramForm.csv_data, "gui", "intermediate_dir", inter_dir);

            paramForm.write_csv(configpath);

            RemoveDuplicatedPoints(originLasPath);

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
                        sw.WriteLine("pdal pipeline \"" + one + originLasName + ".json\"");
                        sw.WriteLine("echo>" + "\"" + one + originLasName + "_B.json\"");
                        sw.WriteLine("pdal info \"" + one + originLasName + "_B.las\" > \"" + one + originLasName + "_B.json\"");
                    }

                    ProcessBatch(one + originLasName + ".bat");
                    LogWrite(resultSavedDirectory + @"\intermediate\" + originLasName + "1.crop" + ".bat 파일 생성");
                }
                {
                    //Process proc = null;
                    //string _batDir = resultSavedDirectory + @"\intermediate\";
                    ////MessageBox.Show(_batDir);

                    ////var CurrentDirectory = Directory.GetCurrentDirectory();
                    ////MessageBox.Show(CurrentDirectory);

                    //proc = new Process();

                    //proc.StartInfo.UseShellExecute = true;

                    //proc.StartInfo.WorkingDirectory = _batDir;
                    //proc.StartInfo.FileName = one + originLasName + ".bat";
                    //proc.StartInfo.CreateNoWindow = false;
                    ////MessageBox.Show("proc.info : " + proc.StartInfo.WorkingDirectory + proc.StartInfo.FileName);
                    //proc.Start();
                    //proc.WaitForExit();
                    //proc.Close();

                    string strFile1 = resultSavedDirectory + @"\intermediate\" + one + originLasName + "_B.las";
                    FileInfo fileInfo1 = new FileInfo(strFile1);//파일 있는지 확인 있을때(true), 없으면(false)
                    if (fileInfo1.Exists)
                    {
                        string dat_str = "";
                        try
                        {
                            string JsonText1 = System.IO.File.ReadAllText(resultSavedDirectory + @"\intermediate\" + one + originLasName + "_B.json");
                            JObject JsonData1 = JObject.Parse(JsonText1);
                            JToken minx = JsonData1["stats"]["bbox"]["native"]["bbox"]["minx"];
                            JToken maxx = JsonData1["stats"]["bbox"]["native"]["bbox"]["maxx"];
                            JToken miny = JsonData1["stats"]["bbox"]["native"]["bbox"]["miny"];
                            JToken maxy = JsonData1["stats"]["bbox"]["native"]["bbox"]["maxy"];
                            //str을 만들었으니 이제 그 데이터를 dat 파일에 넣는다.

                            dat_str = $"xmin={minx} xmax={maxx} ymin={miny} ymax={maxy}";
                            paramForm.setParam(paramForm.csv_data, "filters.crop", "bufferd_dat", dat_str);
                            paramForm.write_csv(configpath);
                        }
                        catch
                        {
                            LogWrite(resultSavedDirectory + @"\intermediate\" + one + originLasName + "_B.dat 파일 생성 오류.");
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
            double centerX = double.Parse(tbPlotCircleX.Text);
            double centerY = double.Parse(tbPlotCircleY.Text);
            double radius = double.Parse(tbPlotCircleR.Text);
            double buffer = double.Parse(paramForm.getParam(paramForm.csv_data, "filters.crop", "buffer"));
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
                   new JProperty ("compression","laszip"),
                   new JProperty("filename",  one + originLasName + "_B.las")
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

            double xmin = double.Parse(tbPlotRecXmin.Text);
            double ymin = double.Parse(tbPlotRecYmin.Text);
            double xmax = double.Parse(tbPlotRecXmax.Text);
            double ymax = double.Parse(tbPlotRecYmax.Text);
            double buffer = double.Parse(paramForm.getParam(paramForm.csv_data, "filters.crop","buffer"));

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
                   new JProperty("compression", "laszip"),
                    new JProperty("filename", one + originLasName + "_B.las")
               );
                File.WriteAllText(resultSavedDirectory + @"\intermediate\" + one + originLasName + ".json",
                     "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");

                LogWrite(resultSavedDirectory + @"\intermediate\" + one + originLasName + "_B.las 파일을 생성했습니다.");
            }
        }

        /// Cropping step, 사용자가 입력한 좌표를(다각형의 꼭짓점) 읽어온 후 표준지를 다각형으로 자릅니다.
        private void MakePolygonPlot()
        {
            double buffer = double.Parse(paramForm.getParam(paramForm.csv_data, "filters.crop","buffer"));
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
                   new JProperty("compression", "laszip"),
                   new JProperty("filename", one + originLasName + "_B.las")
               );
                File.WriteAllText(resultSavedDirectory + @"\intermediate\" + one + originLasName + ".json",
                    "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");

                LogWrite(resultSavedDirectory + @"\intermediate\" + one + originLasName + "_B.las 파일을 생성했습니다.");
            }
        }


        //데이터 전처리 단계, 1~6단계 JSON 생성, 7~8단계 csv append line
        private void Outlier()
        {
            string second = "level2_outlierRemoved_";
            {
                JObject secondin = new JObject(
               new JProperty("type", "readers.las"),
               new JProperty("filename", "level1_cropped_" + originLasName + "_B.las")
             );
                JObject Outlier = new JObject(
                  new JProperty("type", "filters.outlier"),
                  new JProperty("method", "statistical"),
                  new JProperty("mean_k", paramForm.getParam(paramForm.csv_data, "filters.outlier", "mean_k")),
                  new JProperty("multiplier", paramForm.getParam(paramForm.csv_data, "filters.outlier", "multiplier"))
              );
                JObject secondout = new JObject(
                   new JProperty("type", "writers.las"),
                   new JProperty("compression", "laszip"),
                   new JProperty("filename", second + originLasName + ".las")
               );
                File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + second + originLasName + ".json",
                    "[" + secondin.ToString() + ", " + Outlier.ToString() + ", " + secondout.ToString() + "]");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + second + originLasName + ".json 파일을 생성했습니다.");
            }
        }
        private void Normalization()
        {
            string three = "level3_nomarlized_";
            {
                JObject thirdin = new JObject(
              new JProperty("type", "readers.las"),
              new JProperty("filename", "level2_outlierRemoved_" + originLasName + ".las")
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
                   new JProperty("cell", paramForm.getParam(paramForm.csv_data, "filters.smrf", "cell")),
                   new JProperty("window", paramForm.getParam(paramForm.csv_data, "filters.smrf", "window")),
                   new JProperty("slope", paramForm.getParam(paramForm.csv_data, "filters.smrf", "slope")),
                   new JProperty("threshold", paramForm.getParam(paramForm.csv_data, "filters.smrf", "threshold")),
                   new JProperty("scalar", paramForm.getParam(paramForm.csv_data, "filters.smrf", "scalar"))
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
                   new JProperty("filename", three + originLasName + ".las")
               );
                File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + three + originLasName + ".json", "[" + thirdin.ToString() + ", " + Assign.ToString() + ", " + elm.ToString() + ", " + smrf.ToString() + ", " + hagnn.ToString() + ", " + ferry.ToString() + ", " + Writers.ToString() + "]");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + three + originLasName + ".json 파일을 생성했습니다.");
            }
        }
        

        //private void Subsampling()
        //{
        //    string three = "level3_subsampled_crown_";

        //    //MessageBox.Show("sun resultSD : " + resultSavedDirectory);
        //    {
        //        JObject Readers = new JObject(
        //          new JProperty("type", "readers.las"),
        //          new JProperty("filename", "\"" + originLasName + "_CSlice.las\"")
        //      );
        //        JObject sonSpec = new JObject(
        //           new JProperty("type", "filters.voxeldownsize"),
        //           new JProperty("cell", paramForm.getParam(paramForm.csv_data, "filters.sample", "cell")),
        //           new JProperty("mode", "center")
        //       );
        //        JObject Writers = new JObject(
        //           new JProperty("type", "writers.las"),
        //           new JProperty("compression", "laszip"),
        //           new JProperty("filename", "\"" + three + originLasName + ".las\"")
        //       );
        //        File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + three + originLasName + ".json", "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");
        //        LogWrite(resultSavedDirectory + shape + @"\intermediate\" + three + originLasName + ".json 파일을 생성했습니다..");
        //    }

        //    /*if (radioButton2.Checked)
        //    {
        //        //voxeldown
        //        {
        //            JObject Readers = new JObject(
        //              new JProperty("type", "readers.las"),
        //              new JProperty("filename", "\"" + "level3_nomarlized_" + originLasName + ".las")
        //          );
        //            JObject sonSpec = new JObject(
        //               new JProperty("type", "filters.sample"),
        //               new JProperty("cell", paramForm.getParam(paramForm.csv_data, "filters.sample", "cell")),
        //           );
        //            JObject Writers = new JObject(
        //               new JProperty("type", "writers.las"),
        //               new JProperty("filename", "\"" + four + originLasName + ".las")
        //           );
        //            File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + four + originLasName + ".json", "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");
        //            LogWrite(resultSavedDirectory + shape + @"\intermediate\" + four + originLasName + ".json 파일을 생성했습니다..");
        //        }
        //    }*/
        //}

        //private void Merge()
        //{
        //    string five = "level5_merged_";
        //    var json = new JObject(
        //    new JProperty("pipeline", new JArray(
        //        new JObject(
        //            new JProperty("type", "readers.las"),
        //            new JProperty("filename", originLasName + "_CSlice.las")
        //        ),
        //        new JObject(
        //            new JProperty("type", "readers.las"),
        //            new JProperty("filename", originLasName + "_TSlice.las")
        //        ),
        //        new JObject(
        //            new JProperty("type", "filters.merge")
        //        ),
        //        new JObject(
        //            new JProperty("type", "writers.las"),
        //            new JProperty("compression", "laszip"),
        //            new JProperty("filename", five + originLasName + ".las")
        //        )))
        //    );
        //    File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + five + originLasName + ".json", json.ToString());
        //    LogWrite(resultSavedDirectory + shape + @"\intermediate\" + five + originLasName + ".json 파일을 생성했습니다.");

        //}
        //private void MakeOriginPlot()
        //{
        //    string four = "level4_croporiginPlot_";
        //    {
        //        double centerX = paramForm.gui.centerX;
        //        double centerY = paramForm.gui.centerY;

        //        if (shape == "_circle")
        //        {
        //            double radius = paramForm.gui.radius;
        //            {
        //                JObject Readers = new JObject(
        //                  new JProperty("type", "readers.las"),
        //                new JProperty("filename","level3_nomarlized_" + originLasName + ".las")
        //               );
        //                JObject sonSpec = new JObject(
        //                   new JProperty("type", "filters.crop"),
        //                   new JProperty("point", "POINT(" + centerX + " " + centerY + ")"),
        //                new JProperty("distance", radius)
        //               );
        //                JObject Writers = new JObject(
        //                   new JProperty("type", "writers.las"),
        //                   new JProperty("compression", "laszip"),
        //                   new JProperty("filename", four + originLasName + "_O.las")
        //               );
        //                File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + four + originLasName + ".json", "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");
        //                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + four + originLasName + ".json 파일을 생성했습니다.");
        //            }
        //        }
        //        //square
        //        else if (shape == "_rectangle")
        //        {
        //            double xmin = paramForm.gui.xMin;
        //            double ymin = paramForm.gui.yMin;
        //            double xmax = paramForm.gui.xMax;
        //            double ymax = paramForm.gui.yMax;
        //            {
        //                //default
        //                JObject Readers = new JObject(
        //                  new JProperty("type", "readers.las"),
        //                  new JProperty("filename", "\"" + "level3_nomarlized_" + originLasName + ".las")
        //                );
        //                JObject sonSpec = new JObject(
        //                    new JProperty("type", "filters.crop"),
        //                    new JProperty("bounds", "([" + xmin + "," + xmax + "],[" + ymin + "," + ymax + "])")
        //                );
        //                JObject Writers = new JObject(
        //                   new JProperty("type", "writers.las"),
        //                   new JProperty("compression", "laszip"),
        //                   new JProperty("filename", "\"" + four + originLasName + "_O.las")
        //                );
        //                File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + four + originLasName + ".json", "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");
        //                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + four + originLasName + ".json 파일을 생성했습니다.");
        //            }
        //        }
        //        //polygon
        //        else
        //        {
        //            string points = paramForm.gui.pointList;
        //            poly_points = points.Replace(',', ' ');
        //            {
        //                JObject Readers = new JObject(
        //                  new JProperty("type", "readers.las"),
        //                  new JProperty("filename", "\"" + "level3_nomarlized_" + originLasName + ".las")
        //                );
        //                JObject sonSpec = new JObject(
        //                    new JProperty("type", "filters.crop"),
        //                    new JProperty("polygon", "POLYGON((" + points + "))")
        //                );
        //                JObject Writers = new JObject(
        //                   new JProperty("type", "writers.las"),
        //                   new JProperty("compression", "laszip"),
        //                   new JProperty("filename", "\"" + four + originLasName + "_O.las")
        //                );
        //                File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + four + originLasName + ".json", "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");
        //                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + four + originLasName + ".json 파일을 생성했습니다.");
        //            }
        //        }
        //    }
        //}

        private void ExtractOriginDat()
        {
            string six = "level6_extractOriginDat_"; //crop 후 dat 파일 만들기
            string dat_str = "";                    
            {
                if (shape == "_polygon")
                {
                    try
                    {
                        paramForm.setParam(paramForm.csv_data, "filters.crop", "origin_dat", poly_points);
                        paramForm.write_csv(configpath);
                    }
                    catch (Exception ex)
                    {
                        LogWrite(resultSavedDirectory + @"\intermediate\" + six + originLasName + "_O.dat 파일 생성 오류.");
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

                            dat_str = $"xmin={minx} xmax={maxx} ymin={miny} ymax={maxy} cx={centerX} cy={centerY} radius={radius}";
                            paramForm.setParam(paramForm.csv_data, "filters.crop", "origin_dat", dat_str);
                            paramForm.write_csv(configpath);
                        }
                        else
                        {
                            minx = lasSize.minx > (paramForm.gui.xMin) ? lasSize.minx : (paramForm.gui.xMin);
                            maxx = lasSize.maxx < (paramForm.gui.xMax) ? lasSize.maxx : (paramForm.gui.xMax);
                            miny = lasSize.miny > (paramForm.gui.yMin) ? lasSize.miny : (paramForm.gui.yMin);
                            maxy = lasSize.maxy < (paramForm.gui.yMax) ? lasSize.maxy : (paramForm.gui.yMax);

                            dat_str = $"xmin={minx} xmax={maxx} ymin={miny} ymax={maxy}";
                            paramForm.setParam(paramForm.csv_data, "filters.crop", "origin_dat", dat_str);
                            paramForm.write_csv(configpath);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogWrite(resultSavedDirectory + @"\intermediate\" + six + originLasName + "_O.dat 파일을 생성 오류.");
                        return;
                    }
                }
            }
            LogWrite(resultSavedDirectory + shape + @"\intermediate\" + six + originLasName + "_O.dat 파일을 생성했습니다.");
        }
        private void Turn_Las_into_PCD()
        {
            string four = "level4_LAStoPCD";
            //string sevenoneone = "level7-1-1_LAStoPCDforTslice_";
            //string sevenonetwo = "level7-1-2_LAStoPCDforCslice_";
            //LAStoPCD
            {
                JObject Readers = new JObject(
                  new JProperty("type", "readers.las"),
                new JProperty("filename", "level3_nomarlized_" + originLasName + ".las")
               );
                JObject Writers = new JObject(
                   new JProperty("type", "writers.pcd"),
                   new JProperty("filename", four + originLasName + ".pcd"),
                   //new JProperty("precision", 12),
                   //new JProperty("order", "X=Double:12,Y=Double:12,Z=Double:12"),
                   new JProperty("compression", "ascii") // 바이너리 PCD 출력 설정
               );
                File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + four + originLasName + ".json", "[" + Readers.ToString() + ", " + Writers.ToString() + "]");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + four + originLasName + ".json 파일을 생성했습니다.");
            }
            //{
            //    JObject Readers = new JObject(
            //      new JProperty("type", "readers.las"),
            //    new JProperty("filename", originLasName + "_TSlice.las")
            //   );
            //    JObject Writers = new JObject(
            //       new JProperty("type", "writers.pcd"),
            //       new JProperty("filename", sevenoneone + originLasName + ".pcd"),
            //       new JProperty("precision", 12),
            //       new JProperty("order", "X,Y,Z"),
            //       new JProperty("compression", "ascii") // 바이너리 PCD 출력 설정
            //   );
            //    File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + sevenoneone + originLasName + ".json", "[" + Readers.ToString() + ", " + Writers.ToString() + "]");
            //    LogWrite(resultSavedDirectory + shape + @"\intermediate\" + sevenoneone + originLasName + ".json 파일을 생성했습니다.");
            //}
            //{
            //    JObject Readers = new JObject(
            //      new JProperty("type", "readers.las"),
            //      new JProperty("filename", originLasName + "_CSlice.las")
            //   );
            //    JObject Writers = new JObject(
            //       new JProperty("type", "writers.pcd"),
            //       new JProperty("filename", sevenonetwo + originLasName + ".pcd"),
            //       new JProperty("precision", 12),
            //       new JProperty("order", "X,Y,Z"),
            //       new JProperty("compression", "ascii") // 바이너리 PCD 출력 설정
            //   );
            //    File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + sevenonetwo + originLasName + ".json", "[" + Readers.ToString() + ", " + Writers.ToString() + "]");
            //    LogWrite(resultSavedDirectory + shape + @"\intermediate\" + sevenonetwo + originLasName + ".json 파일을 생성했습니다.");
            //}
            
        }

        private void MakeSliceFile()
        {
            /*string fourone = "level4-1_trunkslice_";
            string fourtwo = "level4-2_crownslice_";
            //trunkslice
            {
                JObject Readers = new JObject(
                  new JProperty("type", "readers.las"),
                  new JProperty("filename", "level3_nomarlized_" + originLasName + ".las")
              );
                JObject sonSpec = new JObject(
                   new JProperty("type", "filters.range"),
                   new JProperty("limits", @"Z[" + paramForm.getParam(paramForm.csv_data, "filters.range.trunk", "minheight") + ":"
                   + paramForm.getParam(paramForm.csv_data, "filters.range.trunk", "maxheight") + "]")
               );
                JObject Writers = new JObject(
                   new JProperty("type", "writers.las"),
                   new JProperty("compression", "laszip"),
                   new JProperty("filename", originLasName + "_TSlice.las")
               );
                File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + fourone + originLasName + ".json", "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + fourone + originLasName + ".json 파일을 생성했습니다.");
            }
            //crownslice
            {
                JObject Readers = new JObject(
                  new JProperty("type", "readers.las"),
                  new JProperty("filename", "level3_nomarlized_" + originLasName + ".las")
              );
                JObject sonSpec = new JObject(
                   new JProperty("type", "filters.range"),
                   new JProperty("limits", @"Z[" + paramForm.getParam(paramForm.csv_data, "filters.range.trunk", "maxheight") + ":"
                   + paramForm.getParam(paramForm.csv_data, "filters.range.crown", "maxheight") + "]")
               );
                JObject Writers = new JObject(
                   new JProperty("type", "writers.las"),
                   new JProperty("compression", "laszip"),
                   new JProperty("filename", originLasName + "_CSlice.las")
               );
                File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + fourtwo + originLasName + ".json", "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + fourtwo + originLasName + ".json 파일을 생성했습니다.");
            }*/


            string sevenone = "level7-1_trunkslice_";
            string seventwo = "level7-2_crownslice_";
            //trunkslice
            {
                JObject Readers = new JObject(
                  new JProperty("type", "readers.pcd"),
                  new JProperty("filename", "level4_LAStoPCD" + originLasName + ".pcd")
              );
                JObject sonSpec = new JObject(
                   new JProperty("type", "filters.range"),
                   new JProperty("limits", @"Z[" + paramForm.getParam(paramForm.csv_data, "filters.range.trunk", "minheight") + ":"
                   + paramForm.getParam(paramForm.csv_data, "filters.range.trunk", "maxheight") + "]")
               );
                JObject Writers = new JObject(
                   new JProperty("type", "writers.pcd"),
                   new JProperty("filename", sevenone + originLasName + ".pcd")
                   //new JProperty("precision", 12),
                   //new JProperty("order", "X=Double:12,Y=Double:12,Z=Double:12"),
                   //new JProperty("compression", "binary")
               );
                File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + sevenone + originLasName + ".json", "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + sevenone + originLasName + ".json 파일을 생성했습니다.");
            }
            //crownslice
            {
                JObject Readers = new JObject(
                  new JProperty("type", "readers.pcd"),
                  new JProperty("filename", "level4_LAStoPCD" + originLasName + ".pcd")
              );
                JObject sonSpec = new JObject(
                   new JProperty("type", "filters.range"),
                   new JProperty("limits", @"Z[" + paramForm.getParam(paramForm.csv_data, "filters.range.crown", "minheight") + ":"
                   + paramForm.getParam(paramForm.csv_data, "filters.range.crown", "maxheight") + "]")
               );
                JObject Writers = new JObject(
                   new JProperty("type", "writers.pcd"),
                   new JProperty("filename", seventwo + originLasName + ".pcd")
                   //new JProperty("precision", 12),
                   //new JProperty("order", "X=Double:12,Y=Double:12,Z=Double:12"),
                   //new JProperty("compression", "binary")
               );
                File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + seventwo + originLasName + ".json", "[" + Readers.ToString() + ", " + sonSpec.ToString() + ", " + Writers.ToString() + "]");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + seventwo + originLasName + ".json 파일을 생성했습니다.");
            }
        }

        private void AppendSeventhCSVFile()
        {
            string trunkslicefile = "";

            String FolderName = resultSavedDirectory + shape + @"\intermediate";
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(FolderName);
            foreach (System.IO.FileInfo fi in di.GetFiles())
            {
                if (fi.Extension.ToLower().CompareTo(".pcd") == 0 && fi.Name.Contains(originLasName) == true && fi.Name.ToLower().Contains("trunkslice") == true)
                {
                    trunkslicefile = fi.FullName;
                }
            }

            paramForm.setParam(paramForm.csv_data, "csp_segmentstem", "trunk_slice_file", trunkslicefile);

            try
            {
                paramForm.write_csv(configpath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSV 파일에 내용을 추가하는 중 오류 발생: " + ex.Message);
            }
        }
        private void AppendEighthCSVFile()
        {
            List<String> filenames_pcd = new List<String>();

            string crownslicefile = "";
            string FolderName = resultSavedDirectory + shape + @"\intermediate";
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(FolderName);
            foreach (System.IO.FileInfo fi in di.GetFiles())
            {
                if (fi.Extension.ToLower().CompareTo(".pcd") == 0 && fi.Name.ToLower().Contains(originLasName.ToLower()) == true && fi.Name.ToLower().Contains("_trunk_") == true)
                {
                    filenames_pcd.Add(fi.FullName);
                }
            }
            foreach (System.IO.FileInfo fi in di.GetFiles())
            {
                if (fi.Extension.ToLower().CompareTo(".pcd") == 0 && fi.Name.ToLower().Contains(originLasName.ToLower()) == true && fi.Name.ToLower().Contains("crownslice") == true)
                {
                    crownslicefile = fi.FullName;
                }
            }

            paramForm.setParam(paramForm.csv_data, "csp_segmentcrown", "trunk_files", string.Join(" ", filenames_pcd));
            paramForm.setParam(paramForm.csv_data, "csp_segmentcrown", "crown_slice_file", crownslicefile);

            // CSV 파일에 내용 추가
            try
            {
                paramForm.write_csv(configpath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSV 파일에 내용을 추가하는 중 오류 발생: " + ex.Message);
            }
        }
        //배치파일 실행 코드
        private void ProcessBatch(string batFile)
        {
            string output = "";
            using (Process proc = new Process())
            {
                //proc.StartInfo.WorkingDirectory = resultSavedDirectory + shape + @"\intermediate\";
                proc.StartInfo.FileName = resultSavedDirectory + shape + @"\intermediate\" + batFile;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.StandardOutputEncoding = System.Text.Encoding.UTF8;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.OutputDataReceived += new DataReceivedEventHandler(OutputDataReceived);

                proc.Start();
                proc.BeginOutputReadLine();
                proc.WaitForExit();
            }
            /*
            //num(ex. "level1_~";
            Process proc = null;
            string _batDir = resultSavedDirectory + shape + @"\intermediate\";
            proc = new Process();
            //proc.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.WorkingDirectory = _batDir;
            proc.StartInfo.FileName = num + originLasName + ".bat";
            proc.StartInfo.CreateNoWindow = false;
            proc.Start();
            proc.WaitForExit();
            proc.Close();
            */
        }

        private void OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null && progressDialog != null)
            {
                foreach(Control control in progressDialog.Controls)
                {
                    if(control.GetType() == typeof(System.Windows.Forms.TextBox)) 
                    {
                        control.Invoke(new Action(() => control.Text += e.Data + Environment.NewLine));
                    }
                }
            }
        }

        //배치파일 작성 코드
        private void RunFileZero(string target_path)
        {
            string zero = "level0_DuplicateRemove_";
            //7단계 treeseg_findstems 
            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + zero + originLasName + ".bat";
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
                    sw.WriteLine("laszip \"" + target_path + "\"");
                    sw.WriteLine();
                }
                
                ProcessBatch(zero + originLasName + ".bat");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + originLasName + zero + ".bat 파일을 생성했습니다.");
            }
        }

        private void RunFileSecond()
        {
            string two = "level2_outlierRemoved_";
            //2단계
            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + two + originLasName + ".bat";
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
                    sw.WriteLine("pdal pipeline \"" + two + originLasName + ".json\"");
                }
                
                ProcessBatch(two + originLasName + ".bat");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + two + originLasName + ".bat 파일을 생성했습니다.");
            }
        }
        private void RunFileThird()
        {
            string three = "level3_nomarlized_";
            //3단계
            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + three + originLasName + ".bat";
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
                    sw.WriteLine("pdal pipeline \"" + three + originLasName + ".json\"");
                }
                
                ProcessBatch(three + originLasName + ".bat");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + three + originLasName + ".bat 파일을 생성했습니다.");
            }
        }
        
        private void RunFileFourth()
        {
            string four = "level4_LAStoPCD";

            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + four + originLasName + ".bat";

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
                    sw.WriteLine("pdal pipeline \"" + four + originLasName + ".json\"");
                    sw.WriteLine("Delete_duplication \"" + four + originLasName + ".pcd\"");
                }
                ProcessBatch(four + originLasName + ".bat");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + four + originLasName + ".bat 파일을 생성했습니다.");
            }

            /*7-1단계
            //{
            //    string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + sevenoneone + originLasName + ".bat";
            //    if (!File.Exists(batFilePath))
            //    {
            //        using (FileStream fs = File.Create(batFilePath))
            //        {
            //            fs.Close();
            //        }
            //    }
            //    using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
            //    {
            //        sw.WriteLine("chcp 65001");
            //        sw.WriteLine("cls");
            //        sw.WriteLine("@ECHO OFF");
            //        sw.WriteLine("echo TSlice LAS to PCD 변환 중...");
            //        sw.WriteLine("pdal pipeline \"" + sevenoneone + originLasName + ".json\"");
            //        sw.WriteLine("Delete_duplication \"" + sevenoneone + originLasName + ".pcd\"");
            //    }
            //    progressLog += ProcessBatch(sevenoneone + originLasName + ".pcd") + Environment.NewLine;
            //    LogWrite(resultSavedDirectory + shape + @"\intermediate\" + sevenoneone + originLasName + ".bat 파일을 생성했습니다.");
            //}
            //7-2단계
            //{
            //    string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + sevenonetwo + originLasName + ".bat";
            //    if (!File.Exists(batFilePath))
            //    {
            //        using (FileStream fs = File.Create(batFilePath))
            //        {
            //            fs.Close();
            //        }
            //    }
            //    using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
            //    {
            //        sw.WriteLine("chcp 65001");
            //        sw.WriteLine("cls");
            //        sw.WriteLine("@ECHO OFF");
            //        sw.WriteLine("echo CSlice Las to PCD 변환 중...");
            //        sw.WriteLine("pdal pipeline " + sevenonetwo + originLasName + ".json");
            //        sw.WriteLine("Delete_duplication " + sevenonetwo + originLasName + ".pcd");
            //    }
            //    progressLog += ProcessBatch(sevenonetwo + originLasName + ".pcd") + Environment.NewLine;
            //    LogWrite(resultSavedDirectory + shape + @"\intermediate\" + sevenonetwo + originLasName + ".bat 파일을 생성했습니다.");
            }*/


        }

        //private void RunFileThird()
        //{
        //    string three = "level3_subsampled_crown_";
        //    {
        //        string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + three + originLasName + ".bat";
        //        if (!File.Exists(batFilePath))
        //        {
        //            using (FileStream fs = File.Create(batFilePath))
        //            {
        //                fs.Close();
        //            }
        //        }
        //        using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
        //        {
        //            sw.WriteLine("chcp 65001");
        //            sw.WriteLine("cls");
        //            sw.WriteLine("@ECHO OFF");
        //            sw.WriteLine("echo 수관 부분 파일 축소 중...");
        //            sw.WriteLine("pdal pipeline \"" + three + originLasName + ".json\"");
        //        }
        //        progressLog += ProcessBatch(three + originLasName + ".json") + Environment.NewLine;
        //        LogWrite(resultSavedDirectory + shape + @"\intermediate\" + three + originLasName + ".bat 파일을 생성했습니다.");
        //    }

        //}
        //private void RunFileFifth()
        //{
        //    string five = "level5_merged_";
        //    {
        //        string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + five + originLasName + ".bat";

        //        if (!File.Exists(batFilePath))
        //        {
        //            using (FileStream fs = File.Create(batFilePath))
        //            {
        //                fs.Close();
        //            }
        //        }
        //        using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
        //        {
        //            sw.WriteLine("chcp 65001");
        //            sw.WriteLine("cls");
        //            sw.WriteLine("@ECHO OFF");
        //            sw.WriteLine("echo 수관 부분 수간 부분 영역 합치는 중...");
        //            sw.WriteLine("pdal pipeline \"" + five + originLasName + ".json\"");
        //        }
        //        progressLog += ProcessBatch(five + originLasName + ".json") + Environment.NewLine;
        //        LogWrite(resultSavedDirectory + shape + @"\intermediate\" + five + originLasName + ".bat 파일을 생성했습니다.");
        //    }
        //}

        //private void RunFileSixth()
        //{
        //    string centerX = paramForm.gui.centerX.ToString();
        //    string centerY = paramForm.gui.centerY.ToString();

        //    string four = "level4_croporiginPlot_"; //crop 후 dat 파일 만들기
        //    {
        //        //crop origin plot
        //        string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + four + originLasName + ".bat";
        //        if (!File.Exists(batFilePath))
        //        {
        //            using (FileStream fs = File.Create(batFilePath))
        //            {
        //                fs.Close();
        //            }
        //        }
        //        using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
        //        {
        //            sw.WriteLine("chcp 65001");
        //            sw.WriteLine("cls");
        //            sw.WriteLine("@ECHO OFF");
        //            sw.WriteLine("echo 원본 표준지 분류 중...");
        //            sw.WriteLine("pdal pipeline \"" + four + originLasName + ".json\"");
        //            sw.WriteLine("echo>" + "\"" + four + originLasName + "_O.json\"");
        //            sw.WriteLine("pdal info " + "\"" + four + originLasName + "_O.las\" > " + "\"" + four + originLasName + "_O.json\"");
        //        }
        //        progressLog += ProcessBatch(four + originLasName + "_O.las") + Environment.NewLine;;
        //        //process batch file to make dat file
        //        string dat_str = "";
        //        string strFile1 = resultSavedDirectory + shape + @"\intermediate\" + four + originLasName + "_O.las";
        //        FileInfo fileInfo1 = new FileInfo(strFile1);//파일 있는지 확인 있을때(true), 없으면(false)
        //        if (fileInfo1.Exists)
        //        {
        //            if (shape == "_polygon")
        //            {
        //                try
        //                {
        //                    //MessageBox.Show(strFile1);
        //                    /*string JsonText1 = System.IO.File.ReadAllText(resultSavedDirectory + shape + @"\intermediate\" + fourtwo + originLasName + "_O.json");

        //                    JObject JsonData1 = JObject.Parse(JsonText1);

        //                    JArray coordinatesArray = (JArray)JsonData1["stats"]["bbox"]["native"]["boundary"]["coordinates"][0];

        //                    string points = "";
        //                    for (int i = 0; i < coordinatesArray.Count; i++)
        //                    {
        //                        JArray vertex = (JArray)coordinatesArray[i];
        //                        points += vertex[0] + " ";
        //                        points += vertex[1] + " ";
        //                    }

        //                    dat_str = points;*/
        //                    paramForm.setParam(paramForm.csv_data, "filters.crop", "origin_dat", poly_points);
        //                    paramForm.write_csv(configpath);

        //                    //File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + fourtwo + originLasName + "_O.dat", points);

        //                }
        //                catch (Exception ex)
        //                {
        //                    LogWrite(resultSavedDirectory + @"\intermediate\" + four + originLasName + "_O.dat 파일을 생성 오류.");
        //                    return;
        //                }
        //            }
        //            else
        //            {
        //                try
        //                {
        //                    //MessageBox.Show(strFile1);
        //                    string JsonText1 = System.IO.File.ReadAllText(resultSavedDirectory + shape + @"\intermediate\" + six + originLasName + "_O.json");

        //                    JObject JsonData1 = JObject.Parse(JsonText1);

        //                    JToken minx = JsonData1["stats"]["bbox"]["native"]["bbox"]["minx"];
        //                    JToken maxx = JsonData1["stats"]["bbox"]["native"]["bbox"]["maxx"];
        //                    JToken miny = JsonData1["stats"]["bbox"]["native"]["bbox"]["miny"];
        //                    JToken maxy = JsonData1["stats"]["bbox"]["native"]["bbox"]["maxy"];

        //                    //passvalue centerx, centery, radius를 추가하기
        //                    if (shape == "_circle")
        //                    {
        //                        double radius = paramForm.gui.radius;

        //                        //File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + fourtwo + originLasName + "_O.dat",
        //                        //    minx + " " + maxx + " " + miny + " " + maxy + " " + centerX + " " + centerY + " " + radius);

        //                        dat_str = $"xmin={minx} xmax={maxx} ymin={miny} ymax={maxy} cx={centerX} cy={centerY} radius={radius}";
        //                        paramForm.setParam(paramForm.csv_data, "filters.crop", "origin_dat", dat_str);
        //                        paramForm.write_csv(configpath);
        //                    }
        //                    else
        //                    {
        //                        //File.WriteAllText(resultSavedDirectory + shape + @"\intermediate\" + fourtwo + originLasName + "_O.dat", minx + " " + maxx + " " + miny + " " + maxy);

        //                        dat_str = $"xmin={minx} xmax={maxx} ymin={miny} ymax={maxy}";
        //                        paramForm.setParam(paramForm.csv_data, "filters.crop", "origin_dat", dat_str);
        //                        paramForm.write_csv(configpath);
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    LogWrite(resultSavedDirectory + @"\intermediate\" + four + originLasName + "_O.dat 파일을 생성 오류.");
        //                    return;
        //                }
        //            }
        //        }
        //        LogWrite(resultSavedDirectory + shape + @"\intermediate\" + four + originLasName + "_O.dat 파일을 생성했습니다.");
        //    }
        //}

        private void RunFileSeventh()
        {
            //string four = "level4_makeslicefile_";
            //string fourone = "level4-1_trunkslice_";
            //string fourtwo = "level4-2_crownslice_";

            string seven = "level7_Slicing_";
            string sevenone = "level7-1_trunkslice_";
            string seventwo = "level7-2_crownslice_";

            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + seven + originLasName + ".bat";
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
                    sw.WriteLine("pdal pipeline \"" + sevenone + originLasName + ".json\"");
                    sw.WriteLine("pdal pipeline \"" + seventwo + originLasName + ".json\"");
                }
                ProcessBatch(seven + originLasName + ".bat");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + seven + originLasName + ".bat 파일을 생성했습니다.");
            }

            AppendSeventhCSVFile();
        }
        private void RunFileEighth()
        {
            string eight = "level8_segmentTrunk_";
            //7단계 treeseg_findstems 
            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + eight + originLasName + ".bat";
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
                    sw.WriteLine("csp_segmentstem \"" + configpath + "\"");
                    sw.WriteLine();
                }
                ProcessBatch(eight + originLasName + ".bat");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + originLasName + eight + ".bat 파일을 생성했습니다.");
            }
            AppendEighthCSVFile();
        }
        private void RunFileNinth()
        {
            string nine = "level9_segmentCrown_";
            //9단계 개별목 추출 
            {
                string batFilePath = resultSavedDirectory + shape + @"\\intermediate\" + nine + originLasName + ".bat";
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
                    sw.WriteLine($"cd {this.resultSavedDirectory + shape + @"\intermediate"}");
                    sw.WriteLine("csp_segmentcrown \"" + configpath + "\"");
                    sw.WriteLine();
                    sw.WriteLine("set destination=\"{0}\"", destination);
                    sw.WriteLine();
                    sw.WriteLine("for /r %%i in (*{0}*.pcd) do (", tree_name);
                    sw.WriteLine("    move \"%%i\" \"%destination%\"");
                    sw.WriteLine(")");
                }
                ProcessBatch(nine + originLasName + ".bat");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + nine + originLasName + ".bat 파일을 생성했습니다.");
            }

        }
        private void RunFileTenth()
        {
            string ten = "level10_measure_DBH_treeHeight_";
            //10단계 산림정보 속성 계산
            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + ten + originLasName + ".bat";
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
                    sw.WriteLine("measure " + @"../tree" + " \"" + configpath + "\"");
                }
                ProcessBatch(ten + originLasName + ".bat");
                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + ten + originLasName + ".bat 파일을 생성했습니다.");
            }
        }

        private void RunFileEleventh()
        {
            string eleven = "level11_PCDtoLAS";
            {
                string batFilePath = resultSavedDirectory + shape + @"\intermediate\" + eleven + originLasName + ".bat";
                if (!File.Exists(batFilePath))
                {
                    using FileStream fs = File.Create(batFilePath);
                }
                string FolderName = ".\\";
                string FolderName2 = "..\\tree";
                using (StreamWriter sw = new StreamWriter(new FileStream(batFilePath, FileMode.OpenOrCreate), Encoding.Default))
                {
                    sw.WriteLine("@ECHO OFF");
                    sw.WriteLine("chcp 65001 > nul");
                    sw.WriteLine("cls");
                    sw.WriteLine("echo pcd파일 las파일로 변환중");
                    sw.WriteLine($"cd {this.resultSavedDirectory + shape + @"\intermediate"}");
                    //sw.WriteLine("PCD2LAS " + FolderName);
                    sw.WriteLine("PCD2LAS " + FolderName2);

                    //pcd 지우는 코드  ---> 배포 시 주석 풀기
                    sw.WriteLine("for /r \"..\\tree\" %%i in (*.pcd) do (");
                    sw.WriteLine("    del \"%%i\"");
                    sw.WriteLine(")");

                    //sw.WriteLine("for /r \".\" %%i in (*.pcd) do (");
                    //sw.WriteLine("    del \"%%i\"");
                    //sw.WriteLine(")");


                    //intermediate 폴더 숨김처리 코드
                    //sw.WriteLine("attrib +h ../intermediate");
                }

                ProcessBatch(eleven + originLasName + ".bat");
                /*try
                {
                    ChangeLasName();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("10단계 오류 : " + ex.ToString());
                }*/

                MakeInfoFile();

                string FilePath = resultSavedDirectory + shape + @"\Log\Log_" + DateTime.Today.ToString("yyyyMMdd") + ".log";
                try
                {
                    write_config(FilePath);
                }
                catch (Exception e) { MessageBox.Show(e.ToString()); }

                LogWrite(resultSavedDirectory + shape + @"\intermediate\" + eleven + originLasName + ".bat 파일을 생성했습니다.");
            }
        }


        //LAS파일 이름 변경 코드
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

                //Subsampling();
                //Merge();
                Outlier();
                Normalization();
                MakeSliceFile();
                //MakeOriginPlot();
                Turn_Las_into_PCD();                
                progress++;
                mainProgressSet(progress);
                //=====

                RunFileSecond();
                progress++;
                mainProgressSet(progress);

                RunFileThird();
                progress++;
                mainProgressSet(progress);

                RunFileFourth();
                progress++;
                mainProgressSet(progress);

                //RunFileFifth();
                progress++;
                mainProgressSet(progress);

                //RunFileSixth();
                ExtractOriginDat();
                progress++;
                mainProgressSet(progress);

                RunFileSeventh();
                progress++;
                mainProgressSet(progress);

                RunFileEighth();//trunk
                progress++;
                mainProgressSet(progress);
                if (CatchError(resultP + @"\intermediate\", 8)) return;

                RunFileNinth();//tree
                progress++;
                mainProgressSet(progress);
                if (CatchError(resultP + @"\tree\", 9)) return;

                RunFileTenth();//measure
                


                if (CatchError(resultP + @"\tree\", 10)) return;

                RunFileEleventh();
                progress++;
                mainProgressSet(progress);
                //del_inter();

                using (StreamWriter sw = File.CreateText(@"C:\testLog.txt"))
                {
                    if(!File.Exists(@"C:\testLog.txt"))
                    {
                        MessageBox.Show("txt파일 미생성");
                        return;
                    }
                    sw.Write(progressLog);
                }

                progressDialog.Close();
            }
            else
            {
                MessageBox.Show("1단계 산출물 에러");
            }
        }
        //파일 생성을 기준, 에러 확인 코드
        bool CatchError(string path, int level)
        {
            bool isError = true;
            string find;
            if (level == 8)
            {
                find = "_trunk";
            }
            else if (level == 9)
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
                LogWrite(level + "단계 파일을 가져오는 중에 오류가 발생했습니다 : " + ex.Message);
            }

            return isError;
        }
        //LAS 데이터 텍스트 박스 change 이벤트 처리 코드
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

        private void write_config(string path)
        {
            string filePath = path;

            // CSV 내용 생성
            StringBuilder csvContent = new StringBuilder();

            for (int i = 0; i < paramForm.csv_data.Count; i++)
            {
                string str = "";
                for (int j = 0; j < 4; j++)
                {
                    if (j == 1)
                        continue;
                    if(j==3)
                    {
                        str += paramForm.csv_data[i][j];
                        break;
                    }
                    str += paramForm.csv_data[i][j] + ",";
                }
                csvContent.AppendLine(str);
            }

            // CSV 파일 생성 및 내용 기록
            try
            {
                File.AppendAllText(filePath, csvContent.ToString(), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSV 파일 작성 중 오류 발생: " + ex.Message);
            }
        }

        //일단 대기
        private bool IsLasSizeValid()
        {
            if (lasSize.minx == 0 && lasSize.miny == 0 && lasSize.maxx == 0 && lasSize.maxy == 0)
            {
                return false;
            }
            else if (lasSize.miny == lasSize.maxy || lasSize.minx == lasSize.maxx)
            {
                MessageBox.Show("Las파일의 사이즈가 적합하지 않습니다." +
                    $"Lasfile Size\nXmin={lasSize.minx}\nXmax={lasSize.maxx}\n" +
                    $"Ymin={lasSize.miny}\nYmax={lasSize.maxy}",
                    "Las File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void RemoveDuplicatedPoints(string filename)
        {
            string dupRemovedfile = Path.GetFileNameWithoutExtension(filename.Insert(filename.Length - 4, "_XYZ"));
            //MessageBox.Show(filename);
            //MessageBox.Show(dupRemovedfile);

            String FolderName = originLasDirectory;
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(FolderName);
            foreach (System.IO.FileInfo fi in di.GetFiles())
            {
                if (Path.GetFileNameWithoutExtension(fi.Name) == dupRemovedfile)
                {
                    originLasName = Path.GetFileNameWithoutExtension(fi.Name);
                    //MessageBox.Show(originLasName);
                    return;
                }
            }
            RunFileZero(filename);
            originLasName = dupRemovedfile;
        }

    }
}
