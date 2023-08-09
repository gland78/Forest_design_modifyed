using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace WinFormsAppTest
{
    public partial class MainForm : Form
    {
        public enum configFileType
        {
            Default,
            Recent,
            Preset
        }

        private PlotForm? pFrm;

        //슬라이딩 메뉴의 최대, 최소 폭 크기 및 그 차이
        const int MAX_SLIDING_WIDTH = 384;
        const int MIN_SLIDING_WIDTH = 65;
        const int SLIDING_GAP = MAX_SLIDING_WIDTH - MIN_SLIDING_WIDTH;

        //슬라이딩 메뉴에 확장, 축소에 따른 메뉴 버튼 크기
        const int MIN_ICON_WIDTH = 42;
        const int MAX_ICON_WIDTH = 370;

        bool menuOpen = false;

        Point relativeMformPos = new Point();

        bool isMformDrag = false;

        string configPath = System.IO.Directory.GetParent(System.Environment.CurrentDirectory).Parent + @"\bin\";
        string[] reqDi = { "", "preSetConfig", "recentConfig" };

        public MainForm()
        {
            InitializeComponent();
            DoubleBuffered = true;

            customPanels_Load();

            pnMain.isBorder = false;

            pnSideMenu.Width = MIN_SLIDING_WIDTH;
            tcMainHome.Left -= SLIDING_GAP / 2;

            btnSettings.Width = MIN_ICON_WIDTH;
            btnSettings.Text = "";

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Point screenSize = ((Point)Screen.PrimaryScreen.Bounds.Size);

            this.Location = new Point((screenSize.X - this.Width) / 2, (screenSize.Y - this.Height) / 2);

            mainForm_AddEvent();

            DirectoryInfo configDi;
            foreach (string fileName in reqDi)
            {
                configDi = new DirectoryInfo(configPath + fileName);
                MessageBox.Show(configDi.FullName);
                if (configDi.Exists == false)
                {
                    configDi.Create();
                }
            }
        }

        private void touchConfig(configFileType confType)
        {
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

        //CustomPanel 색 및 테두리 지정(Designer.cs에서 지정하면 컴파일 시 없어짐)
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

        private void btnStart_Click(object sender, EventArgs e)
        {
            pFrm = new PlotForm();
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

                btnSettings.Width = MAX_ICON_WIDTH;
                btnSettings.Text = "            Settings";
            }

            else if (pnSideMenu.Width >= MIN_SLIDING_WIDTH && menuOpen == false)
            {
                pnSideMenu.Width = MIN_SLIDING_WIDTH;
                tcMainHome.Left -= SLIDING_GAP / 2;

                btnSettings.Width = MIN_ICON_WIDTH;
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
    }
}