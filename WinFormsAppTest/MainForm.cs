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
    //델리게이트 메서드들. private인 일반 메서드를 다른 form에서 쓸 수 있게하는 역할
    internal delegate void switchEventHandler(bool onOff);

    public partial class MainForm : Form
    {
        //다른 폼들을 띄우고 delegate를 통해 메서드 전달을 위한 form 변수
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

            //db 변수 초기화, db 생성
            create_dbFile_dbtable();
            UpdateDataInTable("gui", "result_path", res_path);
            //MessageBox.Show(SelectDataFromTable(databaseFileName, "gui", "result_path"));

            FillTextboxes();
            RegistTextBoxHandler();

            //프로그램 창 생성 위치 지정
            Point screenSize = ((Point)Screen.PrimaryScreen.Bounds.Size);
            this.Location = new Point((screenSize.X - this.Width) / 2, (screenSize.Y - this.Height) / 2);

            //프로그램 각종 외형 설정(커스텀 파라미터는 Designer.cs에서 설정 불가)
            customPanels_Load();
            pnMain.isBorder = false;
            tgBtnSettingFileDel.OffBackColor = Color.White;
            tgBtnSettingFileDel.OffToggleColor = Color.Black;
            tgBtnSettingFileDel.OnBackColor = Color.Black;
            tgBtnSettingFileDel.OnToggleColor = Color.White;
            //tgBtnSettingFileDel.SolidStyle = false;

            //lbSettingToggle.Text = tgBtnSettingFileDel.Checked ? "삭제" : "유지";

            //메인폼의 각 컴포넌트 이벤트 설정
            mainForm_AddEvent();
        }

        //메인 폼 로드 전 이벤트 전처리(Designer.cs에 넣으면 찾기가 힘듬)
        private void mainForm_AddEvent()
        {
            //홈버튼 이벤트
            btnHome.Click += btnHome_Click;
            //설정버튼 이벤트
            btnSettings.Click += btnSettings_Click;
            //프로그램 실행 버튼 이벤트
            btnStart.Click += btnStart_Click;

            //이 아래로 설정창의 CustomPanel 객체들 이벤트
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

        //CustomPanel 색 및 테두리 지정(Designer.cs에서 지정하면 컴파일 시 없어짐)
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

        //사이드메뉴 버튼 관련 이벤트 처리 코드
        private void btnStart_Click(object sender, EventArgs e)
        {
            pFrm = new PlotForm(this);
            pFrm.Owner = this;
            pFrm.enableMainFormBtns += new switchEventHandler(switchCoreBtns);
            pFrm.attachStartBtn += new switchEventHandler(startBtnAttach);

            pFrm.Show();

            //plot창을 모달리스로 띄우는 대신 실행 및 설정 적용 관련 기능 버튼 비활성화
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
                dialogResult = MessageBox.Show("현재 설정이 적용되지 않았습니다.\n적용하시겠습니까?", "", MessageBoxButtons.YesNo);
            }

            //예 클릭 시 설정값 적용
            if (dialogResult == DialogResult.Yes)
            {
                btnSettingApply_Click(sender, e);
            }
            //아니요 클릭 시 DB값 읽어서 원상복구
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

        //이 아래 4개 메서드 설정창 파라메터 판넬 마우스 이벤트
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

        //적용하기 버튼 이벤트
        private void btnSettingApply_Click(object sender, EventArgs e)
        {
            UpdateParams();

            MessageBox.Show("적용되었습니다.");
            tcMainHome.SelectedIndex = 0;
        }

        //취소버튼
        //저장하지 않고 기본 config 값으로 텍스트 박스 값 교체 후
        //시작화면으로 이동
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

        //공장초기화 버튼
        private void BtnFactoryReset_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("설정값 관련 파일을 초기화 합니다.", "DB 초기화", MessageBoxButtons.YesNo);

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
            lbSettingToggle.Text = tgBtnSettingFileDel.Checked ? "삭제" : "유지";
        }
    }
}