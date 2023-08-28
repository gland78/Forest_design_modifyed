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
            lvPresetConf = new ListView();
            titleHeader = new ColumnHeader();
            dateHeader = new ColumnHeader();
            infoHeader = new ColumnHeader();
            btnManageTitle = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnManageCancel = new Button();
            btnManageDelete = new Button();
            btnManageInfo = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // lvPresetConf
            // 
            lvPresetConf.Columns.AddRange(new ColumnHeader[] { titleHeader, dateHeader, infoHeader });
            lvPresetConf.Dock = DockStyle.Fill;
            lvPresetConf.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lvPresetConf.FullRowSelect = true;
            lvPresetConf.GridLines = true;
            lvPresetConf.Location = new Point(10, 10);
            lvPresetConf.MultiSelect = false;
            lvPresetConf.Name = "lvPresetConf";
            tableLayoutPanel1.SetRowSpan(lvPresetConf, 5);
            lvPresetConf.Scrollable = false;
            lvPresetConf.Size = new Size(377, 309);
            lvPresetConf.TabIndex = 0;
            lvPresetConf.UseCompatibleStateImageBehavior = false;
            lvPresetConf.View = View.Details;
            lvPresetConf.SizeChanged += lvPresetConf_SizeChanged;
            // 
            // titleHeader
            // 
            titleHeader.Text = "Title";
            titleHeader.Width = 120;
            // 
            // dateHeader
            // 
            dateHeader.Text = "Date";
            dateHeader.Width = 160;
            // 
            // infoHeader
            // 
            infoHeader.Text = "Info";
            infoHeader.Width = 180;
            // 
            // btnManageTitle
            // 
            btnManageTitle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnManageTitle.Location = new Point(395, 50);
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
            tableLayoutPanel1.Size = new Size(497, 329);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // btnManageCancel
            // 
            btnManageCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnManageCancel.Location = new Point(395, 285);
            btnManageCancel.Name = "btnManageCancel";
            btnManageCancel.Size = new Size(92, 32);
            btnManageCancel.TabIndex = 4;
            btnManageCancel.Text = "취소";
            btnManageCancel.UseVisualStyleBackColor = true;
            btnManageCancel.Click += btnManageCancel_Click;
            // 
            // btnManageDelete
            // 
            btnManageDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnManageDelete.Location = new Point(395, 10);
            btnManageDelete.Name = "btnManageDelete";
            btnManageDelete.Size = new Size(92, 32);
            btnManageDelete.TabIndex = 3;
            btnManageDelete.Text = "삭제";
            btnManageDelete.UseVisualStyleBackColor = true;
            btnManageDelete.Click += btnManageDelete_Click;
            // 
            // btnManageInfo
            // 
            btnManageInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnManageInfo.Location = new Point(395, 90);
            btnManageInfo.Name = "btnManageInfo";
            btnManageInfo.Size = new Size(92, 32);
            btnManageInfo.TabIndex = 7;
            btnManageInfo.Text = "설명 수정";
            btnManageInfo.UseVisualStyleBackColor = true;
            btnManageInfo.Click += btnManageInfo_Click;
            // 
            // ManageForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(497, 329);
            Controls.Add(tableLayoutPanel1);
            Name = "ManageForm";
            Text = "ManageForm";
            Load += ManageForm_Load;
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
    }
}