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
            btnHome = new Button();
            btnSettings = new Button();
            btnSlideMenu = new Button();
            btnHide = new Button();
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
            lbSubTitle = new Label();
            btnStart = new Button();
            lbTitle = new Label();
            tpSettings = new TabPage();
            lbSettings = new Label();
            btnSettingSave = new CustomBtn();
            pnSettingTree4 = new CustomPanel();
            customPanel8 = new CustomPanel();
            textBox12 = new TextBox();
            lbMaxDBH = new Label();
            textBox11 = new TextBox();
            lbTreeMinDBH = new Label();
            lbMeasureAttribute = new Label();
            pnSettingTree3 = new CustomPanel();
            textBox10 = new TextBox();
            lbTreeThres = new Label();
            pnSettingTree2 = new CustomPanel();
            lbTreeSmooth = new Label();
            textBox1 = new TextBox();
            pnSettingCrown2 = new CustomPanel();
            lbCrownMaxH = new Label();
            textBox15 = new TextBox();
            lbTreeSegment = new Label();
            pnSettingTrunk2 = new CustomPanel();
            lbTrunkMaxH = new Label();
            textBox19 = new TextBox();
            pnSettingOut2 = new CustomPanel();
            lbCrownSlice = new Label();
            lbTrunkSlice = new Label();
            pnSettingOut3 = new CustomPanel();
            pnSettingMeasure1 = new CustomPanel();
            textBox3 = new TextBox();
            lbMeasureNnear = new Label();
            lbSubsampling = new Label();
            pnSettingTree1 = new CustomPanel();
            lbTreeNnear = new Label();
            textBox4 = new TextBox();
            pnSettingCrown1 = new CustomPanel();
            lbCrownMinH = new Label();
            textBox17 = new TextBox();
            pnSettingTrunk1 = new CustomPanel();
            textBox18 = new TextBox();
            lbTrunkMinH = new Label();
            pnSettingSub1 = new CustomPanel();
            tbSubCellSize = new TextBox();
            lbSubCellSize = new Label();
            pnSideMenu.SuspendLayout();
            pnSettingOut1.SuspendLayout();
            tcMainHome.SuspendLayout();
            tpMainHome.SuspendLayout();
            pnReviewMain.SuspendLayout();
            pnMain.SuspendLayout();
            tpSettings.SuspendLayout();
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
            btnSettings.Font = new Font("나눔고딕 ExtraBold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
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
            // btnHide
            // 
            btnHide.BackColor = Color.LightGray;
            btnHide.BackgroundImageLayout = ImageLayout.Center;
            btnHide.FlatAppearance.BorderColor = SystemColors.AppWorkspace;
            btnHide.FlatAppearance.BorderSize = 0;
            btnHide.FlatAppearance.MouseDownBackColor = Color.LightGray;
            btnHide.FlatAppearance.MouseOverBackColor = Color.WhiteSmoke;
            btnHide.FlatStyle = FlatStyle.Flat;
            btnHide.Image = (Image)resources.GetObject("btnHide.Image");
            btnHide.Location = new Point(1194, 0);
            btnHide.Margin = new Padding(3, 4, 3, 4);
            btnHide.Name = "btnHide";
            btnHide.Size = new Size(43, 30);
            btnHide.TabIndex = 3;
            btnHide.UseVisualStyleBackColor = false;
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
            pnSettingOut1.Location = new Point(24, 265);
            pnSettingOut1.Margin = new Padding(3, 4, 3, 4);
            pnSettingOut1.Name = "pnSettingOut1";
            pnSettingOut1.Size = new Size(850, 58);
            pnSettingOut1.TabIndex = 22;
            // 
            // tbOutlierMethod
            // 
            tbOutlierMethod.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbOutlierMethod.Location = new Point(725, 15);
            tbOutlierMethod.Margin = new Padding(3, 4, 3, 4);
            tbOutlierMethod.Name = "tbOutlierMethod";
            tbOutlierMethod.Size = new Size(109, 29);
            tbOutlierMethod.TabIndex = 20;
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
            tbOutlierMeank.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbOutlierMeank.Location = new Point(725, 15);
            tbOutlierMeank.Margin = new Padding(3, 4, 3, 4);
            tbOutlierMeank.Name = "tbOutlierMeank";
            tbOutlierMeank.Size = new Size(109, 29);
            tbOutlierMeank.TabIndex = 22;
            // 
            // tbOutlierMul
            // 
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
            lbOutlierRemove.Font = new Font("나눔고딕 ExtraBold", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbOutlierRemove.ForeColor = Color.White;
            lbOutlierRemove.Location = new Point(26, 233);
            lbOutlierRemove.Name = "lbOutlierRemove";
            lbOutlierRemove.Size = new Size(154, 24);
            lbOutlierRemove.TabIndex = 16;
            lbOutlierRemove.Text = "OutlierRemove";
            // 
            // tcMainHome
            // 
            tcMainHome.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            tcMainHome.Controls.Add(tpMainHome);
            tcMainHome.Controls.Add(tpSettings);
            tcMainHome.ItemSize = new Size(0, 5);
            tcMainHome.Location = new Point(379, 27);
            tcMainHome.Margin = new Padding(0);
            tcMainHome.Multiline = true;
            tcMainHome.Name = "tcMainHome";
            tcMainHome.Padding = new Point(0, 0);
            tcMainHome.SelectedIndex = 0;
            tcMainHome.Size = new Size(905, 777);
            tcMainHome.TabIndex = 4;
            // 
            // tpMainHome
            // 
            tpMainHome.Controls.Add(pnReviewMain);
            tpMainHome.Controls.Add(pnMain);
            tpMainHome.Location = new Point(4, 9);
            tpMainHome.Name = "tpMainHome";
            tpMainHome.Padding = new Padding(3);
            tpMainHome.Size = new Size(897, 764);
            tpMainHome.TabIndex = 0;
            tpMainHome.UseVisualStyleBackColor = true;
            // 
            // pnReviewMain
            // 
            pnReviewMain.BackColor = Color.Beige;
            pnReviewMain.Controls.Add(pnReview);
            pnReviewMain.Controls.Add(lbReview);
            pnReviewMain.Dock = DockStyle.Bottom;
            pnReviewMain.Location = new Point(3, 442);
            pnReviewMain.Margin = new Padding(3, 4, 3, 4);
            pnReviewMain.Name = "pnReviewMain";
            pnReviewMain.Size = new Size(891, 319);
            pnReviewMain.TabIndex = 9;
            // 
            // pnReview
            // 
            pnReview.AutoScroll = true;
            pnReview.Location = new Point(0, 67);
            pnReview.Margin = new Padding(3, 4, 3, 4);
            pnReview.Name = "pnReview";
            pnReview.Size = new Size(891, 255);
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
            pnMain.Controls.Add(lbSubTitle);
            pnMain.Controls.Add(btnStart);
            pnMain.Controls.Add(lbTitle);
            pnMain.Dock = DockStyle.Top;
            pnMain.Location = new Point(3, 3);
            pnMain.Margin = new Padding(3, 4, 3, 4);
            pnMain.Name = "pnMain";
            pnMain.Size = new Size(891, 452);
            pnMain.TabIndex = 10;
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
            btnStart.Location = new Point(646, 229);
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
            lbTitle.Font = new Font("나눔고딕", 63.75F, FontStyle.Regular, GraphicsUnit.Point);
            lbTitle.Location = new Point(4, 15);
            lbTitle.Name = "lbTitle";
            lbTitle.Size = new Size(343, 98);
            lbTitle.TabIndex = 4;
            lbTitle.Text = "ForestLi\n";
            // 
            // tpSettings
            // 
            tpSettings.AutoScroll = true;
            tpSettings.AutoScrollMargin = new Size(0, 20);
            tpSettings.AutoScrollMinSize = new Size(0, 5);
            tpSettings.BackColor = Color.DimGray;
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
            tpSettings.Size = new Size(897, 764);
            tpSettings.TabIndex = 1;
            // 
            // lbSettings
            // 
            lbSettings.AutoSize = true;
            lbSettings.Font = new Font("맑은 고딕", 24F, FontStyle.Bold, GraphicsUnit.Point);
            lbSettings.ForeColor = Color.White;
            lbSettings.Location = new Point(24, 23);
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
            btnSettingSave.Location = new Point(758, 27);
            btnSettingSave.Name = "btnSettingSave";
            btnSettingSave.Size = new Size(100, 36);
            btnSettingSave.TabIndex = 86;
            btnSettingSave.Text = "저장하기";
            btnSettingSave.TextColor = Color.White;
            btnSettingSave.UseVisualStyleBackColor = false;
            // 
            // pnSettingTree4
            // 
            pnSettingTree4.BackColor = Color.Gray;
            pnSettingTree4.Controls.Add(customPanel8);
            pnSettingTree4.Controls.Add(textBox11);
            pnSettingTree4.Controls.Add(lbTreeMinDBH);
            pnSettingTree4.Location = new Point(24, 1085);
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
            // textBox11
            // 
            textBox11.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox11.Location = new Point(725, 15);
            textBox11.Margin = new Padding(3, 4, 3, 4);
            textBox11.Name = "textBox11";
            textBox11.Size = new Size(109, 29);
            textBox11.TabIndex = 86;
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
            lbMeasureAttribute.Font = new Font("나눔고딕 ExtraBold", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbMeasureAttribute.ForeColor = Color.White;
            lbMeasureAttribute.Location = new Point(26, 1173);
            lbMeasureAttribute.Name = "lbMeasureAttribute";
            lbMeasureAttribute.Size = new Size(182, 24);
            lbMeasureAttribute.TabIndex = 63;
            lbMeasureAttribute.Text = "MeasureAttribute";
            // 
            // pnSettingTree3
            // 
            pnSettingTree3.BackColor = Color.Gray;
            pnSettingTree3.Controls.Add(textBox10);
            pnSettingTree3.Controls.Add(lbTreeThres);
            pnSettingTree3.Location = new Point(24, 1019);
            pnSettingTree3.Margin = new Padding(3, 4, 3, 4);
            pnSettingTree3.Name = "pnSettingTree3";
            pnSettingTree3.Size = new Size(850, 58);
            pnSettingTree3.TabIndex = 83;
            // 
            // textBox10
            // 
            textBox10.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox10.Location = new Point(725, 15);
            textBox10.Margin = new Padding(3, 4, 3, 4);
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(109, 29);
            textBox10.TabIndex = 84;
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
            pnSettingTree2.Controls.Add(textBox1);
            pnSettingTree2.Location = new Point(24, 953);
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
            // textBox1
            // 
            textBox1.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.Location = new Point(725, 15);
            textBox1.Margin = new Padding(3, 4, 3, 4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(109, 29);
            textBox1.TabIndex = 82;
            // 
            // pnSettingCrown2
            // 
            pnSettingCrown2.BackColor = Color.Gray;
            pnSettingCrown2.Controls.Add(lbCrownMaxH);
            pnSettingCrown2.Controls.Add(textBox15);
            pnSettingCrown2.Location = new Point(24, 767);
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
            // textBox15
            // 
            textBox15.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox15.Location = new Point(725, 15);
            textBox15.Margin = new Padding(3, 4, 3, 4);
            textBox15.Name = "textBox15";
            textBox15.Size = new Size(109, 29);
            textBox15.TabIndex = 53;
            // 
            // lbTreeSegment
            // 
            lbTreeSegment.AutoSize = true;
            lbTreeSegment.Font = new Font("나눔고딕 ExtraBold", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbTreeSegment.ForeColor = Color.White;
            lbTreeSegment.Location = new Point(26, 855);
            lbTreeSegment.Name = "lbTreeSegment";
            lbTreeSegment.Size = new Size(137, 24);
            lbTreeSegment.TabIndex = 62;
            lbTreeSegment.Text = "TreeSegment";
            // 
            // pnSettingTrunk2
            // 
            pnSettingTrunk2.BackColor = Color.Gray;
            pnSettingTrunk2.Controls.Add(lbTrunkMaxH);
            pnSettingTrunk2.Controls.Add(textBox19);
            pnSettingTrunk2.Location = new Point(24, 581);
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
            // textBox19
            // 
            textBox19.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox19.Location = new Point(725, 15);
            textBox19.Margin = new Padding(3, 4, 3, 4);
            textBox19.Name = "textBox19";
            textBox19.Size = new Size(109, 29);
            textBox19.TabIndex = 55;
            // 
            // pnSettingOut2
            // 
            pnSettingOut2.BackColor = Color.Gray;
            pnSettingOut2.Controls.Add(tbOutlierMeank);
            pnSettingOut2.Controls.Add(lbOutlierMeank);
            pnSettingOut2.Location = new Point(24, 331);
            pnSettingOut2.Margin = new Padding(3, 4, 3, 4);
            pnSettingOut2.Name = "pnSettingOut2";
            pnSettingOut2.Size = new Size(850, 58);
            pnSettingOut2.TabIndex = 23;
            // 
            // lbCrownSlice
            // 
            lbCrownSlice.AutoSize = true;
            lbCrownSlice.Font = new Font("나눔고딕 ExtraBold", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbCrownSlice.ForeColor = Color.White;
            lbCrownSlice.Location = new Point(26, 669);
            lbCrownSlice.Name = "lbCrownSlice";
            lbCrownSlice.Size = new Size(118, 24);
            lbCrownSlice.TabIndex = 47;
            lbCrownSlice.Text = "CrownSlice";
            // 
            // lbTrunkSlice
            // 
            lbTrunkSlice.AutoSize = true;
            lbTrunkSlice.Font = new Font("나눔고딕 ExtraBold", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbTrunkSlice.ForeColor = Color.White;
            lbTrunkSlice.Location = new Point(26, 483);
            lbTrunkSlice.Name = "lbTrunkSlice";
            lbTrunkSlice.Size = new Size(108, 24);
            lbTrunkSlice.TabIndex = 46;
            lbTrunkSlice.Text = "TrunkSlice";
            // 
            // pnSettingOut3
            // 
            pnSettingOut3.BackColor = Color.Gray;
            pnSettingOut3.Controls.Add(tbOutlierMul);
            pnSettingOut3.Controls.Add(lbOutlierMul);
            pnSettingOut3.Location = new Point(24, 397);
            pnSettingOut3.Margin = new Padding(3, 4, 3, 4);
            pnSettingOut3.Name = "pnSettingOut3";
            pnSettingOut3.Size = new Size(850, 58);
            pnSettingOut3.TabIndex = 23;
            // 
            // pnSettingMeasure1
            // 
            pnSettingMeasure1.BackColor = Color.Gray;
            pnSettingMeasure1.Controls.Add(textBox3);
            pnSettingMeasure1.Controls.Add(lbMeasureNnear);
            pnSettingMeasure1.Location = new Point(24, 1205);
            pnSettingMeasure1.Margin = new Padding(3, 4, 3, 4);
            pnSettingMeasure1.Name = "pnSettingMeasure1";
            pnSettingMeasure1.Size = new Size(850, 58);
            pnSettingMeasure1.TabIndex = 78;
            // 
            // textBox3
            // 
            textBox3.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox3.Location = new Point(725, 15);
            textBox3.Margin = new Padding(3, 4, 3, 4);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(109, 29);
            textBox3.TabIndex = 67;
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
            lbSubsampling.Font = new Font("나눔고딕 ExtraBold", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbSubsampling.ForeColor = Color.White;
            lbSubsampling.Location = new Point(26, 113);
            lbSubsampling.Name = "lbSubsampling";
            lbSubsampling.Size = new Size(137, 24);
            lbSubsampling.TabIndex = 22;
            lbSubsampling.Text = "SubSampling";
            // 
            // pnSettingTree1
            // 
            pnSettingTree1.BackColor = Color.Gray;
            pnSettingTree1.Controls.Add(lbTreeNnear);
            pnSettingTree1.Controls.Add(textBox4);
            pnSettingTree1.Location = new Point(24, 887);
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
            // textBox4
            // 
            textBox4.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox4.Location = new Point(725, 15);
            textBox4.Margin = new Padding(3, 4, 3, 4);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(109, 29);
            textBox4.TabIndex = 80;
            // 
            // pnSettingCrown1
            // 
            pnSettingCrown1.BackColor = Color.Gray;
            pnSettingCrown1.Controls.Add(lbCrownMinH);
            pnSettingCrown1.Controls.Add(textBox17);
            pnSettingCrown1.Location = new Point(24, 701);
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
            // textBox17
            // 
            textBox17.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox17.Location = new Point(725, 15);
            textBox17.Margin = new Padding(3, 4, 3, 4);
            textBox17.Name = "textBox17";
            textBox17.Size = new Size(109, 29);
            textBox17.TabIndex = 52;
            // 
            // pnSettingTrunk1
            // 
            pnSettingTrunk1.BackColor = Color.Gray;
            pnSettingTrunk1.Controls.Add(textBox18);
            pnSettingTrunk1.Controls.Add(lbTrunkMinH);
            pnSettingTrunk1.Location = new Point(24, 515);
            pnSettingTrunk1.Margin = new Padding(3, 4, 3, 4);
            pnSettingTrunk1.Name = "pnSettingTrunk1";
            pnSettingTrunk1.Size = new Size(850, 58);
            pnSettingTrunk1.TabIndex = 57;
            // 
            // textBox18
            // 
            textBox18.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox18.Location = new Point(725, 15);
            textBox18.Margin = new Padding(3, 4, 3, 4);
            textBox18.Name = "textBox18";
            textBox18.Size = new Size(109, 29);
            textBox18.TabIndex = 51;
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
            pnSettingSub1.Location = new Point(24, 145);
            pnSettingSub1.Margin = new Padding(3, 4, 3, 4);
            pnSettingSub1.Name = "pnSettingSub1";
            pnSettingSub1.Size = new Size(850, 58);
            pnSettingSub1.TabIndex = 25;
            // 
            // tbSubCellSize
            // 
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
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
        private Button btnHide;
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
        private TextBox textBox15;
        private TextBox textBox17;
        private Label lbCrownSlice;
        private Label lbCrownMaxH;
        private Label lbCrownMinH;
        private CustomPanel pnSettingTrunk1;
        private TextBox textBox19;
        private Label lbTrunkSlice;
        private Label lbTrunkMinH;
        private Label lbTrunkMaxH;
        private TextBox textBox18;
        private CustomPanel pnSettingMeasure1;
        private TextBox textBox3;
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
        private TextBox textBox11;
        private Label lbTreeMinDBH;
        private CustomPanel pnSettingTree3;
        private TextBox textBox10;
        private Label lbTreeThres;
        private CustomPanel pnSettingTree2;
        private Label lbTreeSmooth;
        private TextBox textBox1;
        private Label lbTreeNnear;
        private TextBox textBox4;
        private CustomBtn btnSettingSave;
        private Label lbSettings;
    }
}