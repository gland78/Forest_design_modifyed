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

        //사이드 메뉴의 최대, 최소 폭 크기 및 그 차이
        const int MAX_SLIDING_WIDTH = 384;
        const int MIN_SLIDING_WIDTH = 65;
        const int SLIDING_GAP = MAX_SLIDING_WIDTH - MIN_SLIDING_WIDTH;

        public MainForm()
        {
            InitializeComponent();
            DoubleBuffered = true;

            customPanels_Load();

            custom_Initialize();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string currentPath = Directory.GetCurrentDirectory();
            DirectoryInfo parentDirectory = Directory.GetParent(currentPath);

            string res_path = Path.Combine(parentDirectory.FullName, "result");

            //bin_path 확인 및 db파일 체크
            create_dbFile_dbtable();
            UpdateDataInTable("gui", "result_path", res_path);
            //MessageBox.Show(SelectDataFromTable(databaseFileName, "gui", "result_path"));

            FillTextboxes();
            RegistTextBoxHandler();

            //프로그램 창 생성 위치 지정
            Point screenSize = ((Point)Screen.PrimaryScreen.Bounds.Size);
            this.Location = new Point((screenSize.X - this.Width) / 2, (screenSize.Y - this.Height) / 2);

            //메인폼의 각 컴포넌트 이벤트 설정
            mainForm_AddEvent();
        }

        private void custom_Initialize()
        {
            pnMain.isBorder = false;

            pnSideMenu.Width = MIN_SLIDING_WIDTH;
            tcMainHome.Left -= SLIDING_GAP / 2;
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
            pnSettingDefault.MouseDown += pnSettingAll_MouseDown;
            pnSettingDefault.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingDefault.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingDefault.MouseUp += pnSettingAll_MouseUp;

            pnSettingPreset.MouseDown += pnSettingAll_MouseDown;
            pnSettingPreset.MouseEnter += pnSettingAll_MouseEnter;
            pnSettingPreset.MouseLeave += pnSettingAll_MouseLeave;
            pnSettingPreset.MouseUp += pnSettingAll_MouseUp;

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

        //삭제 보류
        /*private string textLengthTrim(CustomBtn customBtn, string text)
        {
            string trimedText = text;

            Size size = TextRenderer.MeasureText(trimedText, customBtn.Font);
            double diff = size.Width - customBtn.Width + 5;     //5는 보정값

            if (diff < 0)
            {
                return trimedText;
            }
            //14는 보정값(보정값이 없으면 의도보다 조금 더 길어져서 줄바꿈이 일어남)
            while (diff + 14 > 0)
            {
                trimedText = trimedText.Substring(0, trimedText.Length - 1);
                size = TextRenderer.MeasureText(trimedText + "...", customBtn.Font);
                diff = size.Width - customBtn.Width;
            }

            return trimedText + "...";
        }*/

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

            if (result == false)
            {
                dialogResult = MessageBox.Show("현재 설정이 적용되지 않았습니다.\n적용하시겠습니까?", "", MessageBoxButtons.YesNo);
            }

            //아래 btnSettingApply_Click에서 fileType 변수를 사용하므로 채워준 것

            if (dialogResult == DialogResult.Yes)
            {
                btnSettingApply_Click(sender, e);
            }
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
    }
}