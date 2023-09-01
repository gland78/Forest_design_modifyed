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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            pnSideMenu = new Panel();
            btnPresetManage = new Button();
            btnHome = new Button();
            btnSettings = new Button();
            btnSlideMenu = new Button();
            btnClose = new Button();
            tcMainHome = new CustomTabControl();
            tpMainHome = new TabPage();
            pnReviewMain = new Panel();
            btnRecentInfo = new CustomBtn();
            pnReview = new Panel();
            lbReview = new Label();
            pnMain = new CustomPanel();
            pbLoadingBar = new ProgressBar();
            lbSubTitle = new Label();
            btnStart = new Button();
            lbTitle = new Label();
            tpSettings = new TabPage();
            btnSettingCancel = new CustomBtn();
            btnSettingApply = new CustomBtn();
            pnSettingDefault = new CustomPanel();
            btn_factory_reset = new CustomBtn();
            lbSettingDefault = new Label();
            lbSettingDefaultInfo = new Label();
            btnSettingLoad = new CustomBtn();
            btnSettingSave = new CustomBtn();
            pnSettingPreset = new CustomPanel();
            lbSettingPreset = new Label();
            lbSettingPresetInfo = new Label();
            btnPresetSave = new CustomBtn();
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
            lbSettings = new Label();
            pnSettingCrown2 = new CustomPanel();
            lbCrownMaxH = new Label();
            tbCrownMaxHeight = new TextBox();
            pnSettingTrunk2 = new CustomPanel();
            lbTrunkMaxH = new Label();
            tbTrunkMaxHeight = new TextBox();
            lbCrownSlice = new Label();
            lbTrunkSlice = new Label();
            lbSubsampling = new Label();
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
            ttMainInfo = new ToolTip(components);
            pnSideMenu.SuspendLayout();
            tcMainHome.SuspendLayout();
            tpMainHome.SuspendLayout();
            pnReviewMain.SuspendLayout();
            pnMain.SuspendLayout();
            tpSettings.SuspendLayout();
            pnSettingDefault.SuspendLayout();
            pnSettingPreset.SuspendLayout();
            pnSettingNor5.SuspendLayout();
            pnSettingNor4.SuspendLayout();
            pnSettingNor3.SuspendLayout();
            pnSettingNor2.SuspendLayout();
            pnSettingNor1.SuspendLayout();
            pnSettingCrown2.SuspendLayout();
            pnSettingTrunk2.SuspendLayout();
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
            pnSideMenu.TabIndex = 2;
            pnSideMenu.MouseDown += MainForm_MouseDown;
            pnSideMenu.MouseMove += MainForm_MouseMove;
            pnSideMenu.MouseUp += MainForm_MouseUp;
            // 
            // btnPresetManage
            // 
            btnPresetManage.BackColor = Color.Transparent;
            btnPresetManage.BackgroundImage = (Image)resources.GetObject("btnPresetManage.BackgroundImage");
            btnPresetManage.BackgroundImageLayout = ImageLayout.Center;
            btnPresetManage.CausesValidation = false;
            btnPresetManage.FlatAppearance.BorderColor = Color.SeaGreen;
            btnPresetManage.FlatAppearance.BorderSize = 0;
            btnPresetManage.FlatAppearance.MouseDownBackColor = Color.MediumAquamarine;
            btnPresetManage.FlatAppearance.MouseOverBackColor = Color.MediumSeaGreen;
            btnPresetManage.FlatStyle = FlatStyle.Flat;
            btnPresetManage.Location = new Point(12, 94);
            btnPresetManage.Margin = new Padding(3, 4, 3, 4);
            btnPresetManage.Name = "btnPresetManage";
            btnPresetManage.Size = new Size(38, 34);
            btnPresetManage.TabIndex = 2;
            btnPresetManage.TabStop = false;
            btnPresetManage.UseVisualStyleBackColor = false;
            btnPresetManage.Click += btnPresetManage_Click;
            btnPresetManage.MouseHover += btnPresetManage_MouseHover;
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
            btnHome.TabStop = false;
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
            btnSettings.TabIndex = 3;
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
            btnSlideMenu.TabStop = false;
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
            btnClose.TabIndex = 0;
            btnClose.TabStop = false;
            btnClose.UseVisualStyleBackColor = false;
            // 
            // tcMainHome
            // 
            tcMainHome.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            tcMainHome.Controls.Add(tpMainHome);
            tcMainHome.Controls.Add(tpSettings);
            tcMainHome.ItemSize = new Size(0, 5);
            tcMainHome.Location = new Point(379, 0);
            tcMainHome.Margin = new Padding(0);
            tcMainHome.Multiline = true;
            tcMainHome.Name = "tcMainHome";
            tcMainHome.Padding = new Point(0, 0);
            tcMainHome.SelectedIndex = 0;
            tcMainHome.Size = new Size(905, 800);
            tcMainHome.TabIndex = 3;
            tcMainHome.TabStop = false;
            // 
            // tpMainHome
            // 
            tpMainHome.Controls.Add(pnReviewMain);
            tpMainHome.Controls.Add(pnMain);
            tpMainHome.Location = new Point(4, 9);
            tpMainHome.Margin = new Padding(0);
            tpMainHome.Name = "tpMainHome";
            tpMainHome.Size = new Size(897, 787);
            tpMainHome.TabIndex = 0;
            tpMainHome.UseVisualStyleBackColor = true;
            // 
            // pnReviewMain
            // 
            pnReviewMain.BackColor = Color.Beige;
            pnReviewMain.Controls.Add(btnRecentInfo);
            pnReviewMain.Controls.Add(pnReview);
            pnReviewMain.Controls.Add(lbReview);
            pnReviewMain.Dock = DockStyle.Bottom;
            pnReviewMain.Location = new Point(0, 468);
            pnReviewMain.Margin = new Padding(3, 4, 3, 4);
            pnReviewMain.Name = "pnReviewMain";
            pnReviewMain.Size = new Size(897, 319);
            pnReviewMain.TabIndex = 1;
            // 
            // btnRecentInfo
            // 
            btnRecentInfo.BackColor = Color.Transparent;
            btnRecentInfo.BackgroundColor = Color.Transparent;
            btnRecentInfo.BackgroundImage = (Image)resources.GetObject("btnRecentInfo.BackgroundImage");
            btnRecentInfo.BackgroundImageLayout = ImageLayout.Zoom;
            btnRecentInfo.BorderColor = Color.Transparent;
            btnRecentInfo.BorderRadius = 10;
            btnRecentInfo.BorderSize = 0;
            btnRecentInfo.FlatAppearance.BorderSize = 0;
            btnRecentInfo.FlatStyle = FlatStyle.Flat;
            btnRecentInfo.ForeColor = Color.White;
            btnRecentInfo.Location = new Point(219, 24);
            btnRecentInfo.Name = "btnRecentInfo";
            btnRecentInfo.Size = new Size(20, 20);
            btnRecentInfo.TabIndex = 1;
            btnRecentInfo.TabStop = false;
            btnRecentInfo.TextColor = Color.White;
            btnRecentInfo.UseVisualStyleBackColor = false;
            btnRecentInfo.MouseHover += btnRecentInfo_MouseHover;
            // 
            // pnReview
            // 
            pnReview.AutoScroll = true;
            pnReview.Dock = DockStyle.Bottom;
            pnReview.Location = new Point(0, 64);
            pnReview.Margin = new Padding(3, 4, 3, 4);
            pnReview.Name = "pnReview";
            pnReview.Size = new Size(897, 255);
            pnReview.TabIndex = 2;
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
            pnMain.Size = new Size(897, 482);
            pnMain.TabIndex = 0;
            // 
            // pbLoadingBar
            // 
            pbLoadingBar.Location = new Point(36, 370);
            pbLoadingBar.MarqueeAnimationSpeed = 5;
            pbLoadingBar.Maximum = 10;
            pbLoadingBar.Name = "pbLoadingBar";
            pbLoadingBar.Size = new Size(825, 42);
            pbLoadingBar.TabIndex = 3;
            pbLoadingBar.Visible = false;
            // 
            // lbSubTitle
            // 
            lbSubTitle.AutoSize = true;
            lbSubTitle.Font = new Font("굴림", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbSubTitle.Location = new Point(25, 114);
            lbSubTitle.Name = "lbSubTitle";
            lbSubTitle.Size = new Size(209, 16);
            lbSubTitle.TabIndex = 1;
            lbSubTitle.Text = "Forest ICT Research Center";
            // 
            // btnStart
            // 
            btnStart.Anchor = AnchorStyles.Right;
            btnStart.Font = new Font("굴림", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnStart.Location = new Point(652, 244);
            btnStart.Margin = new Padding(3, 4, 3, 4);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(179, 61);
            btnStart.TabIndex = 2;
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
            lbTitle.TabIndex = 0;
            lbTitle.Text = "ForestLi\n";
            // 
            // tpSettings
            // 
            tpSettings.AutoScroll = true;
            tpSettings.AutoScrollMargin = new Size(0, 20);
            tpSettings.AutoScrollMinSize = new Size(0, 5);
            tpSettings.BackColor = Color.DimGray;
            tpSettings.Controls.Add(btnSettingCancel);
            tpSettings.Controls.Add(btnSettingApply);
            tpSettings.Controls.Add(pnSettingDefault);
            tpSettings.Controls.Add(pnSettingPreset);
            tpSettings.Controls.Add(pnSettingNor5);
            tpSettings.Controls.Add(pnSettingNor4);
            tpSettings.Controls.Add(pnSettingNor3);
            tpSettings.Controls.Add(pnSettingNor2);
            tpSettings.Controls.Add(pnSettingNor1);
            tpSettings.Controls.Add(lbNormalize);
            tpSettings.Controls.Add(lbSettings);
            tpSettings.Controls.Add(pnSettingCrown2);
            tpSettings.Controls.Add(pnSettingTrunk2);
            tpSettings.Controls.Add(lbCrownSlice);
            tpSettings.Controls.Add(lbTrunkSlice);
            tpSettings.Controls.Add(lbSubsampling);
            tpSettings.Controls.Add(pnSettingCrown1);
            tpSettings.Controls.Add(pnSettingTrunk1);
            tpSettings.Controls.Add(pnSettingSub1);
            tpSettings.Location = new Point(4, 9);
            tpSettings.Name = "tpSettings";
            tpSettings.Padding = new Padding(3);
            tpSettings.Size = new Size(897, 787);
            tpSettings.TabIndex = 1;
            // 
            // btnSettingCancel
            // 
            btnSettingCancel.BackColor = Color.FromArgb(64, 64, 64);
            btnSettingCancel.BackgroundColor = Color.FromArgb(64, 64, 64);
            btnSettingCancel.BorderColor = Color.Transparent;
            btnSettingCancel.BorderRadius = 10;
            btnSettingCancel.BorderSize = 1;
            btnSettingCancel.FlatAppearance.BorderSize = 0;
            btnSettingCancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(80, 80, 80);
            btnSettingCancel.FlatStyle = FlatStyle.Flat;
            btnSettingCancel.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnSettingCancel.ForeColor = Color.White;
            btnSettingCancel.Location = new Point(743, 38);
            btnSettingCancel.Name = "btnSettingCancel";
            btnSettingCancel.Size = new Size(115, 40);
            btnSettingCancel.TabIndex = 1;
            btnSettingCancel.TabStop = false;
            btnSettingCancel.Text = "취소";
            btnSettingCancel.TextColor = Color.White;
            btnSettingCancel.UseVisualStyleBackColor = false;
            btnSettingCancel.Click += btnSettingCancel_Click;
            // 
            // btnSettingApply
            // 
            btnSettingApply.BackColor = Color.FromArgb(64, 64, 64);
            btnSettingApply.BackgroundColor = Color.FromArgb(64, 64, 64);
            btnSettingApply.BorderColor = Color.Transparent;
            btnSettingApply.BorderRadius = 10;
            btnSettingApply.BorderSize = 1;
            btnSettingApply.FlatAppearance.BorderSize = 0;
            btnSettingApply.FlatAppearance.MouseDownBackColor = Color.FromArgb(80, 80, 80);
            btnSettingApply.FlatStyle = FlatStyle.Flat;
            btnSettingApply.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnSettingApply.ForeColor = Color.White;
            btnSettingApply.Location = new Point(622, 38);
            btnSettingApply.Name = "btnSettingApply";
            btnSettingApply.Size = new Size(115, 40);
            btnSettingApply.TabIndex = 0;
            btnSettingApply.TabStop = false;
            btnSettingApply.Text = "설정 적용";
            btnSettingApply.TextColor = Color.White;
            btnSettingApply.UseVisualStyleBackColor = false;
            btnSettingApply.Click += btnSettingApply_Click;
            // 
            // pnSettingDefault
            // 
            pnSettingDefault.BackColor = Color.Gray;
            pnSettingDefault.Controls.Add(btn_factory_reset);
            pnSettingDefault.Controls.Add(lbSettingDefault);
            pnSettingDefault.Controls.Add(lbSettingDefaultInfo);
            pnSettingDefault.Controls.Add(btnSettingLoad);
            pnSettingDefault.Controls.Add(btnSettingSave);
            pnSettingDefault.Location = new Point(24, 117);
            pnSettingDefault.Margin = new Padding(3, 4, 3, 4);
            pnSettingDefault.Name = "pnSettingDefault";
            pnSettingDefault.Size = new Size(850, 72);
            pnSettingDefault.TabIndex = 2;
            // 
            // btn_factory_reset
            // 
            btn_factory_reset.BackColor = Color.FromArgb(64, 64, 64);
            btn_factory_reset.BackgroundColor = Color.FromArgb(64, 64, 64);
            btn_factory_reset.BorderColor = Color.Transparent;
            btn_factory_reset.BorderRadius = 10;
            btn_factory_reset.BorderSize = 1;
            btn_factory_reset.FlatAppearance.BorderSize = 0;
            btn_factory_reset.FlatAppearance.MouseDownBackColor = Color.FromArgb(80, 80, 80);
            btn_factory_reset.FlatStyle = FlatStyle.Flat;
            btn_factory_reset.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            btn_factory_reset.ForeColor = Color.White;
            btn_factory_reset.Location = new Point(470, 18);
            btn_factory_reset.Name = "btn_factory_reset";
            btn_factory_reset.Size = new Size(104, 36);
            btn_factory_reset.TabIndex = 0;
            btn_factory_reset.TabStop = false;
            btn_factory_reset.Text = "공장초기화";
            btn_factory_reset.TextColor = Color.White;
            btn_factory_reset.UseVisualStyleBackColor = false;
            btn_factory_reset.Click += btn_factory_reset_Click;
            btn_factory_reset.MouseHover += btn_factory_reset_MouseHover;
            // 
            // lbSettingDefault
            // 
            lbSettingDefault.AutoSize = true;
            lbSettingDefault.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbSettingDefault.ForeColor = Color.White;
            lbSettingDefault.Location = new Point(21, 21);
            lbSettingDefault.Name = "lbSettingDefault";
            lbSettingDefault.Size = new Size(78, 17);
            lbSettingDefault.TabIndex = 3;
            lbSettingDefault.Text = "기본값 관리";
            // 
            // lbSettingDefaultInfo
            // 
            lbSettingDefaultInfo.AutoSize = true;
            lbSettingDefaultInfo.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            lbSettingDefaultInfo.ForeColor = Color.White;
            lbSettingDefaultInfo.Location = new Point(21, 40);
            lbSettingDefaultInfo.Name = "lbSettingDefaultInfo";
            lbSettingDefaultInfo.Size = new Size(367, 13);
            lbSettingDefaultInfo.TabIndex = 4;
            lbSettingDefaultInfo.Text = "현재 설정 상태를 기본값으로 저장하거나, 저장된 기본값으로 변경합니다";
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
            btnSettingLoad.Location = new Point(580, 18);
            btnSettingLoad.Name = "btnSettingLoad";
            btnSettingLoad.Size = new Size(138, 36);
            btnSettingLoad.TabIndex = 1;
            btnSettingLoad.TabStop = false;
            btnSettingLoad.Text = "기본값 불러오기";
            btnSettingLoad.TextColor = Color.White;
            btnSettingLoad.UseVisualStyleBackColor = false;
            btnSettingLoad.Click += btnSettingLoad_Click;
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
            btnSettingSave.Location = new Point(724, 18);
            btnSettingSave.Name = "btnSettingSave";
            btnSettingSave.Size = new Size(110, 36);
            btnSettingSave.TabIndex = 2;
            btnSettingSave.TabStop = false;
            btnSettingSave.Text = "기본값 저장";
            btnSettingSave.TextColor = Color.White;
            btnSettingSave.UseVisualStyleBackColor = false;
            btnSettingSave.Click += btnSettingSave_Click;
            // 
            // pnSettingPreset
            // 
            pnSettingPreset.BackColor = Color.Gray;
            pnSettingPreset.Controls.Add(lbSettingPreset);
            pnSettingPreset.Controls.Add(lbSettingPresetInfo);
            pnSettingPreset.Controls.Add(btnPresetSave);
            pnSettingPreset.Location = new Point(24, 193);
            pnSettingPreset.Margin = new Padding(3, 4, 3, 4);
            pnSettingPreset.Name = "pnSettingPreset";
            pnSettingPreset.Size = new Size(850, 72);
            pnSettingPreset.TabIndex = 3;
            // 
            // lbSettingPreset
            // 
            lbSettingPreset.AutoSize = true;
            lbSettingPreset.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbSettingPreset.ForeColor = Color.White;
            lbSettingPreset.Location = new Point(21, 21);
            lbSettingPreset.Name = "lbSettingPreset";
            lbSettingPreset.Size = new Size(122, 17);
            lbSettingPreset.TabIndex = 1;
            lbSettingPreset.Text = "사용자 설정값 저장";
            // 
            // lbSettingPresetInfo
            // 
            lbSettingPresetInfo.AutoSize = true;
            lbSettingPresetInfo.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            lbSettingPresetInfo.ForeColor = Color.White;
            lbSettingPresetInfo.Location = new Point(21, 40);
            lbSettingPresetInfo.Name = "lbSettingPresetInfo";
            lbSettingPresetInfo.Size = new Size(273, 13);
            lbSettingPresetInfo.TabIndex = 2;
            lbSettingPresetInfo.Text = "현재 설정 상태를 사이드의 사용자 메뉴로 저장합니다";
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
            btnPresetSave.Location = new Point(694, 18);
            btnPresetSave.Name = "btnPresetSave";
            btnPresetSave.Size = new Size(140, 36);
            btnPresetSave.TabIndex = 0;
            btnPresetSave.TabStop = false;
            btnPresetSave.Text = "사용자 설정 저장";
            btnPresetSave.TextColor = Color.White;
            btnPresetSave.UseVisualStyleBackColor = false;
            btnPresetSave.Click += btnPresetSave_Click;
            // 
            // pnSettingNor5
            // 
            pnSettingNor5.BackColor = Color.Gray;
            pnSettingNor5.Controls.Add(tbNorThres);
            pnSettingNor5.Controls.Add(lbNorThres);
            pnSettingNor5.Location = new Point(24, 711);
            pnSettingNor5.Margin = new Padding(3, 4, 3, 4);
            pnSettingNor5.Name = "pnSettingNor5";
            pnSettingNor5.Size = new Size(850, 58);
            pnSettingNor5.TabIndex = 9;
            // 
            // tbNorThres
            // 
            tbNorThres.BorderStyle = BorderStyle.FixedSingle;
            tbNorThres.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbNorThres.Location = new Point(725, 15);
            tbNorThres.Margin = new Padding(3, 4, 3, 4);
            tbNorThres.Name = "tbNorThres";
            tbNorThres.Size = new Size(109, 29);
            tbNorThres.TabIndex = 0;
            // 
            // lbNorThres
            // 
            lbNorThres.AutoSize = true;
            lbNorThres.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbNorThres.ForeColor = Color.White;
            lbNorThres.Location = new Point(21, 21);
            lbNorThres.Name = "lbNorThres";
            lbNorThres.Size = new Size(70, 17);
            lbNorThres.TabIndex = 1;
            lbNorThres.Text = "Threshold";
            // 
            // pnSettingNor4
            // 
            pnSettingNor4.BackColor = Color.Gray;
            pnSettingNor4.Controls.Add(tbNorScalar);
            pnSettingNor4.Controls.Add(lbNorScalar);
            pnSettingNor4.Location = new Point(24, 645);
            pnSettingNor4.Margin = new Padding(3, 4, 3, 4);
            pnSettingNor4.Name = "pnSettingNor4";
            pnSettingNor4.Size = new Size(850, 58);
            pnSettingNor4.TabIndex = 8;
            // 
            // tbNorScalar
            // 
            tbNorScalar.BorderStyle = BorderStyle.FixedSingle;
            tbNorScalar.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbNorScalar.Location = new Point(725, 15);
            tbNorScalar.Margin = new Padding(3, 4, 3, 4);
            tbNorScalar.Name = "tbNorScalar";
            tbNorScalar.Size = new Size(109, 29);
            tbNorScalar.TabIndex = 0;
            // 
            // lbNorScalar
            // 
            lbNorScalar.AutoSize = true;
            lbNorScalar.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbNorScalar.ForeColor = Color.White;
            lbNorScalar.Location = new Point(21, 21);
            lbNorScalar.Name = "lbNorScalar";
            lbNorScalar.Size = new Size(44, 17);
            lbNorScalar.TabIndex = 1;
            lbNorScalar.Text = "Scalar";
            // 
            // pnSettingNor3
            // 
            pnSettingNor3.BackColor = Color.Gray;
            pnSettingNor3.Controls.Add(tbNorSlope);
            pnSettingNor3.Controls.Add(lbNorSlope);
            pnSettingNor3.Location = new Point(24, 579);
            pnSettingNor3.Margin = new Padding(3, 4, 3, 4);
            pnSettingNor3.Name = "pnSettingNor3";
            pnSettingNor3.Size = new Size(850, 58);
            pnSettingNor3.TabIndex = 7;
            // 
            // tbNorSlope
            // 
            tbNorSlope.BorderStyle = BorderStyle.FixedSingle;
            tbNorSlope.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbNorSlope.Location = new Point(725, 15);
            tbNorSlope.Margin = new Padding(3, 4, 3, 4);
            tbNorSlope.Name = "tbNorSlope";
            tbNorSlope.Size = new Size(109, 29);
            tbNorSlope.TabIndex = 0;
            // 
            // lbNorSlope
            // 
            lbNorSlope.AutoSize = true;
            lbNorSlope.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbNorSlope.ForeColor = Color.White;
            lbNorSlope.Location = new Point(21, 21);
            lbNorSlope.Name = "lbNorSlope";
            lbNorSlope.Size = new Size(42, 17);
            lbNorSlope.TabIndex = 1;
            lbNorSlope.Text = "Slope";
            // 
            // pnSettingNor2
            // 
            pnSettingNor2.BackColor = Color.Gray;
            pnSettingNor2.Controls.Add(tbNorWinSize);
            pnSettingNor2.Controls.Add(lbNorWinSize);
            pnSettingNor2.Location = new Point(24, 513);
            pnSettingNor2.Margin = new Padding(3, 4, 3, 4);
            pnSettingNor2.Name = "pnSettingNor2";
            pnSettingNor2.Size = new Size(850, 58);
            pnSettingNor2.TabIndex = 6;
            // 
            // tbNorWinSize
            // 
            tbNorWinSize.BorderStyle = BorderStyle.FixedSingle;
            tbNorWinSize.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbNorWinSize.Location = new Point(725, 15);
            tbNorWinSize.Margin = new Padding(3, 4, 3, 4);
            tbNorWinSize.Name = "tbNorWinSize";
            tbNorWinSize.Size = new Size(109, 29);
            tbNorWinSize.TabIndex = 0;
            // 
            // lbNorWinSize
            // 
            lbNorWinSize.AutoSize = true;
            lbNorWinSize.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbNorWinSize.ForeColor = Color.White;
            lbNorWinSize.Location = new Point(21, 21);
            lbNorWinSize.Name = "lbNorWinSize";
            lbNorWinSize.Size = new Size(88, 17);
            lbNorWinSize.TabIndex = 1;
            lbNorWinSize.Text = "Window Size";
            // 
            // pnSettingNor1
            // 
            pnSettingNor1.BackColor = Color.Gray;
            pnSettingNor1.Controls.Add(tbNorCellSize);
            pnSettingNor1.Controls.Add(lbNorCellSize);
            pnSettingNor1.Location = new Point(24, 447);
            pnSettingNor1.Margin = new Padding(3, 4, 3, 4);
            pnSettingNor1.Name = "pnSettingNor1";
            pnSettingNor1.Size = new Size(850, 58);
            pnSettingNor1.TabIndex = 5;
            // 
            // tbNorCellSize
            // 
            tbNorCellSize.BorderStyle = BorderStyle.FixedSingle;
            tbNorCellSize.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbNorCellSize.Location = new Point(725, 15);
            tbNorCellSize.Margin = new Padding(3, 4, 3, 4);
            tbNorCellSize.Name = "tbNorCellSize";
            tbNorCellSize.Size = new Size(109, 29);
            tbNorCellSize.TabIndex = 0;
            // 
            // lbNorCellSize
            // 
            lbNorCellSize.AutoSize = true;
            lbNorCellSize.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbNorCellSize.ForeColor = Color.White;
            lbNorCellSize.Location = new Point(21, 21);
            lbNorCellSize.Name = "lbNorCellSize";
            lbNorCellSize.Size = new Size(60, 17);
            lbNorCellSize.TabIndex = 1;
            lbNorCellSize.Text = "Cell Size";
            // 
            // lbNormalize
            // 
            lbNormalize.AutoSize = true;
            lbNormalize.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbNormalize.ForeColor = Color.White;
            lbNormalize.Location = new Point(24, 415);
            lbNormalize.Name = "lbNormalize";
            lbNormalize.Size = new Size(117, 25);
            lbNormalize.TabIndex = 16;
            lbNormalize.Text = "Normalize";
            // 
            // lbSettings
            // 
            lbSettings.AutoSize = true;
            lbSettings.Font = new Font("맑은 고딕", 24F, FontStyle.Bold, GraphicsUnit.Point);
            lbSettings.ForeColor = Color.White;
            lbSettings.Location = new Point(24, 36);
            lbSettings.Name = "lbSettings";
            lbSettings.Size = new Size(143, 45);
            lbSettings.TabIndex = 14;
            lbSettings.Text = "Settings";
            // 
            // pnSettingCrown2
            // 
            pnSettingCrown2.BackColor = Color.Gray;
            pnSettingCrown2.Controls.Add(lbCrownMaxH);
            pnSettingCrown2.Controls.Add(tbCrownMaxHeight);
            pnSettingCrown2.Location = new Point(24, 1083);
            pnSettingCrown2.Margin = new Padding(3, 4, 3, 4);
            pnSettingCrown2.Name = "pnSettingCrown2";
            pnSettingCrown2.Size = new Size(850, 58);
            pnSettingCrown2.TabIndex = 13;
            // 
            // lbCrownMaxH
            // 
            lbCrownMaxH.AutoSize = true;
            lbCrownMaxH.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbCrownMaxH.ForeColor = Color.White;
            lbCrownMaxH.Location = new Point(21, 21);
            lbCrownMaxH.Name = "lbCrownMaxH";
            lbCrownMaxH.Size = new Size(77, 17);
            lbCrownMaxH.TabIndex = 1;
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
            tbCrownMaxHeight.TabIndex = 0;
            // 
            // pnSettingTrunk2
            // 
            pnSettingTrunk2.BackColor = Color.Gray;
            pnSettingTrunk2.Controls.Add(lbTrunkMaxH);
            pnSettingTrunk2.Controls.Add(tbTrunkMaxHeight);
            pnSettingTrunk2.Location = new Point(24, 897);
            pnSettingTrunk2.Margin = new Padding(3, 4, 3, 4);
            pnSettingTrunk2.Name = "pnSettingTrunk2";
            pnSettingTrunk2.Size = new Size(850, 58);
            pnSettingTrunk2.TabIndex = 11;
            // 
            // lbTrunkMaxH
            // 
            lbTrunkMaxH.AutoSize = true;
            lbTrunkMaxH.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbTrunkMaxH.ForeColor = Color.White;
            lbTrunkMaxH.Location = new Point(21, 21);
            lbTrunkMaxH.Name = "lbTrunkMaxH";
            lbTrunkMaxH.Size = new Size(77, 17);
            lbTrunkMaxH.TabIndex = 1;
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
            tbTrunkMaxHeight.TabIndex = 0;
            // 
            // lbCrownSlice
            // 
            lbCrownSlice.AutoSize = true;
            lbCrownSlice.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbCrownSlice.ForeColor = Color.White;
            lbCrownSlice.Location = new Point(24, 985);
            lbCrownSlice.Name = "lbCrownSlice";
            lbCrownSlice.Size = new Size(130, 25);
            lbCrownSlice.TabIndex = 18;
            lbCrownSlice.Text = "CrownSlice";
            // 
            // lbTrunkSlice
            // 
            lbTrunkSlice.AutoSize = true;
            lbTrunkSlice.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbTrunkSlice.ForeColor = Color.White;
            lbTrunkSlice.Location = new Point(24, 799);
            lbTrunkSlice.Name = "lbTrunkSlice";
            lbTrunkSlice.Size = new Size(124, 25);
            lbTrunkSlice.TabIndex = 17;
            lbTrunkSlice.Text = "TrunkSlice";
            // 
            // lbSubsampling
            // 
            lbSubsampling.AutoSize = true;
            lbSubsampling.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbSubsampling.ForeColor = Color.White;
            lbSubsampling.Location = new Point(24, 295);
            lbSubsampling.Name = "lbSubsampling";
            lbSubsampling.Size = new Size(150, 25);
            lbSubsampling.TabIndex = 15;
            lbSubsampling.Text = "SubSampling";
            // 
            // pnSettingCrown1
            // 
            pnSettingCrown1.BackColor = Color.Gray;
            pnSettingCrown1.Controls.Add(lbCrownMinH);
            pnSettingCrown1.Controls.Add(tbCrownMinHeight);
            pnSettingCrown1.Location = new Point(24, 1017);
            pnSettingCrown1.Margin = new Padding(3, 4, 3, 4);
            pnSettingCrown1.Name = "pnSettingCrown1";
            pnSettingCrown1.Size = new Size(850, 58);
            pnSettingCrown1.TabIndex = 12;
            // 
            // lbCrownMinH
            // 
            lbCrownMinH.AutoSize = true;
            lbCrownMinH.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbCrownMinH.ForeColor = Color.White;
            lbCrownMinH.Location = new Point(21, 21);
            lbCrownMinH.Name = "lbCrownMinH";
            lbCrownMinH.Size = new Size(75, 17);
            lbCrownMinH.TabIndex = 1;
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
            tbCrownMinHeight.TabIndex = 0;
            // 
            // pnSettingTrunk1
            // 
            pnSettingTrunk1.BackColor = Color.Gray;
            pnSettingTrunk1.Controls.Add(tbTrunkMinHeight);
            pnSettingTrunk1.Controls.Add(lbTrunkMinH);
            pnSettingTrunk1.Location = new Point(24, 831);
            pnSettingTrunk1.Margin = new Padding(3, 4, 3, 4);
            pnSettingTrunk1.Name = "pnSettingTrunk1";
            pnSettingTrunk1.Size = new Size(850, 58);
            pnSettingTrunk1.TabIndex = 10;
            // 
            // tbTrunkMinHeight
            // 
            tbTrunkMinHeight.BorderStyle = BorderStyle.FixedSingle;
            tbTrunkMinHeight.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbTrunkMinHeight.Location = new Point(725, 15);
            tbTrunkMinHeight.Margin = new Padding(3, 4, 3, 4);
            tbTrunkMinHeight.Name = "tbTrunkMinHeight";
            tbTrunkMinHeight.Size = new Size(109, 29);
            tbTrunkMinHeight.TabIndex = 0;
            // 
            // lbTrunkMinH
            // 
            lbTrunkMinH.AutoSize = true;
            lbTrunkMinH.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbTrunkMinH.ForeColor = Color.White;
            lbTrunkMinH.Location = new Point(21, 21);
            lbTrunkMinH.Name = "lbTrunkMinH";
            lbTrunkMinH.Size = new Size(75, 17);
            lbTrunkMinH.TabIndex = 1;
            lbTrunkMinH.Text = "MinHeight";
            // 
            // pnSettingSub1
            // 
            pnSettingSub1.BackColor = Color.Gray;
            pnSettingSub1.Controls.Add(tbSubCellSize);
            pnSettingSub1.Controls.Add(lbSubCellSize);
            pnSettingSub1.Location = new Point(24, 327);
            pnSettingSub1.Margin = new Padding(3, 4, 3, 4);
            pnSettingSub1.Name = "pnSettingSub1";
            pnSettingSub1.Size = new Size(850, 58);
            pnSettingSub1.TabIndex = 4;
            // 
            // tbSubCellSize
            // 
            tbSubCellSize.BorderStyle = BorderStyle.FixedSingle;
            tbSubCellSize.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbSubCellSize.Location = new Point(725, 15);
            tbSubCellSize.Margin = new Padding(3, 4, 3, 4);
            tbSubCellSize.Name = "tbSubCellSize";
            tbSubCellSize.Size = new Size(109, 29);
            tbSubCellSize.TabIndex = 0;
            // 
            // lbSubCellSize
            // 
            lbSubCellSize.AutoSize = true;
            lbSubCellSize.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbSubCellSize.ForeColor = Color.White;
            lbSubCellSize.Location = new Point(21, 21);
            lbSubCellSize.Name = "lbSubCellSize";
            lbSubCellSize.Size = new Size(60, 17);
            lbSubCellSize.TabIndex = 1;
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
            btnHide.TabIndex = 1;
            btnHide.TabStop = false;
            btnHide.UseVisualStyleBackColor = false;
            btnHide.Click += btnHide_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSeaGreen;
            ClientSize = new Size(1280, 800);
            Controls.Add(btnClose);
            Controls.Add(btnHide);
            Controls.Add(pnSideMenu);
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
            tcMainHome.ResumeLayout(false);
            tpMainHome.ResumeLayout(false);
            pnReviewMain.ResumeLayout(false);
            pnReviewMain.PerformLayout();
            pnMain.ResumeLayout(false);
            pnMain.PerformLayout();
            tpSettings.ResumeLayout(false);
            tpSettings.PerformLayout();
            pnSettingDefault.ResumeLayout(false);
            pnSettingDefault.PerformLayout();
            pnSettingPreset.ResumeLayout(false);
            pnSettingPreset.PerformLayout();
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
            pnSettingCrown2.ResumeLayout(false);
            pnSettingCrown2.PerformLayout();
            pnSettingTrunk2.ResumeLayout(false);
            pnSettingTrunk2.PerformLayout();
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
        private TextBox tbSubCellSize;
        private Label lbSubsampling;
        private Label lbSubCellSize;
        private Button btnSettings;
        private CustomPanel pnSettingSub1;
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
        private CustomPanel pnSettingTrunk2;
        private CustomPanel pnSettingCrown2;
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
        private ProgressBar pbLoadingBar;
        private CustomPanel pnSettingPreset;
        private Label lbSettingPreset;
        private Label lbSettingPresetInfo;
        private CustomPanel pnSettingDefault;
        private Label lbSettingDefault;
        private Label lbSettingDefaultInfo;
        private CustomBtn btnSettingApply;
        private CustomBtn btnSettingCancel;
        private CustomBtn btn_factory_reset;
        private CustomBtn btnRecentInfo;
        private ToolTip ttMainInfo;
    }
}