using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;
using WinFormsAppTest.Properties;
using System.Dynamic;
using System.Text.RegularExpressions;
using static WinFormsAppTest.MainForm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace WinFormsAppTest
{
    //델리게이트 메서드들. private인 일반 메서드를 다른 form에서 쓸 수 있게하는 역할
    internal delegate Dictionary<string, double> plotDataHandler();

    internal delegate void customEventHandler();

    internal delegate void configHandler(configFileType type);

    internal delegate void setIntEventHandler(int setValue);

    internal delegate void setStringEventHandler(string setValue);

    internal delegate void switchEventHandler(bool onOff);

    internal delegate void presetReflectHandler(string fileDi, string fileName);

    public partial class MainForm : Form
    {
        //아래 enum 전까지 윈도우폼 최대화 제거 작업
        [DllImport("user32.dll")]
        private static extern bool DeleteMenu(IntPtr hMenu, uint uPosition, uint uFlags);
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        private const uint MF_BYCOMMAND = 0x00000000;
        private const uint SC_MAXIMIZE = 0xF030;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            IntPtr systemMenu = GetSystemMenu(this.Handle, false);
            DeleteMenu(systemMenu, SC_MAXIMIZE, MF_BYCOMMAND);
        }

        public enum configFileType
        {
            Default,
            Recent,
            Preset
        }

        //plot값을 PlotForm으로부터 받기 위한 delegate
        internal plotDataHandler plotSender;

        //다른 폼들을 띄우고 delegate를 통해 메서드 전달을 위한 form 변수
        private PlotForm? pFrm;
        private ManageForm? mFrm;

        //사이드 메뉴의 최대, 최소 폭 크기 및 그 차이
        const int MAX_SLIDING_WIDTH = 384;
        const int MIN_SLIDING_WIDTH = 65;
        const int SLIDING_GAP = MAX_SLIDING_WIDTH - MIN_SLIDING_WIDTH;

        //사이드 메뉴에 확장, 축소에 따른 메뉴 버튼 크기
        const int MIN_BTN_WIDTH = 42;
        const int MAX_BTN_WIDTH = 345;

        //사이드 메뉴에 생성될 첫 버튼의 위치 및 크기, 간격
        //동적 버튼 추가를 위해 필요
        Point PRESET_BTN_POS = new Point(8, 30);
        const int PRESET_BTN_HEIGHT = 45;
        const int PRESET_BTN_GAP = 2;

        //최근 작업 목록 첫 추가 위치 및 크기, 간격
        //동적 버튼 추가를 위해 필요
        Point RECENT_BTN_POS = new Point(22, 0);
        const int RECENT_BTN_WIDTH = 265;
        const int RECENT_BTN_HEIGHT = 225;
        const int RECENT_BTN_GAP = 30;

        bool menuOpen = false;

        Point relativeMformPos = new Point();

        bool isMformDrag = false;

        int flashCount = 0;

        public MainForm()
        {
            InitializeComponent();
            DoubleBuffered = true;

            customPanels_Load();

            pnMain.isBorder = false;

            pnSidePreset.isBorder = true;
            pnSidePreset.borderColor = Color.FromArgb(73, 109, 109);
            pnSidePreset.isFill = true;
            pnSidePreset.fillColor = Color.FromArgb(73, 109, 109);
            pnSidePreset.Visible = false;

            lbSlidePreset.Visible = false;
            btnPresetManage.Location = new Point(14, 183);
            btnPresetManage.Size = new Size(38, 34);

            lbPresetManage = new Label();
            lbPresetManage.Name = "lbPresetManage";
            lbPresetManage.ForeColor = Color.White;
            lbPresetManage.Location = new Point(-2, 218);
            lbPresetManage.TabIndex = 6;
            lbPresetManage.Text = "사용자 설정";
            pnSideMenu.Controls.Add(lbPresetManage);


            pnSideMenu.Width = MIN_SLIDING_WIDTH;
            tcMainHome.Left -= SLIDING_GAP / 2;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!File.Exists(Path.Combine(basePath, "config.csv")))
            {
                MessageBox.Show("config.csv파일이 없습니다. 프로그램 개발 측으로 문의해주세요.\n" +
                    $"filepath : {Environment.CurrentDirectory}", "기본 설정 파일 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            //기본 config.csv 파일 반영
            read_csv(csv_path, csv_data);
            FillTextboxes();
            RegistTextBoxHandler();

            //프로그램 창 생성 위치 지정
            Point screenSize = ((Point)Screen.PrimaryScreen.Bounds.Size);
            this.Location = new Point((screenSize.X - this.Width) / 2, (screenSize.Y - this.Height) / 2);

            //메인폼의 각 컴포넌트 이벤트 설정
            mainForm_AddEvent();

            //사용자 설정값과 최근 작업 기록 버튼 동적 생성
            preConfBtnLoad();
            recentConfBtnLoad();
        }

        //메인 폼 로드 전 이벤트 전처리(Designer.cs에 넣으면 찾기가 힘듬)
        private void mainForm_AddEvent()
        {
            //홈버튼 이벤트
            btnHome.Click += btnHome_Click;
            //설정버튼 이벤트
            btnSettings.Click += btnSettings_Click;
            //사이드 메뉴 접기/열기 버튼 이벤트
            btnSideMenu.Click += btnSlideMenu_Click;
            //프로그램 실행 버튼 이벤트
            btnStart.Click += btnStart_Click;

            //이 아래로 설정창의 CustomPanel 객체들 이벤트
            pnSettingDefault.MouseDown += pnSettingAll_MouseDown;
            pnSettingDefault.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingDefault.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingDefault.MouseUp += pnSettingAll_MouseUp;

            pnSettingPreset.MouseDown += pnSettingAll_MouseDown;
            pnSettingPreset.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingPreset.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingPreset.MouseUp += pnSettingAll_MouseUp;

            pnSettingSub1.MouseDown += pnSettingAll_MouseDown;
            pnSettingSub1.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingSub1.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingSub1.MouseUp += pnSettingAll_MouseUp;

            pnSettingNor1.MouseDown += pnSettingAll_MouseDown;
            pnSettingNor1.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingNor1.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingNor1.MouseUp += pnSettingAll_MouseUp;

            pnSettingNor2.MouseDown += pnSettingAll_MouseDown;
            pnSettingNor2.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingNor2.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingNor2.MouseUp += pnSettingAll_MouseUp;

            pnSettingNor3.MouseDown += pnSettingAll_MouseDown;
            pnSettingNor3.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingNor3.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingNor3.MouseUp += pnSettingAll_MouseUp;

            pnSettingNor4.MouseDown += pnSettingAll_MouseDown;
            pnSettingNor4.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingNor4.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingNor4.MouseUp += pnSettingAll_MouseUp;

            pnSettingNor5.MouseDown += pnSettingAll_MouseDown;
            pnSettingNor5.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingNor5.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingNor5.MouseUp += pnSettingAll_MouseUp;

            pnSettingTrunk1.MouseDown += pnSettingAll_MouseDown;
            pnSettingTrunk1.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingTrunk1.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingTrunk1.MouseUp += pnSettingAll_MouseUp;

            pnSettingTrunk2.MouseDown += pnSettingAll_MouseDown;
            pnSettingTrunk2.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingTrunk2.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingTrunk2.MouseUp += pnSettingAll_MouseUp;

            pnSettingTrunk3.MouseDown += pnSettingAll_MouseDown;
            pnSettingTrunk3.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingTrunk3.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingTrunk3.MouseUp += pnSettingAll_MouseUp;

            pnSettingCrown1.MouseDown += pnSettingAll_MouseDown;
            pnSettingCrown1.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingCrown1.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingCrown1.MouseUp += pnSettingAll_MouseUp;

            //이 밑으로 설정창 setting textBox 이벤트
            /*
            tbSubCellSize.TextChanged += tbSettingsAll_TextChanged;

            tbNorCellSize.TextChanged += tbSettingsAll_TextChanged;
            tbNorWinSize.TextChanged += tbSettingsAll_TextChanged;
            tbNorSlope.TextChanged += tbSettingsAll_TextChanged;
            tbNorScalar.TextChanged += tbSettingsAll_TextChanged;
            tbNorThres.TextChanged += tbSettingsAll_TextChanged;

            tbTrunkMinHeight.TextChanged += tbSettingsAll_TextChanged;
            tbTrunkMaxHeight.TextChanged += tbSettingsAll_TextChanged;
            tbTrunkSmooth.TextChanged += tbSettingsAll_TextChanged;

            tbCrownMinHeight.TextChanged += tbSettingsAll_TextChanged;
            */
        }

        //CustomPanel 색 및 테두리 지정(Designer.cs에서 지정하면 컴파일 시 없어짐)
        private void customPanels_Load()
        {
            Color customPanelColor = Color.Gray;

            pnSettingDefault.BackColor = Color.Transparent;
            pnSettingDefault.isFill = true;
            pnSettingDefault.isBorder = false;
            pnSettingDefault.fillColor = customPanelColor;

            pnSettingPreset.BackColor = Color.Transparent;
            pnSettingPreset.isFill = true;
            pnSettingPreset.isBorder = false;
            pnSettingPreset.fillColor = customPanelColor;

            pnSettingSub1.BackColor = Color.Transparent;
            pnSettingSub1.isFill = true;
            pnSettingSub1.isBorder = false;
            pnSettingSub1.fillColor = customPanelColor;

            pnSettingNor1.BackColor = Color.Transparent;
            pnSettingNor1.isFill = true;
            pnSettingNor1.isBorder = false;
            pnSettingNor1.fillColor = customPanelColor;

            pnSettingNor2.BackColor = Color.Transparent;
            pnSettingNor2.isFill = true;
            pnSettingNor2.isBorder = false;
            pnSettingNor2.fillColor = customPanelColor;

            pnSettingNor3.BackColor = Color.Transparent;
            pnSettingNor3.isFill = true;
            pnSettingNor3.isBorder = false;
            pnSettingNor3.fillColor = customPanelColor;

            pnSettingNor4.BackColor = Color.Transparent;
            pnSettingNor4.isFill = true;
            pnSettingNor4.isBorder = false;
            pnSettingNor4.fillColor = customPanelColor;

            pnSettingNor5.BackColor = Color.Transparent;
            pnSettingNor5.isFill = true;
            pnSettingNor5.isBorder = false;
            pnSettingNor5.fillColor = customPanelColor;

            pnSettingTrunk1.BackColor = Color.Transparent;
            pnSettingTrunk1.isFill = true;
            pnSettingTrunk1.isBorder = false;
            pnSettingTrunk1.fillColor = customPanelColor;

            pnSettingTrunk2.BackColor = Color.Transparent;
            pnSettingTrunk2.isFill = true;
            pnSettingTrunk2.isBorder = false;
            pnSettingTrunk2.fillColor = customPanelColor;

            pnSettingTrunk3.BackColor = Color.Transparent;
            pnSettingTrunk3.isFill = true;
            pnSettingTrunk3.isBorder = false;
            pnSettingTrunk3.fillColor = customPanelColor;

            pnSettingCrown1.BackColor = Color.Transparent;
            pnSettingCrown1.isFill = true;
            pnSettingCrown1.isBorder = false;
            pnSettingCrown1.fillColor = customPanelColor;
        }

        //프리셋 콘피그 파일 갯수만큼 버튼 로드
        private void preConfBtnLoad()
        {
            string fileDi = Path.Combine(basePath, reqDi[(int)configFileType.Preset]);

            if (!Directory.Exists(fileDi))
            {
                Directory.CreateDirectory(fileDi);
            }

            //프리셋 콘피그 저장 장소
            string[] confCheck = Directory.GetFiles(fileDi, "presetConfig*");

            //삽입될 버튼 위치
            Point relativeBtnPos = PRESET_BTN_POS;

            pnSidePreset.Controls.Clear();
            pnSidePreset.Controls.Add(lbSidePresetDate);
            pnSidePreset.Controls.Add(lbSidePresetTitle);

            if (confCheck.Length < 1)
            {
                CustomBtn btnPreConfs = new CustomBtn();
                btnPreConfs.Name = "btnPreDefault";
                btnPreConfs.Location = relativeBtnPos;
                btnPreConfs.Width = MAX_BTN_WIDTH;
                btnPreConfs.Height = PRESET_BTN_HEIGHT;

                btnPreConfs.FlatStyle = FlatStyle.Flat;
                btnPreConfs.FlatAppearance.BorderSize = 0;
                btnPreConfs.FlatAppearance.MouseDownBackColor = Color.FromArgb(112, 144, 144);
                btnPreConfs.FlatAppearance.MouseOverBackColor = Color.FromArgb(96, 128, 128);

                btnPreConfs.BorderRadius = 5;
                btnPreConfs.BorderColor = Color.FromArgb(128, 160, 160);
                btnPreConfs.BorderSize = 4;
                btnPreConfs.BackColor = Color.Transparent;

                btnPreConfs.ForeColor = Color.FromArgb(160, 192, 192);
                btnPreConfs.Font = new Font("나눔 고딕", 16F, FontStyle.Bold, GraphicsUnit.Point);

                btnPreConfs.TextAlign = ContentAlignment.MiddleLeft;


                btnPreConfs.Text = "00/00/00               설정명";

                btnPreConfs.Click += btnPreDefault_Click;

                pnSidePreset.Controls.Add(btnPreConfs);
                return;
            }

            Array.Sort(confCheck);

            for (int i = 0; i < confCheck.Length; i++)
            {
                string filePath = confCheck[i];
                int fileNum = int.Parse(Regex.Replace(Path.GetFileName(filePath), @"\D", ""));
                CustomBtn btnPreConfs = new CustomBtn();
                btnPreConfs.MouseClick += btnPreConf_Click;

                btnPreConfs.Name = Path.GetFileNameWithoutExtension(filePath);
                btnPreConfs.Location = relativeBtnPos;
                btnPreConfs.Width = MAX_BTN_WIDTH;
                btnPreConfs.Height = PRESET_BTN_HEIGHT;
                btnPreConfs.BorderRadius = 5;
                if (fileNum == activatePreset)
                {
                    btnPreConfs.BackColor = Color.FromArgb(40, 72, 72);
                }
                else
                {
                    btnPreConfs.BackColor = Color.FromArgb(86, 123, 123);
                }
                btnPreConfs.ForeColor = Color.White;
                btnPreConfs.FlatAppearance.BorderSize = 0;
                btnPreConfs.FlatAppearance.MouseDownBackColor = Color.FromArgb(112, 144, 144);
                btnPreConfs.FlatAppearance.MouseOverBackColor = Color.FromArgb(96, 128, 128);
                btnPreConfs.FlatStyle = FlatStyle.Flat;
                btnPreConfs.TextAlign = ContentAlignment.MiddleLeft;
                btnPreConfs.Font = new Font("나눔 고딕", 16F, FontStyle.Bold, GraphicsUnit.Point);
                pnSidePreset.Controls.Add(btnPreConfs);

                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line, title = "", date = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("date"))
                        {
                            string lineData = line.Split(',')[3];
                            DateTime dt = Convert.ToDateTime(lineData);
                            lineData = dt.ToString("yy/MM/dd");
                            date = lineData;
                        }

                        if (line.Contains("title"))
                        {
                            string lineData = line.Split(',')[3].Replace('，', ',');
                            title = "       " + lineData;
                        }
                    }
                    if(date == "")
                    {
                        date = "yy/MM/dd";
                    }

                    btnPreConfs.Text = date + title;
                }
                relativeBtnPos.Y = relativeBtnPos.Y + PRESET_BTN_HEIGHT + PRESET_BTN_GAP;
            }
        }

        //최근 작업 콘피그 파일 갯수만큼 버튼 로드
        private void recentConfBtnLoad()
        {
            string fileDi = Path.Combine(basePath, reqDi[(int)configFileType.Recent]);

            if (!Directory.Exists(fileDi))
            {
                Directory.CreateDirectory(fileDi);
            }

            pnReview.Controls.Clear();

            string[] confCheck = Directory.GetFiles(fileDi, "recentConfig*");
            Point relativePos = RECENT_BTN_POS;

            if (confCheck.Length < 1)
            {
                CustomBtn btnRecentConfs = new CustomBtn();
                btnRecentConfs.Name = "btnRecentDefault";
                btnRecentConfs.Location = relativePos;
                btnRecentConfs.Width = RECENT_BTN_WIDTH;
                btnRecentConfs.Height = RECENT_BTN_HEIGHT;
                btnRecentConfs.Margin = new Padding(4, 8, 4, 4);

                btnRecentConfs.FlatStyle = FlatStyle.Flat;
                btnRecentConfs.FlatAppearance.BorderSize = 0;
                btnRecentConfs.FlatAppearance.MouseDownBackColor = Color.FromArgb(233, 233, 238);
                btnRecentConfs.FlatAppearance.MouseOverBackColor = Color.FromArgb(217, 217, 223);

                btnRecentConfs.BorderRadius = 20;
                btnRecentConfs.BorderColor = Color.FromArgb(185, 185, 191);
                btnRecentConfs.BorderSize = 4;
                btnRecentConfs.BackColor = Color.Transparent;

                btnRecentConfs.ForeColor = Color.FromArgb(185, 185, 191);
                btnRecentConfs.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point);

                btnRecentConfs.TextAlign = ContentAlignment.MiddleLeft;

                btnRecentConfs.Text = "  최근 작업한 기록을 표시합니다\n  날짜,\n  추출한 Las파일명,\n  잘라낸 모양,\n  모양에 따른 Plot값\n  순으로 나열됩니다.";

                btnRecentConfs.Click += btnRecentDefault_Click;

                pnReview.Controls.Add(btnRecentConfs);
                return;
            }

            Array.Sort(confCheck);


            int btnNum = 0;
            string btnText = "";

            pnReview.SuspendLayout();

            for (int i = 0; i < confCheck.Length; i++)
            {
                string filePath = confCheck[i];
                int fileNum = int.Parse(Regex.Replace(Path.GetFileName(filePath), @"\D", ""));
                btnText = "";

                CustomBtn btnRecentConfs = new CustomBtn();
                btnRecentConfs.MouseClick += btnRecentConf_Click;
                btnRecentConfs.MouseHover += btnRecentConf_MouseHover;

                pnReview.Controls.Add(btnRecentConfs);
                btnRecentConfs.Location = relativePos;
                btnRecentConfs.Width = RECENT_BTN_WIDTH;
                btnRecentConfs.Height = RECENT_BTN_HEIGHT;
                btnRecentConfs.Margin = new Padding(4, 8, 4, 4);
                if (fileNum == activateRecent)
                {
                    btnRecentConfs.BackColor = Color.FromArgb(106, 126, 145);
                    btnRecentConfs.ForeColor = Color.White;
                }
                else
                {
                    btnRecentConfs.BackColor = Color.LightSteelBlue;
                    btnRecentConfs.ForeColor = SystemColors.ControlText;
                }
                btnRecentConfs.BorderRadius = 20;
                btnRecentConfs.BorderSize = 0;
                btnRecentConfs.FlatAppearance.BorderSize = 0;
                btnRecentConfs.FlatAppearance.MouseDownBackColor = Color.FromArgb(228, 238, 255);
                btnRecentConfs.FlatAppearance.MouseOverBackColor = Color.FromArgb(206, 226, 255);
                btnRecentConfs.FlatStyle = FlatStyle.Flat;
                btnRecentConfs.TextAlign = ContentAlignment.MiddleLeft;
                btnRecentConfs.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);

                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    string shapeStr = "";
                    while ((!sr.EndOfStream))
                    {
                        line = sr.ReadLine();
                        //MessageBox.Show(line);
                        if (line.Contains("title"))
                        {
                            btnText += "  날    짜:  " + line.Split(',')[3] + Environment.NewLine;
                            continue;
                        }
                        else if (line.Contains("Lasfilename"))
                        {
                            string lasName = Path.GetFileName(line.Split(',')[3]);
                            //las파일 이름이 너무 길면 텍스트가 이상해지므로
                            //일정 이상 길이면 자르고 ...으로 생략
                            if (lasName.Length > 16)
                            {
                                lasName = lasName.Substring(0, 16) + "...";
                            }
                            btnText += ("  Las파일:  " + lasName + Environment.NewLine);
                        }
                        else if (line.Contains("selection"))
                        {
                            int selection = int.Parse(line.Split(',')[3]);

                            switch (selection)
                            {
                                case 0:
                                    shapeStr = "circle";
                                    break;
                                case 1:
                                    shapeStr = "rectangle";
                                    break;
                                case 2:
                                    shapeStr = "polygon";
                                    break;
                            }
                            btnText += ("  모    양:  " + shapeStr + Environment.NewLine);
                            continue;
                        }
                        string[] plotData;
                        if (shapeStr != "" && line.Split(',')[2].Equals(shapeStr))
                        {
                            btnText += "\n  Plot 데이터\n";
                            plotData = line.Split(',')[3].Split(" ");
                            foreach (string str in plotData)
                            {
                                btnText += "  " + str + Environment.NewLine;
                            }
                            continue;
                        }
                    }
                }
                btnRecentConfs.Text = btnText.TrimEnd('\r', '\n');
                btnRecentConfs.Name = Path.GetFileNameWithoutExtension(filePath);
                relativePos.X = relativePos.X + RECENT_BTN_WIDTH + RECENT_BTN_GAP;
            }
            if (pnReview.HorizontalScroll.Enabled == true)
            {
                pnReview.AutoScrollMargin = new Size(50, 0);
            }

            pnReview.ResumeLayout(false);
            pnReview.PerformLayout();
        }

        //preConfig 버튼 클릭 이벤트 처리 코드
        private void btnPreConf_Click(object sender, EventArgs e)
        {
            string fileDi = Path.Combine(basePath, reqDi[(int)configFileType.Preset]);
            string fileName = ((Button)sender).Name;
            string fileNum = Regex.Replace(((Button)sender).Name, @"\D", "");

            reflectConfs(fileDi, fileName);

            activateRecent = -1;
            activatePreset = int.Parse(fileNum);

            preConfBtnLoad();
            recentConfBtnLoad();
        }

        //preConfig 버튼이 아예 없을 시의 디폴트 가이드 버튼 이벤트
        private void btnPreDefault_Click(object sender, EventArgs e)
        {
            tcMainHome.SelectedIndex = 1;

            flashCount = 0;
            pnSettingPreset.fillColor = Color.FromArgb(189, 189, 189);
            timerFlashPanel.Start();
        }

        //디폴트 가이드 버튼의 상세 이벤트
        private void timerFlashPanel_Tick(object sender, EventArgs e)
        {
            if (pnSettingPreset.fillColor == Color.Gray)
            {
                pnSettingPreset.fillColor = Color.FromArgb(189, 189, 189);
                pnSettingPreset.Invalidate();
            }
            else
            {
                pnSettingPreset.fillColor = Color.Gray;
                pnSettingPreset.Invalidate();
            }
            flashCount++;

            if (flashCount == 10)
            {
                timerFlashPanel.Stop();
                flashCount = 0;
                pnSettingPreset.fillColor = Color.Gray;
            }
        }

        //recetnConfig 버튼 관련 이벤트 처리 코드
        private void btnRecentConf_Click(object sender, EventArgs e)
        {
            string fileDi = Path.Combine(basePath, reqDi[(int)configFileType.Recent]);
            string fileName = ((Button)sender).Name;
            string fileNum = Regex.Replace(((Button)sender).Name, @"\D", "");

            reflectConfs(fileDi, fileName);

            activatePreset = -1;
            activateRecent = int.Parse(fileNum);

            preConfBtnLoad();
            recentConfBtnLoad();
        }

        private void btnRecentDefault_Click(object sender, EventArgs e)
        {
            tcMainHome.SelectedIndex = 0;

            flashCount = 0;
            btnStart.BackColor = Color.FromArgb(168, 168, 168);
            timerFlashBtn.Start();
        }

        private void timerFlashBtn_Tick(object sender, EventArgs e)
        {
            if (btnStart.BackColor == Color.FromArgb(168, 168, 168))
            {
                btnStart.BackColor = Color.DimGray;
            }
            else
            {
                btnStart.BackColor = Color.FromArgb(168, 168, 168);
            }

            flashCount++;

            if (flashCount == 6)
            {
                flashCount = 0;
                timerFlashBtn.Stop();
                btnStart.BackColor = Color.DimGray;
            }
        }

        //최근 기록 버튼의 생략된 las파일 이름 툴팁 표시
        private void btnRecentConf_MouseHover(object sender, EventArgs e)
        {
            if (((CustomBtn)sender).Text.Contains("..."))
            {
                string fileDi = Path.Combine(basePath, reqDi[(int)configFileType.Recent]);
                string fileName = ((Button)sender).Name + ".csv";
                string csvLine;

                using (StreamReader sr = new StreamReader(Path.Combine(fileDi, fileName)))
                {
                    while ((csvLine = sr.ReadLine()) != null)
                    {
                        if (csvLine.Contains("FileInfo,public,Lasfilename"))
                        {
                            ttMainInfo.SetToolTip((CustomBtn)sender, Path.GetFileName(csvLine.Split(',')[3]));
                        }
                    }
                }
            }
        }

        //사이드메뉴 버튼 관련 이벤트 처리 코드
        private void btnStart_Click(object sender, EventArgs e)
        {
            pFrm = new PlotForm(this);
            pFrm.configTouch += new configHandler(MakeConfig);
            pFrm.mainPaint += new customEventHandler(recentConfBtnLoad);
            pFrm.enableMainFormBtns += new switchEventHandler(switchCoreBtns);
            pFrm.mainProgressSet += new setIntEventHandler(progressSetter);
            pFrm.attachProgressBar += new switchEventHandler(progressAttach);
            pFrm.attachStartBtn += new switchEventHandler(startBtnAttach);

            bool[] applyChecker = new bool[10];
            bool result = true;
            DialogResult dialogResult = DialogResult.No;

            //subsamplng_textboxes
            applyChecker[0] = tbSubCellSize.Text == getParam(csv_data, "filters.sample", "cell");

            //normalize_textboxes
            applyChecker[1] = tbNorCellSize.Text == getParam(csv_data, "filters.smrf", "cell");
            applyChecker[2] = tbNorScalar.Text == getParam(csv_data, "filters.smrf", "scalar");
            applyChecker[3] = tbNorSlope.Text == getParam(csv_data, "filters.smrf", "slope");
            applyChecker[4] = tbNorThres.Text == getParam(csv_data, "filters.smrf", "threshold");
            applyChecker[5] = tbNorWinSize.Text == getParam(csv_data, "filters.smrf", "window");

            //trunkSlice_textboxes
            applyChecker[6] = tbTrunkMinHeight.Text == getParam(csv_data, "filters.range.trunk", "minheight");
            applyChecker[7] = tbTrunkMaxHeight.Text == getParam(csv_data, "filters.range.trunk", "maxheight");
            applyChecker[8] = tbTrunkSmooth.Text == getParam(csv_data, "csp_segmentstem", "smoothness");

            //CrownSlice_textboxes
            applyChecker[9] = tbCrownMinHeight.Text == getParam(csv_data, "filters.range.crown", "minheight");

            foreach (bool checker in applyChecker)
            {
                result = result == checker;
            }

            if (result == false)
            {
                dialogResult = MessageBox.Show("현재 설정이 적용되지 않았습니다.\n적용하시겠습니까?", "", MessageBoxButtons.YesNoCancel);
            }

            fileType = getParam(csv_data, "FileInfo", "fileType");

            if (dialogResult == DialogResult.Yes)
            {
                btnSettingApply_Click(sender, e);
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                return;
            }

            pFrm.Show();

            //plot창을 모달리스로 띄우는 대신 실행 및 설정 적용 관련 기능 버튼 비활성화
            switchCoreBtns(false);
        }

        private void switchCoreBtns(bool onOff)
        {
            switchBtns(pnReview, onOff);
            switchBtns(tpSettings, onOff);
        }

        private void switchBtns(Control control, bool onOff)
        {
            foreach (Control btns in control.Controls)
            {
                if (btns.GetType() == typeof(CustomBtn))
                {
                    btns.Enabled = onOff;
                }
                switchBtns(btns, onOff);
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            tcMainHome.SelectedIndex = 0;
        }

        //사이드 메뉴 여닫기
        private void btnSlideMenu_Click(object sender, EventArgs e)
        {
            menuOpen = !menuOpen;
            if (menuOpen == true)
            {
                pnSideMenu.Width = MAX_SLIDING_WIDTH;
                tcMainHome.Left += SLIDING_GAP / 2;

                lbSlidePreset.Visible = true;
                btnPresetManage.Location = new Point(350, 188);
                btnPresetManage.Size = new Size(22, 22);

                lbPresetManage.Visible = false;
                pnSidePreset.Visible = true;
            }
            else if (menuOpen == false)
            {
                pnSidePreset.Visible = false;
                lbPresetManage.Visible = true;

                pnSideMenu.Width = MIN_SLIDING_WIDTH;
                tcMainHome.Left -= SLIDING_GAP / 2;

                btnPresetManage.Location = new Point(14, 183);
                btnPresetManage.Size = new Size(38, 34);
                lbSlidePreset.Visible = false;
            }
        }
        private void btnSettings_Click(object sender, EventArgs e)
        {
            tcMainHome.SelectedIndex = 1;
        }

        //이 아래 4개 메서드 설정창 파라메터 판넬 마우스 이벤트
        private void pnSettingAll_MouseEnter(object sender, EventArgs e)
        {
            if (sender is CustomPanel)
            {
                CustomPanel cPanel = (CustomPanel)sender;

                cPanel.fillColor = Color.DarkGray;
                cPanel.Invalidate();
            }
        }
        private void pnSettingAll_MouseLeave(object sender, EventArgs e)
        {
            if (sender is CustomPanel)
            {
                CustomPanel cPanel = (CustomPanel)sender;

                cPanel.fillColor = Color.Gray;
                cPanel.Invalidate();
            }
        }
        private void pnSettingAll_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is CustomPanel)
            {
                CustomPanel cPanel = (CustomPanel)sender;

                cPanel.fillColor = Color.Silver;
                cPanel.Invalidate();
            }
        }
        private void pnSettingAll_MouseUp(object sender, MouseEventArgs e)
        {
            if (sender is CustomPanel)
            {
                CustomPanel cPanel = (CustomPanel)sender;

                cPanel.fillColor = Color.DarkGray;
                cPanel.Invalidate();
            }
        }

        //아래 메서드 4개 커스텀 제목표시줄로 인한 창 이동 이벤트 임의 생성
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Location.Y <= 40)
            {
                relativeMformPos = e.Location;
                isMformDrag = true;
            }
        }
        //프로그램 제목을 나타내는 라벨이 윈폼을 가려서 따로 만든 이벤트
        private void lbTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Location.Y <= 20)
            {
                relativeMformPos = e.Location;
                isMformDrag = true;
            }
        }
        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isMformDrag)
            {
                this.Location = new Point(this.Location.X + (e.X - relativeMformPos.X),
                    this.Location.Y + (e.Y - relativeMformPos.Y));
            }
        }
        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            isMformDrag = false;
        }

        //적용하기 버튼 이벤트
        //적용할 때 사용자 설정값이나 최근 기록이 활성화 되어있으면
        //해당 config값과 적용 후의 csv_data의 값을 비교하여
        //비활성화 여부 결정
        private void btnSettingApply_Click(object sender, EventArgs e)
        {
            configFileType confType;
            string fileDi = "";
            string fileName = "";
            string filePath;

            //경우에 따른 파일 경로 설정
            if (activatePreset > -1)
            {
                fileDi = Path.Combine(basePath, reqDi[(int)configFileType.Preset]);
                fileName = "presetConfig" + activatePreset.ToString() + ".csv";
            }
            else if (activateRecent > -1)
            {
                fileDi = Path.Combine(basePath, reqDi[(int)configFileType.Recent]);
                fileName = "recentConfig" + activateRecent.ToString() + ".csv";
            }
            filePath = Path.Combine(fileDi, fileName);

            //csv_data 리스트에 텍스트 박스의 값 적용
            UpdateParams(csv_data);

            //최근 기록 혹은 사용자 설정값 csv 불러올 리스트
            List<List<string>> SelectedCsvData = new List<List<string>>();

            //최근 기록 혹은 사용자 설정값 csv 불러오기
            read_csv(filePath, SelectedCsvData);

            //SelectedCsvData 리스트를 반복문 돌려서 string으로 추출
            string tmp1 = "";
            for (int i = 0; i < SelectedCsvData.Count; i++)
            {
                for (int j = 0; j < SelectedCsvData[i].Count; j++)
                {
                    tmp1 += SelectedCsvData[i][j] + ',';
                }
                tmp1.TrimEnd(',');
                tmp1 += Environment.NewLine;
            }

            //이 시점이 텍스트 박스 값이 반영된 리스트
            UpdateParams(SelectedCsvData);
            //SelectedCsvData 리스트를 반복문 돌려서 string으로 추출
            string tmp2 = "";
            for (int i = 0; i < SelectedCsvData.Count; i++)
            {
                for (int j = 0; j < SelectedCsvData[i].Count; j++)
                {
                    tmp2 += SelectedCsvData[i][j] + ',';
                }
                tmp2.TrimEnd(',');
                tmp2 += Environment.NewLine;
            }
            //두 리스트
            //(한쪽은 설정창 텍스트 박스 값 반영한 csv_data,
            //다른 쪽은 클릭되어 있던 config의 csv_data)를 비교하여
            //두 리스트가 다르면 선택 해제
            if (!tmp1.Equals(tmp2) && fileType.Equals("Preset"))
            {
                foreach (Control cont in pnSidePreset.Controls)
                {
                    //선택된 버튼 activate(Preset/Recent) 변수로 구해서 색 바꾸기 
                    if (cont.Name.Equals("presetConfig" + activatePreset.ToString()))
                    {
                        cont.BackColor = Color.Transparent;
                        cont.Invalidate();
                        activatePreset = -1;
                    }
                }
            }
            else if (!tmp1.Equals(tmp2) && fileType.Equals("Recent"))
            {
                foreach (Control cont in pnReview.Controls)
                {
                    //선택된 버튼 activate(Preset/Recent) 변수로 구해서 색 바꾸기 
                    if (cont.Name.Equals("recentConfig" + activateRecent.ToString()))
                    {
                        cont.BackColor = Color.Khaki;
                        cont.Invalidate();
                        activateRecent = -1;
                    }
                }
            }

            //기타 설정값은 메인폼 텍스트 박스에서 바로 반영되지만,(UpdateParams메서드)
            //plot값들은 적용하기 누르기 전 일시적으로 반영할 곳이 없기에 
            //따로 apply_temp메서드에서 임시로 만든 plot값 구조체에 넣은 값을
            //프로그램에 직접 반영되는 변수들인 gui 구조체로 옮겨담는다
            if (fileType != "")
            {
                apply_temp();
            }
            else
            {
                gui.loadPath = "";
            }
            MessageBox.Show("적용되었습니다.");
            tcMainHome.SelectedIndex = 0;
        }

        //취소버튼
        //저장하지 않고 기존에 적용 되어있던 값으로 텍스트 박스 값 교체 후
        //시작화면으로 이동
        private void btnSettingCancel_Click(object sender, EventArgs e)
        {
            FillTextboxes();
            activatePreset = -1;
            activateRecent = -1;
            preConfBtnLoad();
            recentConfBtnLoad();
        }

        //저장하기 버튼 클릭 이벤트
        private void btnSettingSave_Click(object sender, EventArgs e)
        {
            //변수들 초기화
            UpdateParams(csv_data);
            // csv 작성
            try
            {
                write_csv(csv_path);
                MessageBox.Show("CSV 파일이 수정되었습니다.");
            }
            catch { };
        }

        //사용자 설정값 저장 버튼 클릭 이벤트
        //최근 기록과 달리 사용자 설정값 저장은 plot폼 쪽 데이터를 저장하지 않음.
        //(new!) 사용자 설정값 클릭 상태일 시 값 변경 후 저장하면 수정 가능(예정)
        private void btnPresetSave_Click(object sender, EventArgs e)
        {
            string fileDi = Path.Combine(basePath, reqDi[(int)configFileType.Preset]);
            string[] confCheck = Directory.GetFiles(fileDi, "presetConfig*");

            if (mFrm == null)
            {
                mFrm = new ManageForm(this);
                mFrm.mainPaint += new customEventHandler(this.preConfBtnLoad);
                mFrm.presetReflect += new presetReflectHandler(this.reflectConfs);
            }

            /*
            if (pFrm == null)
            {
                pFrm = new PlotForm(this);
                pFrm.configTouch += new configHandler(MakeConfig);
                pFrm.mainPaint += new customEventHandler(recentConfBtnLoad);
                pFrm.enableMainFormBtns += new switchEventHandler(switchCoreBtns);
                pFrm.mainProgressSet += new setIntEventHandler(progressSetter);
                pFrm.attachProgressBar += new switchEventHandler(progressAttach);
                pFrm.attachStartBtn += new switchEventHandler(startBtnAttach);
            }
            */

            if (activatePreset == -1 && confCheck.Length >= 10)
            {
                MessageBox.Show("사용자 설정은 10개 까지만 저장 가능합니다");
                return;
            }
            MakeConfig(configFileType.Preset);
            preConfBtnLoad();
            if (activatePreset == -1)
            {
                mFrm.ShowDialog();
            }
            else
            {
                MessageBox.Show("적용되었습니다");
            }
        }

        //사용자 설정값 관리 버튼 클릭 이벤트
        private void btnPresetManage_Click(object sender, EventArgs e)
        {
            if (mFrm == null)
            {
                mFrm = new ManageForm(this);
                mFrm.mainPaint += new customEventHandler(this.preConfBtnLoad);
                mFrm.presetReflect += new presetReflectHandler(this.reflectConfs);
            }
            mFrm.ShowDialog();
        }

        //기본값 불러오기 버튼
        private void btnSettingLoad_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("기본값을 적용하시겠습니까?\n저장되지 않은 설정값은 사라집니다.",
                "기본 설정 적용", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            activatePreset = -1;
            activateRecent = -1;
            preConfBtnLoad();
            recentConfBtnLoad();

            read_csv(csv_path, csv_data);
            FillTextboxes();
            fileType = "";
        }

        //공장초기화 버튼
        private void btn_factory_reset_Click(object sender, EventArgs e)
        {
            FactoryReset(csv_path);
        }
        private void btn_factory_reset_MouseHover(object sender, EventArgs e)
        {
            ttMainInfo.SetToolTip(btnFactoryReset, "기본 설정값을 초기로 되돌립니다\n기존의 기본값은 따로 저장되지 않습니다");
        }

        //메인 화면의 Recent Task 도움말
        private void btnRecentInfo_MouseHover(object sender, EventArgs e)
        {
            ttMainInfo.SetToolTip(btnRecentInfo, "최근 작업한 설정값을 보존합니다.\n기록을 클릭할 시 설정창에 반영됩니다.(설정 적용 필요)");
        }

        //메인 사이드 메뉴의 사용자 설정값 관리 버튼 도움말
        private void btnPresetManage_MouseHover(object sender, EventArgs e)
        {
            ttMainInfo.SetToolTip(btnPresetManage, "사용자 설정 관리창을 엽니다");
        }

        //프로그램 동작시 프로그래바,start 버튼 숨김기능 관련 이벤트
        //delegate 통하여 다른 폼에서 사용됨
        private void progressAttach(bool onOff)
        {
            pbLoadingBar.Visible = onOff;
        }

        private void progressSetter(int setValue)
        {
            pbLoadingBar.Value = setValue;
            pbLoadingBar.Invalidate();
        }

        private void startBtnAttach(bool onOff)
        {
            btnStart.Visible = onOff;
        }
    }
}