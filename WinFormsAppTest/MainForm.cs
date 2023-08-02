using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace WinFormsAppTest
{
    public partial class MainForm : Form
    {
        private PlotForm? pFrm;

        //�����̵� �޴��� �ִ�, �ּ� �� ũ��
        const int MAX_SLIDING_WIDTH = 384;
        const int MIN_SLIDING_WIDTH = 65;
        //�����̵� �޴��� ���̴�/������ �ӵ� ����
        const int STEP_SLIDING = 20;
        //���� �����̵� �޴� ũ��
        int posSliding = MIN_SLIDING_WIDTH;

        bool menuOpen = false;

        private Timer timerSliding = new Timer();

        public MainForm()
        {
            InitializeComponent();
            timerSliding.Interval = 5;
            timerSliding.Tick += new EventHandler(timerSliding_Tick);
            pnSideMenu.Width = MIN_SLIDING_WIDTH;
            btnExtractMenu.Text = "";
            btnTidyMenu.Text = "";
            btnCalcMenu.Text = "";
            btnSettingSave.Text = "";
        }

        private void btnStart_Click_1(object sender, EventArgs e)
        {
            pFrm = new PlotForm();
            pFrm.ShowDialog();
        }

        private void btnExtractMenu_Click(object sender, EventArgs e)
        {
            tcMainHome.SelectedIndex = 1;
        }

        private void btnTidyMenu_Click(object sender, EventArgs e)
        {
            tcMainHome.SelectedIndex = 2;
        }

        private void btnCalcMenu_Click(object sender, EventArgs e)
        {
            tcMainHome.SelectedIndex = 3;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            tcMainHome.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSlideMenu_Click(object sender, EventArgs e)
        {
            menuOpen = !menuOpen;
            if (pnSideMenu.Width <= MAX_SLIDING_WIDTH && menuOpen == true)
            {
                btnExtractMenu.Text = "            Extracting";
                btnTidyMenu.Text = "            Tidying up";
                btnCalcMenu.Text = "            Calculating";
                btnSettingSave.Text = "       Settings Save";
            }

            else if (pnSideMenu.Width >= MIN_SLIDING_WIDTH && menuOpen == false)
            {
                btnExtractMenu.Text = "";
                btnTidyMenu.Text = "";
                btnCalcMenu.Text = "";
                btnSettingSave.Text = "";
            }

            timerSliding.Start();
        }

        private void timerSliding_Tick(object sender, EventArgs e)
        {
            if (menuOpen == false)
            {
                //�����̵� �޴��� ����� ����
                posSliding -= STEP_SLIDING;
                tcMainHome.Width -= STEP_SLIDING;
                if (posSliding <= MIN_SLIDING_WIDTH)
                    timerSliding.Stop();
            }
            else
            {
                //�����̵� �޴��� ���̴� ����
                posSliding += STEP_SLIDING;
                tcMainHome.Width += STEP_SLIDING;
                if (posSliding >= MAX_SLIDING_WIDTH)
                    timerSliding.Stop();
            }

            pnSideMenu.Width = posSliding;
        }
    }
}