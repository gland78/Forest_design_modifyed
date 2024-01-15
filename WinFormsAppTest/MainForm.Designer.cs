using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Timer = System.Windows.Forms.Timer;

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
            btnSettings = new CustomBtn();
            lbSetting = new Label();
            lbHome = new Label();
            btnHome = new CustomBtn();
            tcMainHome = new CustomTabControl();
            tpMainHome = new TabPage();
            pnMain = new CustomPanel();
            lbSubTitle = new Label();
            btnStart = new CustomBtn();
            lbTitle = new Label();
            tpSettings = new TabPage();
            pnSettingFileDel = new CustomPanel();
            lbSettingToggle = new Label();
            tgBtnSettingFileDel = new LiDAR_PROJECT.ToggleBtn();
            lbSettingFileDelInfo = new Label();
            lbSettingFileDel = new Label();
            pnSettingCrown1 = new CustomPanel();
            lbCrownVoxel = new Label();
            tbCrownVoxel = new TextBox();
            pnSettingCrown2 = new CustomPanel();
            lbCrownRadius = new Label();
            tbCrownRadius = new TextBox();
            pnSettingReset = new CustomPanel();
            btnSettingReset = new CustomBtn();
            lbSettingResetInfo = new Label();
            lbSettingReset = new Label();
            pnSettingTrunk3 = new CustomPanel();
            lbTrunkSmooth = new Label();
            tbTrunkSmooth = new TextBox();
            btnSettingCancel = new CustomBtn();
            btnSettingApply = new CustomBtn();
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
            pnSettingTrunk2 = new CustomPanel();
            lbTrunkMaxH = new Label();
            tbTrunkMaxHeight = new TextBox();
            lbCrownSlice = new Label();
            lbTrunkSlice = new Label();
            pnSettingCrown3 = new CustomPanel();
            lbCrownMinH = new Label();
            tbCrownMinHeight = new TextBox();
            pnSettingTrunk1 = new CustomPanel();
            tbTrunkMinHeight = new TextBox();
            lbTrunkMinH = new Label();
            ttMainInfo = new ToolTip(components);
            pnSideMenu.SuspendLayout();
            tcMainHome.SuspendLayout();
            tpMainHome.SuspendLayout();
            pnMain.SuspendLayout();
            tpSettings.SuspendLayout();
            pnSettingFileDel.SuspendLayout();
            pnSettingCrown1.SuspendLayout();
            pnSettingCrown2.SuspendLayout();
            pnSettingReset.SuspendLayout();
            pnSettingTrunk3.SuspendLayout();
            pnSettingNor5.SuspendLayout();
            pnSettingNor4.SuspendLayout();
            pnSettingNor3.SuspendLayout();
            pnSettingNor2.SuspendLayout();
            pnSettingNor1.SuspendLayout();
            pnSettingTrunk2.SuspendLayout();
            pnSettingCrown3.SuspendLayout();
            pnSettingTrunk1.SuspendLayout();
            SuspendLayout();
            // 
            // pnSideMenu
            // 
            pnSideMenu.BackColor = Color.FromArgb(60, 94, 94);
            pnSideMenu.BackgroundImageLayout = ImageLayout.Zoom;
            pnSideMenu.Controls.Add(btnSettings);
            pnSideMenu.Controls.Add(lbSetting);
            pnSideMenu.Controls.Add(lbHome);
            pnSideMenu.Controls.Add(btnHome);
            pnSideMenu.Dock = DockStyle.Left;
            pnSideMenu.Location = new Point(0, 0);
            pnSideMenu.Margin = new Padding(3, 4, 3, 4);
            pnSideMenu.Name = "pnSideMenu";
            pnSideMenu.Size = new Size(65, 500);
            pnSideMenu.TabIndex = 2;
            // 
            // btnSettings
            // 
            btnSettings.BackColor = Color.Transparent;
            btnSettings.BackgroundColor = Color.Transparent;
            btnSettings.BorderColor = Color.Transparent;
            btnSettings.BorderRadius = 5;
            btnSettings.BorderSize = 0;
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.FlatAppearance.MouseDownBackColor = Color.FromArgb(128, 162, 162);
            btnSettings.FlatAppearance.MouseOverBackColor = Color.FromArgb(96, 128, 128);
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.ForeColor = Color.White;
            btnSettings.Image = (Image)resources.GetObject("btnSettings.Image");
            btnSettings.Location = new Point(14, 66);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(38, 34);
            btnSettings.TabIndex = 10;
            btnSettings.TextColor = Color.White;
            btnSettings.UseVisualStyleBackColor = false;
            // 
            // lbSetting
            // 
            lbSetting.AutoSize = true;
            lbSetting.ForeColor = Color.White;
            lbSetting.Location = new Point(18, 101);
            lbSetting.Name = "lbSetting";
            lbSetting.Size = new Size(31, 15);
            lbSetting.TabIndex = 9;
            lbSetting.Text = "설정";
            // 
            // lbHome
            // 
            lbHome.AutoSize = true;
            lbHome.ForeColor = Color.White;
            lbHome.Location = new Point(24, 48);
            lbHome.Name = "lbHome";
            lbHome.Size = new Size(19, 15);
            lbHome.TabIndex = 4;
            lbHome.Text = "홈";
            // 
            // btnHome
            // 
            btnHome.BackColor = Color.Transparent;
            btnHome.BackgroundColor = Color.Transparent;
            btnHome.BackgroundImageLayout = ImageLayout.None;
            btnHome.BorderColor = Color.Transparent;
            btnHome.BorderRadius = 5;
            btnHome.BorderSize = 0;
            btnHome.CausesValidation = false;
            btnHome.FlatAppearance.BorderColor = Color.DarkSlateGray;
            btnHome.FlatAppearance.BorderSize = 0;
            btnHome.FlatAppearance.MouseDownBackColor = Color.FromArgb(128, 162, 162);
            btnHome.FlatAppearance.MouseOverBackColor = Color.FromArgb(96, 128, 128);
            btnHome.FlatStyle = FlatStyle.Flat;
            btnHome.ForeColor = Color.White;
            btnHome.Image = (Image)resources.GetObject("btnHome.Image");
            btnHome.Location = new Point(14, 14);
            btnHome.Margin = new Padding(3, 4, 3, 4);
            btnHome.Name = "btnHome";
            btnHome.Size = new Size(38, 34);
            btnHome.TabIndex = 0;
            btnHome.TabStop = false;
            btnHome.TextAlign = ContentAlignment.BottomCenter;
            btnHome.TextColor = Color.White;
            btnHome.TextImageRelation = TextImageRelation.ImageAboveText;
            btnHome.UseVisualStyleBackColor = false;
            // 
            // tcMainHome
            // 
            tcMainHome.Controls.Add(tpMainHome);
            tcMainHome.Controls.Add(tpSettings);
            tcMainHome.Dock = DockStyle.Right;
            tcMainHome.ItemSize = new Size(0, 10);
            tcMainHome.Location = new Point(65, 0);
            tcMainHome.Margin = new Padding(0);
            tcMainHome.Multiline = true;
            tcMainHome.Name = "tcMainHome";
            tcMainHome.Padding = new Point(0, 0);
            tcMainHome.SelectedIndex = 0;
            tcMainHome.Size = new Size(896, 500);
            tcMainHome.TabIndex = 3;
            tcMainHome.TabStop = false;
            // 
            // tpMainHome
            // 
            tpMainHome.Controls.Add(pnMain);
            tpMainHome.Location = new Point(4, 14);
            tpMainHome.Margin = new Padding(0);
            tpMainHome.Name = "tpMainHome";
            tpMainHome.Size = new Size(888, 482);
            tpMainHome.TabIndex = 0;
            tpMainHome.UseVisualStyleBackColor = true;
            // 
            // pnMain
            // 
            pnMain.BackColor = Color.Gainsboro;
            pnMain.BackgroundImageLayout = ImageLayout.Stretch;
            pnMain.Controls.Add(lbSubTitle);
            pnMain.Controls.Add(btnStart);
            pnMain.Controls.Add(lbTitle);
            pnMain.Dock = DockStyle.Fill;
            pnMain.Location = new Point(0, 0);
            pnMain.Margin = new Padding(3, 4, 3, 4);
            pnMain.Name = "pnMain";
            pnMain.Size = new Size(888, 482);
            pnMain.TabIndex = 0;
            // 
            // lbSubTitle
            // 
            lbSubTitle.AutoSize = true;
            lbSubTitle.Font = new Font("굴림", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lbSubTitle.Location = new Point(25, 114);
            lbSubTitle.Name = "lbSubTitle";
            lbSubTitle.Size = new Size(235, 16);
            lbSubTitle.TabIndex = 1;
            lbSubTitle.Text = "Forest ICT Research Center";
            // 
            // btnStart
            // 
            btnStart.Anchor = AnchorStyles.Right;
            btnStart.BackColor = Color.DimGray;
            btnStart.BackgroundColor = Color.DimGray;
            btnStart.BorderColor = Color.Transparent;
            btnStart.BorderRadius = 5;
            btnStart.BorderSize = 0;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Font = new Font("굴림", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnStart.ForeColor = Color.White;
            btnStart.Location = new Point(643, 270);
            btnStart.Margin = new Padding(3, 4, 3, 4);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(179, 61);
            btnStart.TabIndex = 2;
            btnStart.Text = "Start";
            btnStart.TextColor = Color.White;
            btnStart.UseVisualStyleBackColor = false;
            // 
            // lbTitle
            // 
            lbTitle.AutoSize = true;
            lbTitle.Font = new Font("Microsoft Sans Serif", 63.75F, FontStyle.Regular, GraphicsUnit.Point);
            lbTitle.Location = new Point(6, 20);
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
            tpSettings.BackColor = Color.Gainsboro;
            tpSettings.Controls.Add(pnSettingFileDel);
            tpSettings.Controls.Add(pnSettingCrown1);
            tpSettings.Controls.Add(pnSettingCrown2);
            tpSettings.Controls.Add(pnSettingReset);
            tpSettings.Controls.Add(pnSettingTrunk3);
            tpSettings.Controls.Add(btnSettingCancel);
            tpSettings.Controls.Add(btnSettingApply);
            tpSettings.Controls.Add(pnSettingNor5);
            tpSettings.Controls.Add(pnSettingNor4);
            tpSettings.Controls.Add(pnSettingNor3);
            tpSettings.Controls.Add(pnSettingNor2);
            tpSettings.Controls.Add(pnSettingNor1);
            tpSettings.Controls.Add(lbNormalize);
            tpSettings.Controls.Add(lbSettings);
            tpSettings.Controls.Add(pnSettingTrunk2);
            tpSettings.Controls.Add(lbCrownSlice);
            tpSettings.Controls.Add(lbTrunkSlice);
            tpSettings.Controls.Add(pnSettingCrown3);
            tpSettings.Controls.Add(pnSettingTrunk1);
            tpSettings.Location = new Point(4, 14);
            tpSettings.Name = "tpSettings";
            tpSettings.Padding = new Padding(3);
            tpSettings.Size = new Size(888, 482);
            tpSettings.TabIndex = 1;
            // 
            // pnSettingFileDel
            // 
            pnSettingFileDel.BackColor = Color.Gray;
            pnSettingFileDel.Controls.Add(lbSettingToggle);
            pnSettingFileDel.Controls.Add(tgBtnSettingFileDel);
            pnSettingFileDel.Controls.Add(lbSettingFileDelInfo);
            pnSettingFileDel.Controls.Add(lbSettingFileDel);
            pnSettingFileDel.Location = new Point(24, 187);
            pnSettingFileDel.Margin = new Padding(3, 4, 3, 4);
            pnSettingFileDel.Name = "pnSettingFileDel";
            pnSettingFileDel.Size = new Size(850, 62);
            pnSettingFileDel.TabIndex = 20;
            // 
            // lbSettingToggle
            // 
            lbSettingToggle.AutoSize = true;
            lbSettingToggle.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbSettingToggle.ForeColor = Color.White;
            lbSettingToggle.Location = new Point(704, 23);
            lbSettingToggle.Name = "lbSettingToggle";
            lbSettingToggle.Size = new Size(34, 17);
            lbSettingToggle.TabIndex = 4;
            lbSettingToggle.Text = "유지";
            // 
            // tgBtnSettingFileDel
            // 
            tgBtnSettingFileDel.AutoSize = true;
            tgBtnSettingFileDel.BackColor = Color.White;
            tgBtnSettingFileDel.ForeColor = Color.Black;
            tgBtnSettingFileDel.Location = new Point(740, 16);
            tgBtnSettingFileDel.MaximumSize = new Size(65, 30);
            tgBtnSettingFileDel.MinimumSize = new Size(81, 30);
            tgBtnSettingFileDel.Name = "tgBtnSettingFileDel";
            tgBtnSettingFileDel.OffBackColor = Color.Gray;
            tgBtnSettingFileDel.OffToggleColor = Color.Gainsboro;
            tgBtnSettingFileDel.OnBackColor = Color.MediumSlateBlue;
            tgBtnSettingFileDel.OnToggleColor = Color.WhiteSmoke;
            tgBtnSettingFileDel.Size = new Size(81, 30);
            tgBtnSettingFileDel.TabIndex = 3;
            tgBtnSettingFileDel.UseVisualStyleBackColor = false;
            tgBtnSettingFileDel.CheckedChanged += tgBtnSettingFileDel_CheckedChanged;
            // 
            // lbSettingFileDelInfo
            // 
            lbSettingFileDelInfo.AutoSize = true;
            lbSettingFileDelInfo.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            lbSettingFileDelInfo.ForeColor = Color.White;
            lbSettingFileDelInfo.Location = new Point(18, 31);
            lbSettingFileDelInfo.Name = "lbSettingFileDelInfo";
            lbSettingFileDelInfo.Size = new Size(296, 17);
            lbSettingFileDelInfo.TabIndex = 2;
            lbSettingFileDelInfo.Text = "개체목 추출 과정에서 나온 파일들을 삭제합니다.";
            // 
            // lbSettingFileDel
            // 
            lbSettingFileDel.AutoSize = true;
            lbSettingFileDel.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbSettingFileDel.ForeColor = Color.White;
            lbSettingFileDel.Location = new Point(19, 15);
            lbSettingFileDel.Name = "lbSettingFileDel";
            lbSettingFileDel.Size = new Size(127, 17);
            lbSettingFileDel.TabIndex = 1;
            lbSettingFileDel.Text = "작업 과정 파일 정리";
            // 
            // pnSettingCrown1
            // 
            pnSettingCrown1.BackColor = Color.Gray;
            pnSettingCrown1.Controls.Add(lbCrownVoxel);
            pnSettingCrown1.Controls.Add(tbCrownVoxel);
            pnSettingCrown1.Location = new Point(24, 943);
            pnSettingCrown1.Margin = new Padding(3, 4, 3, 4);
            pnSettingCrown1.Name = "pnSettingCrown1";
            pnSettingCrown1.Size = new Size(850, 58);
            pnSettingCrown1.TabIndex = 14;
            // 
            // lbCrownVoxel
            // 
            lbCrownVoxel.AutoSize = true;
            lbCrownVoxel.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbCrownVoxel.ForeColor = Color.White;
            lbCrownVoxel.Location = new Point(21, 21);
            lbCrownVoxel.Name = "lbCrownVoxel";
            lbCrownVoxel.Size = new Size(86, 17);
            lbCrownVoxel.TabIndex = 1;
            lbCrownVoxel.Text = "VoxelLength";
            // 
            // tbCrownVoxel
            // 
            tbCrownVoxel.BorderStyle = BorderStyle.FixedSingle;
            tbCrownVoxel.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbCrownVoxel.Location = new Point(725, 15);
            tbCrownVoxel.Margin = new Padding(3, 4, 3, 4);
            tbCrownVoxel.Name = "tbCrownVoxel";
            tbCrownVoxel.Size = new Size(109, 29);
            tbCrownVoxel.TabIndex = 0;
            // 
            // pnSettingCrown2
            // 
            pnSettingCrown2.BackColor = Color.Gray;
            pnSettingCrown2.Controls.Add(lbCrownRadius);
            pnSettingCrown2.Controls.Add(tbCrownRadius);
            pnSettingCrown2.Location = new Point(24, 1009);
            pnSettingCrown2.Margin = new Padding(3, 4, 3, 4);
            pnSettingCrown2.Name = "pnSettingCrown2";
            pnSettingCrown2.Size = new Size(850, 58);
            pnSettingCrown2.TabIndex = 13;
            // 
            // lbCrownRadius
            // 
            lbCrownRadius.AutoSize = true;
            lbCrownRadius.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbCrownRadius.ForeColor = Color.White;
            lbCrownRadius.Location = new Point(21, 21);
            lbCrownRadius.Name = "lbCrownRadius";
            lbCrownRadius.Size = new Size(88, 17);
            lbCrownRadius.TabIndex = 1;
            lbCrownRadius.Text = "CrownRadius";
            // 
            // tbCrownRadius
            // 
            tbCrownRadius.BorderStyle = BorderStyle.FixedSingle;
            tbCrownRadius.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbCrownRadius.Location = new Point(725, 15);
            tbCrownRadius.Margin = new Padding(3, 4, 3, 4);
            tbCrownRadius.Name = "tbCrownRadius";
            tbCrownRadius.Size = new Size(109, 29);
            tbCrownRadius.TabIndex = 0;
            // 
            // pnSettingReset
            // 
            pnSettingReset.BackColor = Color.Gray;
            pnSettingReset.Controls.Add(btnSettingReset);
            pnSettingReset.Controls.Add(lbSettingResetInfo);
            pnSettingReset.Controls.Add(lbSettingReset);
            pnSettingReset.Location = new Point(24, 117);
            pnSettingReset.Margin = new Padding(3, 4, 3, 4);
            pnSettingReset.Name = "pnSettingReset";
            pnSettingReset.Size = new Size(850, 62);
            pnSettingReset.TabIndex = 19;
            // 
            // btnSettingReset
            // 
            btnSettingReset.BackColor = Color.FromArgb(64, 64, 64);
            btnSettingReset.BackgroundColor = Color.FromArgb(64, 64, 64);
            btnSettingReset.BorderColor = Color.Transparent;
            btnSettingReset.BorderRadius = 10;
            btnSettingReset.BorderSize = 1;
            btnSettingReset.FlatAppearance.BorderSize = 0;
            btnSettingReset.FlatAppearance.MouseDownBackColor = Color.FromArgb(80, 80, 80);
            btnSettingReset.FlatStyle = FlatStyle.Flat;
            btnSettingReset.Font = new Font("맑은 고딕", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnSettingReset.ForeColor = Color.White;
            btnSettingReset.Location = new Point(725, 14);
            btnSettingReset.Name = "btnSettingReset";
            btnSettingReset.Size = new Size(109, 34);
            btnSettingReset.TabIndex = 3;
            btnSettingReset.TabStop = false;
            btnSettingReset.Text = "DB 초기화";
            btnSettingReset.TextColor = Color.White;
            btnSettingReset.UseVisualStyleBackColor = false;
            btnSettingReset.Click += BtnFactoryReset_Click;
            // 
            // lbSettingResetInfo
            // 
            lbSettingResetInfo.AutoSize = true;
            lbSettingResetInfo.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            lbSettingResetInfo.ForeColor = Color.White;
            lbSettingResetInfo.Location = new Point(18, 31);
            lbSettingResetInfo.Name = "lbSettingResetInfo";
            lbSettingResetInfo.Size = new Size(306, 17);
            lbSettingResetInfo.TabIndex = 2;
            lbSettingResetInfo.Text = "설정값을 불러오는데 오류가 있을 시 실행해주세요";
            // 
            // lbSettingReset
            // 
            lbSettingReset.AutoSize = true;
            lbSettingReset.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbSettingReset.ForeColor = Color.White;
            lbSettingReset.Location = new Point(19, 15);
            lbSettingReset.Name = "lbSettingReset";
            lbSettingReset.Size = new Size(70, 17);
            lbSettingReset.TabIndex = 1;
            lbSettingReset.Text = "DB 초기화";
            // 
            // pnSettingTrunk3
            // 
            pnSettingTrunk3.BackColor = Color.Gray;
            pnSettingTrunk3.Controls.Add(lbTrunkSmooth);
            pnSettingTrunk3.Controls.Add(tbTrunkSmooth);
            pnSettingTrunk3.Location = new Point(24, 823);
            pnSettingTrunk3.Margin = new Padding(3, 4, 3, 4);
            pnSettingTrunk3.Name = "pnSettingTrunk3";
            pnSettingTrunk3.Size = new Size(850, 58);
            pnSettingTrunk3.TabIndex = 12;
            // 
            // lbTrunkSmooth
            // 
            lbTrunkSmooth.AutoSize = true;
            lbTrunkSmooth.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbTrunkSmooth.ForeColor = Color.White;
            lbTrunkSmooth.Location = new Point(21, 21);
            lbTrunkSmooth.Name = "lbTrunkSmooth";
            lbTrunkSmooth.Size = new Size(83, 17);
            lbTrunkSmooth.TabIndex = 1;
            lbTrunkSmooth.Text = "Smoothness";
            // 
            // tbTrunkSmooth
            // 
            tbTrunkSmooth.BorderStyle = BorderStyle.FixedSingle;
            tbTrunkSmooth.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbTrunkSmooth.Location = new Point(725, 15);
            tbTrunkSmooth.Margin = new Padding(3, 4, 3, 4);
            tbTrunkSmooth.Name = "tbTrunkSmooth";
            tbTrunkSmooth.Size = new Size(109, 29);
            tbTrunkSmooth.TabIndex = 0;
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
            // pnSettingNor5
            // 
            pnSettingNor5.BackColor = Color.Gray;
            pnSettingNor5.Controls.Add(tbNorThres);
            pnSettingNor5.Controls.Add(lbNorThres);
            pnSettingNor5.Location = new Point(24, 571);
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
            pnSettingNor4.Location = new Point(24, 505);
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
            pnSettingNor3.Location = new Point(24, 439);
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
            pnSettingNor2.Location = new Point(24, 373);
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
            pnSettingNor1.Location = new Point(24, 307);
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
            lbNormalize.ForeColor = Color.Black;
            lbNormalize.Location = new Point(24, 275);
            lbNormalize.Name = "lbNormalize";
            lbNormalize.Size = new Size(117, 25);
            lbNormalize.TabIndex = 16;
            lbNormalize.Text = "Normalize";
            // 
            // lbSettings
            // 
            lbSettings.AutoSize = true;
            lbSettings.BackColor = Color.Transparent;
            lbSettings.Font = new Font("맑은 고딕", 24F, FontStyle.Bold, GraphicsUnit.Point);
            lbSettings.ForeColor = Color.Black;
            lbSettings.Location = new Point(24, 36);
            lbSettings.Name = "lbSettings";
            lbSettings.Size = new Size(143, 45);
            lbSettings.TabIndex = 14;
            lbSettings.Text = "Settings";
            // 
            // pnSettingTrunk2
            // 
            pnSettingTrunk2.BackColor = Color.Gray;
            pnSettingTrunk2.Controls.Add(lbTrunkMaxH);
            pnSettingTrunk2.Controls.Add(tbTrunkMaxHeight);
            pnSettingTrunk2.Location = new Point(24, 757);
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
            lbCrownSlice.ForeColor = Color.Black;
            lbCrownSlice.Location = new Point(24, 911);
            lbCrownSlice.Name = "lbCrownSlice";
            lbCrownSlice.Size = new Size(130, 25);
            lbCrownSlice.TabIndex = 18;
            lbCrownSlice.Text = "CrownSlice";
            // 
            // lbTrunkSlice
            // 
            lbTrunkSlice.AutoSize = true;
            lbTrunkSlice.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbTrunkSlice.ForeColor = Color.Black;
            lbTrunkSlice.Location = new Point(24, 659);
            lbTrunkSlice.Name = "lbTrunkSlice";
            lbTrunkSlice.Size = new Size(124, 25);
            lbTrunkSlice.TabIndex = 17;
            lbTrunkSlice.Text = "TrunkSlice";
            // 
            // pnSettingCrown3
            // 
            pnSettingCrown3.BackColor = Color.Gray;
            pnSettingCrown3.Controls.Add(lbCrownMinH);
            pnSettingCrown3.Controls.Add(tbCrownMinHeight);
            pnSettingCrown3.Location = new Point(24, 1075);
            pnSettingCrown3.Margin = new Padding(3, 4, 3, 4);
            pnSettingCrown3.Name = "pnSettingCrown3";
            pnSettingCrown3.Size = new Size(850, 58);
            pnSettingCrown3.TabIndex = 12;
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
            pnSettingTrunk1.Location = new Point(24, 691);
            pnSettingTrunk1.Margin = new Padding(3, 4, 3, 4);
            pnSettingTrunk1.Name = "pnSettingTrunk1";
            pnSettingTrunk1.Size = new Size(850, 58);
            pnSettingTrunk1.TabIndex = 10;
            // 
            // tbTrunkMinHeight
            // 
            tbTrunkMinHeight.BorderStyle = BorderStyle.FixedSingle;
            tbTrunkMinHeight.Enabled = false;
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateGray;
            ClientSize = new Size(961, 500);
            Controls.Add(tcMainHome);
            Controls.Add(pnSideMenu);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "ForestLi";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            pnSideMenu.ResumeLayout(false);
            pnSideMenu.PerformLayout();
            tcMainHome.ResumeLayout(false);
            tpMainHome.ResumeLayout(false);
            pnMain.ResumeLayout(false);
            pnMain.PerformLayout();
            tpSettings.ResumeLayout(false);
            tpSettings.PerformLayout();
            pnSettingFileDel.ResumeLayout(false);
            pnSettingFileDel.PerformLayout();
            pnSettingCrown1.ResumeLayout(false);
            pnSettingCrown1.PerformLayout();
            pnSettingCrown2.ResumeLayout(false);
            pnSettingCrown2.PerformLayout();
            pnSettingReset.ResumeLayout(false);
            pnSettingReset.PerformLayout();
            pnSettingTrunk3.ResumeLayout(false);
            pnSettingTrunk3.PerformLayout();
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
            pnSettingTrunk2.ResumeLayout(false);
            pnSettingTrunk2.PerformLayout();
            pnSettingCrown3.ResumeLayout(false);
            pnSettingCrown3.PerformLayout();
            pnSettingTrunk1.ResumeLayout(false);
            pnSettingTrunk1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private CustomBtn btnHome;
        private CustomTabControl tcMainHome;
        private Panel pnSideMenu;
        private TabPage tpMainHome;
        private CustomPanel pnMain;
        private Label lbSubTitle;
        private CustomBtn btnStart;
        private Label lbTitle;
        private TabPage tpSettings;
        private CustomPanel pnSettingCrown3;
        private TextBox tbCrownMinHeight;
        private Label lbCrownSlice;
        private Label lbCrownMinH;
        private CustomPanel pnSettingTrunk1;
        private TextBox tbTrunkMaxHeight;
        private Label lbTrunkSlice;
        private Label lbTrunkMinH;
        private Label lbTrunkMaxH;
        private TextBox tbTrunkMinHeight;
        private CustomPanel pnSettingTrunk2;
        private Label lbSettings;
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
        private CustomBtn btnSettingApply;
        private CustomBtn btnSettingCancel;
        private ToolTip ttMainInfo;
        private Label lbTrunkSmooth;
        private CustomPanel pnSettingTrunk3;
        private TextBox tbTrunkSmooth;
        private Label lbHome;
        private Label lbPresetManage;
        private Label lbSetting;
        private CustomBtn btnSettings;
        private CustomPanel pnSettingReset;
        private Label lbSettingReset;
        private Label lbSettingResetInfo;
        private CustomBtn btnSettingReset;
        private CustomPanel pnSettingCrown2;
        private Label lbCrownRadius;
        private TextBox tbCrownRadius;
        private CustomPanel pnSettingCrown1;
        private Label lbCrownVoxel;
        private TextBox tbCrownVoxel;
        private CustomPanel pnSettingFileDel;
        private Label lbSettingFileDelInfo;
        private Label lbSettingFileDel;
        private LiDAR_PROJECT.ToggleBtn tgBtnSettingFileDel;
        private Label lbSettingToggle;
    }
}