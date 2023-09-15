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
    //��������Ʈ �޼����. private�� �Ϲ� �޼��带 �ٸ� form���� �� �� �ְ��ϴ� ����
    internal delegate Dictionary<string, double> plotDataHandler();

    internal delegate void customEventHandler();

    internal delegate void configHandler(configFileType type);

    internal delegate void setIntEventHandler(int setValue);

    internal delegate void setStringEventHandler(string setValue);

    internal delegate void switchEventHandler(bool onOff);

    internal delegate void presetReflectHandler(string fileDi, string fileName);

    public partial class MainForm : Form
    {
        //�Ʒ� enum ������ �������� �ִ�ȭ ���� �۾�
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

        //plot���� PlotForm���κ��� �ޱ� ���� delegate
        internal plotDataHandler plotSender;

        //�ٸ� ������ ���� delegate�� ���� �޼��� ������ ���� form ����
        private PlotForm? pFrm;
        private ManageForm? mFrm;

        //���̵� �޴��� �ִ�, �ּ� �� ũ�� �� �� ����
        const int MAX_SLIDING_WIDTH = 384;
        const int MIN_SLIDING_WIDTH = 65;
        const int SLIDING_GAP = MAX_SLIDING_WIDTH - MIN_SLIDING_WIDTH;

        //���̵� �޴��� Ȯ��, ��ҿ� ���� �޴� ��ư ũ��
        const int MIN_BTN_WIDTH = 42;
        const int MAX_BTN_WIDTH = 345;

        //���̵� �޴��� ������ ù ��ư�� ��ġ �� ũ��, ����
        //���� ��ư �߰��� ���� �ʿ�
        Point PRESET_BTN_POS = new Point(8, 30);
        const int PRESET_BTN_HEIGHT = 45;
        const int PRESET_BTN_GAP = 2;

        //�ֱ� �۾� ��� ù �߰� ��ġ �� ũ��, ����
        //���� ��ư �߰��� ���� �ʿ�
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
            lbPresetManage.Text = "����� ����";
            pnSideMenu.Controls.Add(lbPresetManage);


            pnSideMenu.Width = MIN_SLIDING_WIDTH;
            tcMainHome.Left -= SLIDING_GAP / 2;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!File.Exists(Path.Combine(basePath, "config.csv")))
            {
                MessageBox.Show("config.csv������ �����ϴ�. ���α׷� ���� ������ �������ּ���.\n" +
                    $"filepath : {Environment.CurrentDirectory}", "�⺻ ���� ���� ����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            //�⺻ config.csv ���� �ݿ�
            read_csv(csv_path, csv_data);
            FillTextboxes();
            RegistTextBoxHandler();

            //���α׷� â ���� ��ġ ����
            Point screenSize = ((Point)Screen.PrimaryScreen.Bounds.Size);
            this.Location = new Point((screenSize.X - this.Width) / 2, (screenSize.Y - this.Height) / 2);

            //�������� �� ������Ʈ �̺�Ʈ ����
            mainForm_AddEvent();

            //����� �������� �ֱ� �۾� ��� ��ư ���� ����
            preConfBtnLoad();
            recentConfBtnLoad();
        }

        //���� �� �ε� �� �̺�Ʈ ��ó��(Designer.cs�� ������ ã�Ⱑ ����)
        private void mainForm_AddEvent()
        {
            //Ȩ��ư �̺�Ʈ
            btnHome.Click += btnHome_Click;
            //������ư �̺�Ʈ
            btnSettings.Click += btnSettings_Click;
            //���̵� �޴� ����/���� ��ư �̺�Ʈ
            btnSideMenu.Click += btnSlideMenu_Click;
            //���α׷� ���� ��ư �̺�Ʈ
            btnStart.Click += btnStart_Click;

            //�� �Ʒ��� ����â�� CustomPanel ��ü�� �̺�Ʈ
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

            //�� ������ ����â setting textBox �̺�Ʈ
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

        //CustomPanel �� �� �׵θ� ����(Designer.cs���� �����ϸ� ������ �� ������)
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

        //������ ���Ǳ� ���� ������ŭ ��ư �ε�
        private void preConfBtnLoad()
        {
            string fileDi = Path.Combine(basePath, reqDi[(int)configFileType.Preset]);

            if (!Directory.Exists(fileDi))
            {
                Directory.CreateDirectory(fileDi);
            }

            //������ ���Ǳ� ���� ���
            string[] confCheck = Directory.GetFiles(fileDi, "presetConfig*");

            //���Ե� ��ư ��ġ
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
                btnPreConfs.Font = new Font("���� ���", 16F, FontStyle.Bold, GraphicsUnit.Point);

                btnPreConfs.TextAlign = ContentAlignment.MiddleLeft;


                btnPreConfs.Text = "00/00/00               ������";

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
                btnPreConfs.Font = new Font("���� ���", 16F, FontStyle.Bold, GraphicsUnit.Point);
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
                            string lineData = line.Split(',')[3].Replace('��', ',');
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

        //�ֱ� �۾� ���Ǳ� ���� ������ŭ ��ư �ε�
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
                btnRecentConfs.Font = new Font("���� ���", 12F, FontStyle.Bold, GraphicsUnit.Point);

                btnRecentConfs.TextAlign = ContentAlignment.MiddleLeft;

                btnRecentConfs.Text = "  �ֱ� �۾��� ����� ǥ���մϴ�\n  ��¥,\n  ������ Las���ϸ�,\n  �߶� ���,\n  ��翡 ���� Plot��\n  ������ �����˴ϴ�.";

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
                btnRecentConfs.Font = new Font("���� ���", 12F, FontStyle.Regular, GraphicsUnit.Point);

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
                            btnText += "  ��    ¥:  " + line.Split(',')[3] + Environment.NewLine;
                            continue;
                        }
                        else if (line.Contains("Lasfilename"))
                        {
                            string lasName = Path.GetFileName(line.Split(',')[3]);
                            //las���� �̸��� �ʹ� ��� �ؽ�Ʈ�� �̻������Ƿ�
                            //���� �̻� ���̸� �ڸ��� ...���� ����
                            if (lasName.Length > 16)
                            {
                                lasName = lasName.Substring(0, 16) + "...";
                            }
                            btnText += ("  Las����:  " + lasName + Environment.NewLine);
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
                            btnText += ("  ��    ��:  " + shapeStr + Environment.NewLine);
                            continue;
                        }
                        string[] plotData;
                        if (shapeStr != "" && line.Split(',')[2].Equals(shapeStr))
                        {
                            btnText += "\n  Plot ������\n";
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

        //preConfig ��ư Ŭ�� �̺�Ʈ ó�� �ڵ�
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

        //preConfig ��ư�� �ƿ� ���� ���� ����Ʈ ���̵� ��ư �̺�Ʈ
        private void btnPreDefault_Click(object sender, EventArgs e)
        {
            tcMainHome.SelectedIndex = 1;

            flashCount = 0;
            pnSettingPreset.fillColor = Color.FromArgb(189, 189, 189);
            timerFlashPanel.Start();
        }

        //����Ʈ ���̵� ��ư�� �� �̺�Ʈ
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

        //recetnConfig ��ư ���� �̺�Ʈ ó�� �ڵ�
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

        //�ֱ� ��� ��ư�� ������ las���� �̸� ���� ǥ��
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

        //���̵�޴� ��ư ���� �̺�Ʈ ó�� �ڵ�
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
                dialogResult = MessageBox.Show("���� ������ ������� �ʾҽ��ϴ�.\n�����Ͻðڽ��ϱ�?", "", MessageBoxButtons.YesNoCancel);
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

            //plotâ�� ��޸����� ���� ��� ���� �� ���� ���� ���� ��� ��ư ��Ȱ��ȭ
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

        //���̵� �޴� ���ݱ�
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

        //�Ʒ� �޼��� 4�� Ŀ���� ����ǥ���ٷ� ���� â �̵� �̺�Ʈ ���� ����
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Location.Y <= 40)
            {
                relativeMformPos = e.Location;
                isMformDrag = true;
            }
        }
        //���α׷� ������ ��Ÿ���� ���� ������ ������ ���� ���� �̺�Ʈ
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

        //�����ϱ� ��ư �̺�Ʈ
        //������ �� ����� �������̳� �ֱ� ����� Ȱ��ȭ �Ǿ�������
        //�ش� config���� ���� ���� csv_data�� ���� ���Ͽ�
        //��Ȱ��ȭ ���� ����
        private void btnSettingApply_Click(object sender, EventArgs e)
        {
            configFileType confType;
            string fileDi = "";
            string fileName = "";
            string filePath;

            //��쿡 ���� ���� ��� ����
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

            //csv_data ����Ʈ�� �ؽ�Ʈ �ڽ��� �� ����
            UpdateParams(csv_data);

            //�ֱ� ��� Ȥ�� ����� ������ csv �ҷ��� ����Ʈ
            List<List<string>> SelectedCsvData = new List<List<string>>();

            //�ֱ� ��� Ȥ�� ����� ������ csv �ҷ�����
            read_csv(filePath, SelectedCsvData);

            //SelectedCsvData ����Ʈ�� �ݺ��� ������ string���� ����
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

            //�� ������ �ؽ�Ʈ �ڽ� ���� �ݿ��� ����Ʈ
            UpdateParams(SelectedCsvData);
            //SelectedCsvData ����Ʈ�� �ݺ��� ������ string���� ����
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
            //�� ����Ʈ
            //(������ ����â �ؽ�Ʈ �ڽ� �� �ݿ��� csv_data,
            //�ٸ� ���� Ŭ���Ǿ� �ִ� config�� csv_data)�� ���Ͽ�
            //�� ����Ʈ�� �ٸ��� ���� ����
            if (!tmp1.Equals(tmp2) && fileType.Equals("Preset"))
            {
                foreach (Control cont in pnSidePreset.Controls)
                {
                    //���õ� ��ư activate(Preset/Recent) ������ ���ؼ� �� �ٲٱ� 
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
                    //���õ� ��ư activate(Preset/Recent) ������ ���ؼ� �� �ٲٱ� 
                    if (cont.Name.Equals("recentConfig" + activateRecent.ToString()))
                    {
                        cont.BackColor = Color.Khaki;
                        cont.Invalidate();
                        activateRecent = -1;
                    }
                }
            }

            //��Ÿ �������� ������ �ؽ�Ʈ �ڽ����� �ٷ� �ݿ�������,(UpdateParams�޼���)
            //plot������ �����ϱ� ������ �� �Ͻ������� �ݿ��� ���� ���⿡ 
            //���� apply_temp�޼��忡�� �ӽ÷� ���� plot�� ����ü�� ���� ����
            //���α׷��� ���� �ݿ��Ǵ� �������� gui ����ü�� �Űܴ�´�
            if (fileType != "")
            {
                apply_temp();
            }
            else
            {
                gui.loadPath = "";
            }
            MessageBox.Show("����Ǿ����ϴ�.");
            tcMainHome.SelectedIndex = 0;
        }

        //��ҹ�ư
        //�������� �ʰ� ������ ���� �Ǿ��ִ� ������ �ؽ�Ʈ �ڽ� �� ��ü ��
        //����ȭ������ �̵�
        private void btnSettingCancel_Click(object sender, EventArgs e)
        {
            FillTextboxes();
            activatePreset = -1;
            activateRecent = -1;
            preConfBtnLoad();
            recentConfBtnLoad();
        }

        //�����ϱ� ��ư Ŭ�� �̺�Ʈ
        private void btnSettingSave_Click(object sender, EventArgs e)
        {
            //������ �ʱ�ȭ
            UpdateParams(csv_data);
            // csv �ۼ�
            try
            {
                write_csv(csv_path);
                MessageBox.Show("CSV ������ �����Ǿ����ϴ�.");
            }
            catch { };
        }

        //����� ������ ���� ��ư Ŭ�� �̺�Ʈ
        //�ֱ� ��ϰ� �޸� ����� ������ ������ plot�� �� �����͸� �������� ����.
        //(new!) ����� ������ Ŭ�� ������ �� �� ���� �� �����ϸ� ���� ����(����)
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
                MessageBox.Show("����� ������ 10�� ������ ���� �����մϴ�");
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
                MessageBox.Show("����Ǿ����ϴ�");
            }
        }

        //����� ������ ���� ��ư Ŭ�� �̺�Ʈ
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

        //�⺻�� �ҷ����� ��ư
        private void btnSettingLoad_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("�⺻���� �����Ͻðڽ��ϱ�?\n������� ���� �������� ������ϴ�.",
                "�⺻ ���� ����", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
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

        //�����ʱ�ȭ ��ư
        private void btn_factory_reset_Click(object sender, EventArgs e)
        {
            FactoryReset(csv_path);
        }
        private void btn_factory_reset_MouseHover(object sender, EventArgs e)
        {
            ttMainInfo.SetToolTip(btnFactoryReset, "�⺻ �������� �ʱ�� �ǵ����ϴ�\n������ �⺻���� ���� ������� �ʽ��ϴ�");
        }

        //���� ȭ���� Recent Task ����
        private void btnRecentInfo_MouseHover(object sender, EventArgs e)
        {
            ttMainInfo.SetToolTip(btnRecentInfo, "�ֱ� �۾��� �������� �����մϴ�.\n����� Ŭ���� �� ����â�� �ݿ��˴ϴ�.(���� ���� �ʿ�)");
        }

        //���� ���̵� �޴��� ����� ������ ���� ��ư ����
        private void btnPresetManage_MouseHover(object sender, EventArgs e)
        {
            ttMainInfo.SetToolTip(btnPresetManage, "����� ���� ����â�� ���ϴ�");
        }

        //���α׷� ���۽� ���α׷���,start ��ư ������ ���� �̺�Ʈ
        //delegate ���Ͽ� �ٸ� ������ ����
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