using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        Point relativeRpanelPos = new Point();

        bool isMformDrag = false;

        internal static string configPath = Directory.GetParent(System.Environment.CurrentDirectory) + @"\bin\";
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
            if (!File.Exists(configPath + "config.json"))
            {
                MakeConfig(configFileType.Default);
            }

            //ReadConfig();
            Initialize_Params();
            FillTextboxes();
            RegistTextBoxHandler();

            Point screenSize = ((Point)Screen.PrimaryScreen.Bounds.Size);

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

            pnSettingOut1.MouseDown += pnSettingAll_MouseDown;
            pnSettingOut1.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingOut1.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingOut1.MouseUp += pnSettingAll_MouseUp;

            pnSettingOut2.MouseDown += pnSettingAll_MouseDown;
            pnSettingOut2.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingOut2.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingOut2.MouseUp += pnSettingAll_MouseUp;

            pnSettingOut3.MouseDown += pnSettingAll_MouseDown;
            pnSettingOut3.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingOut3.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingOut3.MouseUp += pnSettingAll_MouseUp;

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

            pnSettingTree1.MouseDown += pnSettingAll_MouseDown;
            pnSettingTree1.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingTree1.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingTree1.MouseUp += pnSettingAll_MouseUp;

            pnSettingTree2.MouseDown += pnSettingAll_MouseDown;
            pnSettingTree2.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingTree2.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingTree2.MouseUp += pnSettingAll_MouseUp;

            pnSettingTree3.MouseDown += pnSettingAll_MouseDown;
            pnSettingTree3.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingTree3.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingTree3.MouseUp += pnSettingAll_MouseUp;

            pnSettingTree4.MouseDown += pnSettingAll_MouseDown;
            pnSettingTree4.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingTree4.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingTree4.MouseUp += pnSettingAll_MouseUp;

            pnSettingMeasure1.MouseDown += pnSettingAll_MouseDown;
            pnSettingMeasure1.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingMeasure1.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingMeasure1.MouseUp += pnSettingAll_MouseUp;
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

            pnSettingOut1.BackColor = Color.Transparent;
            pnSettingOut1.isFill = true;
            pnSettingOut1.isBorder = false;
            pnSettingOut1.fillColor = Color.Gray;

            pnSettingOut2.BackColor = Color.Transparent;
            pnSettingOut2.isFill = true;
            pnSettingOut2.isBorder = false;
            pnSettingOut2.fillColor = Color.Gray;

            pnSettingOut3.BackColor = Color.Transparent;
            pnSettingOut3.isFill = true;
            pnSettingOut3.isBorder = false;
            pnSettingOut3.fillColor = Color.Gray;

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

            pnSettingTree1.BackColor = Color.Transparent;
            pnSettingTree1.isFill = true;
            pnSettingTree1.isBorder = false;
            pnSettingTree1.fillColor = Color.Gray;

            pnSettingTree2.BackColor = Color.Transparent;
            pnSettingTree2.isFill = true;
            pnSettingTree2.isBorder = false;
            pnSettingTree2.fillColor = Color.Gray;

            pnSettingTree3.BackColor = Color.Transparent;
            pnSettingTree3.isFill = true;
            pnSettingTree3.isBorder = false;
            pnSettingTree3.fillColor = Color.Gray;

            pnSettingTree4.BackColor = Color.Transparent;
            pnSettingTree4.isFill = true;
            pnSettingTree4.isBorder = false;
            pnSettingTree4.fillColor = Color.Gray;

            pnSettingMeasure1.BackColor = Color.Transparent;
            pnSettingMeasure1.isFill = true;
            pnSettingMeasure1.isBorder = false;
            pnSettingMeasure1.fillColor = Color.Gray;
        }

        //프리셋 콘피그 파일 갯수만큼 버튼 로드
        private void preConfBtnLoad()
        {
            string filePath = Path.Combine(configPath, reqDi[(int)configFileType.Preset]);

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
                btnPreConfs.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point);
                btnPreConfs.Image = Image.FromFile(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName
                    + @"\Resources\btnPreConf.Image.png");
                using (StreamReader sr = new StreamReader(conf))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("title"))
                        {
                            btnPreConfs.Text = "            " + line.Substring(line.IndexOf(":") + 3, line.Length - line.IndexOf(":") - 4);
                            break;
                        }
                    }
                }
                btnPreConfs.Name = "presetConfig" + btnNum++.ToString();
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
            string filePath = Path.Combine(configPath, reqDi[(int)configFileType.Recent]);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            pnReview.Controls.Clear();

            string[] confCheck = Directory.GetFiles(filePath, "RecentConfig*");

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
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("title"))
                        {
                            btnText += "   " + line.Substring(line.IndexOf(":") + 3, line.Length - line.IndexOf(":") - 5) + "\n";
                        }
                        if (line.Contains("selection"))
                        {
                            int selection = int.Parse(Regex.Replace(line, @"[^0-9]", ""));

                            switch (selection)
                            {
                                case 0:
                                    shapeStr = "Circle";
                                    break;
                                case 1:
                                    shapeStr = "Rectangle";
                                    break;
                                case 2:
                                    shapeStr = "Polygon";
                                    break;
                            }
                            btnText += "   " + shapeStr + "\n";
                        }
                        if (shapeStr == "Circle" && (line.Contains("cx") || line.Contains("cy") || line.Contains("radius")))
                        {
                            btnText += "   " + Regex.Replace(line, @"[^0-9a-zA-Z:]", "") + "\n";
                        }
                        else if (shapeStr == "Rectangle" && (line.Contains("xmin") || line.Contains("ymin") || line.Contains("xmax") || line.Contains("ymax")))
                        {
                            btnText += "   " + Regex.Replace(line, @"[^0-9a-zA-Z:]", "") + "\n";
                        }
                    }
                }
                btnRecentConfs.Text = btnText;
                btnRecentConfs.Name = "recentConfig" + btnNum++.ToString();
                relativePos.X = relativePos.X + RECENT_BTN_WIDTH + RECENT_BTN_GAP;
            }
            if (pnReview.HorizontalScroll.Enabled == true)
            {
                pnReview.AutoScrollMargin = new Size(50, 0);
            }

            pnReview.ResumeLayout(false);
            pnReview.PerformLayout();
        }

        private void btnRecentConf_Click(object sender, EventArgs e)
        {
            string[] confCheck = Directory.GetFiles(Path.Combine(configPath, reqDi[(int)configFileType.Recent]), "recentConfig*");

            foreach (string conf in confCheck)
            {
                string fileName = conf.Substring(conf.IndexOf("recentConfig"), conf.Length - conf.IndexOf("recentConfig") - 5);
                if (((Button)sender).Name == fileName)
                {
                    string json = File.ReadAllText(conf);
                    var jsonArray = JsonConvert.DeserializeObject<dynamic>(json);

                    //subsamplng_textboxes
                    dynamic JObject = jsonArray[3].Sub;
                    tbSubCellSize.Text = JObject.Sub_cell.ToString();

                    //outlierRemoving_textboxes
                    JObject = jsonArray[4].Outlier;
                    tbOutlierMeank.Text = JObject.mean_k.ToString();
                    tbOutlierMul.Text = JObject.multiplier.ToString();

                    //normalize_textboxes
                    JObject = jsonArray[5].Ground;
                    tbNorCellSize.Text = JObject.Ground_cell.ToString();
                    tbNorScalar.Text = JObject.scalar.ToString();
                    tbNorSlope.Text = JObject.slope.ToString();
                    tbNorThres.Text = JObject.threshold.ToString();
                    tbNorWinSize.Text = JObject.window.ToString();

                    //trunkSlice_textboxes
                    JObject = jsonArray[6].TSlice;
                    tbTrunkMinHeight.Text = JObject.T_minheight.ToString();
                    tbTrunkMaxHeight.Text = JObject.T_maxheight.ToString();

                    //CrownSlice_textboxes
                    JObject = jsonArray[7].CSlice;
                    tbCrownMinHeight.Text = JObject.C_minheight.ToString();
                    tbCrownMaxHeight.Text = JObject.C_maxheight.ToString();

                    ////treeSegment_textbox
                    JObject = jsonArray[8].Crownseg;
                    tbTreeSegNN.Text = JObject.Crown_nnearest.ToString();

                    ////trunkSegment_textboxes
                    JObject = jsonArray[10].SegmentStem;
                    tbTreeSegSmooth.Text = JObject.smoothness.ToString();
                    tbTreeSegMinDBH.Text = JObject.mindbh.ToString();
                    tbTreeSegHeightThres.Text = JObject.heightThreshold.ToString();

                    //measure_textbox
                    JObject = jsonArray[9].Measure;
                    tbMeasureNN.Text = JObject.Measure_nnearest.ToString();
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

        private void btnPreConf_Click(object sender, EventArgs e)
        {
            string[] confCheck = Directory.GetFiles(Path.Combine(configPath, reqDi[(int)configFileType.Preset]), "PresetConfig*");



            foreach (string conf in confCheck)
            {
                string fileName = conf.Substring(conf.IndexOf("presetConfig"), conf.Length - conf.IndexOf("presetConfig") - 5);
                if (((Button)sender).Name == fileName)
                {
                    string json = File.ReadAllText(conf);
                    var jsonArray = JsonConvert.DeserializeObject<dynamic>(json);

                    //subsamplng_textboxes
                    dynamic JObject = jsonArray[3].Sub;
                    tbSubCellSize.Text = JObject.Sub_cell.ToString();

                    //outlierRemoving_textboxes
                    JObject = jsonArray[4].Outlier;
                    tbOutlierMeank.Text = JObject.mean_k.ToString();
                    tbOutlierMul.Text = JObject.multiplier.ToString();

                    //normalize_textboxes
                    JObject = jsonArray[5].Ground;
                    tbNorCellSize.Text = JObject.Ground_cell.ToString();
                    tbNorScalar.Text = JObject.scalar.ToString();
                    tbNorSlope.Text = JObject.slope.ToString();
                    tbNorThres.Text = JObject.threshold.ToString();
                    tbNorWinSize.Text = JObject.window.ToString();

                    //trunkSlice_textboxes
                    JObject = jsonArray[6].TSlice;
                    tbTrunkMinHeight.Text = JObject.T_minheight.ToString();
                    tbTrunkMaxHeight.Text = JObject.T_maxheight.ToString();

                    //CrownSlice_textboxes
                    JObject = jsonArray[7].CSlice;
                    tbCrownMinHeight.Text = JObject.C_minheight.ToString();
                    tbCrownMaxHeight.Text = JObject.C_maxheight.ToString();

                    ////treeSegment_textbox
                    JObject = jsonArray[8].Crownseg;
                    tbTreeSegNN.Text = JObject.Crown_nnearest.ToString();

                    ////trunkSegment_textboxes
                    JObject = jsonArray[10].SegmentStem;
                    tbTreeSegSmooth.Text = JObject.smoothness.ToString();
                    tbTreeSegMinDBH.Text = JObject.mindbh.ToString();
                    tbTreeSegHeightThres.Text = JObject.heightThreshold.ToString();

                    //measure_textbox
                    JObject = jsonArray[9].Measure;
                    tbMeasureNN.Text = JObject.Measure_nnearest.ToString();
                }
            }

            tcMainHome.SelectedIndex = 1;
        }

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

        private void btnSettingSave_Click(object sender, EventArgs e)
        {
            /*
            //반지름, 사각형 좌표 무결성 검사
            bool integrity = CheckIntegrity();
            if (!integrity)
            {
                MessageBox.Show("좌표설정 혹은 반지름 설정에 문제가 있습니다. 수정해주세요.");
                return;
            }*/
            //변수들 초기화
            UpdateParams();

            //제이슨 파일에 쓰기
            string filePath = "..\\bin\\config.json"; ;
            try
            {
                // Read the existing JSON file
                string jsonString = File.ReadAllText(filePath);
                JArray jsonArray = JArray.Parse(jsonString);

                // Update the values in the JSON objects based on the TextBox inputs
                foreach (JObject jsonObject in jsonArray)
                {
                    /*if (jsonObject.ContainsKey("GUI"))
                    {
                        JObject guiObject = jsonObject["GUI"] as JObject;
                        if (guiObject != null && guiObject.ContainsKey("circle"))
                        {
                            JObject circleObject = guiObject["circle"] as JObject;
                            if (circleObject != null)
                            {
                                circleObject["cx"] = plot_xcenter;
                                circleObject["cy"] = plot_ycenter;
                                circleObject["radius"] = plot_radius;
                            }

                        }
                        JObject rectangleObject = guiObject["rectangle"] as JObject;
                        if (rectangleObject != null)
                        {
                            rectangleObject["xmin"] = plot_Xmin;
                            rectangleObject["ymin"] = plot_Ymin;
                            rectangleObject["xmax"] = plot_Xmax;
                            rectangleObject["ymax"] = plot_Ymax;
                        }
                        JObject result_path = guiObject["result_path"] as JObject;
                        if (result_path != null)
                        {
                            result_path["result_path"] = plot_resPath;
                        }
                    }*/

                    if (jsonObject.ContainsKey("Crop"))
                    {
                        jsonObject["Crop"]["buffer"] = crop.buffer;
                    }

                    if (jsonObject.ContainsKey("Sub"))
                    {
                        jsonObject["Sub"]["Sub_cell"] = subsampling.cellSize;
                    }
                    if (jsonObject.ContainsKey("Outlier"))
                    {
                        jsonObject["Outlier"]["method"] = outlier.method;
                        jsonObject["Outlier"]["mean_k"] = outlier.mean_k;
                        jsonObject["Outlier"]["multiplier"] = outlier.Multiplier;
                    }
                    if (jsonObject.ContainsKey("Ground"))
                    {
                        jsonObject["Ground"]["Ground_cell"] = groundseg.cellSize;
                        jsonObject["Ground"]["window"] = groundseg.windowSize;
                        jsonObject["Ground"]["slope"] = groundseg.slope;
                        jsonObject["Ground"]["scalar"] = groundseg.scalar;
                        jsonObject["Ground"]["threshold"] = groundseg.threshold;
                    }

                    if (jsonObject.ContainsKey("TSlice"))
                    {
                        jsonObject["TSlice"]["T_minheight"] = tSlice.minHeight;
                        jsonObject["TSlice"]["T_maxheight"] = tSlice.maxHeight;
                    }
                    if (jsonObject.ContainsKey("CSlice"))
                    {
                        jsonObject["CSlice"]["C_minheight"] = cSlice.minHeight;
                        jsonObject["CSlice"]["C_maxheight"] = cSlice.maxHeight;
                    }
                    if (jsonObject.ContainsKey("Crownseg"))
                    {
                        jsonObject["Crownseg"]["Crown_nnearest"] = crownSeg.CrownNN;
                    }
                    if (jsonObject.ContainsKey("Measure"))
                    {
                        jsonObject["Measure"]["Measure_nnearest"] = measure.MeasureNN;
                        jsonObject["Measure"]["minRad"] = measure.minRad;
                        jsonObject["Measure"]["maxRad"] = measure.maxRad;
                        jsonObject["Measure"]["iterations"] = measure.iterations;
                    }
                    if (jsonObject.ContainsKey("SegmentStem"))
                    {
                        jsonObject["SegmentStem"]["smoothness"] = segmentStem.smoothness;
                        jsonObject["SegmentStem"]["mindbh"] = segmentStem.minDBH;
                        jsonObject["SegmentStem"]["maxdbh"] = segmentStem.maxDBH;
                    }
                }

                // Save the modified JSON back to the file
                File.WriteAllText(filePath, jsonArray.ToString());
                MessageBox.Show("변수가 성공적으로 수정되었습니다.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating JSON file: " + ex.Message);
            }
        }

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

        private void btnSettingLoad_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("기본값을 적용하시겠습니까?\n저장되지 않은 설정값은 사라집니다.",
                "기본 설정 적용", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }

            Initialize_Params();
            FillTextboxes();
        }

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

        private void btnSettingApply_Click(object sender, EventArgs e)
        {
            UpdateParams();
            MessageBox.Show("적용되었습니다.");
            tcMainHome.SelectedIndex = 0;
        }
    }
}