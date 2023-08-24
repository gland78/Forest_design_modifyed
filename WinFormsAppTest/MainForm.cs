using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;
using WinFormsAppTest.Properties;
using System.Dynamic;
using System.Text.RegularExpressions;
using static WinFormsAppTest.MainForm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WinFormsAppTest
{
    internal delegate Dictionary<string, double> plotDataHandler();

    internal delegate void customEventHandler();

    internal delegate void configHandler(configFileType type);

    internal delegate void setterEventHandler(int setValue);

    internal delegate void switchEventHandler(bool onOff);

    public partial class MainForm : Form
    {
        public enum configFileType
        {
            Default,
            Recent,
            Preset
        }

        internal plotDataHandler plotSender;

        private PlotForm? pFrm;
        private ManageForm? mFrm;

        //사이드 메뉴의 최대, 최소 폭 크기 및 그 차이
        const int MAX_SLIDING_WIDTH = 384;
        const int MIN_SLIDING_WIDTH = 65;
        const int SLIDING_GAP = MAX_SLIDING_WIDTH - MIN_SLIDING_WIDTH;

        //사이드 메뉴에 확장, 축소에 따른 메뉴 버튼 크기
        const int MIN_BTN_WIDTH = 42;
        const int MAX_BTN_WIDTH = 370;

        //사이드 메뉴에 생성될 첫 버튼의 위치 및 크기, 간격
        Point PRESET_BTN_POS = new Point(12, 170);
        const int PRESET_BTN_WIDTH = 370;
        const int PRESET_BTN_HEIGHT = 45;
        const int PRESET_BTN_GAP = 5;

        //최근 작업 목록 첫 추가 위치 및 크기, 간격
        Point RECENT_BTN_POS = new Point(50, 27);
        const int RECENT_BTN_WIDTH = 205;
        const int RECENT_BTN_HEIGHT = 165;
        const int RECENT_BTN_GAP = 30;

        bool menuOpen = false;

        Point relativeMformPos = new Point();

        bool isMformDrag = false;

        //exe파일이 위치한 기본 폴더와 그 내부 config 폴더들
        internal static string basePath = (Environment.CurrentDirectory).ToString();
        internal static string[] reqDi = { "", "RecentConfig", "PresetConfig" };

        public MainForm()
        {
            InitializeComponent();
            DoubleBuffered = true;

            customPanels_Load();

            pnMain.isBorder = false;

            pnSideMenu.Width = MIN_SLIDING_WIDTH;
            tcMainHome.Left -= SLIDING_GAP / 2;

            btnSettings.Width = MIN_BTN_WIDTH;
            btnSettings.Text = "";

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!File.Exists(Path.Combine(basePath, "config.csv")))
            {
                MessageBox.Show("config.csv파일이 없습니다. 프로그램 개발 측으로 문의해주세요.\n" +
                    $"filepath : {Environment.CurrentDirectory}", "기본 설정 파일 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            read_csv(csv_path);
            FillTextboxes();
            RegistTextBoxHandler();

            Point screenSize = ((Point)Screen.PrimaryScreen.Bounds.Size);

            //메인 폼 로드 전 이벤트 전처리(Designer.cs에 넣으면 찾기가 힘듬)
            this.Location = new Point((screenSize.X - this.Width) / 2, (screenSize.Y - this.Height) / 2);

            mainForm_AddEvent();

            preConfBtnLoad();
            recentConfBtnLoad();

            foreach (Control cont in pnSideMenu.Controls)
            {
                if (cont.Name.Contains("presetConfig"))
                {
                    cont.ForeColor = Color.SeaGreen;
                    cont.Width = MIN_BTN_WIDTH;
                }
            }
        }

        //메인 폼 로드 전 이벤트 전처리(Designer.cs에 넣으면 찾기가 힘듬)
        private void mainForm_AddEvent()
        {
            //홈버튼 이벤트
            btnHome.Click += btnHome_Click;
            //설정버튼 이벤트
            btnSettings.Click += btnSettings_Click;
            //사이드 메뉴 접기/열기 버튼 이벤트
            btnSlideMenu.Click += btnSlideMenu_Click;
            //창 닫기 버튼 이벤트
            btnClose.Click += btnClose_Click;
            //프로그램 실행 버튼 이벤트
            btnStart.Click += btnStart_Click;

            //이 아래로 전부 설정창의 CustomPanel 객체들 이벤트
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

            pnSettingCrown1.MouseDown += pnSettingAll_MouseDown;
            pnSettingCrown1.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingCrown1.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingCrown1.MouseUp += pnSettingAll_MouseUp;

            pnSettingCrown2.MouseDown += pnSettingAll_MouseDown;
            pnSettingCrown2.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingCrown2.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingCrown2.MouseUp += pnSettingAll_MouseUp;
        }

        //CustomPanel 색 및 테두리 지정(Designer.cs에서 지정하면 컴파일 시 없어짐)
        private void customPanels_Load()
        {
            pnSettingDefault.BackColor = Color.Transparent;
            pnSettingDefault.isFill = true;
            pnSettingDefault.isBorder = false;
            pnSettingDefault.fillColor = Color.Gray;

            pnSettingPreset.BackColor = Color.Transparent;
            pnSettingPreset.isFill = true;
            pnSettingPreset.isBorder = false;
            pnSettingPreset.fillColor = Color.Gray;

            pnSettingSub1.BackColor = Color.Transparent;
            pnSettingSub1.isFill = true;
            pnSettingSub1.isBorder = false;
            pnSettingSub1.fillColor = Color.Gray;

            pnSettingNor1.BackColor = Color.Transparent;
            pnSettingNor1.isFill = true;
            pnSettingNor1.isBorder = false;
            pnSettingNor1.fillColor = Color.Gray;

            pnSettingNor2.BackColor = Color.Transparent;
            pnSettingNor2.isFill = true;
            pnSettingNor2.isBorder = false;
            pnSettingNor2.fillColor = Color.Gray;

            pnSettingNor3.BackColor = Color.Transparent;
            pnSettingNor3.isFill = true;
            pnSettingNor3.isBorder = false;
            pnSettingNor3.fillColor = Color.Gray;

            pnSettingNor4.BackColor = Color.Transparent;
            pnSettingNor4.isFill = true;
            pnSettingNor4.isBorder = false;
            pnSettingNor4.fillColor = Color.Gray;

            pnSettingNor5.BackColor = Color.Transparent;
            pnSettingNor5.isFill = true;
            pnSettingNor5.isBorder = false;
            pnSettingNor5.fillColor = Color.Gray;

            pnSettingTrunk1.BackColor = Color.Transparent;
            pnSettingTrunk1.isFill = true;
            pnSettingTrunk1.isBorder = false;
            pnSettingTrunk1.fillColor = Color.Gray;

            pnSettingTrunk2.BackColor = Color.Transparent;
            pnSettingTrunk2.isFill = true;
            pnSettingTrunk2.isBorder = false;
            pnSettingTrunk2.fillColor = Color.Gray;

            pnSettingCrown1.BackColor = Color.Transparent;
            pnSettingCrown1.isFill = true;
            pnSettingCrown1.isBorder = false;
            pnSettingCrown1.fillColor = Color.Gray;

            pnSettingCrown2.BackColor = Color.Transparent;
            pnSettingCrown2.isFill = true;
            pnSettingCrown2.isBorder = false;
            pnSettingCrown2.fillColor = Color.Gray;
        }

        //프리셋 콘피그 파일 갯수만큼 버튼 로드
        private void preConfBtnLoad()
        {
            string filePath = Path.Combine(basePath, reqDi[(int)configFileType.Preset]);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            pnSideMenu.Controls.Clear();

            pnSideMenu.Controls.Add(btnHome);
            pnSideMenu.Controls.Add(btnSettings);
            pnSideMenu.Controls.Add(btnSlideMenu);
            pnSideMenu.Controls.Add(btnPresetManage);

            //프리셋 콘피그 저장 장소
            string[] confCheck = Directory.GetFiles(filePath, "presetConfig*");

            if (confCheck.Length < 1)
            {
                return;
            }

            Array.Sort(confCheck);

            Point relativeBtnPos = PRESET_BTN_POS;
            int btnNum = 0;

            pnSideMenu.SuspendLayout();
            foreach (string conf in confCheck)
            {
                Button btnPreConfs = new Button();
                btnPreConfs.MouseClick += btnPreConf_Click;

                pnSideMenu.Controls.Add(btnPreConfs);
                btnPreConfs.Location = relativeBtnPos;
                btnPreConfs.Width = PRESET_BTN_WIDTH;
                btnPreConfs.Height = PRESET_BTN_HEIGHT;
                btnPreConfs.BackColor = Color.Transparent;
                btnPreConfs.FlatAppearance.BorderSize = 0;
                btnPreConfs.FlatAppearance.MouseDownBackColor = Color.MediumAquamarine;
                btnPreConfs.FlatAppearance.MouseOverBackColor = Color.MediumSeaGreen;
                btnPreConfs.FlatStyle = FlatStyle.Flat;
                btnPreConfs.ImageAlign = ContentAlignment.MiddleLeft;
                btnPreConfs.TextAlign = ContentAlignment.MiddleLeft;
                btnPreConfs.Font = new Font("맑은 고딕", 18F, FontStyle.Bold, GraphicsUnit.Point);
                btnPreConfs.Image = Image.FromFile(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName
                    + @"\Resources\btnPreConf.Image.png");
                using (StreamReader sr = new StreamReader(conf))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("title"))
                        {
                            btnPreConfs.Text = "            " + line.Split(',')[3];
                            break;
                        }
                    }
                }
                btnPreConfs.Name = Path.GetFileName(conf);
                relativeBtnPos.Y = relativeBtnPos.Y + PRESET_BTN_HEIGHT + PRESET_BTN_GAP;
                if (menuOpen == false)
                {
                    btnPreConfs.ForeColor = Color.SeaGreen;
                    btnPreConfs.Width = MIN_BTN_WIDTH;
                }
            }
            pnSideMenu.ResumeLayout(false);
            pnSideMenu.PerformLayout();
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

            if (confCheck.Length < 1)
            {
                return;
            }

            Array.Sort(confCheck);

            Point relativePos = RECENT_BTN_POS;
            int btnNum = 0;
            string btnText = "";

            pnReview.SuspendLayout();

            foreach (string conf in confCheck)
            {
                btnText = "";

                CustomBtn btnRecentConfs = new CustomBtn();
                btnRecentConfs.MouseClick += btnRecentConf_Click;
                btnRecentConfs.MouseDown += btnRecentConf_Down;
                btnRecentConfs.MouseUp += btnRecentConf_Up;

                pnReview.Controls.Add(btnRecentConfs);
                btnRecentConfs.Location = relativePos;
                btnRecentConfs.Width = RECENT_BTN_WIDTH;
                btnRecentConfs.Height = RECENT_BTN_HEIGHT;
                btnRecentConfs.Margin = new Padding(4, 8, 4, 4);
                btnRecentConfs.BackgroundColor = Color.MintCream;
                btnRecentConfs.BorderRadius = 20;
                btnRecentConfs.BorderSize = 0;
                btnRecentConfs.FlatAppearance.BorderSize = 0;
                btnRecentConfs.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 255, 255);
                btnRecentConfs.FlatAppearance.MouseOverBackColor = Color.FromArgb(250, 255, 250);
                btnRecentConfs.FlatStyle = FlatStyle.Flat;
                btnRecentConfs.TextAlign = ContentAlignment.MiddleLeft;
                btnRecentConfs.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
                btnRecentConfs.ForeColor = SystemColors.ControlText;
                using (StreamReader sr = new StreamReader(conf))
                {
                    string line;
                    string shapeStr = "";
                    while ((!sr.EndOfStream))
                    {
                        line = sr.ReadLine();
                        //MessageBox.Show(line);
                        if (line.Contains("title"))
                        {
                            btnText += "   " + line.Split(',')[3];
                            continue;
                        }
                        if (line.Contains("selection"))
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
                            btnText += "   " + shapeStr + "\n";
                            continue;
                        }
                        string[] plotData;
                        if (line.Contains(shapeStr))
                        {
                            plotData = line.Split(',')[3].Split(" ");
                            foreach (string str in plotData)
                            {
                                btnText += "   " + str + "\n";
                            }
                            continue;
                        }
                    }
                }
                btnRecentConfs.Text = btnText;
                btnRecentConfs.Name = Path.GetFileName(conf);
                relativePos.X = relativePos.X + RECENT_BTN_WIDTH + RECENT_BTN_GAP;
            }
            if (pnReview.HorizontalScroll.Enabled == true)
            {
                pnReview.AutoScrollMargin = new Size(50, 0);
            }

            pnReview.ResumeLayout(false);
            pnReview.PerformLayout();
        }

        //recetnConfig 버튼 관련 이벤트 처리 코드
        private void btnRecentConf_Click(object sender, EventArgs e)
        {
            string fileDi = Path.Combine(basePath, reqDi[(int)configFileType.Recent]);
            string[] confCheck = Directory.GetFiles(fileDi, "recentConfig*");
            string fileName = ((Button)sender).Name;

            string? csvLines;

            using (StreamReader sr = new StreamReader(Path.Combine(fileDi, fileName)))
            {
                while ((csvLines = sr.ReadLine()) != null)
                {
                    if (csvLines.Contains("sample") && csvLines.Contains("cell"))
                    {
                        tbSubCellSize.Text = csvLines.Split(',')[3];
                    }

                    else if (csvLines.Contains("smrf") && csvLines.Contains("cell"))
                    {
                        tbNorCellSize.Text = csvLines.Split(",")[3];
                    }

                    else if (csvLines.Contains("smrf") && csvLines.Contains("window"))
                    {
                        tbNorWinSize.Text = csvLines.Split(",")[3];
                    }

                    else if (csvLines.Contains("smrf") && csvLines.Contains("slope"))
                    {
                        tbNorSlope.Text = csvLines.Split(",")[3];
                    }

                    else if (csvLines.Contains("smrf") && csvLines.Contains("scalar"))
                    {
                        tbNorScalar.Text = csvLines.Split(",")[3];
                    }

                    else if (csvLines.Contains("smrf") && csvLines.Contains("scalar"))
                    {
                        tbNorScalar.Text = csvLines.Split(",")[3];
                    }

                    else if (csvLines.Contains("smrf") && csvLines.Contains("threshold"))
                    {
                        tbNorThres.Text = csvLines.Split(",")[3];
                    }

                    else if (csvLines.Contains("range") && csvLines.Contains("trunk") && csvLines.Contains("minheight"))
                    {
                        tbTrunkMinHeight.Text = csvLines.Split(",")[3];
                    }

                    else if (csvLines.Contains("range") && csvLines.Contains("trunk") && csvLines.Contains("maxheight"))
                    {
                        tbTrunkMaxHeight.Text = csvLines.Split(",")[3];
                    }

                    else if (csvLines.Contains("range") && csvLines.Contains("crown") && csvLines.Contains("minheight"))
                    {
                        tbCrownMinHeight.Text = csvLines.Split(",")[3];
                    }

                    else if (csvLines.Contains("range") && csvLines.Contains("crown") && csvLines.Contains("maxheight"))
                    {
                        tbCrownMaxHeight.Text = csvLines.Split(",")[3];
                    }
                }
            }
            tcMainHome.SelectedIndex = 1;
        }

        private void btnRecentConf_Down(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.FlatStyle = FlatStyle.Popup;
            btn.Invalidate();
        }

        private void btnRecentConf_Up(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Invalidate();
        }
        //preConfig 버튼 클릭 이벤트 처리 코드
        private void btnPreConf_Click(object sender, EventArgs e)
        {
            string[] confCheck = Directory.GetFiles(Path.Combine(basePath, reqDi[(int)configFileType.Preset]), "PresetConfig*");

            foreach (string conf in confCheck)
            {
                string fileName = conf.Substring(conf.IndexOf("presetConfig"), conf.Length - conf.IndexOf("presetConfig") - 4);
                if (((Button)sender).Name == fileName + ".csv")
                {
                    read_csv(conf);
                    FillTextboxes();
                }
            }

            tcMainHome.SelectedIndex = 1;
        }

        //사이드메뉴 버튼 관련 이벤트 처리 코드
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (pFrm == null)
            {
                pFrm = new PlotForm(this);
                pFrm.configTouch += new configHandler(MakeConfig);
                pFrm.mainPaint += new customEventHandler(recentConfBtnLoad);
                pFrm.mainProgressSet += new setterEventHandler(progressSetter);
                pFrm.attachProgressBar += new switchEventHandler(progressAttach);
                pFrm.attachStartBtn += new switchEventHandler(startBtnAttach);
            }
            pFrm.ShowDialog();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            tcMainHome.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //사이드 메뉴 여닫기
        private void btnSlideMenu_Click(object sender, EventArgs e)
        {
            menuOpen = !menuOpen;
            if (pnSideMenu.Width <= MAX_SLIDING_WIDTH && menuOpen == true)
            {
                pnSideMenu.Width = MAX_SLIDING_WIDTH;
                tcMainHome.Left += SLIDING_GAP / 2;

                foreach (Control cont in pnSideMenu.Controls)
                {
                    if (cont.Name.Contains("presetConfig"))
                    {
                        cont.ForeColor = SystemColors.ControlText;
                        cont.Width = MAX_BTN_WIDTH;
                    }
                }
                btnSettings.Width = MAX_BTN_WIDTH;
                btnSettings.Text = "            Settings";
            }

            else if (pnSideMenu.Width >= MIN_SLIDING_WIDTH && menuOpen == false)
            {
                pnSideMenu.Width = MIN_SLIDING_WIDTH;
                tcMainHome.Left -= SLIDING_GAP / 2;

                foreach (Control cont in pnSideMenu.Controls)
                {
                    if (cont.Name.Contains("presetConfig"))
                    {
                        cont.ForeColor = Color.SeaGreen;
                        cont.Width = MIN_BTN_WIDTH;
                    }
                }
                btnSettings.Width = MIN_BTN_WIDTH;
                btnSettings.Text = "";
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


        //아래 메서드 3개 커스텀 제목표시줄로 인한 창 이동 이벤트 임의 생성
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Location.Y <= 30)
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

        //저장하기 버튼 클릭 이벤트
        private void btnSettingSave_Click(object sender, EventArgs e)
        {
            //변수들 초기화
            UpdateParams();
            // csv 작성
            try
            {
                write_csv(csv_path);
                MessageBox.Show("CSV 파일이 수정되었습니다.");
            }
            catch { };
        }

        //프리셋 저장 버튼 클릭 이벤트
        private void btnPresetSave_Click(object sender, EventArgs e)
        {
            if (mFrm == null)
            {
                mFrm = new ManageForm();
                mFrm.mainPaint += new customEventHandler(this.preConfBtnLoad);
            }

            if (pFrm == null)
            {
                pFrm = new PlotForm(this);
                pFrm.configTouch += new configHandler(MakeConfig);
                pFrm.mainPaint += new customEventHandler(recentConfBtnLoad);
            }
            MakeConfig(configFileType.Preset);

            mFrm.ShowDialog();
            preConfBtnLoad();
        }

        //프리셋 관리 버튼 클릭 이벤트
        private void btnPresetManage_Click(object sender, EventArgs e)
        {
            if (mFrm == null)
            {
                mFrm = new ManageForm();
                mFrm.mainPaint += new customEventHandler(this.preConfBtnLoad);
            }
            mFrm.ShowDialog();
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //기본값 불러오기 버튼
        private void btnSettingLoad_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("기본값을 적용하시겠습니까?\n저장되지 않은 설정값은 사라집니다.",
                "기본 설정 적용", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            read_csv(csv_path);
            FillTextboxes();
        }

        //프로그램 동작시 프로그래바,start 버튼 숨김기능 관련 이벤트
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

        //적용하기 버튼 이벤트
        private void btnSettingApply_Click(object sender, EventArgs e)
        {
            UpdateParams();
            MessageBox.Show("적용되었습니다.");
            tcMainHome.SelectedIndex = 0;
        }

        //취소버튼 
        private void btnSettingCancel_Click(object sender, EventArgs e)
        {
            FillTextboxes();
            tcMainHome.SelectedIndex = 0;
        }

        //공장초기화 버튼
        private void btn_factory_reset_Click(object sender, EventArgs e)
        {
            FactoryReset(csv_path);
            MessageBox.Show("설정 파일이 초기화 되었습니다.");
        }
    }
}