﻿using System;
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
using static WinFormsAppTest.MainForm;
using Newtonsoft.Json.Linq;
using System.Security.Policy;

namespace WinFormsAppTest
{
    public partial class PlotForm : Form
    {
        MainForm paramForm;

        internal configHandler configTouch;

        internal customEventHandler mainPaint;

        internal setterEventHandler mainProgressSet;
        internal switchEventHandler attachProgressBar;

        internal switchEventHandler attachStartBtn;

        public PlotForm(MainForm paramForm)
        {
            InitializeComponent();
            pnPlotSelection.isFill = true;
            pnPlotSelection.isBorder = false;
            pnPlotSelection.borderColor = Color.Honeydew;
            pnPlotData.isFill = true;
            pnPlotData.isBorder = false;
            pnPlotData.borderColor = Color.Honeydew;

            cbPlotShape.SelectedIndex = 0;
            this.paramForm = paramForm;
            paramForm.plotSender += new plotDataHandler(plotPacker);
        }

        //수정 요망
        private Dictionary<string, double> plotPacker()
        {
            Dictionary<string, double> plots = new Dictionary<string, double>();
            plots.Add("selection", cbPlotShape.SelectedIndex);

            plots.Add("cx", paramForm.gui.centerX);
            plots.Add("cy", paramForm.gui.centerY);
            plots.Add("radius", paramForm.gui.radius);

            plots.Add("xmin", paramForm.gui.xMin);
            plots.Add("ymin", paramForm.gui.yMin);
            plots.Add("xmax", paramForm.gui.xMax);
            plots.Add("ymax", paramForm.gui.yMax);

            return plots;
        }

        private void PlotForm_Load(object sender, EventArgs e)
        {
            initTextBox();
            resultPath = paramForm.gui.resultPath;

            ////////////////////////////////////////////////////////////////
            attachStartBtn(false);

            Point screenSize = ((Point)Screen.PrimaryScreen.Bounds.Size);

            this.Location = new Point((screenSize.X - this.Width) / 2, (screenSize.Y - this.Height) / 2);

            cbPlotShape.SelectedIndex = 0;
        }

        private void btnPlotCancel_Click(object sender, EventArgs e)
        {
            attachProgressBar(false);
            attachStartBtn(true);
            this.Close();
        }

        //원본 Las 파일 찾기 버튼
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

            validation(filePath);
        }

