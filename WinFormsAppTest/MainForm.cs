using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WinFormsAppTest.Properties;
using System.Dynamic;
using System.Text.RegularExpressions;
using static WinFormsAppTest.MainForm;

namespace WinFormsAppTest
{
    internal delegate Dictionary<string, double> plotDataHandler();

    internal delegate void customEventHandler();

    internal delegate void configHandler(configFileType type);

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

        //���̵� �޴��� �ִ�, �ּ� �� ũ�� �� �� ����
        const int MAX_SLIDING_WIDTH = 384;
        const int MIN_SLIDING_WIDTH = 65;
        const int SLIDING_GAP = MAX_SLIDING_WIDTH - MIN_SLIDING_WIDTH;

        //���̵� �޴��� Ȯ��, ��ҿ� ���� �޴� ��ư ũ��
        const int MIN_BTN_WIDTH = 42;
        const int MAX_BTN_WIDTH = 370;

        //���̵� �޴��� ������ ù ��ư�� ��ġ �� ũ��, ����
        Point PRESET_BTN_POS = new Point(12, 170);
        const int PRESET_BTN_WIDTH = 370;
        const int PRESET_BTN_HEIGHT = 45;
        const int PRESET_BTN_GAP = 5;

        //�ֱ� �۾� ��� ù �߰� ��ġ �� ũ��, ����
        Point RECENT_BTN_POS = new Point(50, 27);
        const int RECENT_BTN_WIDTH = 205;
        const int RECENT_BTN_HEIGHT = 165;
        const int RECENT_BTN_GAP = 30;

        bool menuOpen = false;

        Point relativeMformPos = new Point();

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

        //���� �� �ε� �� �̺�Ʈ ��ó��(Designer.cs�� ������ ã�Ⱑ ����)
        private void mainForm_AddEvent()
        {
            //Ȩ��ư �̺�Ʈ
            btnHome.Click += btnHome_Click;
            //������ư �̺�Ʈ
            btnSettings.Click += btnSettings_Click;
            //���̵� �޴� ����/���� ��ư �̺�Ʈ
            btnSlideMenu.Click += btnSlideMenu_Click;
            //â �ݱ� ��ư �̺�Ʈ
            btnClose.Click += btnClose_Click;
            //���α׷� ���� ��ư �̺�Ʈ
            btnStart.Click += btnStart_Click;

            //�� �Ʒ��� ���� ����â�� CustomPanel ��ü�� �̺�Ʈ
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

        //CustomPanel �� �� �׵θ� ����(Designer.cs���� �����ϸ� ������ �� ������)
        private void customPanels_Load()
        {
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

        //������ ���Ǳ� ���� ������ŭ ��ư �ε�
        private void preConfBtnLoad()
        {
            pnSideMenu.Controls.Clear();

            pnSideMenu.Controls.Add(btnHome);
            pnSideMenu.Controls.Add(btnHide);
            pnSideMenu.Controls.Add(btnSettings);
            pnSideMenu.Controls.Add(btnSlideMenu);
            pnSideMenu.Controls.Add(btnPresetManage);

            //������ ���Ǳ� ���� ���
            string[] confCheck = Directory.GetFiles(Path.Combine(configPath, reqDi[(int)configFileType.Preset]), "PresetConfig*");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
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
                btnPreConfs.Image = (Image)resources.GetObject("btnPreConf.Image");
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

        //�ֱ� �۾� ���Ǳ� ���� ������ŭ ��ư �ε�
        private void recentConfBtnLoad()
        {
            pnReview.Controls.Clear();

            string[] confCheck = Directory.GetFiles(Path.Combine(configPath, reqDi[(int)configFileType.Recent]), "RecentConfig*");
            Array.Sort(confCheck);

            Point relativePos = RECENT_BTN_POS;
            int btnNum = confCheck.Length - 1;
            string btnText = "";

            pnReview.SuspendLayout();

            foreach (string conf in confCheck)
            {
                btnText = "";

                CustomBtn btnRecentConfs = new CustomBtn();
                btnRecentConfs.MouseClick += btnRecentConf_Click;

                pnReview.Controls.Add(btnRecentConfs);
                btnRecentConfs.Location = relativePos;
                btnRecentConfs.Width = RECENT_BTN_WIDTH;
                btnRecentConfs.Height = RECENT_BTN_HEIGHT;
                btnRecentConfs.Margin = new Padding(4, 8, 4, 4);
                btnRecentConfs.BackColor = Color.MintCream;
                btnRecentConfs.BackgroundColor = Color.MintCream;
                btnRecentConfs.BorderColor = Color.Transparent;
                btnRecentConfs.BorderRadius = 20;
                btnRecentConfs.BorderSize = 0;
                btnRecentConfs.FlatAppearance.BorderSize = 0;
                btnRecentConfs.FlatAppearance.MouseDownBackColor = Color.FromArgb(246, 255, 253);
                btnRecentConfs.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 255, 250);
                btnRecentConfs.FlatStyle = FlatStyle.Flat;
                btnRecentConfs.TextAlign = ContentAlignment.MiddleLeft;
                btnRecentConfs.Font = new Font("���� ���", 12F, FontStyle.Regular, GraphicsUnit.Point);
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
                        if (shapeStr == "Circle" && line.Contains("cx") || line.Contains("cy") || line.Contains("radius"))
                        {
                            btnText += "   " + Regex.Replace(line, @"[^0-9a-zA-Z:]", "") + "\n";
                        }
                        else if (shapeStr == "Rectangle" && line.Contains("xmin") || line.Contains("ymin") || line.Contains("xmax") || line.Contains("ymax"))
                        {
                            btnText += "   " + Regex.Replace(line, @"[^0-9a-zA-Z:]", "") + "\n";
                        }
                    }
                }
                btnRecentConfs.Text = btnText;
                btnRecentConfs.Name = "recentConfig" + btnNum--.ToString();
                relativePos.X = relativePos.X + RECENT_BTN_WIDTH + RECENT_BTN_GAP;
            }
            pnReview.ResumeLayout(false);
            pnReview.PerformLayout();
        }

