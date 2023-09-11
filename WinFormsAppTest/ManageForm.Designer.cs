namespace WinFormsAppTest
{
    partial class ManageForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageForm));
            lvPresetConf = new ListView();
            titleHeader = new ColumnHeader();
            dateHeader = new ColumnHeader();
            infoHeader = new ColumnHeader();
            cmsManage = new ContextMenuStrip(components);
            tsmiPresetUp = new ToolStripMenuItem();
            tsmiPresetDown = new ToolStripMenuItem();
            btnManageTitle = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnManageInfo = new Button();
            btnManageDelete = new Button();
            btnManageCancel = new Button();
            cmsManage.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // lvPresetConf
            // 
            lvPresetConf.Columns.AddRange(new ColumnHeader[] { titleHeader, dateHeader, infoHeader });
            lvPresetConf.ContextMenuStrip = cmsManage;
            lvPresetConf.Dock = DockStyle.Fill;
            lvPresetConf.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lvPresetConf.FullRowSelect = true;
            lvPresetConf.GridLines = true;
            lvPresetConf.Location = new Point(10, 10);
            lvPresetConf.MultiSelect = false;
            lvPresetConf.Name = "lvPresetConf";
            tableLayoutPanel1.SetRowSpan(lvPresetConf, 5);
            lvPresetConf.Scrollable = false;
            lvPresetConf.Size = new Size(564, 278);
            lvPresetConf.TabIndex = 4;
            lvPresetConf.UseCompatibleStateImageBehavior = false;
            lvPresetConf.View = View.Details;
            lvPresetConf.SizeChanged += lvPresetConf_SizeChanged;
            lvPresetConf.KeyDown += lvPresetConf_KeyDown;
            lvPresetConf.MouseDoubleClick += lvPresetConf_MouseDoubleClick;
            // 
            // titleHeader
            // 
            titleHeader.Text = "Title";
            titleHeader.Width = 120;
            // 
            // dateHeader
            // 
            dateHeader.Text = "Date";
            dateHeader.Width = 170;
            // 
            // infoHeader
            // 
            infoHeader.Text = "Info";
            infoHeader.Width = 180;
            // 
            // cmsManage
            // 
            cmsManage.Items.AddRange(new ToolStripItem[] { tsmiPresetUp, tsmiPresetDown });
            cmsManage.Name = "cmsManage";
            cmsManage.Size = new Size(181, 70);
            // 
            // tsmiPresetUp
            // 
            tsmiPresetUp.Name = "tsmiPresetUp";
            tsmiPresetUp.Size = new Size(180, 22);
            tsmiPresetUp.Text = "위로 올리기";
            tsmiPresetUp.Click += tsmiPresetUp_Click;
            // 
            // tsmiPresetDown
            // 
            tsmiPresetDown.Name = "tsmiPresetDown";
            tsmiPresetDown.Size = new Size(180, 22);
            tsmiPresetDown.Text = "아래로 내리기";
            tsmiPresetDown.Click += tsmiPresetDown_Click;
            // 
            // btnManageTitle
            // 
            btnManageTitle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnManageTitle.Location = new Point(582, 50);
            btnManageTitle.Name = "btnManageTitle";
            btnManageTitle.Size = new Size(92, 32);
            btnManageTitle.TabIndex = 1;
            btnManageTitle.Text = "제목 수정";
            btnManageTitle.UseVisualStyleBackColor = true;
            btnManageTitle.Click += btnManageTitle_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.Controls.Add(btnManageInfo, 0, 2);
            tableLayoutPanel1.Controls.Add(btnManageDelete, 1, 0);
            tableLayoutPanel1.Controls.Add(btnManageTitle, 1, 1);
            tableLayoutPanel1.Controls.Add(lvPresetConf, 0, 0);
            tableLayoutPanel1.Controls.Add(btnManageCancel, 1, 4);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(5);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(7);
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(684, 298);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // btnManageInfo
            // 
            btnManageInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnManageInfo.Location = new Point(582, 90);
            btnManageInfo.Name = "btnManageInfo";
            btnManageInfo.Size = new Size(92, 32);
            btnManageInfo.TabIndex = 2;
            btnManageInfo.Text = "설명 수정";
            btnManageInfo.UseVisualStyleBackColor = true;
            btnManageInfo.Click += btnManageInfo_Click;
            // 
            // btnManageDelete
            // 
            btnManageDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnManageDelete.Location = new Point(582, 10);
            btnManageDelete.Name = "btnManageDelete";
            btnManageDelete.Size = new Size(92, 32);
            btnManageDelete.TabIndex = 0;
            btnManageDelete.Text = "설정 삭제";
            btnManageDelete.UseVisualStyleBackColor = true;
            btnManageDelete.Click += btnManageDelete_Click;
            // 
            // btnManageCancel
            // 
            btnManageCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnManageCancel.Location = new Point(582, 254);
            btnManageCancel.Name = "btnManageCancel";
            btnManageCancel.Size = new Size(92, 32);
            btnManageCancel.TabIndex = 3;
            btnManageCancel.Text = "닫기";
            btnManageCancel.UseVisualStyleBackColor = true;
            btnManageCancel.Click += btnManageCancel_Click;
            // 
            // ManageForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 298);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ManageForm";
            Text = "ManageForm";
            Load += ManageForm_Load;
            KeyDown += ManageForm_KeyDown;
            cmsManage.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListView lvPresetConf;
        private ColumnHeader titleHeader;
        private ColumnHeader infoHeader;
        private Button btnManageTitle;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnManageDelete;
        private Button btnManageCancel;
        private ColumnHeader dateHeader;
        private Button btnManageInfo;
        private ContextMenuStrip cmsManage;
        private ToolStripMenuItem tsmiPresetUp;
        private ToolStripMenuItem tsmiPresetDown;
    }
}