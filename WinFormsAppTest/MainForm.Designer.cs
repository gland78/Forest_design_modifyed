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
            btnSettingSave = new Button();
            btnTidyMenu = new Button();
            btnCalcMenu = new Button();
            btnExtractMenu = new Button();
            btnSlideMenu = new Button();
            btnHome = new Button();
            btnClose = new Button();
            button3 = new Button();
            button4 = new Button();
            panel2 = new Panel();
            tbOutlierMeank = new TextBox();
            tbOutlierMul = new TextBox();
            tbOutlierMethod = new TextBox();
            lbOutlierMul = new Label();
            lbOutlierMeank = new Label();
            lbOutlierMethod = new Label();
            lbOutlierRemove = new Label();
            tcMainHome = new CustomTabControl();
            tpMainHome = new TabPage();
            pnReviewMain = new Panel();
            pnReview = new Panel();
            lbReview = new Label();
            pnMain = new Panel();
            lbSubTitle = new Label();
            btnStart = new Button();
            lbTitle = new Label();
            tpMainExtract = new TabPage();
            panel1 = new Panel();
            tbSubCellSize = new TextBox();
            lbSubsampling = new Label();
            lbSubCellSize = new Label();
            tpMainTidying = new TabPage();
            panel4 = new Panel();
            textBox19 = new TextBox();
            lbTrunkSlice = new Label();
            lbTrunkMinH = new Label();
            lbTrunkMaxH = new Label();
            textBox18 = new TextBox();
            panel5 = new Panel();
            textBox15 = new TextBox();
            textBox17 = new TextBox();
            lbCrownSlice = new Label();
            lbCrownMaxH = new Label();
            lbCrownMinH = new Label();
            tpMainCalc = new TabPage();
            panel6 = new Panel();
            textBox12 = new TextBox();
            lbTreeSegment = new Label();
            textBox11 = new TextBox();
            lbTreeNnear = new Label();
            textBox10 = new TextBox();
            lbTreeSmooth = new Label();
            textBox1 = new TextBox();
            lbTreeThres = new Label();
            lbTreeMinDBH = new Label();
            textBox4 = new TextBox();
            lbMaxDBH = new Label();
            panel7 = new Panel();
            textBox3 = new TextBox();
            lbMeasureAttribute = new Label();
            lbMeasureNnear = new Label();
            pnSideMenu.SuspendLayout();
            panel2.SuspendLayout();
            tcMainHome.SuspendLayout();
            tpMainHome.SuspendLayout();
            pnReviewMain.SuspendLayout();
            pnMain.SuspendLayout();
            tpMainExtract.SuspendLayout();
            panel1.SuspendLayout();
            tpMainTidying.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            tpMainCalc.SuspendLayout();
            panel6.SuspendLayout();
            panel7.SuspendLayout();
            SuspendLayout();
            // 
            // pnSideMenu
            // 
            pnSideMenu.BackColor = Color.SeaGreen;
            pnSideMenu.Controls.Add(btnSettingSave);
            pnSideMenu.Controls.Add(btnTidyMenu);
            pnSideMenu.Controls.Add(btnCalcMenu);
            pnSideMenu.Controls.Add(btnExtractMenu);
            pnSideMenu.Controls.Add(btnSlideMenu);
            pnSideMenu.Controls.Add(btnHome);
            pnSideMenu.Dock = DockStyle.Left;
            pnSideMenu.Location = new Point(0, 0);
            pnSideMenu.Margin = new Padding(3, 4, 3, 4);
            pnSideMenu.Name = "pnSideMenu";
            pnSideMenu.Size = new Size(384, 800);
            pnSideMenu.TabIndex = 0;
            // 
            // btnSettingSave
            // 
            btnSettingSave.BackColor = Color.Transparent;
            btnSettingSave.FlatAppearance.BorderSize = 0;
            btnSettingSave.FlatAppearance.MouseDownBackColor = Color.MediumAquamarine;
            btnSettingSave.FlatAppearance.MouseOverBackColor = Color.MediumSeaGreen;
            btnSettingSave.FlatStyle = FlatStyle.Flat;
            btnSettingSave.Font = new Font("나눔고딕 ExtraBold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnSettingSave.Image = (Image)resources.GetObject("btnSettingSave.Image");
            btnSettingSave.ImageAlign = ContentAlignment.MiddleLeft;
            btnSettingSave.Location = new Point(0, 755);
            btnSettingSave.Margin = new Padding(3, 4, 3, 4);
            btnSettingSave.Name = "btnSettingSave";
            btnSettingSave.Size = new Size(189, 45);
            btnSettingSave.TabIndex = 8;
            btnSettingSave.Text = "       Settings Save";
            btnSettingSave.UseVisualStyleBackColor = false;
            // 
            // btnTidyMenu
            // 
            btnTidyMenu.BackColor = Color.Transparent;
            btnTidyMenu.FlatAppearance.BorderSize = 0;
            btnTidyMenu.FlatAppearance.MouseDownBackColor = Color.MediumAquamarine;
            btnTidyMenu.FlatAppearance.MouseOverBackColor = Color.MediumSeaGreen;
            btnTidyMenu.FlatStyle = FlatStyle.Flat;
            btnTidyMenu.Font = new Font("나눔고딕 ExtraBold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnTidyMenu.Image = (Image)resources.GetObject("btnTidyMenu.Image");
            btnTidyMenu.ImageAlign = ContentAlignment.MiddleLeft;
            btnTidyMenu.Location = new Point(9, 162);
            btnTidyMenu.Margin = new Padding(3, 4, 3, 4);
            btnTidyMenu.Name = "btnTidyMenu";
            btnTidyMenu.Size = new Size(373, 45);
            btnTidyMenu.TabIndex = 7;
            btnTidyMenu.Text = "            Tidying up";
            btnTidyMenu.TextAlign = ContentAlignment.MiddleLeft;
            btnTidyMenu.UseVisualStyleBackColor = false;
            btnTidyMenu.Click += btnTidyMenu_Click;
            // 
            // btnCalcMenu
            // 
            btnCalcMenu.BackColor = Color.Transparent;
            btnCalcMenu.FlatAppearance.BorderSize = 0;
            btnCalcMenu.FlatAppearance.MouseDownBackColor = Color.MediumAquamarine;
            btnCalcMenu.FlatAppearance.MouseOverBackColor = Color.MediumSeaGreen;
            btnCalcMenu.FlatStyle = FlatStyle.Flat;
            btnCalcMenu.Font = new Font("나눔고딕 ExtraBold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnCalcMenu.Image = (Image)resources.GetObject("btnCalcMenu.Image");
            btnCalcMenu.ImageAlign = ContentAlignment.MiddleLeft;
            btnCalcMenu.Location = new Point(9, 214);
            btnCalcMenu.Margin = new Padding(3, 4, 3, 4);
            btnCalcMenu.Name = "btnCalcMenu";
            btnCalcMenu.Size = new Size(373, 45);
            btnCalcMenu.TabIndex = 6;
            btnCalcMenu.Text = "            Calculating";
            btnCalcMenu.TextAlign = ContentAlignment.MiddleLeft;
            btnCalcMenu.UseVisualStyleBackColor = false;
            btnCalcMenu.Click += btnCalcMenu_Click;
            // 
            // btnExtractMenu
            // 
            btnExtractMenu.BackColor = Color.Transparent;
            btnExtractMenu.FlatAppearance.BorderSize = 0;
            btnExtractMenu.FlatAppearance.MouseDownBackColor = Color.MediumAquamarine;
            btnExtractMenu.FlatAppearance.MouseOverBackColor = Color.MediumSeaGreen;
            btnExtractMenu.FlatStyle = FlatStyle.Flat;
            btnExtractMenu.Font = new Font("나눔고딕 ExtraBold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnExtractMenu.Image = (Image)resources.GetObject("btnExtractMenu.Image");
            btnExtractMenu.ImageAlign = ContentAlignment.MiddleLeft;
            btnExtractMenu.Location = new Point(9, 110);
            btnExtractMenu.Margin = new Padding(3, 4, 3, 4);
            btnExtractMenu.Name = "btnExtractMenu";
            btnExtractMenu.Size = new Size(373, 45);
            btnExtractMenu.TabIndex = 2;
            btnExtractMenu.Text = "            Extracting";
            btnExtractMenu.TextAlign = ContentAlignment.MiddleLeft;
            btnExtractMenu.UseVisualStyleBackColor = false;
            btnExtractMenu.Click += btnExtractMenu_Click;
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
            btnSlideMenu.Click += btnSlideMenu_Click;
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
            btnHome.Click += btnHome_Click;
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
            btnClose.Location = new Point(1236, 0);
            btnClose.Margin = new Padding(3, 4, 3, 4);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(43, 30);
            btnClose.TabIndex = 1;
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.LightGray;
            button3.BackgroundImageLayout = ImageLayout.Center;
            button3.FlatAppearance.BorderColor = SystemColors.AppWorkspace;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.MouseDownBackColor = Color.LightGray;
            button3.FlatAppearance.MouseOverBackColor = Color.WhiteSmoke;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Image = (Image)resources.GetObject("button3.Image");
            button3.Location = new Point(1193, 0);
            button3.Margin = new Padding(3, 4, 3, 4);
            button3.Name = "button3";
            button3.Size = new Size(43, 30);
            button3.TabIndex = 2;
            button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            button4.BackColor = Color.LightGray;
            button4.BackgroundImageLayout = ImageLayout.Center;
            button4.FlatAppearance.BorderColor = SystemColors.AppWorkspace;
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatAppearance.MouseDownBackColor = Color.LightGray;
            button4.FlatAppearance.MouseOverBackColor = Color.WhiteSmoke;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Image = (Image)resources.GetObject("button4.Image");
            button4.Location = new Point(1150, 0);
            button4.Margin = new Padding(3, 4, 3, 4);
            button4.Name = "button4";
            button4.Size = new Size(43, 30);
            button4.TabIndex = 3;
            button4.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(tbOutlierMeank);
            panel2.Controls.Add(tbOutlierMul);
            panel2.Controls.Add(tbOutlierMethod);
            panel2.Controls.Add(lbOutlierMul);
            panel2.Controls.Add(lbOutlierMeank);
            panel2.Controls.Add(lbOutlierMethod);
            panel2.Controls.Add(lbOutlierRemove);
            panel2.Location = new Point(22, 210);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(840, 195);
            panel2.TabIndex = 22;
            // 
            // tbOutlierMeank
            // 
            tbOutlierMeank.Location = new Point(188, 105);
            tbOutlierMeank.Margin = new Padding(3, 4, 3, 4);
            tbOutlierMeank.Name = "tbOutlierMeank";
            tbOutlierMeank.Size = new Size(109, 23);
            tbOutlierMeank.TabIndex = 22;
            // 
            // tbOutlierMul
            // 
            tbOutlierMul.Location = new Point(188, 149);
            tbOutlierMul.Margin = new Padding(3, 4, 3, 4);
            tbOutlierMul.Name = "tbOutlierMul";
            tbOutlierMul.Size = new Size(109, 23);
            tbOutlierMul.TabIndex = 21;
            // 
            // tbOutlierMethod
            // 
            tbOutlierMethod.Location = new Point(188, 61);
            tbOutlierMethod.Margin = new Padding(3, 4, 3, 4);
            tbOutlierMethod.Name = "tbOutlierMethod";
            tbOutlierMethod.Size = new Size(109, 23);
            tbOutlierMethod.TabIndex = 20;
            // 
            // lbOutlierMul
            // 
            lbOutlierMul.AutoSize = true;
            lbOutlierMul.Location = new Point(76, 153);
            lbOutlierMul.Name = "lbOutlierMul";
            lbOutlierMul.Size = new Size(61, 15);
            lbOutlierMul.TabIndex = 19;
            lbOutlierMul.Text = "Mulitiplier";
            // 
            // lbOutlierMeank
            // 
            lbOutlierMeank.AutoSize = true;
            lbOutlierMeank.Location = new Point(76, 109);
            lbOutlierMeank.Name = "lbOutlierMeank";
            lbOutlierMeank.Size = new Size(43, 15);
            lbOutlierMeank.TabIndex = 18;
            lbOutlierMeank.Text = "Meank";
            // 
            // lbOutlierMethod
            // 
            lbOutlierMethod.AutoSize = true;
            lbOutlierMethod.Location = new Point(76, 65);
            lbOutlierMethod.Name = "lbOutlierMethod";
            lbOutlierMethod.Size = new Size(49, 15);
            lbOutlierMethod.TabIndex = 17;
            lbOutlierMethod.Text = "Method";
            // 
            // lbOutlierRemove
            // 
            lbOutlierRemove.AutoSize = true;
            lbOutlierRemove.Font = new Font("굴림", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            lbOutlierRemove.Location = new Point(23, 18);
            lbOutlierRemove.Name = "lbOutlierRemove";
            lbOutlierRemove.Size = new Size(133, 21);
            lbOutlierRemove.TabIndex = 16;
            lbOutlierRemove.Text = "OutlierRemove";
            // 
            // tcMainHome
            // 
            tcMainHome.Controls.Add(tpMainHome);
            tcMainHome.Controls.Add(tpMainExtract);
            tcMainHome.Controls.Add(tpMainTidying);
            tcMainHome.Controls.Add(tpMainCalc);
            tcMainHome.ItemSize = new Size(0, 5);
            tcMainHome.Location = new Point(379, 30);
            tcMainHome.Multiline = true;
            tcMainHome.Name = "tcMainHome";
            tcMainHome.SelectedIndex = 0;
            tcMainHome.Size = new Size(909, 770);
            tcMainHome.TabIndex = 4;
            // 
            // tpMainHome
            // 
            tpMainHome.Controls.Add(pnReviewMain);
            tpMainHome.Controls.Add(pnMain);
            tpMainHome.Location = new Point(4, 9);
            tpMainHome.Name = "tpMainHome";
            tpMainHome.Padding = new Padding(3);
            tpMainHome.Size = new Size(901, 757);
            tpMainHome.TabIndex = 0;
            tpMainHome.UseVisualStyleBackColor = true;
            // 
            // pnReviewMain
            // 
            pnReviewMain.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            pnReviewMain.BackColor = Color.Beige;
            pnReviewMain.Controls.Add(pnReview);
            pnReviewMain.Controls.Add(lbReview);
            pnReviewMain.Location = new Point(0, 443);
            pnReviewMain.Margin = new Padding(3, 4, 3, 4);
            pnReviewMain.Name = "pnReviewMain";
            pnReviewMain.Size = new Size(906, 317);
            pnReviewMain.TabIndex = 9;
            // 
            // pnReview
            // 
            pnReview.AutoScroll = true;
            pnReview.Location = new Point(0, 67);
            pnReview.Margin = new Padding(3, 4, 3, 4);
            pnReview.Name = "pnReview";
            pnReview.Size = new Size(901, 251);
            pnReview.TabIndex = 1;
            // 
            // lbReview
            // 
            lbReview.AutoSize = true;
            lbReview.Font = new Font("굴림", 24F, FontStyle.Regular, GraphicsUnit.Point);
            lbReview.Location = new Point(16, 17);
            lbReview.Name = "lbReview";
            lbReview.Size = new Size(199, 32);
            lbReview.TabIndex = 0;
            lbReview.Text = "Recent Task";
            lbReview.TextAlign = ContentAlignment.BottomLeft;
            // 
            // pnMain
            // 
            pnMain.Controls.Add(lbSubTitle);
            pnMain.Controls.Add(btnStart);
            pnMain.Controls.Add(lbTitle);
            pnMain.Location = new Point(0, 0);
            pnMain.Margin = new Padding(3, 4, 3, 4);
            pnMain.Name = "pnMain";
            pnMain.Size = new Size(908, 442);
            pnMain.TabIndex = 10;
            // 
            // lbSubTitle
            // 
            lbSubTitle.AutoSize = true;
            lbSubTitle.Font = new Font("굴림", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbSubTitle.Location = new Point(18, 100);
            lbSubTitle.Name = "lbSubTitle";
            lbSubTitle.Size = new Size(209, 16);
            lbSubTitle.TabIndex = 5;
            lbSubTitle.Text = "Forest ICT Research Center";
            // 
            // btnStart
            // 
            btnStart.Anchor = AnchorStyles.Right;
            btnStart.Font = new Font("굴림", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnStart.Location = new Point(663, 212);
            btnStart.Margin = new Padding(3, 4, 3, 4);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(179, 61);
            btnStart.TabIndex = 7;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click_1;
            // 
            // lbTitle
            // 
            lbTitle.AutoSize = true;
            lbTitle.Font = new Font("나눔고딕", 63.75F, FontStyle.Regular, GraphicsUnit.Point);
            lbTitle.Location = new Point(-3, 1);
            lbTitle.Name = "lbTitle";
            lbTitle.Size = new Size(343, 98);
            lbTitle.TabIndex = 4;
            lbTitle.Text = "ForestLi\n";
            // 
            // tpMainExtract
            // 
            tpMainExtract.Controls.Add(panel1);
            tpMainExtract.Controls.Add(panel2);
            tpMainExtract.Location = new Point(4, 9);
            tpMainExtract.Name = "tpMainExtract";
            tpMainExtract.Padding = new Padding(3);
            tpMainExtract.Size = new Size(901, 757);
            tpMainExtract.TabIndex = 1;
            tpMainExtract.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(tbSubCellSize);
            panel1.Controls.Add(lbSubsampling);
            panel1.Controls.Add(lbSubCellSize);
            panel1.Location = new Point(23, 50);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(840, 105);
            panel1.TabIndex = 25;
            // 
            // tbSubCellSize
            // 
            tbSubCellSize.Location = new Point(187, 60);
            tbSubCellSize.Margin = new Padding(3, 4, 3, 4);
            tbSubCellSize.Name = "tbSubCellSize";
            tbSubCellSize.Size = new Size(109, 23);
            tbSubCellSize.TabIndex = 24;
            // 
            // lbSubsampling
            // 
            lbSubsampling.AutoSize = true;
            lbSubsampling.Font = new Font("굴림", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            lbSubsampling.Location = new Point(23, 18);
            lbSubsampling.Name = "lbSubsampling";
            lbSubsampling.Size = new Size(118, 21);
            lbSubsampling.TabIndex = 22;
            lbSubsampling.Text = "SubSampling";
            // 
            // lbSubCellSize
            // 
            lbSubCellSize.AutoSize = true;
            lbSubCellSize.Location = new Point(75, 65);
            lbSubCellSize.Name = "lbSubCellSize";
            lbSubCellSize.Size = new Size(53, 15);
            lbSubCellSize.TabIndex = 23;
            lbSubCellSize.Text = "Cell Size";
            // 
            // tpMainTidying
            // 
            tpMainTidying.Controls.Add(panel4);
            tpMainTidying.Controls.Add(panel5);
            tpMainTidying.Location = new Point(4, 9);
            tpMainTidying.Name = "tpMainTidying";
            tpMainTidying.Padding = new Padding(3);
            tpMainTidying.Size = new Size(901, 757);
            tpMainTidying.TabIndex = 2;
            tpMainTidying.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            panel4.Controls.Add(textBox19);
            panel4.Controls.Add(lbTrunkSlice);
            panel4.Controls.Add(lbTrunkMinH);
            panel4.Controls.Add(lbTrunkMaxH);
            panel4.Controls.Add(textBox18);
            panel4.Location = new Point(23, 50);
            panel4.Margin = new Padding(3, 4, 3, 4);
            panel4.Name = "panel4";
            panel4.Size = new Size(840, 150);
            panel4.TabIndex = 56;
            // 
            // textBox19
            // 
            textBox19.Location = new Point(187, 104);
            textBox19.Margin = new Padding(3, 4, 3, 4);
            textBox19.Name = "textBox19";
            textBox19.Size = new Size(109, 23);
            textBox19.TabIndex = 55;
            // 
            // lbTrunkSlice
            // 
            lbTrunkSlice.AutoSize = true;
            lbTrunkSlice.Font = new Font("굴림", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            lbTrunkSlice.Location = new Point(23, 18);
            lbTrunkSlice.Name = "lbTrunkSlice";
            lbTrunkSlice.Size = new Size(97, 21);
            lbTrunkSlice.TabIndex = 46;
            lbTrunkSlice.Text = "TrunkSlice";
            // 
            // lbTrunkMinH
            // 
            lbTrunkMinH.AutoSize = true;
            lbTrunkMinH.Location = new Point(75, 65);
            lbTrunkMinH.Name = "lbTrunkMinH";
            lbTrunkMinH.Size = new Size(64, 15);
            lbTrunkMinH.TabIndex = 48;
            lbTrunkMinH.Text = "MinHeight";
            // 
            // lbTrunkMaxH
            // 
            lbTrunkMaxH.AutoSize = true;
            lbTrunkMaxH.Location = new Point(75, 109);
            lbTrunkMaxH.Name = "lbTrunkMaxH";
            lbTrunkMaxH.Size = new Size(66, 15);
            lbTrunkMaxH.TabIndex = 54;
            lbTrunkMaxH.Text = "MaxHeight";
            // 
            // textBox18
            // 
            textBox18.Location = new Point(187, 61);
            textBox18.Margin = new Padding(3, 4, 3, 4);
            textBox18.Name = "textBox18";
            textBox18.Size = new Size(109, 23);
            textBox18.TabIndex = 51;
            // 
            // panel5
            // 
            panel5.Controls.Add(textBox15);
            panel5.Controls.Add(textBox17);
            panel5.Controls.Add(lbCrownSlice);
            panel5.Controls.Add(lbCrownMaxH);
            panel5.Controls.Add(lbCrownMinH);
            panel5.Location = new Point(23, 255);
            panel5.Margin = new Padding(3, 4, 3, 4);
            panel5.Name = "panel5";
            panel5.Size = new Size(840, 150);
            panel5.TabIndex = 57;
            // 
            // textBox15
            // 
            textBox15.Location = new Point(187, 104);
            textBox15.Margin = new Padding(3, 4, 3, 4);
            textBox15.Name = "textBox15";
            textBox15.Size = new Size(109, 23);
            textBox15.TabIndex = 53;
            // 
            // textBox17
            // 
            textBox17.Location = new Point(187, 61);
            textBox17.Margin = new Padding(3, 4, 3, 4);
            textBox17.Name = "textBox17";
            textBox17.Size = new Size(109, 23);
            textBox17.TabIndex = 52;
            // 
            // lbCrownSlice
            // 
            lbCrownSlice.AutoSize = true;
            lbCrownSlice.Font = new Font("굴림", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            lbCrownSlice.Location = new Point(23, 18);
            lbCrownSlice.Name = "lbCrownSlice";
            lbCrownSlice.Size = new Size(104, 21);
            lbCrownSlice.TabIndex = 47;
            lbCrownSlice.Text = "CrownSlice";
            // 
            // lbCrownMaxH
            // 
            lbCrownMaxH.AutoSize = true;
            lbCrownMaxH.Location = new Point(75, 109);
            lbCrownMaxH.Name = "lbCrownMaxH";
            lbCrownMaxH.Size = new Size(66, 15);
            lbCrownMaxH.TabIndex = 50;
            lbCrownMaxH.Text = "MaxHeight";
            // 
            // lbCrownMinH
            // 
            lbCrownMinH.AutoSize = true;
            lbCrownMinH.Location = new Point(75, 65);
            lbCrownMinH.Name = "lbCrownMinH";
            lbCrownMinH.Size = new Size(64, 15);
            lbCrownMinH.TabIndex = 49;
            lbCrownMinH.Text = "MinHeight";
            // 
            // tpMainCalc
            // 
            tpMainCalc.Controls.Add(panel6);
            tpMainCalc.Controls.Add(panel7);
            tpMainCalc.Location = new Point(4, 9);
            tpMainCalc.Name = "tpMainCalc";
            tpMainCalc.Padding = new Padding(3);
            tpMainCalc.Size = new Size(901, 757);
            tpMainCalc.TabIndex = 3;
            tpMainCalc.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            panel6.Controls.Add(textBox12);
            panel6.Controls.Add(lbTreeSegment);
            panel6.Controls.Add(textBox11);
            panel6.Controls.Add(lbTreeNnear);
            panel6.Controls.Add(textBox10);
            panel6.Controls.Add(lbTreeSmooth);
            panel6.Controls.Add(textBox1);
            panel6.Controls.Add(lbTreeThres);
            panel6.Controls.Add(lbTreeMinDBH);
            panel6.Controls.Add(textBox4);
            panel6.Controls.Add(lbMaxDBH);
            panel6.Location = new Point(23, 50);
            panel6.Margin = new Padding(3, 4, 3, 4);
            panel6.Name = "panel6";
            panel6.Size = new Size(840, 285);
            panel6.TabIndex = 76;
            // 
            // textBox12
            // 
            textBox12.Location = new Point(187, 237);
            textBox12.Margin = new Padding(3, 4, 3, 4);
            textBox12.Name = "textBox12";
            textBox12.Size = new Size(109, 23);
            textBox12.TabIndex = 75;
            // 
            // lbTreeSegment
            // 
            lbTreeSegment.AutoSize = true;
            lbTreeSegment.Font = new Font("굴림", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            lbTreeSegment.Location = new Point(23, 18);
            lbTreeSegment.Name = "lbTreeSegment";
            lbTreeSegment.Size = new Size(125, 21);
            lbTreeSegment.TabIndex = 62;
            lbTreeSegment.Text = "TreeSegment";
            // 
            // textBox11
            // 
            textBox11.Location = new Point(187, 193);
            textBox11.Margin = new Padding(3, 4, 3, 4);
            textBox11.Name = "textBox11";
            textBox11.Size = new Size(109, 23);
            textBox11.TabIndex = 73;
            // 
            // lbTreeNnear
            // 
            lbTreeNnear.AutoSize = true;
            lbTreeNnear.Location = new Point(75, 65);
            lbTreeNnear.Name = "lbTreeNnear";
            lbTreeNnear.Size = new Size(54, 15);
            lbTreeNnear.TabIndex = 64;
            lbTreeNnear.Text = "Nnearest";
            // 
            // textBox10
            // 
            textBox10.Location = new Point(187, 149);
            textBox10.Margin = new Padding(3, 4, 3, 4);
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(109, 23);
            textBox10.TabIndex = 71;
            // 
            // lbTreeSmooth
            // 
            lbTreeSmooth.AutoSize = true;
            lbTreeSmooth.Location = new Point(75, 109);
            lbTreeSmooth.Name = "lbTreeSmooth";
            lbTreeSmooth.Size = new Size(73, 15);
            lbTreeSmooth.TabIndex = 68;
            lbTreeSmooth.Text = "Smoothness";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(187, 105);
            textBox1.Margin = new Padding(3, 4, 3, 4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(109, 23);
            textBox1.TabIndex = 69;
            // 
            // lbTreeThres
            // 
            lbTreeThres.AutoSize = true;
            lbTreeThres.Location = new Point(75, 153);
            lbTreeThres.Name = "lbTreeThres";
            lbTreeThres.Size = new Size(99, 15);
            lbTreeThres.TabIndex = 70;
            lbTreeThres.Text = "Threshold Height";
            // 
            // lbTreeMinDBH
            // 
            lbTreeMinDBH.AutoSize = true;
            lbTreeMinDBH.Location = new Point(75, 197);
            lbTreeMinDBH.Name = "lbTreeMinDBH";
            lbTreeMinDBH.Size = new Size(53, 15);
            lbTreeMinDBH.TabIndex = 72;
            lbTreeMinDBH.Text = "MinDBH";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(187, 61);
            textBox4.Margin = new Padding(3, 4, 3, 4);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(109, 23);
            textBox4.TabIndex = 66;
            // 
            // lbMaxDBH
            // 
            lbMaxDBH.AutoSize = true;
            lbMaxDBH.Location = new Point(75, 241);
            lbMaxDBH.Name = "lbMaxDBH";
            lbMaxDBH.Size = new Size(55, 15);
            lbMaxDBH.TabIndex = 74;
            lbMaxDBH.Text = "MaxDBH";
            // 
            // panel7
            // 
            panel7.Controls.Add(textBox3);
            panel7.Controls.Add(lbMeasureAttribute);
            panel7.Controls.Add(lbMeasureNnear);
            panel7.Location = new Point(25, 390);
            panel7.Margin = new Padding(3, 4, 3, 4);
            panel7.Name = "panel7";
            panel7.Size = new Size(840, 105);
            panel7.TabIndex = 77;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(187, 60);
            textBox3.Margin = new Padding(3, 4, 3, 4);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(109, 23);
            textBox3.TabIndex = 67;
            // 
            // lbMeasureAttribute
            // 
            lbMeasureAttribute.AutoSize = true;
            lbMeasureAttribute.Font = new Font("굴림", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            lbMeasureAttribute.Location = new Point(23, 18);
            lbMeasureAttribute.Name = "lbMeasureAttribute";
            lbMeasureAttribute.Size = new Size(151, 21);
            lbMeasureAttribute.TabIndex = 63;
            lbMeasureAttribute.Text = "MeasureAttribute";
            // 
            // lbMeasureNnear
            // 
            lbMeasureNnear.AutoSize = true;
            lbMeasureNnear.Location = new Point(75, 64);
            lbMeasureNnear.Name = "lbMeasureNnear";
            lbMeasureNnear.Size = new Size(54, 15);
            lbMeasureNnear.TabIndex = 65;
            lbMeasureNnear.Text = "Nnearest";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 800);
            Controls.Add(pnSideMenu);
            Controls.Add(tcMainHome);
            Controls.Add(btnClose);
            Controls.Add(button3);
            Controls.Add(button4);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "ForestLi";
            pnSideMenu.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            tcMainHome.ResumeLayout(false);
            tpMainHome.ResumeLayout(false);
            pnReviewMain.ResumeLayout(false);
            pnReviewMain.PerformLayout();
            pnMain.ResumeLayout(false);
            pnMain.PerformLayout();
            tpMainExtract.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tpMainTidying.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            tpMainCalc.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnSideMenu;
        private Button btnHome;
        private Button btnSlideMenu;
        private Button btnClose;
        private Button button3;
        private Button button4;
        private Button btnExtractMenu;
        private Button btnTidyMenu;
        private Button btnCalcMenu;
        private Panel panel2;
        private CustomTabControl tcMainHome;
        private TabPage tpMainHome;
        private Panel pnReviewMain;
        private Panel pnReview;
        private Label lbReview;
        private Panel pnMain;
        private Label lbSubTitle;
        private Button btnStart;
        private Label lbTitle;
        private TabPage tpMainExtract;
        private TabPage tpMainTidying;
        private TextBox tbOutlierMeank;
        private TextBox tbOutlierMul;
        private TextBox tbOutlierMethod;
        private Label lbOutlierMul;
        private Label lbOutlierMeank;
        private Label lbOutlierMethod;
        private Label lbOutlierRemove;
        private Panel panel1;
        private TextBox tbSubCellSize;
        private Label lbSubsampling;
        private Label lbSubCellSize;
        private TextBox textBox19;
        private TextBox textBox15;
        private TextBox textBox17;
        private TextBox textBox18;
        private Label lbTrunkMaxH;
        private Label lbCrownMaxH;
        private Label lbCrownMinH;
        private Label lbTrunkMinH;
        private Label lbCrownSlice;
        private Label lbTrunkSlice;
        private Panel panel4;
        private Panel panel5;
        private TabPage tpMainCalc;
        private TextBox textBox12;
        private TextBox textBox11;
        private TextBox textBox10;
        private TextBox textBox1;
        private TextBox textBox3;
        private TextBox textBox4;
        private Label lbMaxDBH;
        private Label lbTreeMinDBH;
        private Label lbTreeThres;
        private Label lbTreeSmooth;
        private Label lbMeasureNnear;
        private Label lbTreeNnear;
        private Label lbMeasureAttribute;
        private Label lbTreeSegment;
        private Panel panel6;
        private Panel panel7;
        private Button btnSettingSave;
    }
}