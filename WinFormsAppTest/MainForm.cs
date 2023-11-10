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
using System.Security.Cryptography;
using System.Data.SQLite;
using System.Diagnostics;

namespace WinFormsAppTest
{
    //��������Ʈ �޼����. private�� �Ϲ� �޼��带 �ٸ� form���� �� �� �ְ��ϴ� ����
    internal delegate void switchEventHandler(bool onOff);

    internal delegate void presetReflectHandler(string fileDi, string fileName);

    public partial class MainForm : Form
    {
        //�ٸ� ������ ���� delegate�� ���� �޼��� ������ ���� form ����
        private PlotForm? pFrm;

        internal static string configPath = (Environment.CurrentDirectory).ToString();
        internal static string[] reqDi = { "", "RecentConfig", "PresetConfig" };

        public MainForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            MaximizeBox = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string currentPath = Directory.GetCurrentDirectory();
            DirectoryInfo parentDirectory = Directory.GetParent(currentPath);

            string res_path = Path.Combine(parentDirectory.FullName, "result");

            //db ���� �ʱ�ȭ, db ����
            create_dbFile_dbtable();
            UpdateDataInTable("gui", "result_path", res_path);
            //MessageBox.Show(SelectDataFromTable(databaseFileName, "gui", "result_path"));

            FillTextboxes();
            RegistTextBoxHandler();
            //���α׷� â ���� ��ġ ����
            Point screenSize = ((Point)Screen.PrimaryScreen.Bounds.Size);

            //���� �� �ε� �� �̺�Ʈ ��ó��(Designer.cs�� ������ ã�Ⱑ ����)
            this.Location = new Point((screenSize.X - this.Width) / 2, (screenSize.Y - this.Height) / 2);

            //���α׷� ���� ���� ����(Ŀ���� �Ķ���ʹ� Designer.cs���� ���� �Ұ�)
            customPanels_Load();
            pnMain.isBorder = false;

            //�������� �� ������Ʈ �̺�Ʈ ����
            mainForm_AddEvent();
        }

        //���� �� �ε� �� �̺�Ʈ ��ó��(Designer.cs�� ������ ã�Ⱑ ����)
        private void mainForm_AddEvent()
        {
            //Ȩ��ư �̺�Ʈ
            btnHome.Click += btnHome_Click;
            //������ư �̺�Ʈ
            btnSettings.Click += btnSettings_Click;
            //���α׷� ���� ��ư �̺�Ʈ
            btnStart.Click += btnStart_Click;

            //�� �Ʒ��� ����â�� CustomPanel ��ü�� �̺�Ʈ
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
        }

