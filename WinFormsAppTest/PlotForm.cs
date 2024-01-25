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
using System.Xml.Linq;
using static WinFormsAppTest.MainForm;
using Newtonsoft.Json.Linq;
using System.Security.Policy;
using System.Reflection;

namespace WinFormsAppTest
{
    public partial class PlotForm : Form
    {
        MainForm paramForm;

        internal switchEventHandler enableMainFormBtns;
        internal switchEventHandler attachStartBtn;

        public PlotForm(MainForm paramForm)
        {
            InitializeComponent();

            SetPlotFormComponent();

            this.paramForm = paramForm;

            lasSizeInit();
        }
        private void PlotForm_Load(object sender, EventArgs e)
        {
            //각종 DB 연결을 위한 파라메터 전처리
            bin_folder = paramForm.bin_folder;
            databaseFileName = paramForm.databaseFileName;

            //DB에서 GUI 구조체로 미리 담아둔 plot값을 textBox로 읽어옴
            initTextBox();
            //결과 폴더 경로 설정
            resultPath = paramForm.SelectDataFromTable(databaseFileName, "gui", "result_path");

            this.KeyPreview = true;

            //메인폼의 start 버튼 감추기(중복 실행 방지)
            attachStartBtn(false);

            cbPlotShape.SelectedIndex = 0;
        }
        //plotForm 컴포넌트들 세팅
        private void SetPlotFormComponent()
        {
            pnPlotSelection.isBorder = false;
            pnPlotSelection.borderColor = Color.White;
            pnPlotData.isBorder = false;
            pnPlotData.borderColor = Color.White;
            cbPlotShape.SelectedIndex = 0;
        }
        private void btnPlotCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //원본 Las 파일 찾기 버튼
        private void btnPlotData_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = @"..\LIDAR_PROJECT";
                openFileDialog.Filter = "las files (*.las)|*.las";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;

                    if (Path.GetExtension(filePath) != ".las")
                    {
                        MessageBox.Show("las 확장자 파일만 불러와주세요.");
                        return;
                    }

