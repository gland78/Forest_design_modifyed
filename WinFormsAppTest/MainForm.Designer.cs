namespace WinFormsAppTest
{
    partial class MainForm

    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            pnSideMenu = new Panel();
            btnPresetManage = new Button();
            btnHome = new Button();
            btnSettings = new Button();
            btnSlideMenu = new Button();
            btnClose = new Button();
            pnSettingOut1 = new CustomPanel();
            tbOutlierMethod = new TextBox();
            lbOutlierMethod = new Label();
            tbOutlierMeank = new TextBox();
            tbOutlierMul = new TextBox();
            lbOutlierMul = new Label();
            lbOutlierMeank = new Label();
            lbOutlierRemove = new Label();
            tcMainHome = new CustomTabControl();
            tpMainHome = new TabPage();
            pnReviewMain = new Panel();
            pnReview = new Panel();
            lbReview = new Label();
            pnMain = new CustomPanel();
            pbLoadingBar = new ProgressBar();
            lbSubTitle = new Label();
            btnStart = new Button();
            lbTitle = new Label();
            tpSettings = new TabPage();
            btnSettingLoad = new CustomBtn();
            pnSettingNor5 = new CustomPanel();
            tbNorThres = new TextBox();
            lbNorThres = new Label();
            pnSettingNor4 = new CustomPanel();
            tbNorScalar = new TextBox();
            lbNorScalar = new Label();
            pnSettingNor3 = new CustomPanel();
            tbNorSlope = new TextBox();
            lbNorSlope = new Label();
            pnSettingNor2 = new CustomPanel();
            tbNorWinSize = new TextBox();
            lbNorWinSize = new Label();
            pnSettingNor1 = new CustomPanel();
            tbNorCellSize = new TextBox();
            lbNorCellSize = new Label();
            lbNormalize = new Label();
            btnPresetSave = new CustomBtn();
            lbSettings = new Label();
            btnSettingSave = new CustomBtn();
            pnSettingTree4 = new CustomPanel();
            customPanel8 = new CustomPanel();
            textBox12 = new TextBox();
            lbMaxDBH = new Label();
            tbTreeSegMinDBH = new TextBox();
            lbTreeMinDBH = new Label();
            lbMeasureAttribute = new Label();
            pnSettingTree3 = new CustomPanel();
            tbTreeSegHeightThres = new TextBox();
            lbTreeThres = new Label();
            pnSettingTree2 = new CustomPanel();
            lbTreeSmooth = new Label();
            tbTreeSegSmooth = new TextBox();
            pnSettingCrown2 = new CustomPanel();
            lbCrownMaxH = new Label();
            tbCrownMaxHeight = new TextBox();
            lbTreeSegment = new Label();
            pnSettingTrunk2 = new CustomPanel();
            lbTrunkMaxH = new Label();
            tbTrunkMaxHeight = new TextBox();
            pnSettingOut2 = new CustomPanel();
            lbCrownSlice = new Label();
            lbTrunkSlice = new Label();
            pnSettingOut3 = new CustomPanel();
            pnSettingMeasure1 = new CustomPanel();
            tbMeasureNN = new TextBox();
            lbMeasureNnear = new Label();
            lbSubsampling = new Label();
            pnSettingTree1 = new CustomPanel();
            lbTreeNnear = new Label();
            tbTreeSegNN = new TextBox();
            pnSettingCrown1 = new CustomPanel();
            lbCrownMinH = new Label();
            tbCrownMinHeight = new TextBox();
            pnSettingTrunk1 = new CustomPanel();
            tbTrunkMinHeight = new TextBox();
            lbTrunkMinH = new Label();
            pnSettingSub1 = new CustomPanel();
            tbSubCellSize = new TextBox();
            lbSubCellSize = new Label();
            btnHide = new Button();
            pnSideMenu.SuspendLayout();
            pnSettingOut1.SuspendLayout();
            tcMainHome.SuspendLayout();
            tpMainHome.SuspendLayout();
            pnReviewMain.SuspendLayout();
            pnMain.SuspendLayout();
            tpSettings.SuspendLayout();
            pnSettingNor5.SuspendLayout();
            pnSettingNor4.SuspendLayout();
            pnSettingNor3.SuspendLayout();
            pnSettingNor2.SuspendLayout();
            pnSettingNor1.SuspendLayout();
            pnSettingTree4.SuspendLayout();
            customPanel8.SuspendLayout();
            pnSettingTree3.SuspendLayout();
            pnSettingTree2.SuspendLayout();
            pnSettingCrown2.SuspendLayout();
            pnSettingTrunk2.SuspendLayout();
            pnSettingOut2.SuspendLayout();
            pnSettingOut3.SuspendLayout();
            pnSettingMeasure1.SuspendLayout();
            pnSettingTree1.SuspendLayout();
            pnSettingCrown1.SuspendLayout();
            pnSettingTrunk1.SuspendLayout();
            pnSettingSub1.SuspendLayout();
            SuspendLayout();
            // 
            // pnSideMenu
            // 
            pnSideMenu.BackColor = Color.SeaGreen;
            pnSideMenu.BackgroundImageLayout = ImageLayout.Zoom;
            pnSideMenu.Controls.Add(btnPresetManage);
            pnSideMenu.Controls.Add(btnHome);
            pnSideMenu.Controls.Add(btnSettings);
            pnSideMenu.Controls.Add(btnSlideMenu);
            pnSideMenu.Dock = DockStyle.Left;
            pnSideMenu.Location = new Point(0, 0);
            pnSideMenu.Margin = new Padding(3, 4, 3, 4);
            pnSideMenu.Name = "pnSideMenu";
            pnSideMenu.Size = new Size(384, 800);
            pnSideMenu.TabIndex = 0;
            pnSideMenu.MouseDown += MainForm_MouseDown;
            pnSideMenu.MouseMove += MainForm_MouseMove;
            pnSideMenu.MouseUp += MainForm_MouseUp;
            // 
            // btnPresetManage
            // 
            btnPresetManage.BackColor = Color.Transparent;
            btnPresetManage.BackgroundImageLayout = ImageLayout.None;
            btnPresetManage.CausesValidation = false;
            btnPresetManage.FlatAppearance.BorderColor = Color.SeaGreen;
            btnPresetManage.FlatAppearance.BorderSize = 0;
            btnPresetManage.FlatAppearance.MouseDownBackColor = Color.MediumAquamarine;
            btnPresetManage.FlatAppearance.MouseOverBackColor = Color.MediumSeaGreen;
            btnPresetManage.FlatStyle = FlatStyle.Flat;
            btnPresetManage.Image = (Image)resources.GetObject("btnPresetManage.Image");
            btnPresetManage.Location = new Point(12, 94);
            btnPresetManage.Margin = new Padding(3, 4, 3, 4);
            btnPresetManage.Name = "btnPresetManage";
            btnPresetManage.Size = new Size(38, 34);
            btnPresetManage.TabIndex = 9;
            btnPresetManage.UseVisualStyleBackColor = false;
            btnPresetManage.Click += btnPresetManage_Click;
            // 
            // btnHome
            // 
            btnHome.BackColor = Color.Transparent;
            btnHome.BackgroundImageLayout = ImageLayout.None;
            btnHome.CausesValidation = false;
            btnHome.FlatAppearance.BorderColor = Color.SeaGreen;
            btnHome.FlatAppearance.BorderSize = 0;
            btnHome.FlatAppearance.MouseDownBackColor = Color.MediumAquamarine;
            btnHome.FlatAppearance.MouseOverBackColor = Color.MediumSeaGreen;
            btnHome.FlatStyle = FlatStyle.Flat;
            btnHome.Image = (Image)resources.GetObject("btnHome.Image");
            btnHome.Location = new Point(12, 12);
            btnHome.Margin = new Padding(3, 4, 3, 4);
            btnHome.Name = "btnHome";
            btnHome.Size = new Size(38, 34);
            btnHome.TabIndex = 0;
            btnHome.UseVisualStyleBackColor = false;
            // 
            // btnSettings
            // 
            btnSettings.BackColor = Color.Transparent;
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.FlatAppearance.MouseDownBackColor = Color.MediumAquamarine;
            btnSettings.FlatAppearance.MouseOverBackColor = Color.MediumSeaGreen;
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnSettings.ForeColor = SystemColors.ControlText;
            btnSettings.Image = (Image)resources.GetObject("btnSettings.Image");
            btnSettings.ImageAlign = ContentAlignment.MiddleLeft;
            btnSettings.Location = new Point(12, 754);
            btnSettings.Margin = new Padding(3, 4, 3, 4);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(370, 45);
            btnSettings.TabIndex = 8;
            btnSettings.Text = "            Settings";
            btnSettings.TextAlign = ContentAlignment.MiddleLeft;
            btnSettings.UseVisualStyleBackColor = false;
            // 
            // btnSlideMenu
            // 
            btnSlideMenu.BackColor = Color.Transparent;
            btnSlideMenu.BackgroundImageLayout = ImageLayout.None;
            btnSlideMenu.CausesValidation = false;
            btnSlideMenu.FlatAppearance.BorderColor = Color.SeaGreen;
            btnSlideMenu.FlatAppearance.BorderSize = 0;
            btnSlideMenu.FlatAppearance.MouseDownBackColor = Color.MediumAquamarine;
            btnSlideMenu.FlatAppearance.MouseOverBackColor = Color.MediumSeaGreen;
            btnSlideMenu.FlatStyle = FlatStyle.Flat;
            btnSlideMenu.Image = (Image)resources.GetObject("btnSlideMenu.Image");
            btnSlideMenu.Location = new Point(12, 52);
            btnSlideMenu.Margin = new Padding(3, 4, 3, 4);
            btnSlideMenu.Name = "btnSlideMenu";
            btnSlideMenu.Size = new Size(38, 34);
            btnSlideMenu.TabIndex = 1;
            btnSlideMenu.UseVisualStyleBackColor = false;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.LightGray;
            btnClose.BackgroundImageLayout = ImageLayout.Center;
            btnClose.FlatAppearance.BorderColor = SystemColors.AppWorkspace;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 128);
            btnClose.FlatAppearance.MouseOverBackColor = Color.Red;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Image = (Image)resources.GetObject("btnClose.Image");
            btnClose.Location = new Point(1237, 0);
            btnClose.Margin = new Padding(3, 4, 3, 4);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(43, 30);
            btnClose.TabIndex = 1;
            btnClose.UseVisualStyleBackColor = false;
            // 
            // pnSettingOut1
            // 
            pnSettingOut1.BackColor = Color.Gray;
            pnSettingOut1.Controls.Add(tbOutlierMethod);
            pnSettingOut1.Controls.Add(lbOutlierMethod);
            pnSettingOut1.Location = new Point(24, 377);
            pnSettingOut1.Margin = new Padding(3, 4, 3, 4);
            pnSettingOut1.Name = "pnSettingOut1";
            pnSettingOut1.Size = new Size(850, 58);
            pnSettingOut1.TabIndex = 22;
            // 
            // tbOutlierMethod
            // 
            tbOutlierMethod.BorderStyle = BorderStyle.FixedSingle;
            tbOutlierMethod.Enabled = false;
            tbOutlierMethod.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbOutlierMethod.Location = new Point(725, 15);
            tbOutlierMethod.Margin = new Padding(3, 4, 3, 4);
            tbOutlierMethod.Name = "tbOutlierMethod";
            tbOutlierMethod.ReadOnly = true;
            tbOutlierMethod.Size = new Size(109, 29);
            tbOutlierMethod.TabIndex = 20;
            tbOutlierMethod.TabStop = false;
            tbOutlierMethod.Text = "statistical";
            // 
            // lbOutlierMethod
            // 
            lbOutlierMethod.AutoSize = true;
            lbOutlierMethod.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbOutlierMethod.ForeColor = Color.White;
            lbOutlierMethod.Location = new Point(21, 21);
            lbOutlierMethod.Name = "lbOutlierMethod";
            lbOutlierMethod.Size = new Size(57, 17);
            lbOutlierMethod.TabIndex = 17;
            lbOutlierMethod.Text = "Method";
            // 
            // tbOutlierMeank
            // 
            tbOutlierMeank.BorderStyle = BorderStyle.FixedSingle;
            tbOutlierMeank.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbOutlierMeank.Location = new Point(725, 15);
            tbOutlierMeank.Margin = new Padding(3, 4, 3, 4);
            tbOutlierMeank.Name = "tbOutlierMeank";
            tbOutlierMeank.Size = new Size(109, 29);
            tbOutlierMeank.TabIndex = 22;
            // 
            // tbOutlierMul
            // 
            tbOutlierMul.BorderStyle = BorderStyle.FixedSingle;
            tbOutlierMul.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbOutlierMul.Location = new Point(725, 15);
            tbOutlierMul.Margin = new Padding(3, 4, 3, 4);
            tbOutlierMul.Name = "tbOutlierMul";
            tbOutlierMul.Size = new Size(109, 29);
            tbOutlierMul.TabIndex = 21;
            // 
            // lbOutlierMul
            // 
            lbOutlierMul.AutoSize = true;
            lbOutlierMul.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbOutlierMul.ForeColor = Color.White;
            lbOutlierMul.Location = new Point(21, 21);
            lbOutlierMul.Name = "lbOutlierMul";
            lbOutlierMul.Size = new Size(74, 17);
            lbOutlierMul.TabIndex = 19;
            lbOutlierMul.Text = "Mulitiplier";
            // 
            // lbOutlierMeank
            // 
            lbOutlierMeank.AutoSize = true;
            lbOutlierMeank.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbOutlierMeank.ForeColor = Color.White;
            lbOutlierMeank.Location = new Point(21, 21);
            lbOutlierMeank.Name = "lbOutlierMeank";
            lbOutlierMeank.Size = new Size(50, 17);
            lbOutlierMeank.TabIndex = 18;
            lbOutlierMeank.Text = "Meank";
            // 
            // lbOutlierRemove
            // 
            lbOutlierRemove.AutoSize = true;
            lbOutlierRemove.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbOutlierRemove.ForeColor = Color.White;
            lbOutlierRemove.Location = new Point(24, 345);
            lbOutlierRemove.Name = "lbOutlierRemove";
            lbOutlierRemove.Size = new Size(167, 25);
            lbOutlierRemove.TabIndex = 16;
            lbOutlierRemove.Text = "OutlierRemove";
            // 
            // tcMainHome
            // 
            tcMainHome.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            tcMainHome.Controls.Add(tpMainHome);
            tcMainHome.Controls.Add(tpSettings);
            tcMainHome.ItemSize = new Size(0, 5);
            tcMainHome.Location = new Point(379, 30);
            tcMainHome.Margin = new Padding(0);
            tcMainHome.Multiline = true;
            tcMainHome.Name = "tcMainHome";
            tcMainHome.Padding = new Point(0, 0);
            tcMainHome.SelectedIndex = 0;
            tcMainHome.Size = new Size(905, 770);
            tcMainHome.TabIndex = 4;
            // 
            // tpMainHome
            // 
            tpMainHome.Controls.Add(pnReviewMain);
            tpMainHome.Controls.Add(pnMain);
            tpMainHome.Location = new Point(4, 9);
            tpMainHome.Margin = new Padding(0);
            tpMainHome.Name = "tpMainHome";
            tpMainHome.Size = new Size(897, 757);
            tpMainHome.TabIndex = 0;
            tpMainHome.UseVisualStyleBackColor = true;
            // 
            // pnReviewMain
            // 
            pnReviewMain.BackColor = Color.Beige;
            pnReviewMain.Controls.Add(pnReview);
            pnReviewMain.Controls.Add(lbReview);
            pnReviewMain.Dock = DockStyle.Bottom;
            pnReviewMain.Location = new Point(0, 438);
            pnReviewMain.Margin = new Padding(3, 4, 3, 4);
            pnReviewMain.Name = "pnReviewMain";
            pnReviewMain.Size = new Size(897, 319);
            pnReviewMain.TabIndex = 9;
            // 
            // pnReview
            // 
            pnReview.AutoScroll = true;
            pnReview.Dock = DockStyle.Bottom;
            pnReview.Location = new Point(0, 64);
            pnReview.Margin = new Padding(3, 4, 3, 4);
            pnReview.Name = "pnReview";
            pnReview.Size = new Size(897, 255);
            pnReview.TabIndex = 1;
            // 
            // lbReview
            // 
            lbReview.AutoSize = true;
            lbReview.Font = new Font("굴림", 24F, FontStyle.Regular, GraphicsUnit.Point);
            lbReview.Location = new Point(17, 18);
            lbReview.Name = "lbReview";
            lbReview.Size = new Size(199, 32);
            lbReview.TabIndex = 0;
            lbReview.Text = "Recent Task";
            lbReview.TextAlign = ContentAlignment.BottomLeft;
            // 
            // pnMain
            // 
            pnMain.BackgroundImage = (Image)resources.GetObject("pnMain.BackgroundImage");
            pnMain.BackgroundImageLayout = ImageLayout.Stretch;
            pnMain.Controls.Add(pbLoadingBar);
            pnMain.Controls.Add(lbSubTitle);
            pnMain.Controls.Add(btnStart);
            pnMain.Controls.Add(lbTitle);
            pnMain.Dock = DockStyle.Top;
            pnMain.Location = new Point(0, 0);
            pnMain.Margin = new Padding(3, 4, 3, 4);
            pnMain.Name = "pnMain";
            pnMain.Size = new Size(897, 452);
            pnMain.TabIndex = 10;
            // 
            // pbLoadingBar
            // 
            pbLoadingBar.Location = new Point(36, 370);
            pbLoadingBar.MarqueeAnimationSpeed = 5;
            pbLoadingBar.Maximum = 10;
            pbLoadingBar.Name = "pbLoadingBar";
            pbLoadingBar.Size = new Size(825, 42);
            pbLoadingBar.TabIndex = 8;
            pbLoadingBar.Visible = false;
            // 
            // lbSubTitle
            // 
            lbSubTitle.AutoSize = true;
            lbSubTitle.Font = new Font("굴림", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbSubTitle.Location = new Point(25, 114);
            lbSubTitle.Name = "lbSubTitle";
            lbSubTitle.Size = new Size(209, 16);
            lbSubTitle.TabIndex = 5;
            lbSubTitle.Text = "Forest ICT Research Center";
            // 
            // btnStart
            // 
            btnStart.Anchor = AnchorStyles.Right;
            btnStart.Font = new Font("굴림", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnStart.Location = new Point(652, 229);
            btnStart.Margin = new Padding(3, 4, 3, 4);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(179, 61);
            btnStart.TabIndex = 7;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            // 
            // lbTitle
            // 
            lbTitle.AutoSize = true;
            lbTitle.Font = new Font("Microsoft Sans Serif", 63.75F, FontStyle.Regular, GraphicsUnit.Point);
            lbTitle.Location = new Point(4, 15);
            lbTitle.Name = "lbTitle";
            lbTitle.Size = new Size(348, 96);
            lbTitle.TabIndex = 4;
            lbTitle.Text = "ForestLi\n";
            // 
            // tpSettings
            // 
            tpSettings.AutoScroll = true;
            tpSettings.AutoScrollMargin = new Size(0, 20);
            tpSettings.AutoScrollMinSize = new Size(0, 5);
            tpSettings.BackColor = Color.DimGray;
            tpSettings.Controls.Add(btnSettingLoad);
            tpSettings.Controls.Add(pnSettingNor5);
            tpSettings.Controls.Add(pnSettingNor4);
            tpSettings.Controls.Add(pnSettingNor3);
            tpSettings.Controls.Add(pnSettingNor2);
            tpSettings.Controls.Add(pnSettingNor1);
            tpSettings.Controls.Add(lbNormalize);
            tpSettings.Controls.Add(btnPresetSave);
            tpSettings.Controls.Add(lbSettings);
            tpSettings.Controls.Add(btnSettingSave);
            tpSettings.Controls.Add(pnSettingTree4);
            tpSettings.Controls.Add(lbMeasureAttribute);
            tpSettings.Controls.Add(pnSettingTree3);
            tpSettings.Controls.Add(pnSettingTree2);
            tpSettings.Controls.Add(pnSettingCrown2);
            tpSettings.Controls.Add(lbTreeSegment);
            tpSettings.Controls.Add(pnSettingTrunk2);
            tpSettings.Controls.Add(pnSettingOut2);
            tpSettings.Controls.Add(lbCrownSlice);
            tpSettings.Controls.Add(lbTrunkSlice);
            tpSettings.Controls.Add(pnSettingOut3);
            tpSettings.Controls.Add(pnSettingMeasure1);
            tpSettings.Controls.Add(lbSubsampling);
            tpSettings.Controls.Add(pnSettingTree1);
            tpSettings.Controls.Add(pnSettingCrown1);
            tpSettings.Controls.Add(pnSettingTrunk1);
            tpSettings.Controls.Add(pnSettingSub1);
            tpSettings.Controls.Add(lbOutlierRemove);
            tpSettings.Controls.Add(pnSettingOut1);
            tpSettings.Location = new Point(4, 9);
            tpSettings.Name = "tpSettings";
            tpSettings.Padding = new Padding(3);
            tpSettings.Size = new Size(897, 757);
            tpSettings.TabIndex = 1;
            // 
            // btnSettingLoad
            // 
            btnSettingLoad.BackColor = Color.FromArgb(64, 64, 64);
            btnSettingLoad.BackgroundColor = Color.FromArgb(64, 64, 64);
            btnSettingLoad.BorderColor = Color.Transparent;
            btnSettingLoad.BorderRadius = 10;
            btnSettingLoad.BorderSize = 1;
            btnSettingLoad.FlatAppearance.BorderSize = 0;
            btnSettingLoad.FlatAppearance.MouseDownBackColor = Color.FromArgb(80, 80, 80);
            btnSettingLoad.FlatStyle = FlatStyle.Flat;
            btnSettingLoad.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnSettingLoad.ForeColor = Color.White;
            btnSettingLoad.Location = new Point(612, 27);
            btnSettingLoad.Name = "btnSettingLoad";
            btnSettingLoad.Size = new Size(130, 36);
            btnSettingLoad.TabIndex = 90;
            btnSettingLoad.Text = "기본값 불러오기";
            btnSettingLoad.TextColor = Color.White;
            btnSettingLoad.UseVisualStyleBackColor = false;
            btnSettingLoad.Click += btnSettingLoad_Click;
            // 
            // pnSettingNor5
            // 
            pnSettingNor5.BackColor = Color.Gray;
            pnSettingNor5.Controls.Add(tbNorThres);
            pnSettingNor5.Controls.Add(lbNorThres);
            pnSettingNor5.Location = new Point(24, 894);
            pnSettingNor5.Margin = new Padding(3, 4, 3, 4);
            pnSettingNor5.Name = "pnSettingNor5";
            pnSettingNor5.Size = new Size(850, 58);
            pnSettingNor5.TabIndex = 28;
            // 
            // tbNorThres
            // 
            tbNorThres.BorderStyle = BorderStyle.FixedSingle;
            tbNorThres.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbNorThres.Location = new Point(725, 15);
            tbNorThres.Margin = new Padding(3, 4, 3, 4);
            tbNorThres.Name = "tbNorThres";
            tbNorThres.Size = new Size(109, 29);
            tbNorThres.TabIndex = 24;
            // 
            // lbNorThres
            // 
            lbNorThres.AutoSize = true;
            lbNorThres.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbNorThres.ForeColor = Color.White;
            lbNorThres.Location = new Point(21, 21);
            lbNorThres.Name = "lbNorThres";
            lbNorThres.Size = new Size(70, 17);
            lbNorThres.TabIndex = 23;
            lbNorThres.Text = "Threshold";
            // 
            // pnSettingNor4
            // 
            pnSettingNor4.BackColor = Color.Gray;
            pnSettingNor4.Controls.Add(tbNorScalar);
            pnSettingNor4.Controls.Add(lbNorScalar);
            pnSettingNor4.Location = new Point(24, 828);
            pnSettingNor4.Margin = new Padding(3, 4, 3, 4);
            pnSettingNor4.Name = "pnSettingNor4";
            pnSettingNor4.Size = new Size(850, 58);
            pnSettingNor4.TabIndex = 28;
            // 
            // tbNorScalar
            // 
            tbNorScalar.BorderStyle = BorderStyle.FixedSingle;
            tbNorScalar.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbNorScalar.Location = new Point(725, 15);
            tbNorScalar.Margin = new Padding(3, 4, 3, 4);
            tbNorScalar.Name = "tbNorScalar";
            tbNorScalar.Size = new Size(109, 29);
            tbNorScalar.TabIndex = 24;
            // 
            // lbNorScalar
            // 
            lbNorScalar.AutoSize = true;
            lbNorScalar.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbNorScalar.ForeColor = Color.White;
            lbNorScalar.Location = new Point(21, 21);
            lbNorScalar.Name = "lbNorScalar";
            lbNorScalar.Size = new Size(44, 17);
            lbNorScalar.TabIndex = 23;
            lbNorScalar.Text = "Scalar";
            // 
            // pnSettingNor3
            // 
            pnSettingNor3.BackColor = Color.Gray;
            pnSettingNor3.Controls.Add(tbNorSlope);
            pnSettingNor3.Controls.Add(lbNorSlope);
            pnSettingNor3.Location = new Point(24, 762);
            pnSettingNor3.Margin = new Padding(3, 4, 3, 4);
            pnSettingNor3.Name = "pnSettingNor3";
            pnSettingNor3.Size = new Size(850, 58);
            pnSettingNor3.TabIndex = 27;
            // 
            // tbNorSlope
            // 
            tbNorSlope.BorderStyle = BorderStyle.FixedSingle;
            tbNorSlope.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbNorSlope.Location = new Point(725, 15);
            tbNorSlope.Margin = new Padding(3, 4, 3, 4);
            tbNorSlope.Name = "tbNorSlope";
            tbNorSlope.Size = new Size(109, 29);
            tbNorSlope.TabIndex = 24;
            // 
            // lbNorSlope
            // 
            lbNorSlope.AutoSize = true;
            lbNorSlope.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbNorSlope.ForeColor = Color.White;
            lbNorSlope.Location = new Point(21, 21);
            lbNorSlope.Name = "lbNorSlope";
            lbNorSlope.Size = new Size(42, 17);
            lbNorSlope.TabIndex = 23;
            lbNorSlope.Text = "Slope";
            // 
            // pnSettingNor2
            // 
            pnSettingNor2.BackColor = Color.Gray;
            pnSettingNor2.Controls.Add(tbNorWinSize);
            pnSettingNor2.Controls.Add(lbNorWinSize);
            pnSettingNor2.Location = new Point(24, 696);
            pnSettingNor2.Margin = new Padding(3, 4, 3, 4);
            pnSettingNor2.Name = "pnSettingNor2";
            pnSettingNor2.Size = new Size(850, 58);
            pnSettingNor2.TabIndex = 27;
            // 
            // tbNorWinSize
            // 
            tbNorWinSize.BorderStyle = BorderStyle.FixedSingle;
            tbNorWinSize.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbNorWinSize.Location = new Point(725, 15);
            tbNorWinSize.Margin = new Padding(3, 4, 3, 4);
            tbNorWinSize.Name = "tbNorWinSize";
            tbNorWinSize.Size = new Size(109, 29);
            tbNorWinSize.TabIndex = 24;
            // 
            // lbNorWinSize
            // 
            lbNorWinSize.AutoSize = true;
            lbNorWinSize.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbNorWinSize.ForeColor = Color.White;
            lbNorWinSize.Location = new Point(21, 21);
            lbNorWinSize.Name = "lbNorWinSize";
            lbNorWinSize.Size = new Size(88, 17);
            lbNorWinSize.TabIndex = 23;
            lbNorWinSize.Text = "Window Size";
            // 
            // pnSettingNor1
            // 
            pnSettingNor1.BackColor = Color.Gray;
            pnSettingNor1.Controls.Add(tbNorCellSize);
            pnSettingNor1.Controls.Add(lbNorCellSize);
            pnSettingNor1.Location = new Point(24, 630);
            pnSettingNor1.Margin = new Padding(3, 4, 3, 4);
            pnSettingNor1.Name = "pnSettingNor1";
            pnSettingNor1.Size = new Size(850, 58);
            pnSettingNor1.TabIndex = 26;
            // 
            // tbNorCellSize
            // 
            tbNorCellSize.BorderStyle = BorderStyle.FixedSingle;
            tbNorCellSize.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbNorCellSize.Location = new Point(725, 15);
            tbNorCellSize.Margin = new Padding(3, 4, 3, 4);
            tbNorCellSize.Name = "tbNorCellSize";
            tbNorCellSize.Size = new Size(109, 29);
            tbNorCellSize.TabIndex = 24;
            // 
            // lbNorCellSize
            // 
            lbNorCellSize.AutoSize = true;
            lbNorCellSize.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbNorCellSize.ForeColor = Color.White;
            lbNorCellSize.Location = new Point(21, 21);
            lbNorCellSize.Name = "lbNorCellSize";
            lbNorCellSize.Size = new Size(60, 17);
            lbNorCellSize.TabIndex = 23;
            lbNorCellSize.Text = "Cell Size";
            // 
            // lbNormalize
            // 
            lbNormalize.AutoSize = true;
            lbNormalize.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbNormalize.ForeColor = Color.White;
            lbNormalize.Location = new Point(24, 597);
            lbNormalize.Name = "lbNormalize";
            lbNormalize.Size = new Size(117, 25);
            lbNormalize.TabIndex = 89;
            lbNormalize.Text = "Normalize";
            // 
            // btnPresetSave
            // 
            btnPresetSave.BackColor = Color.FromArgb(64, 64, 64);
            btnPresetSave.BackgroundColor = Color.FromArgb(64, 64, 64);
            btnPresetSave.BorderColor = Color.Transparent;
            btnPresetSave.BorderRadius = 10;
            btnPresetSave.BorderSize = 1;
            btnPresetSave.FlatAppearance.BorderSize = 0;
            btnPresetSave.FlatAppearance.MouseDownBackColor = Color.FromArgb(80, 80, 80);
            btnPresetSave.FlatStyle = FlatStyle.Flat;
            btnPresetSave.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnPresetSave.ForeColor = Color.White;
            btnPresetSave.Location = new Point(490, 27);
            btnPresetSave.Name = "btnPresetSave";
            btnPresetSave.Size = new Size(116, 36);
            btnPresetSave.TabIndex = 88;
            btnPresetSave.Text = "프리셋 저장";
            btnPresetSave.TextColor = Color.White;
            btnPresetSave.UseVisualStyleBackColor = false;
            btnPresetSave.Click += btnPresetSave_Click;
            // 
            // lbSettings
            // 
            lbSettings.AutoSize = true;
            lbSettings.Font = new Font("맑은 고딕", 24F, FontStyle.Bold, GraphicsUnit.Point);
            lbSettings.ForeColor = Color.White;
            lbSettings.Location = new Point(24, 135);
            lbSettings.Name = "lbSettings";
            lbSettings.Size = new Size(143, 45);
            lbSettings.TabIndex = 87;
            lbSettings.Text = "Settings";
            // 
            // btnSettingSave
            // 
            btnSettingSave.BackColor = Color.FromArgb(64, 64, 64);
            btnSettingSave.BackgroundColor = Color.FromArgb(64, 64, 64);
            btnSettingSave.BorderColor = Color.Transparent;
            btnSettingSave.BorderRadius = 10;
            btnSettingSave.BorderSize = 1;
            btnSettingSave.FlatAppearance.BorderSize = 0;
            btnSettingSave.FlatAppearance.MouseDownBackColor = Color.FromArgb(80, 80, 80);
            btnSettingSave.FlatStyle = FlatStyle.Flat;
            btnSettingSave.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnSettingSave.ForeColor = Color.White;
            btnSettingSave.Location = new Point(748, 27);
            btnSettingSave.Name = "btnSettingSave";
            btnSettingSave.Size = new Size(110, 36);
            btnSettingSave.TabIndex = 86;
            btnSettingSave.Text = "기본값 저장";
            btnSettingSave.TextColor = Color.White;
            btnSettingSave.UseVisualStyleBackColor = false;
            btnSettingSave.Click += btnSettingSave_Click;
            // 
            // pnSettingTree4
            // 
            pnSettingTree4.BackColor = Color.Gray;
            pnSettingTree4.Controls.Add(customPanel8);
            pnSettingTree4.Controls.Add(tbTreeSegMinDBH);
            pnSettingTree4.Controls.Add(lbTreeMinDBH);
            pnSettingTree4.Location = new Point(24, 1584);
            pnSettingTree4.Margin = new Padding(3, 4, 3, 4);
            pnSettingTree4.Name = "pnSettingTree4";
            pnSettingTree4.Size = new Size(850, 58);
            pnSettingTree4.TabIndex = 85;
            // 
            // customPanel8
            // 
            customPanel8.BackColor = Color.Gray;
            customPanel8.Controls.Add(textBox12);
            customPanel8.Controls.Add(lbMaxDBH);
            customPanel8.Location = new Point(6, 55);
            customPanel8.Margin = new Padding(3, 4, 3, 4);
            customPanel8.Name = "customPanel8";
            customPanel8.Size = new Size(850, 58);
            customPanel8.TabIndex = 87;
            // 
            // textBox12
            // 
            textBox12.Location = new Point(154, 12);
            textBox12.Margin = new Padding(3, 4, 3, 4);
            textBox12.Name = "textBox12";
            textBox12.Size = new Size(109, 23);
            textBox12.TabIndex = 88;
            // 
            // lbMaxDBH
            // 
            lbMaxDBH.AutoSize = true;
            lbMaxDBH.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbMaxDBH.Location = new Point(42, 16);
            lbMaxDBH.Name = "lbMaxDBH";
            lbMaxDBH.Size = new Size(63, 17);
            lbMaxDBH.TabIndex = 87;
            lbMaxDBH.Text = "MaxDBH";
            // 
            // tbTreeSegMinDBH
            // 
            tbTreeSegMinDBH.BorderStyle = BorderStyle.FixedSingle;
            tbTreeSegMinDBH.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbTreeSegMinDBH.Location = new Point(725, 15);
            tbTreeSegMinDBH.Margin = new Padding(3, 4, 3, 4);
            tbTreeSegMinDBH.Name = "tbTreeSegMinDBH";
            tbTreeSegMinDBH.Size = new Size(109, 29);
            tbTreeSegMinDBH.TabIndex = 86;
            // 
            // lbTreeMinDBH
            // 
            lbTreeMinDBH.AutoSize = true;
            lbTreeMinDBH.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbTreeMinDBH.ForeColor = Color.White;
            lbTreeMinDBH.Location = new Point(21, 21);
            lbTreeMinDBH.Name = "lbTreeMinDBH";
            lbTreeMinDBH.Size = new Size(61, 17);
            lbTreeMinDBH.TabIndex = 85;
            lbTreeMinDBH.Text = "MinDBH";
            // 
            // lbMeasureAttribute
            // 
            lbMeasureAttribute.AutoSize = true;
            lbMeasureAttribute.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbMeasureAttribute.ForeColor = Color.White;
            lbMeasureAttribute.Location = new Point(24, 1672);
            lbMeasureAttribute.Name = "lbMeasureAttribute";
            lbMeasureAttribute.Size = new Size(192, 25);
            lbMeasureAttribute.TabIndex = 63;
            lbMeasureAttribute.Text = "MeasureAttribute";
            // 
            // pnSettingTree3
            // 
            pnSettingTree3.BackColor = Color.Gray;
            pnSettingTree3.Controls.Add(tbTreeSegHeightThres);
            pnSettingTree3.Controls.Add(lbTreeThres);
            pnSettingTree3.Location = new Point(24, 1518);
            pnSettingTree3.Margin = new Padding(3, 4, 3, 4);
            pnSettingTree3.Name = "pnSettingTree3";
            pnSettingTree3.Size = new Size(850, 58);
            pnSettingTree3.TabIndex = 83;
            // 
            // tbTreeSegHeightThres
            // 
            tbTreeSegHeightThres.BorderStyle = BorderStyle.FixedSingle;
            tbTreeSegHeightThres.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbTreeSegHeightThres.Location = new Point(725, 15);
            tbTreeSegHeightThres.Margin = new Padding(3, 4, 3, 4);
            tbTreeSegHeightThres.Name = "tbTreeSegHeightThres";
            tbTreeSegHeightThres.Size = new Size(109, 29);
            tbTreeSegHeightThres.TabIndex = 84;
            // 
            // lbTreeThres
            // 
            lbTreeThres.AutoSize = true;
            lbTreeThres.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbTreeThres.ForeColor = Color.White;
            lbTreeThres.Location = new Point(21, 21);
            lbTreeThres.Name = "lbTreeThres";
            lbTreeThres.Size = new Size(117, 17);
            lbTreeThres.TabIndex = 83;
            lbTreeThres.Text = "Threshold Height";
            // 
            // pnSettingTree2
            // 
            pnSettingTree2.BackColor = Color.Gray;
            pnSettingTree2.Controls.Add(lbTreeSmooth);
            pnSettingTree2.Controls.Add(tbTreeSegSmooth);
            pnSettingTree2.Location = new Point(24, 1452);
            pnSettingTree2.Margin = new Padding(3, 4, 3, 4);
            pnSettingTree2.Name = "pnSettingTree2";
            pnSettingTree2.Size = new Size(850, 58);
            pnSettingTree2.TabIndex = 78;
            // 
            // lbTreeSmooth
            // 
            lbTreeSmooth.AutoSize = true;
            lbTreeSmooth.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbTreeSmooth.ForeColor = Color.White;
            lbTreeSmooth.Location = new Point(21, 21);
            lbTreeSmooth.Name = "lbTreeSmooth";
            lbTreeSmooth.Size = new Size(83, 17);
            lbTreeSmooth.TabIndex = 81;
            lbTreeSmooth.Text = "Smoothness";
            // 
            // tbTreeSegSmooth
            // 
            tbTreeSegSmooth.BorderStyle = BorderStyle.FixedSingle;
            tbTreeSegSmooth.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbTreeSegSmooth.Location = new Point(725, 15);
            tbTreeSegSmooth.Margin = new Padding(3, 4, 3, 4);
            tbTreeSegSmooth.Name = "tbTreeSegSmooth";
            tbTreeSegSmooth.Size = new Size(109, 29);
            tbTreeSegSmooth.TabIndex = 82;
            // 
            // pnSettingCrown2
            // 
            pnSettingCrown2.BackColor = Color.Gray;
            pnSettingCrown2.Controls.Add(lbCrownMaxH);
            pnSettingCrown2.Controls.Add(tbCrownMaxHeight);
            pnSettingCrown2.Location = new Point(24, 1266);
            pnSettingCrown2.Margin = new Padding(3, 4, 3, 4);
            pnSettingCrown2.Name = "pnSettingCrown2";
            pnSettingCrown2.Size = new Size(850, 58);
            pnSettingCrown2.TabIndex = 59;
            // 
            // lbCrownMaxH
            // 
            lbCrownMaxH.AutoSize = true;
            lbCrownMaxH.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbCrownMaxH.ForeColor = Color.White;
            lbCrownMaxH.Location = new Point(21, 21);
            lbCrownMaxH.Name = "lbCrownMaxH";
            lbCrownMaxH.Size = new Size(77, 17);
            lbCrownMaxH.TabIndex = 50;
            lbCrownMaxH.Text = "MaxHeight";
            // 
            // tbCrownMaxHeight
            // 
            tbCrownMaxHeight.BorderStyle = BorderStyle.FixedSingle;
            tbCrownMaxHeight.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbCrownMaxHeight.Location = new Point(725, 15);
            tbCrownMaxHeight.Margin = new Padding(3, 4, 3, 4);
            tbCrownMaxHeight.Name = "tbCrownMaxHeight";
            tbCrownMaxHeight.Size = new Size(109, 29);
            tbCrownMaxHeight.TabIndex = 53;
            // 
            // lbTreeSegment
            // 
            lbTreeSegment.AutoSize = true;
            lbTreeSegment.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbTreeSegment.ForeColor = Color.White;
            lbTreeSegment.Location = new Point(24, 1354);
            lbTreeSegment.Name = "lbTreeSegment";
            lbTreeSegment.Size = new Size(152, 25);
            lbTreeSegment.TabIndex = 62;
            lbTreeSegment.Text = "TreeSegment";
            // 
            // pnSettingTrunk2
            // 
            pnSettingTrunk2.BackColor = Color.Gray;
            pnSettingTrunk2.Controls.Add(lbTrunkMaxH);
            pnSettingTrunk2.Controls.Add(tbTrunkMaxHeight);
            pnSettingTrunk2.Location = new Point(24, 1080);
            pnSettingTrunk2.Margin = new Padding(3, 4, 3, 4);
            pnSettingTrunk2.Name = "pnSettingTrunk2";
            pnSettingTrunk2.Size = new Size(850, 58);
            pnSettingTrunk2.TabIndex = 58;
            // 
            // lbTrunkMaxH
            // 
            lbTrunkMaxH.AutoSize = true;
            lbTrunkMaxH.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbTrunkMaxH.ForeColor = Color.White;
            lbTrunkMaxH.Location = new Point(21, 21);
            lbTrunkMaxH.Name = "lbTrunkMaxH";
            lbTrunkMaxH.Size = new Size(77, 17);
            lbTrunkMaxH.TabIndex = 54;
            lbTrunkMaxH.Text = "MaxHeight";
            // 
            // tbTrunkMaxHeight
            // 
            tbTrunkMaxHeight.BorderStyle = BorderStyle.FixedSingle;
            tbTrunkMaxHeight.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbTrunkMaxHeight.Location = new Point(725, 15);
            tbTrunkMaxHeight.Margin = new Padding(3, 4, 3, 4);
            tbTrunkMaxHeight.Name = "tbTrunkMaxHeight";
            tbTrunkMaxHeight.Size = new Size(109, 29);
            tbTrunkMaxHeight.TabIndex = 55;
            // 
            // pnSettingOut2
            // 
            pnSettingOut2.BackColor = Color.Gray;
            pnSettingOut2.Controls.Add(tbOutlierMeank);
            pnSettingOut2.Controls.Add(lbOutlierMeank);
            pnSettingOut2.Location = new Point(24, 443);
            pnSettingOut2.Margin = new Padding(3, 4, 3, 4);
            pnSettingOut2.Name = "pnSettingOut2";
            pnSettingOut2.Size = new Size(850, 58);
            pnSettingOut2.TabIndex = 23;
            // 
            // lbCrownSlice
            // 
            lbCrownSlice.AutoSize = true;
            lbCrownSlice.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbCrownSlice.ForeColor = Color.White;
            lbCrownSlice.Location = new Point(24, 1168);
            lbCrownSlice.Name = "lbCrownSlice";
            lbCrownSlice.Size = new Size(130, 25);
            lbCrownSlice.TabIndex = 47;
            lbCrownSlice.Text = "CrownSlice";
            // 
            // lbTrunkSlice
            // 
            lbTrunkSlice.AutoSize = true;
            lbTrunkSlice.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbTrunkSlice.ForeColor = Color.White;
            lbTrunkSlice.Location = new Point(24, 982);
            lbTrunkSlice.Name = "lbTrunkSlice";
            lbTrunkSlice.Size = new Size(124, 25);
            lbTrunkSlice.TabIndex = 46;
            lbTrunkSlice.Text = "TrunkSlice";
            // 
            // pnSettingOut3
            // 
            pnSettingOut3.BackColor = Color.Gray;
            pnSettingOut3.Controls.Add(tbOutlierMul);
            pnSettingOut3.Controls.Add(lbOutlierMul);
            pnSettingOut3.Location = new Point(24, 509);
            pnSettingOut3.Margin = new Padding(3, 4, 3, 4);
            pnSettingOut3.Name = "pnSettingOut3";
            pnSettingOut3.Size = new Size(850, 58);
            pnSettingOut3.TabIndex = 23;
            // 
            // pnSettingMeasure1
            // 
            pnSettingMeasure1.BackColor = Color.Gray;
            pnSettingMeasure1.Controls.Add(tbMeasureNN);
            pnSettingMeasure1.Controls.Add(lbMeasureNnear);
            pnSettingMeasure1.Location = new Point(24, 1704);
            pnSettingMeasure1.Margin = new Padding(3, 4, 3, 4);
            pnSettingMeasure1.Name = "pnSettingMeasure1";
            pnSettingMeasure1.Size = new Size(850, 58);
            pnSettingMeasure1.TabIndex = 78;
            // 
            // tbMeasureNN
            // 
            tbMeasureNN.BorderStyle = BorderStyle.FixedSingle;
            tbMeasureNN.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbMeasureNN.Location = new Point(725, 15);
            tbMeasureNN.Margin = new Padding(3, 4, 3, 4);
            tbMeasureNN.Name = "tbMeasureNN";
            tbMeasureNN.Size = new Size(109, 29);
            tbMeasureNN.TabIndex = 67;
            // 
            // lbMeasureNnear
            // 
            lbMeasureNnear.AutoSize = true;
            lbMeasureNnear.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbMeasureNnear.ForeColor = Color.White;
            lbMeasureNnear.Location = new Point(19, 21);
            lbMeasureNnear.Name = "lbMeasureNnear";
            lbMeasureNnear.Size = new Size(63, 17);
            lbMeasureNnear.TabIndex = 65;
            lbMeasureNnear.Text = "Nnearest";
            // 
            // lbSubsampling
            // 
            lbSubsampling.AutoSize = true;
            lbSubsampling.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbSubsampling.ForeColor = Color.White;
            lbSubsampling.Location = new Point(24, 225);
            lbSubsampling.Name = "lbSubsampling";
            lbSubsampling.Size = new Size(150, 25);
            lbSubsampling.TabIndex = 22;
            lbSubsampling.Text = "SubSampling";
            // 
            // pnSettingTree1
            // 
            pnSettingTree1.BackColor = Color.Gray;
            pnSettingTree1.Controls.Add(lbTreeNnear);
            pnSettingTree1.Controls.Add(tbTreeSegNN);
            pnSettingTree1.Location = new Point(24, 1386);
            pnSettingTree1.Margin = new Padding(3, 4, 3, 4);
            pnSettingTree1.Name = "pnSettingTree1";
            pnSettingTree1.Size = new Size(850, 58);
            pnSettingTree1.TabIndex = 77;
            // 
            // lbTreeNnear
            // 
            lbTreeNnear.AutoSize = true;
            lbTreeNnear.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbTreeNnear.ForeColor = Color.White;
            lbTreeNnear.Location = new Point(21, 21);
            lbTreeNnear.Name = "lbTreeNnear";
            lbTreeNnear.Size = new Size(63, 17);
            lbTreeNnear.TabIndex = 79;
            lbTreeNnear.Text = "Nnearest";
            // 
            // tbTreeSegNN
            // 
            tbTreeSegNN.BorderStyle = BorderStyle.FixedSingle;
            tbTreeSegNN.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbTreeSegNN.Location = new Point(725, 15);
            tbTreeSegNN.Margin = new Padding(3, 4, 3, 4);
            tbTreeSegNN.Name = "tbTreeSegNN";
            tbTreeSegNN.Size = new Size(109, 29);
            tbTreeSegNN.TabIndex = 80;
            // 
            // pnSettingCrown1
            // 
            pnSettingCrown1.BackColor = Color.Gray;
            pnSettingCrown1.Controls.Add(lbCrownMinH);
            pnSettingCrown1.Controls.Add(tbCrownMinHeight);
            pnSettingCrown1.Location = new Point(24, 1200);
            pnSettingCrown1.Margin = new Padding(3, 4, 3, 4);
            pnSettingCrown1.Name = "pnSettingCrown1";
            pnSettingCrown1.Size = new Size(850, 58);
            pnSettingCrown1.TabIndex = 58;
            // 
            // lbCrownMinH
            // 
            lbCrownMinH.AutoSize = true;
            lbCrownMinH.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbCrownMinH.ForeColor = Color.White;
            lbCrownMinH.Location = new Point(21, 21);
            lbCrownMinH.Name = "lbCrownMinH";
            lbCrownMinH.Size = new Size(75, 17);
            lbCrownMinH.TabIndex = 49;
            lbCrownMinH.Text = "MinHeight";
            // 
            // tbCrownMinHeight
            // 
            tbCrownMinHeight.BorderStyle = BorderStyle.FixedSingle;
            tbCrownMinHeight.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbCrownMinHeight.Location = new Point(725, 15);
            tbCrownMinHeight.Margin = new Padding(3, 4, 3, 4);
            tbCrownMinHeight.Name = "tbCrownMinHeight";
            tbCrownMinHeight.Size = new Size(109, 29);
            tbCrownMinHeight.TabIndex = 52;
            // 
            // pnSettingTrunk1
            // 
            pnSettingTrunk1.BackColor = Color.Gray;
            pnSettingTrunk1.Controls.Add(tbTrunkMinHeight);
            pnSettingTrunk1.Controls.Add(lbTrunkMinH);
            pnSettingTrunk1.Location = new Point(24, 1014);
            pnSettingTrunk1.Margin = new Padding(3, 4, 3, 4);
            pnSettingTrunk1.Name = "pnSettingTrunk1";
            pnSettingTrunk1.Size = new Size(850, 58);
            pnSettingTrunk1.TabIndex = 57;
            // 
            // tbTrunkMinHeight
            // 
            tbTrunkMinHeight.BorderStyle = BorderStyle.FixedSingle;
            tbTrunkMinHeight.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbTrunkMinHeight.Location = new Point(725, 15);
            tbTrunkMinHeight.Margin = new Padding(3, 4, 3, 4);
            tbTrunkMinHeight.Name = "tbTrunkMinHeight";
            tbTrunkMinHeight.Size = new Size(109, 29);
            tbTrunkMinHeight.TabIndex = 51;
            // 
            // lbTrunkMinH
            // 
            lbTrunkMinH.AutoSize = true;
            lbTrunkMinH.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbTrunkMinH.ForeColor = Color.White;
            lbTrunkMinH.Location = new Point(21, 21);
            lbTrunkMinH.Name = "lbTrunkMinH";
            lbTrunkMinH.Size = new Size(75, 17);
            lbTrunkMinH.TabIndex = 48;
            lbTrunkMinH.Text = "MinHeight";
            // 
            // pnSettingSub1
            // 
            pnSettingSub1.BackColor = Color.Gray;
            pnSettingSub1.Controls.Add(tbSubCellSize);
            pnSettingSub1.Controls.Add(lbSubCellSize);
            pnSettingSub1.Location = new Point(24, 257);
            pnSettingSub1.Margin = new Padding(3, 4, 3, 4);
            pnSettingSub1.Name = "pnSettingSub1";
            pnSettingSub1.Size = new Size(850, 58);
            pnSettingSub1.TabIndex = 25;
            // 
            // tbSubCellSize
            // 
            tbSubCellSize.BorderStyle = BorderStyle.FixedSingle;
            tbSubCellSize.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbSubCellSize.Location = new Point(725, 15);
            tbSubCellSize.Margin = new Padding(3, 4, 3, 4);
            tbSubCellSize.Name = "tbSubCellSize";
            tbSubCellSize.Size = new Size(109, 29);
            tbSubCellSize.TabIndex = 24;
            // 
            // lbSubCellSize
            // 
            lbSubCellSize.AutoSize = true;
            lbSubCellSize.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbSubCellSize.ForeColor = Color.White;
            lbSubCellSize.Location = new Point(21, 21);
            lbSubCellSize.Name = "lbSubCellSize";
            lbSubCellSize.Size = new Size(60, 17);
            lbSubCellSize.TabIndex = 23;
            lbSubCellSize.Text = "Cell Size";
            // 
            // btnHide
            // 
            btnHide.BackColor = Color.LightGray;
            btnHide.FlatAppearance.BorderSize = 0;
            btnHide.FlatAppearance.MouseDownBackColor = Color.WhiteSmoke;
            btnHide.FlatAppearance.MouseOverBackColor = Color.Gainsboro;
            btnHide.FlatStyle = FlatStyle.Flat;
            btnHide.Image = (Image)resources.GetObject("btnHide.Image");
            btnHide.Location = new Point(1194, 0);
            btnHide.Name = "btnHide";
            btnHide.Size = new Size(43, 30);
            btnHide.TabIndex = 7;
            btnHide.UseVisualStyleBackColor = false;
            btnHide.Click += btnHide_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSeaGreen;
            ClientSize = new Size(1280, 800);
            Controls.Add(btnHide);
            Controls.Add(pnSideMenu);
            Controls.Add(btnClose);
            Controls.Add(tcMainHome);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "ForestLi";
            Load += MainForm_Load;
            MouseDown += MainForm_MouseDown;
            MouseMove += MainForm_MouseMove;
            MouseUp += MainForm_MouseUp;
            pnSideMenu.ResumeLayout(false);
            pnSettingOut1.ResumeLayout(false);
            pnSettingOut1.PerformLayout();
            tcMainHome.ResumeLayout(false);
            tpMainHome.ResumeLayout(false);
            pnReviewMain.ResumeLayout(false);
            pnReviewMain.PerformLayout();
            pnMain.ResumeLayout(false);
            pnMain.PerformLayout();
            tpSettings.ResumeLayout(false);
            tpSettings.PerformLayout();
            pnSettingNor5.ResumeLayout(false);
            pnSettingNor5.PerformLayout();
            pnSettingNor4.ResumeLayout(false);
            pnSettingNor4.PerformLayout();
            pnSettingNor3.ResumeLayout(false);
            pnSettingNor3.PerformLayout();
            pnSettingNor2.ResumeLayout(false);
            pnSettingNor2.PerformLayout();
            pnSettingNor1.ResumeLayout(false);
            pnSettingNor1.PerformLayout();
            pnSettingTree4.ResumeLayout(false);
            pnSettingTree4.PerformLayout();
            customPanel8.ResumeLayout(false);
            customPanel8.PerformLayout();
            pnSettingTree3.ResumeLayout(false);
            pnSettingTree3.PerformLayout();
            pnSettingTree2.ResumeLayout(false);
            pnSettingTree2.PerformLayout();
            pnSettingCrown2.ResumeLayout(false);
            pnSettingCrown2.PerformLayout();
            pnSettingTrunk2.ResumeLayout(false);
            pnSettingTrunk2.PerformLayout();
            pnSettingOut2.ResumeLayout(false);
            pnSettingOut2.PerformLayout();
            pnSettingOut3.ResumeLayout(false);
            pnSettingOut3.PerformLayout();
            pnSettingMeasure1.ResumeLayout(false);
            pnSettingMeasure1.PerformLayout();
            pnSettingTree1.ResumeLayout(false);
            pnSettingTree1.PerformLayout();
            pnSettingCrown1.ResumeLayout(false);
            pnSettingCrown1.PerformLayout();
            pnSettingTrunk1.ResumeLayout(false);
            pnSettingTrunk1.PerformLayout();
            pnSettingSub1.ResumeLayout(false);
            pnSettingSub1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnSideMenu;
        private Button btnHome;
        private Button btnSlideMenu;
        private Button btnClose;
        private CustomTabControl tcMainHome;
        private TabPage tpMainHome;
        private Panel pnReviewMain;
        private Panel pnReview;
        private Label lbReview;
        private CustomPanel pnMain;
        private Label lbSubTitle;
        private Button btnStart;
        private Label lbTitle;
        private TabPage tpSettings;
        private TextBox tbOutlierMeank;
        private TextBox tbOutlierMul;
        private TextBox tbOutlierMethod;
        private Label lbOutlierMul;
        private Label lbOutlierMeank;
        private Label lbOutlierMethod;
        private Label lbOutlierRemove;
        private TextBox tbSubCellSize;
        private Label lbSubsampling;
        private Label lbSubCellSize;
        private Button btnSettings;
        private CustomPanel pnSettingOut1;
        private CustomPanel pnSettingSub1;
        private CustomPanel pnSettingTree1;
        private Label lbTreeSegment;
        private CustomPanel pnSettingCrown1;
        private TextBox tbCrownMaxHeight;
        private TextBox tbCrownMinHeight;
        private Label lbCrownSlice;
        private Label lbCrownMaxH;
        private Label lbCrownMinH;
        private CustomPanel pnSettingTrunk1;
        private TextBox tbTrunkMaxHeight;
        private Label lbTrunkSlice;
        private Label lbTrunkMinH;
        private Label lbTrunkMaxH;
        private TextBox tbTrunkMinHeight;
        private CustomPanel pnSettingMeasure1;
        private TextBox tbMeasureNN;
        private Label lbMeasureAttribute;
        private Label lbMeasureNnear;
        private CustomPanel pnSettingOut2;
        private CustomPanel pnSettingOut3;
        private CustomPanel pnSettingTrunk2;
        private CustomPanel pnSettingCrown2;
        private CustomPanel pnSettingTree4;
        private CustomPanel customPanel8;
        private TextBox textBox12;
        private Label lbMaxDBH;
        private TextBox tbTreeSegMinDBH;
        private Label lbTreeMinDBH;
        private CustomPanel pnSettingTree3;
        private TextBox tbTreeSegHeightThres;
        private Label lbTreeThres;
        private CustomPanel pnSettingTree2;
        private Label lbTreeSmooth;
        private TextBox tbTreeSegSmooth;
        private Label lbTreeNnear;
        private TextBox tbTreeSegNN;
        private CustomBtn btnSettingSave;
        private Label lbSettings;
        private CustomBtn btnPresetSave;
        private CustomPanel pnSettingNor5;
        private TextBox tbNorThres;
        private Label lbNorThres;
        private CustomPanel pnSettingNor4;
        private TextBox tbNorScalar;
        private Label lbNorScalar;
        private CustomPanel pnSettingNor3;
        private TextBox tbNorSlope;
        private Label lbNorSlope;
        private CustomPanel pnSettingNor2;
        private TextBox tbNorWinSize;
        private Label lbNorWinSize;
        private CustomPanel pnSettingNor1;
        private TextBox tbNorCellSize;
        private Label lbNorCellSize;
        private Label lbNormalize;
        private Button btnPresetManage;
        private Button btnHide;
        private CustomBtn btnSettingLoad;
        private ProgressBar progressBar1;
        private ProgressBar pbLoadingBar;
    }
}