        /// 다각형 꼭짓점 정보 파일 읽기 버튼
        /// 해당 텍스트 파일을 읽어서 x,y값을 gui 구조체의 pointList에 저장
        private void c_btnPlotPolySet_Click(object sender, EventArgs e)
        {
            String filePath = string.Empty;

            //포인트리스트
            List<string[]> tempPointList = new List<string[]>();

            //파일 읽기 윈도우 실행
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                string pointList = "";
                //파일 변수 저장
                openFileDialog.InitialDirectory = @"..\LIDAR_PROJECT";
                openFileDialog.Filter = "txt files (*.txt)|*.txt";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                //파일 윈도우에서 OK버튼 클릭시 발생 이벤트 작성
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //파일변수명 저장
                    filePath = openFileDialog.FileName;
                    tbPlotPolySet.Text = Path.GetFileName(filePath);

                    //입력 스트림 생성
                    using (StreamReader sr = new StreamReader(filePath))
                    {

                        //포인트 리스트 파일 읽기
                        while (!sr.EndOfStream)
                        {
                            //파일 입력 변수
                            string inputPolygonPoints = sr.ReadLine();

                            //',' 기준으로 파싱하여 문자열배열리스트에 저장
                            tempPointList.Add(inputPolygonPoints.Split(','));
                        }
                    }

                    point[] points = new point[tempPointList.Count];
                    //frm 구조체에 포인트 리스트 변수 저장
                    for (int i = 0; i < tempPointList.Count; i++)
                    {
                        points[i].x = double.Parse(tempPointList[i][0]);
                        points[i].y = double.Parse(tempPointList[i][1]);
                        for (int j = 1; j < tempPointList[i].Length; j++)
                        {
                            pointList += (tempPointList[i][j - 1] + " "); ;
                        }
                        pointList += ", ";
                    }


                    FindExtremeCoordinates(points);

                    //frm 구조체에 시작점 저장
                    pointList += tempPointList[0][0] + " ";
                    pointList += tempPointList[0][1];
                    //=============================================
                    //MessageBox.Show(pointList);
                    paramForm.gui.pointList = pointList;
                }//if문
            }
        }
        /// 전체 과정 실행 버튼
        private void btnPlotOK_Click(object sender, EventArgs e)
        {
            ///////////////////////////////////////////////////////////////////////
            attachProgressBar(true);
            //최근 작업 config 생성
            string fileDi = Path.Combine(configPath, reqDi[(int)configFileType.Recent]);

            if (!Directory.Exists(fileDi))
            {
                Directory.CreateDirectory(fileDi);
            }
            string[] confCheck = Directory.GetFiles(fileDi, "recentConfig*");
            configTouch(configFileType.Recent);
            mainPaint();


            //무결성 검사
            bool isEmptyVal_cir = tbPlotCircleX.Text == "" && tbPlotCircleY.Text == "" && tbPlotCircleR.Text == "";//원형 표준지에 필요한 값들이 비어있는경우
            bool isRadiusZero = (Double.Parse(tbPlotCircleR.Text) <= 0);//radius값이 0인지 확인
            bool isEmptyVal_rec = tbPlotRecXmin.Text == "" && tbPlotRecXmax.Text == "" && tbPlotRecYmin.Text == "" && tbPlotRecYmax.Text == "";//사각형 표준지에 필요한 값들이 비어있는경우
            bool hasEqualXY = (Double.Parse(tbPlotRecXmax.Text) <= Double.Parse(tbPlotRecXmin.Text)) || (Double.Parse(tbPlotRecYmax.Text) <= Double.Parse(tbPlotRecYmin.Text));//LT의 x, RB의 x || LT의 y, RB의 y값이 같은지 확인

            if (cbPlotShape.SelectedIndex == 0 && (isEmptyVal_cir || isRadiusZero))
            {
                MessageBox.Show("Circle Shape 필수 입력 값 오류.");
                return;
            }
            else if (cbPlotShape.SelectedIndex == 2 && tbPlotPolySet.Text == "")
            {
                MessageBox.Show("Polygon Shape 필수 입력 값 오류.");
                return;
            }
            else if (cbPlotShape.SelectedIndex == 1 && (isEmptyVal_rec || hasEqualXY))
            {
                MessageBox.Show("Rectangle Shape 필수 입력 값 오류.");
                return;
            }
            else if (cbPlotShape.SelectedIndex >= 3)
            {
                MessageBox.Show("표준지 모양을 선택해주세요.");
                return;
            }


            //콤보 박스의 선택된 인덱스에 따라 표준지 모양 전역변수 초기화
            if (cbPlotShape.SelectedIndex == 0)
            {
                shape = "_circle";
            }
            else if (cbPlotShape.SelectedIndex == 1)
            {
                shape = "_rectangle";
            }
            else
            {
                shape = "_polygon";
            }


            //MessageBox.Show(resultPath);
            resultSavedDirectory = resultPath + @"\" + DateTime.Now.ToString("yyyyMMdd_HH_mm_") + originLasName;
            //MessageBox.Show(resultSavedDirectory);

            progress = 0;
            mainProgressSet(progress);
            attachProgressBar(true);

            //csv 초기화

            preProAndExcuteStep();


            paramForm.write_csv(paramForm.csv_path);

            if (progress == 10)
            {
                MessageBox.Show("실행 완료");
            }
            else
            {
                MessageBox.Show(progress + "단계 에러");
            }
        }


        //아래 두 메서드 PlotForm 마우스 드래그 시 윈폼 이동 이벤트
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

        //PlotForm 콤보박스 이벤트
        private void cbPlotShape_SelectedIndexChanged(object sender, EventArgs e)
        {
            tcPlot.SelectedIndex = cbPlotShape.SelectedIndex;
        }
        //텍스트 박스 무결성 체크
        private void tbPlotCircleX_Leave(object sender, EventArgs e)
        {
            int dummy1 = 1;
            double dummy2 = 0;
            if (int.TryParse(tbPlotCircleX.Text, out dummy1))
            {
                tbPlotCircleX.Text = dummy1.ToString() + ".0";
            }
            else if (!double.TryParse(tbPlotCircleX.Text, out dummy2))
            {
                MessageBox.Show("실수형 숫자만 입력할 수 있습니다.");
                tbPlotCircleX.Clear();
                this.ActiveControl = tbPlotCircleX;
                return;
            }
            paramForm.gui.centerX = double.Parse(tbPlotCircleX.Text);
        }
        private void tbPlotCircleY_Leave(object sender, EventArgs e)
        {
            int dummy1 = 1;
            double dummy2 = 0;
            if (int.TryParse(tbPlotCircleY.Text, out dummy1))
            {
                tbPlotCircleY.Text = dummy1.ToString() + ".0";
            }
            else if (!double.TryParse(tbPlotCircleY.Text, out dummy2))
            {
                MessageBox.Show("실수형 숫자만 입력할 수 있습니다.");
                tbPlotCircleY.Clear();
                this.ActiveControl = tbPlotCircleY;
                return;
            }
            paramForm.gui.centerY = double.Parse(tbPlotCircleY.Text);
        }
        private void tbPlotCircleR_Leave(object sender, EventArgs e)
        {
            int dummy1 = 1;
            double dummy2 = 0;
            if (int.TryParse(tbPlotCircleR.Text, out dummy1))
            {
                tbPlotCircleR.Text = dummy1.ToString() + ".0";
            }
            else if (!double.TryParse(tbPlotCircleR.Text, out dummy2))
            {
                MessageBox.Show("실수형 숫자만 입력할 수 있습니다.");
                tbPlotCircleR.Clear();
                this.ActiveControl = tbPlotCircleR;
                return;
            }
            paramForm.gui.radius = double.Parse(tbPlotCircleR.Text);
        }
        private void tbPlotRecXmin_Leave(object sender, EventArgs e)
        {
            int dummy1 = 1;
            double dummy2 = 0;
            if (int.TryParse(tbPlotRecXmin.Text, out dummy1))
            {
                tbPlotRecXmin.Text = dummy1.ToString() + ".0";
            }
            else if (!double.TryParse(tbPlotRecXmin.Text, out dummy2))
            {
                MessageBox.Show("실수형 숫자만 입력할 수 있습니다.");
                tbPlotRecXmin.Clear();
                this.ActiveControl = tbPlotRecXmin;
                return;
            }
            paramForm.gui.xMin = double.Parse(tbPlotRecXmin.Text);
        }
        private void tbPlotRecXmax_Leave(object sender, EventArgs e)
        {
            int dummy1 = 1;
            double dummy2 = 0;
            if (int.TryParse(tbPlotRecXmax.Text, out dummy1))
            {
                tbPlotRecXmax.Text = dummy1.ToString() + ".0";
            }
            else if (!double.TryParse(tbPlotRecXmax.Text, out dummy2))
            {
                MessageBox.Show("실수형 숫자만 입력할 수 있습니다.");
                tbPlotRecXmax.Clear();
                this.ActiveControl = tbPlotRecXmax;
                return;
            }
            paramForm.gui.xMax = double.Parse(tbPlotRecXmax.Text);
        }
        private void tbPlotRecYmin_Leave(object sender, EventArgs e)
        {
            int dummy1 = 1;
            double dummy2 = 0;
            if (int.TryParse(tbPlotRecYmin.Text, out dummy1))
            {
                tbPlotRecYmin.Text = dummy1.ToString() + ".0";
            }
            else if (!double.TryParse(tbPlotRecYmin.Text, out dummy2))
            {
                MessageBox.Show("실수형 숫자만 입력할 수 있습니다.");
                tbPlotRecYmin.Clear();
                this.ActiveControl = tbPlotRecYmin;
                return;
            }
            paramForm.gui.yMin = double.Parse(tbPlotRecYmin.Text);
        }
        private void tbPlotRecYmax_Leave(object sender, EventArgs e)
        {
            int dummy1 = 1;
            double dummy2 = 0;
            if (int.TryParse(tbPlotRecYmax.Text, out dummy1))
            {
                tbPlotRecYmax.Text = dummy1.ToString() + ".0";
            }
            else if (!double.TryParse(tbPlotRecYmax.Text, out dummy2))
            {
                MessageBox.Show("실수형 숫자만 입력할 수 있습니다.");
                tbPlotRecYmax.Clear();
                this.ActiveControl = tbPlotRecYmax;
                return;
            }
            paramForm.gui.yMax = double.Parse(tbPlotRecYmax.Text);
        }
        //텍스트박스 초기화
        private void initTextBox()
        {
            tbPlotCircleX.Text = paramForm.gui.centerX.ToString();
            tbPlotCircleY.Text = paramForm.gui.centerY.ToString();
            tbPlotCircleR.Text = paramForm.gui.radius.ToString();

            tbPlotRecXmin.Text = paramForm.gui.xMin.ToString();
            tbPlotRecXmax.Text = paramForm.gui.xMax.ToString();
            tbPlotRecYmin.Text = paramForm.gui.yMin.ToString();
            tbPlotRecYmax.Text = paramForm.gui.yMax.ToString();
        }

        //사용자가 입력한 좌표값이 올바른지 체크하기 위한 함수
        private void validation(string filepath)
        {
            string infoDir = @"./LAS_info";
            string filename = Path.GetFileNameWithoutExtension(filepath);
            if (!Directory.Exists(infoDir))
            {
                Directory.CreateDirectory(infoDir);
            }
            try
            {
                // Create the target folder if it doesn't exist

                string dat_filePath = Path.Combine(infoDir, filename + ".dat");
                if (!File.Exists(dat_filePath))
                {
                    MakeInfo(filepath, infoDir);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reorganizing folder: {ex.Message}");
            }

        }
        private void MakeInfo(string filepath, string dirpath)
        {
            string filename = Path.GetFileNameWithoutExtension(filepath);
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = false
            };
            using (Process process = new Process { StartInfo = psi })
            {
                process.Start();
                string str = "Extracting information from the first run target file...";

                process.StandardInput.WriteLine(@"echo OFF");
                process.StandardInput.WriteLine($@"@echo {str} & pdal info {filepath} > {Path.Combine(dirpath, filename)}.json");
                process.StandardInput.WriteLine("exit");
                process.WaitForExit();
            }

            FileInfo fileInfo1 = new FileInfo(Path.Combine(dirpath, $"{filename}.json"));
            if (fileInfo1.Exists)
            {
                try
                {
                    string JsonText1 = System.IO.File.ReadAllText(fileInfo1.FullName);
                    JObject JsonData1 = JObject.Parse(JsonText1);
                    JToken minx = JsonData1["stats"]["bbox"]["native"]["bbox"]["minx"];
                    JToken maxx = JsonData1["stats"]["bbox"]["native"]["bbox"]["maxx"];
                    JToken miny = JsonData1["stats"]["bbox"]["native"]["bbox"]["miny"];
                    JToken maxy = JsonData1["stats"]["bbox"]["native"]["bbox"]["maxy"];

                    // Create the .dat file in the specified directory
                    string datFilePath = Path.Combine(dirpath, $"{filename}.dat");
                    File.WriteAllText(datFilePath, minx + " " + maxx + " " + miny + " " + maxy);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{filename}.dat 파일 생성 오류.");
                    return;
                }
            }
            else
            {
                MessageBox.Show($"{filename}.json 파일을 찾을 수 없음");
            }
        }
    }
}