        //CustomPanel �� �� �׵θ� ����(Designer.cs���� �����ϸ� ������ �� ������)
        private void customPanels_Load()
        {
            Color customPanelColor = Color.Gray;

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

        //���� ����
        /*private string textLengthTrim(CustomBtn customBtn, string text)
        {
            string trimedText = text;

            Size size = TextRenderer.MeasureText(trimedText, customBtn.Font);
            double diff = size.Width - customBtn.Width + 5;     //5�� ������

            if (diff < 0)
            {
                return trimedText;
                trimedText = trimedText.Substring(0, trimedText.Length - 1);
                size = TextRenderer.MeasureText(trimedText + "...", customBtn.Font);
                diff = size.Width - customBtn.Width;
            }
            }
            pnSideMenu.ResumeLayout(false);
            pnSideMenu.PerformLayout();
        }

            return trimedText + "...";
        }*/

        //���̵�޴� ��ư ���� �̺�Ʈ ó�� �ڵ�
        private void btnStart_Click(object sender, EventArgs e)
        {
            pFrm = new PlotForm(this);
            pFrm.Owner = this;
            pFrm.enableMainFormBtns += new switchEventHandler(switchCoreBtns);
            pFrm.attachStartBtn += new switchEventHandler(startBtnAttach);

            pFrm.Show();

        private void switchCoreBtns(bool onOff)
        {
            switchBtns(tpSettings, onOff);
        }
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
            if (tcMainHome.SelectedIndex == 0)
            {
                return;
            }

            bool[] applyChecker = new bool[9];
            bool result = true;
            DialogResult dialogResult = DialogResult.No;

            //normalize_textboxes
            applyChecker[0] = tbNorCellSize.Text == SelectDataFromTable(databaseFileName, "filters_smrf", "cell");
            applyChecker[1] = tbNorScalar.Text == SelectDataFromTable(databaseFileName, "filters_smrf", "scalar");
            applyChecker[2] = tbNorSlope.Text == SelectDataFromTable(databaseFileName, "filters_smrf", "slope");
            applyChecker[3] = tbNorThres.Text == SelectDataFromTable(databaseFileName, "filters_smrf", "threshold");
            applyChecker[4] = tbNorWinSize.Text == SelectDataFromTable(databaseFileName, "filters_smrf", "window");
            //trunkSlice_textboxes
            applyChecker[5] = tbTrunkMinHeight.Text == SelectDataFromTable(databaseFileName, "filters_range_trunk", "minheight");
            applyChecker[6] = tbTrunkMaxHeight.Text == SelectDataFromTable(databaseFileName, "filters_range_trunk", "maxheight");
            applyChecker[7] = tbTrunkSmooth.Text == SelectDataFromTable(databaseFileName, "csp_segmentstem", "smoothness");

            //CrownSlice_textboxes
            applyChecker[8] = tbCrownMinHeight.Text == SelectDataFromTable(databaseFileName, "filters_range_crown", "minheight");

            foreach (bool checker in applyChecker)
            {
                result = (result && result == checker);
            }
            string[] confCheck = Directory.GetFiles(fileDi, "recentConfig*");
            string fileName = ((Button)sender).Name;

            string? csvLines;

            using (StreamReader sr = new StreamReader(Path.Combine(fileDi, fileName)))
            //�� Ŭ�� �� ������ ����
            if (dialogResult == DialogResult.Yes)
            {
                btnSettingApply_Click(sender, e);
            }
            //�ƴϿ� Ŭ�� �� DB�� �о ���󺹱�
            else if (dialogResult == DialogResult.No)
            {
                FillTextboxes();
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

            if (result == false)
            {
                dialogResult = MessageBox.Show("���� ������ ������� �ʾҽ��ϴ�.\n�����Ͻðڽ��ϱ�?", "", MessageBoxButtons.YesNo);
            }

        private void btnRecentConf_Up(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Invalidate();
        }
        //preConfig ��ư Ŭ�� �̺�Ʈ ó�� �ڵ�
        private void btnPreConf_Click(object sender, EventArgs e)
        {
            string[] confCheck = Directory.GetFiles(Path.Combine(configPath, reqDi[(int)configFileType.Preset]), "PresetConfig*");

            foreach (string conf in confCheck)
            {
                string fileName = conf.Substring(conf.IndexOf("presetConfig"), conf.Length - conf.IndexOf("presetConfig") - 4);
                if (((Button)sender).Name == fileName + ".csv")
                {
                    read_csv(conf);
                    FillTextboxes();
                }
            }
        {
            MessageBox.Show("����Ǿ����ϴ�.");
            tcMainHome.SelectedIndex = 0;
        }
        //�� �Ʒ� 4�� �޼��� ����â �Ķ���� �ǳ� ���콺 �̺�Ʈ
        //��ҹ�ư
        //�������� �ʰ� �⺻ config ������ �ؽ�Ʈ �ڽ� �� ��ü ��
        //����ȭ������ �̵�
        private void btnSettingCancel_Click(object sender, EventArgs e)
        {
            FillTextboxes();
            tcMainHome.SelectedIndex = 0;
        }
        private void pnSettingAll_MouseLeave(object sender, EventArgs e)
        {
            if (sender is CustomPanel)
            {
                CustomPanel cPanel = (CustomPanel)sender;

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process[] allProc = Process.GetProcesses();
                cPanel.Invalidate();
            foreach (Process procs in allProc)
            {
                try
                {
                    if (procs.ProcessName == "ForestLi")
                        procs.Kill();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            {
                mFrm = new ManageForm();
                mFrm.mainPaint += new customEventHandler(this.preConfBtnLoad);
            }

            //FactoryReset(csv_path);
            DeleteAllTables(databaseFileName, tablename);
            CreateTable(databaseFileName, tablename);
            insert_initial_data();
                pFrm = new PlotForm(this);
                pFrm.configTouch += new configHandler(MakeConfig);
                pFrm.mainPaint += new customEventHandler(recentConfBtnLoad);
            }
            MakeConfig(configFileType.Preset);

            mFrm.ShowDialog();
            preConfBtnLoad();
        }

        //������ ���� ��ư Ŭ�� �̺�Ʈ
        private void btnPresetManage_Click(object sender, EventArgs e)
        {
            if (mFrm == null)
            {
                mFrm = new ManageForm();
                mFrm.mainPaint += new customEventHandler(this.preConfBtnLoad);
            }
            mFrm.ShowDialog();
        }

        private void startBtnAttach(bool onOff)
        {
            btnStart.Visible = onOff;
        }

        //�⺻�� �ҷ����� ��ư
        private void btnSettingLoad_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("�⺻���� �����Ͻðڽ��ϱ�?\n������� ���� �������� ������ϴ�.",
                "�⺻ ���� ����", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            read_csv(csv_path);
            FillTextboxes();
        }

        //���α׷� ���۽� ���α׷���,start ��ư ������ ���� �̺�Ʈ
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

        //�����ϱ� ��ư �̺�Ʈ
        private void btnSettingApply_Click(object sender, EventArgs e)
        {
            UpdateParams();
            MessageBox.Show("����Ǿ����ϴ�.");
            tcMainHome.SelectedIndex = 0;
        }

        //��ҹ�ư 
        private void btnSettingCancel_Click(object sender, EventArgs e)
        {
            FillTextboxes();
            tcMainHome.SelectedIndex = 0;
        }

        //�����ʱ�ȭ ��ư
        private void btn_factory_reset_Click(object sender, EventArgs e)
        {
            FactoryReset(csv_path);
            MessageBox.Show("���� ������ �ʱ�ȭ �Ǿ����ϴ�.");
        }
    }
}