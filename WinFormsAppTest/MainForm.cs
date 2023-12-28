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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsAppTest
{
    //��������Ʈ �޼����. private�� �Ϲ� �޼��带 �ٸ� form���� �� �� �ְ��ϴ� ����
    internal delegate void switchEventHandler(bool onOff);

    public partial class MainForm : Form
    {
        //�ٸ� ������ ���� delegate�� ���� �޼��� ������ ���� form ����
        private PlotForm? pFrm;

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
            this.Location = new Point((screenSize.X - this.Width) / 2, (screenSize.Y - this.Height) / 2);

            //���α׷� ���� ���� ����(Ŀ���� �Ķ���ʹ� Designer.cs���� ���� �Ұ�)
            customPanels_Load();
            pnMain.isBorder = false;
            tgBtnSettingFileDel.OffBackColor = Color.White;
            tgBtnSettingFileDel.OffToggleColor = Color.Black;
            tgBtnSettingFileDel.OnBackColor = Color.Black;
            tgBtnSettingFileDel.OnToggleColor = Color.White;
            //tgBtnSettingFileDel.SolidStyle = false;

            //lbSettingToggle.Text = tgBtnSettingFileDel.Checked ? "����" : "����";

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
            pnSettingReset.MouseDown += pnSettingAll_MouseDown;
            pnSettingReset.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingReset.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingReset.MouseUp += pnSettingAll_MouseUp;

            pnSettingFileDel.MouseDown += pnSettingAll_MouseDown;
            pnSettingFileDel.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingFileDel.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingFileDel.MouseUp += pnSettingAll_MouseUp;

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

            pnSettingCrown2.MouseDown += pnSettingAll_MouseDown;
            pnSettingCrown2.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingCrown2.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingCrown2.MouseUp += pnSettingAll_MouseUp;

            pnSettingCrown3.MouseDown += pnSettingAll_MouseDown;
            pnSettingCrown3.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingCrown3.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingCrown3.MouseUp += pnSettingAll_MouseUp;
        }

        //CustomPanel �� �� �׵θ� ����(Designer.cs���� �����ϸ� ������ �� ������)
        private void customPanels_Load()
        {
            Color customPanelColor = Color.Gray;

            pnSettingReset.BackColor = Color.Transparent;
            pnSettingReset.isFill = true;
            pnSettingReset.isBorder = false;
            pnSettingReset.fillColor = customPanelColor;

            pnSettingFileDel.BackColor = Color.Transparent;
            pnSettingFileDel.isFill = true;
            pnSettingFileDel.isBorder = false;
            pnSettingFileDel.fillColor = customPanelColor;

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

            pnSettingCrown2.BackColor = Color.Transparent;
            pnSettingCrown2.isFill = true;
            pnSettingCrown2.isBorder = false;
            pnSettingCrown2.fillColor = customPanelColor;

            pnSettingCrown3.BackColor = Color.Transparent;
            pnSettingCrown3.isFill = true;
            pnSettingCrown3.isBorder = false;
            pnSettingCrown3.fillColor = customPanelColor;
        }

        //���̵�޴� ��ư ���� �̺�Ʈ ó�� �ڵ�
        private void btnStart_Click(object sender, EventArgs e)
        {
            pFrm = new PlotForm(this);
            pFrm.Owner = this;
            pFrm.enableMainFormBtns += new switchEventHandler(switchCoreBtns);
            pFrm.attachStartBtn += new switchEventHandler(startBtnAttach);

            pFrm.Show();

            //plotâ�� ��޸����� ���� ��� ���� �� ���� ���� ���� ��� ��ư ��Ȱ��ȭ
            switchCoreBtns(false);
        }

        private void switchCoreBtns(bool onOff)
        {
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
            if (tcMainHome.SelectedIndex == 0)
            {
                return;
            }

            bool[] applyChecker = new bool[12];
            bool result = true;
            DialogResult dialogResult = DialogResult.No;

            //del_inter
            applyChecker[0] = tgBtnSettingFileDel.Checked == (SelectDataFromTable(databaseFileName, "gui", "del_inter") == "true" ? true : false);

            //normalize_textboxes
            applyChecker[1] = tbNorCellSize.Text == SelectDataFromTable(databaseFileName, "filters_smrf", "cell");
            applyChecker[2] = tbNorScalar.Text == SelectDataFromTable(databaseFileName, "filters_smrf", "scalar");
            applyChecker[3] = tbNorSlope.Text == SelectDataFromTable(databaseFileName, "filters_smrf", "slope");
            applyChecker[4] = tbNorThres.Text == SelectDataFromTable(databaseFileName, "filters_smrf", "threshold");
            applyChecker[5] = tbNorWinSize.Text == SelectDataFromTable(databaseFileName, "filters_smrf", "window");
            //trunkSlice_textboxes
            applyChecker[6] = tbTrunkMinHeight.Text == SelectDataFromTable(databaseFileName, "filters_range_trunk", "minheight");
            applyChecker[7] = tbTrunkMaxHeight.Text == SelectDataFromTable(databaseFileName, "filters_range_trunk", "maxheight");
            applyChecker[8] = tbTrunkSmooth.Text == SelectDataFromTable(databaseFileName, "csp_segmentstem", "smoothness");

            //CrownSlice_textboxes
            applyChecker[9] = tbCrownVoxel.Text == SelectDataFromTable(databaseFileName, "csp_segmentcrown", "voxel_length");
            applyChecker[10] = tbCrownRadius.Text == SelectDataFromTable(databaseFileName, "csp_segmentcrown", "crown_radius");
            applyChecker[11] = tbCrownMinHeight.Text == SelectDataFromTable(databaseFileName, "filters_range_crown", "minheight");

            foreach (bool checker in applyChecker)
            {
                result = (result && result == checker);
            }

            if (result == false)
            {
                dialogResult = MessageBox.Show("���� ������ ������� �ʾҽ��ϴ�.\n�����Ͻðڽ��ϱ�?", "", MessageBoxButtons.YesNo);
            }

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
            tcMainHome.SelectedIndex = 0;
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

                foreach (Control control in cPanel.Controls)
                {
                    control.Invalidate();
                }
            }
        }
        private void pnSettingAll_MouseLeave(object sender, EventArgs e)
        {
            if (sender is CustomPanel)
            {
                CustomPanel cPanel = (CustomPanel)sender;

                cPanel.fillColor = Color.Gray;
                cPanel.Invalidate();

                foreach (Control control in cPanel.Controls)
                {
                    control.Invalidate();
                }
            }
        }
        private void pnSettingAll_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is CustomPanel)
            {
                CustomPanel cPanel = (CustomPanel)sender;

                cPanel.fillColor = Color.Silver;
                cPanel.Invalidate();

                foreach (Control control in cPanel.Controls)
                {
                    control.Invalidate();
                }
            }
        }
        private void pnSettingAll_MouseUp(object sender, MouseEventArgs e)
        {
            if (sender is CustomPanel)
            {
                CustomPanel cPanel = (CustomPanel)sender;

                cPanel.fillColor = Color.DarkGray;
                cPanel.Invalidate();

                foreach (Control control in cPanel.Controls)
                {
                    control.Invalidate();
                }
            }
        }

        //�����ϱ� ��ư �̺�Ʈ
        private void btnSettingApply_Click(object sender, EventArgs e)
        {
            UpdateParams();

            MessageBox.Show("����Ǿ����ϴ�.");
            tcMainHome.SelectedIndex = 0;
        }

        //��ҹ�ư
        //�������� �ʰ� �⺻ config ������ �ؽ�Ʈ �ڽ� �� ��ü ��
        //����ȭ������ �̵�
        private void btnSettingCancel_Click(object sender, EventArgs e)
        {
            FillTextboxes();
            tcMainHome.SelectedIndex = 0;
        }

        private void startBtnAttach(bool onOff)
        {
            btnStart.Visible = onOff;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process[] allProc = Process.GetProcesses();

            foreach (Process procs in allProc)
            {
                try
                {
                    if (procs.ProcessName == "ForestLi")
                        procs.Kill();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Main Closing Error: " + ex.Message);
                    Application.Exit();
                }
            }
        }

        //�����ʱ�ȭ ��ư
        private void BtnFactoryReset_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("������ ���� ������ �ʱ�ȭ �մϴ�.", "DB �ʱ�ȭ", MessageBoxButtons.YesNo);

            if (DialogResult == DialogResult.No)
            {
                return;
            }

            //FactoryReset(csv_path);
            DeleteAllTables(databaseFileName, tablename);
            CreateTable(databaseFileName, tablename);
            insert_initial_data();
        }

        private void tgBtnSettingFileDel_CheckedChanged(object sender, EventArgs e)
        {
            lbSettingToggle.Text = tgBtnSettingFileDel.Checked ? "����" : "����";
        }
    }
}