                    tbPlotData.Text = filePath;
                    Validation(filePath);
                }
            }
        }
        /// 다각형 꼭짓점 정보 파일 읽기 버튼
        /// 해당 텍스트 파일을 읽어서 x,y값을 gui 구조체의 pointList에 저장
        private void btnPlotPolySet_Click(object sender, EventArgs e)
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

                        //상대좌표
                        //double x= double.Parse(tempPointList[i][0]) - double.Parse(paramForm.SelectDataFromTable(databaseFileName, "gui", "org_xmin"));
                        //double y = double.Parse(tempPointList[i][0]) - double.Parse(paramForm.SelectDataFromTable(databaseFileName, "gui", "org_ymin"));
                        //poly_points += x.ToString() + " " + y.ToString() + " ";

                        poly_points += tempPointList[i][0] + " " + tempPointList[i][1] + " ";


                        for (int j = 1; j < tempPointList[i].Length; j++)
                        {
                            pointList += (tempPointList[i][j - 1] + " ");
                        }
                        pointList += ", ";
                    }


                    FindExtremeCoordinates(points);

                    //frm 구조체에 시작점 저장
                    pointList += tempPointList[0][0] + " ";
                    pointList += tempPointList[0][1];
                    //=============================================
                    paramForm.gui.pointList = pointList;
                }//if문
            }
        }
        /// 전체 과정 실행 버튼
        private async void btnPlotOK_Click(object sender, EventArgs e)
        {
            progress = 0;

            paramForm.UpdateDataInTable(databaseFileName, "gui", "origin_las_file", tbPlotData.Text);

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

            //구조체 변수 업데이트
            paramForm.gui.centerX = double.Parse(tbPlotCircleX.Text);
            paramForm.gui.centerY = double.Parse(tbPlotCircleY.Text);
            paramForm.gui.radius = double.Parse(tbPlotCircleR.Text);
            paramForm.gui.xMin = double.Parse(tbPlotRecXmin.Text);
            paramForm.gui.xMax = double.Parse(tbPlotRecXmax.Text);
            paramForm.gui.yMin = double.Parse(tbPlotRecYmin.Text);
            paramForm.gui.yMax = double.Parse(tbPlotRecYmax.Text);

            string resultOriginLas = (originLasName.Length > 20 ? originLasName.Substring(0, 20) : originLasName).Replace(' ', '_');
            resultSavedDirectory = resultPath + @"\" + DateTime.Now.ToString("MMdd_HHmmss_") + resultOriginLas;

            ProgressDialog_Create();

            btnPlotData.Enabled = false;

            //winform을 실행하는 스레드와 다른 스레드에서 본 기능을 실행하기 위함(나름의 자원 분산)
            await Task.Run(() =>
            {
                //각 단계 실행
                preProAndExcuteStep();

                if (progress == 10)
                {
                    MessageBox.Show("작업 완료");
                    //progressDialog.Close();
                }
                else
                {
                    MessageBox.Show(progress + " 단계 에러");
                }
            });

            btnPlotData.Enabled = true;
        }
        private void ProgressDialog_Create()
        {
            if (progressDialog != null)
            {
                return;
            }

            progressDialog = new Form
            {
                Width = 600,
                Height = 400,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
                Name = "progressDialog",
                Owner = paramForm
            };

            progressTextBox = new TextBox();
            progressTextBox.Width = 550;
            progressTextBox.Height = 280;
            progressTextBox.ReadOnly = true;
            progressTextBox.Multiline = true;
            progressTextBox.Location = new Point(17, 70);
            progressTextBox.ScrollBars = ScrollBars.Vertical;
            progressTextBox.BackColor = Color.White;
            progressTextBox.Font = new Font("맑은 고딕", 14F, FontStyle.Regular, GraphicsUnit.Point);
            progressTextBox.BorderStyle = BorderStyle.FixedSingle;

            pbLoadingBar = new ProgressBar();
            pbLoadingBar.Location = new Point(17, 20);
            pbLoadingBar.MarqueeAnimationSpeed = 5;
            pbLoadingBar.Maximum = 10;
            pbLoadingBar.Name = "pbLoadingBar";
            pbLoadingBar.Size = new Size(550, 30);

            progressDialog.MaximizeBox = false;
            progressDialog.FormClosing += FormDisposing;
            progressDialog.Controls.Add(pbLoadingBar);
            progressDialog.Controls.Add(progressTextBox);
            progressDialog.Show();
        }
        private void FormDisposing(object sender, FormClosingEventArgs e)
        {
            progressDialog = null;
            Process proc;


            while (batchPidList.Count > 0)
            {
                try
                {
                    proc = Process.GetProcessById(batchPidList[0]);

                    if (!proc.HasExited)
                    {
                        proc.Kill();
                        bool IsExited = proc.WaitForExit(1000);
                    }

                }
                catch (Exception ex)
                {
                    //이미 죽은 프로세스이므로 무시
                }

                batchPidList.RemoveAt(0);
            }
        }
        private void ProgressBarSet(int level)
        {
            if (pbLoadingBar.InvokeRequired)
            {
                pbLoadingBar.Invoke(new Action(() =>
                {
                    pbLoadingBar.Value = level;
                    pbLoadingBar.Invalidate();
                }));
            }
            else
            {
                pbLoadingBar.Value = level;
                pbLoadingBar.Invalidate();
            }
        }
        //PlotForm 콤보박스 이벤트
        private void cbPlotShape_SelectedIndexChanged(object sender, EventArgs e)
        {
            tcPlot.SelectedIndex = cbPlotShape.SelectedIndex;
            cbPlotShape.Focus();
        }
        //텍스트 박스 무결성 체크 및 las파일 크기 placeholder 처리
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
            double cx = double.Parse(tbPlotCircleX.Text);
            if (tbPlotData.Text != "" && cx < lasSize.minx)
            {
                MessageBox.Show($"선택하신 las 파일의 최소 x좌표값보다 작습니다.\n{lasSize.minx}보다 크게 설정해주세요");
                tbPlotCircleX.Clear();
                tbPlotCircleX.Text = lasSize.minx.ToString();
                this.ActiveControl = tbPlotCircleX;
                return;
            }
            if (tbPlotData.Text != "" && cx > lasSize.maxx)
            {
                MessageBox.Show($"선택하신 las 파일의 최대 x좌표값보다 큽니다.\n{lasSize.maxx}보다 작게 설정해주세요");
                tbPlotCircleX.Clear();
                tbPlotCircleX.Text = lasSize.maxx.ToString();
                this.ActiveControl = tbPlotCircleX;
                return;
            }
            paramForm.gui.centerX = cx;
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
            double cy = double.Parse(tbPlotCircleY.Text);
            if (tbPlotData.Text != "" && cy < lasSize.miny)
            {
                MessageBox.Show($"선택하신 las 파일의 최소 y좌표값보다 작습니다.\n{lasSize.miny} 이상으로 설정해주세요");
                tbPlotCircleY.Clear();
                tbPlotCircleY.Text = lasSize.miny.ToString();
                this.ActiveControl = tbPlotCircleY;
                return;
            }
            if (tbPlotData.Text != "" && cy > lasSize.maxy)
            {
                MessageBox.Show($"선택하신 las 파일의 최대 y좌표값보다 큽니다.\n{lasSize.maxy} 이하로 설정해주세요");
                tbPlotCircleY.Clear();
                tbPlotCircleY.Text = lasSize.maxy.ToString();
                this.ActiveControl = tbPlotCircleY;
                return;
            }

            paramForm.gui.centerY = cy;
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
            if (tbPlotData.Text != "" && double.Parse(tbPlotRecXmin.Text) < lasSize.minx)
            {
                MessageBox.Show($"선택하신 las 파일의 최소 x좌표값보다 작습니다.{lasSize.minx} 이상으로 설정해주세요");
                tbPlotRecXmin.Clear();
                tbPlotRecXmin.Text = lasSize.minx.ToString();
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
            if (tbPlotData.Text != "" && double.Parse(tbPlotRecXmax.Text) > lasSize.maxx)
            {
                MessageBox.Show($"선택하신 las 파일의 최대 x좌표값보다 큽니다.{lasSize.maxx} 이하로 설정해주세요");
                tbPlotRecXmax.Clear();
                tbPlotRecXmax.Text = lasSize.maxx.ToString();
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
            if (tbPlotData.Text != "" && double.Parse(tbPlotRecYmin.Text) < lasSize.miny)
            {
                MessageBox.Show($"선택하신 las 파일의 최소 y좌표값보다 작습니다.\n{lasSize.miny} 이상으로 설정해주세요");
                tbPlotRecYmin.Clear();
                tbPlotRecYmin.Text = lasSize.miny.ToString();
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
            if (tbPlotData.Text != "" && double.Parse(tbPlotRecYmax.Text) > lasSize.maxy)
            {
                MessageBox.Show($"선택하신 las 파일의 최대 y좌표값보다 큽니다.\n{lasSize.maxy} 이하로 설정해주세요");
                tbPlotRecYmax.Clear();
                tbPlotRecYmax.Text = lasSize.maxy.ToString();
                this.ActiveControl = tbPlotRecYmax;
                return;
            }
            paramForm.gui.yMax = double.Parse(tbPlotRecYmax.Text);
        }

        //Gui 구조체 -> plot값 텍스트박스 초기화
        private void initTextBox()
        {
            tbPlotCircleX.Text = paramForm.gui.centerX.ToString();
            tbPlotCircleY.Text = paramForm.gui.centerY.ToString();
            tbPlotCircleR.Text = paramForm.gui.radius.ToString();

            tbPlotRecXmin.Text = paramForm.gui.xMin.ToString();
            tbPlotRecXmax.Text = paramForm.gui.xMax.ToString();
            tbPlotRecYmin.Text = paramForm.gui.yMin.ToString();
            tbPlotRecYmax.Text = paramForm.gui.yMax.ToString();

            tbPlotData.Text = paramForm.gui.loadPath;
        }
        //사용자가 입력한 좌표값이 올바른지 체크하기 위한 함수들
        private async void Validation(string filePath)
        {
            string infoDir = Path.Combine(basePath, "LAS_info");
            string fileName = Path.GetFileNameWithoutExtension(filePath);

            if (!Directory.Exists(infoDir))
            {
                Directory.CreateDirectory(infoDir);
            }
            try
            {
                //Las파일에서 이미 뽑아놓은 Las파일 크기 정보가 있는 지 체크 후
                //정보를 읽거나, MakeInfo로 정보 만들고 읽기(만들어진 정보는 .dat파일)
                string dat_filePath = Path.Combine(infoDir, fileName + ".dat");
                if (!File.Exists(dat_filePath))
                {
                    //progressDialog -> 사용자를 위한 로딩창
                    var extractDialog = new Form
                    {
                        Width = 250,
                        Height = 250,
                        FormBorderStyle = FormBorderStyle.None,
                        TransparencyKey = Color.Transparent,
                        BackColor = TransparencyKey,
                        StartPosition = FormStartPosition.CenterScreen,
                        ShowInTaskbar = false,
                        Owner = this
                    };

                    PictureBox gifBox = new PictureBox
                    {
                        Dock = DockStyle.Fill,
                        Image = Image.FromFile(Environment.CurrentDirectory.ToString() + @"\LoadingPx.gif"),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        BackColor = TransparencyKey
                    };
                    if (Image.FromFile(Environment.CurrentDirectory.ToString() + @"\LoadingPx.gif") == null)
                    {
                        MessageBox.Show("LoadingPx.gif 파일이 없습니다.");
                        return;
                    }

                    extractDialog.Controls.Add(gifBox);
                    extractDialog.Show();

                    //Task를 이용한 이유 : MakeInfo와 progressDialog의 pictureBox gif가
                    //동시에 동작하게 하기 위함.(안쓰면 쓰레드 우선순위 문제로 gif 애니메이션이 안움직임)
                    btnPlotOK.Enabled = false;
                    await Task.Run(() => MakeInfo(filePath, infoDir));
                    btnPlotOK.Enabled = true;

                    extractDialog.Dispose();
                }
                //Las파일 크기 정보 읽기
                readInfo(filePath, infoDir);
                //MessageBox.Show(lasSize.minx + " " + lasSize.miny + " " + lasSize.maxx + " " + lasSize.maxy);

                //읽은 Las파일 크기가 정상적이지 않을 경우
                if (!IsLasSizeValid())
                {
                    MessageBox.Show("Las파일의 사이즈가 적합하지 않습니다." +
                    $"Lasfile Size\nXmin={lasSize.minx}\nXmax={lasSize.maxx}\n" +
                    $"Ymin={lasSize.miny}\nYmax={lasSize.maxy}\n다시 실행해주세요.",
                    "Las File Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    foreach (string fName in Directory.GetFiles(infoDir, $"{fileName}.*"))
                    {
                        File.Delete(fName);
                    }

                    tbPlotData.Text = "";

                    return;
                }

                /*
                tbPlotCircleX.Text = (Math.Truncate((lasSize.maxx + lasSize.minx) / 2 * 1000) / 1000).ToString();
                tbPlotCircleY.Text = (Math.Truncate((lasSize.maxy + lasSize.miny) / 2 * 1000) / 1000).ToString();

                tbPlotRecXmax.Text = (Math.Truncate(lasSize.maxx * 1000) / 1000).ToString();
                tbPlotRecXmin.Text = (Math.Truncate(lasSize.minx * 1000) / 1000).ToString();
                tbPlotRecYmax.Text = (Math.Truncate(lasSize.maxy * 1000) / 1000).ToString();
                tbPlotRecYmin.Text = (Math.Truncate(lasSize.miny * 1000) / 1000).ToString();
                */

                paramForm.gui.centerX = Math.Truncate((lasSize.maxx + lasSize.minx) / 2 * 1000) / 1000;
                paramForm.gui.centerY = Math.Truncate((lasSize.maxy + lasSize.miny) / 2 * 1000) / 1000;

                paramForm.gui.xMin = Math.Truncate(lasSize.minx * 1000) / 1000;
                paramForm.gui.xMax = Math.Truncate(lasSize.maxx * 1000) / 1000;
                paramForm.gui.yMin = Math.Truncate(lasSize.miny * 1000) / 1000;
                paramForm.gui.yMax = Math.Truncate(lasSize.maxy * 1000) / 1000;

                initTextBox();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reorganizing folder: {ex.Message}");
            }
        }
        //PDAL로 Las파일에서 정보를 뽑은 json을 만들고, 거기서 크기 정보만 빼서 .dat파일로 생성
        private async Task MakeInfo(string filePath, string dirPath)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);

            //프로세스 클래스로 pdal 명령어 실행
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (Process process = new Process { StartInfo = psi })
            {
                process.Start();
                //process.StandardInput.WriteLine($"echo {str}");
                //process.StandardInput.WriteLine("echo off");
                process.StandardInput.WriteLine($"pdal info \"{filePath}\" > \"{Path.Combine(dirPath, fileName)}.json\"");
                process.StandardInput.WriteLine("exit");
                process.WaitForExit();
            }

            //실행되어 추출된 파일을 읽고, 구조체에 넣는 과정
            FileInfo fileInfo1 = new FileInfo(Path.Combine(dirPath, $"{fileName}.json"));
            if (!fileInfo1.Exists)
            {
                MessageBox.Show($"{fileName}.json 파일을 찾을 수 없음");
                return;
            }

            try
            {
                //LasSize 구조체의 객체인 lasSize에 각 크기 정보 담기
                string jsonText = File.ReadAllText(fileInfo1.FullName);
                JToken jsonSizeTok = JObject.Parse(jsonText)["stats"]["bbox"]["native"]["bbox"];
                lasSize.minx = (double)jsonSizeTok["minx"];
                lasSize.maxx = (double)jsonSizeTok["maxx"];
                lasSize.miny = (double)jsonSizeTok["miny"];
                lasSize.maxy = (double)jsonSizeTok["maxy"];
                lasSize.minz = (double)jsonSizeTok["minz"];
                lasSize.maxz = (double)jsonSizeTok["maxz"];


                //lasSize에 담긴 정보로.dat파일 만들기
                string datFilePath = Path.Combine(dirPath, $"{fileName}.dat");
                string sizeText = "";
                //FieldInfo[] 배열에 구조체의 정보 중 필드에 관한 정보만 담고
                FieldInfo[] lasProp = typeof(LasSize).GetFields();

                //해당 필드의 이름과 맞는 lasSize에 담긴 정보를 "이름,정보" 형태로 한줄 씩 sizeText에 담아서
                foreach (FieldInfo fi in lasProp)
                {
                    sizeText += $"{fi.Name},{fi.GetValue(lasSize)}\n";
                }
                //datFilePath에 담긴 파일 경로명으로 저장한다.
                File.WriteAllText(datFilePath, sizeText);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{fileName}.dat 파일 생성 오류.");
                return;
            }
        }
        private void readInfo(string filePath, string dirPath)
        {
            string datPath = Path.Combine(dirPath, Path.GetFileNameWithoutExtension(filePath) + ".dat");
            try
            {
                string sizeLine = "";
                string[] sizeTok;
                using (StreamReader reader = new StreamReader(datPath))
                {
                    while ((sizeLine = reader.ReadLine()) != null)
                    {
                        sizeTok = sizeLine.Split(',');
                        switch (sizeTok[0])
                        {
                            case "maxx":
                                lasSize.maxx = double.Parse(sizeTok[1]);
                                break;
                            case "minx":
                                lasSize.minx = double.Parse(sizeTok[1]);
                                break;
                            case "maxy":
                                lasSize.maxy = double.Parse(sizeTok[1]);
                                break;
                            case "miny":
                                lasSize.miny = double.Parse(sizeTok[1]);
                                break;
                            case "minz":
                                lasSize.minz = double.Parse(sizeTok[1]);
                                break;
                            case "maxz":
                                lasSize.maxz = double.Parse(sizeTok[1]);
                                break;
                        }
                    }
                }
                paramForm.UpdateDataInTable(databaseFileName, "gui", "org_xmin", lasSize.minx.ToString());
                paramForm.UpdateDataInTable(databaseFileName, "gui", "org_ymin", lasSize.miny.ToString());
                paramForm.UpdateDataInTable(databaseFileName, "gui", "org_zmin", lasSize.minz.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show($"Las파일 사이즈 읽기 실패\n{e}", "사이즈 파일 읽기 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        //esc키로 창 닫기
        private void PlotForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void PlotForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            attachStartBtn(true);
            enableMainFormBtns(true);
            this.Dispose();
        }
        private async void CloudCompareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string lasPath = tbPlotData.Text;
            string exePath = FindProgramPath("CloudCompare.exe");

            if (exePath == "")
            {
                MessageBox.Show("CloudCompare 프로그램의 경로를 환경 변수(Path)에 추가해주세요");
                return;
            }

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = $"\"{exePath}\"",
                Arguments = $"\"{lasPath}\"",
                UseShellExecute = true,
                CreateNoWindow = true
            };

            await Task.Run(() =>
            {
                using (Process process = new Process { StartInfo = psi })
                {
                    process.Start();
                    process.WaitForExit();
                }
            });
        }
        private string FindProgramPath(string programName)
        {
            string paths = Environment.GetEnvironmentVariable("Path");
            foreach (string path in paths.Split(';'))
            {
                string programPath = Path.Combine(path, programName);
                if (File.Exists(programPath))
                {
                    return programPath;
                }
            }
            return "";
        }
        private void lasSizeInit()
        {
            lasSize.minx = paramForm.gui.xMin;
            lasSize.maxx = paramForm.gui.xMax;
            lasSize.miny = paramForm.gui.yMin;
            lasSize.maxy = paramForm.gui.yMax;
        }
    }
}
