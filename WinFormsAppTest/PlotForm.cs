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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WinFormsAppTest
{
    public partial class PlotForm : Form
    {
        MainForm paramForm;
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
        }

        private void PlotForm_Load(object sender, EventArgs e)
        {
            initTextBox();
            resultPath = paramForm.gui.resultPath;
            Point screenSize = ((Point)Screen.PrimaryScreen.Bounds.Size);


            this.Location = new Point((screenSize.X - this.Width) / 2, (screenSize.Y - this.Height) / 2);
        }

        private void btnPlotCancle_Click(object sender, EventArgs e)
        {
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
    }
}