        private void btnRecentConf_Click(object sender, EventArgs e)
        {
            string[] confCheck = Directory.GetFiles(Path.Combine(configPath, reqDi[(int)configFileType.Preset]), "RecentConfig*");
            if (MessageBox.Show("�ش� ������ �����Ͻðڽ��ϱ�?\n������� ���� �������� ������ϴ�.",
                "�ֱ� �۾���� ����", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }

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
        }

        private void btnPreConf_Click(object sender, EventArgs e)
        {
            string[] confCheck = Directory.GetFiles(Path.Combine(configPath, reqDi[(int)configFileType.Preset]), "PresetConfig*");
            if (MessageBox.Show("������ �������� �����Ͻðڽ��ϱ�?\n������� ���� �������� ������ϴ�.",
                "������ ����", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
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
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (pFrm == null)
            {
                pFrm = new PlotForm(this);
                pFrm.configTouch += new configHandler(MakeConfig);
                pFrm.mainPaint += new customEventHandler(recentConfBtnLoad);
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

        //���̵� �޴� ���ݱ�
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

        //�� �Ʒ� 4�� �޼��� ����â �Ķ���� �ǳ� ���콺 �̺�Ʈ
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

        //�Ʒ� �޼��� 3�� Ŀ���� ����ǥ���ٷ� ���� â �̵� �̺�Ʈ ���� ����
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




        /// <summary>
        /// config �����ϱ� ��ư ���� �ʿ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSettingSave_Click(object sender, EventArgs e)
        {
            /*
            //������, �簢�� ��ǥ ���Ἲ �˻�
            bool integrity = CheckIntegrity();
            if (!integrity)
            {
                MessageBox.Show("��ǥ���� Ȥ�� ������ ������ ������ �ֽ��ϴ�. �������ּ���.");
                return;
            }*/
            //������ �ʱ�ȭ
            UpdateParams();

            //���̽� ���Ͽ� ����
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
                MessageBox.Show("������ ���������� �����Ǿ����ϴ�.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating JSON file: " + ex.Message);
            }
        }

        private void btnPresetSave_Click(object sender, EventArgs e)
        {
            MakeConfig(configFileType.Preset);
            if (mFrm == null)
            {
                mFrm = new ManageForm();
                mFrm.mainPaint += new customEventHandler(this.preConfBtnLoad);
            }
            preConfBtnLoad();
            mFrm.ShowDialog();
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
    }
}