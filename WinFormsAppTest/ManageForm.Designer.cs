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
            numHeader = new ColumnHeader();
            titleHeader = new ColumnHeader();
            infoHeader = new ColumnHeader();
            btnManageTitle = new Button();
            btnManageDelete = new Button();
            SuspendLayout();
            // 
            // lvPresetConf
            // 
            lvPresetConf.Columns.AddRange(new ColumnHeader[] { numHeader, titleHeader, infoHeader });
            lvPresetConf.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lvPresetConf.FullRowSelect = true;
            lvPresetConf.GridLines = true;
            lvPresetConf.Location = new Point(12, 12);
            lvPresetConf.MultiSelect = false;
            lvPresetConf.Name = "lvPresetConf";
            lvPresetConf.Size = new Size(363, 305);
            lvPresetConf.TabIndex = 0;
            lvPresetConf.UseCompatibleStateImageBehavior = false;
            lvPresetConf.View = View.Details;
            // 
            // numHeader
            // 
            numHeader.Tag = "Numeric";
            numHeader.Text = "No";
            numHeader.Width = 40;
            // 
            // titleHeader
            // 
            titleHeader.Text = "Title";
            titleHeader.Width = 120;
            // 
            // infoHeader
            // 
            infoHeader.Text = "Parameter";
            infoHeader.Width = 180;
            // 
            // btnManageTitle
            // 
            btnManageTitle.Location = new Point(391, 12);
            btnManageTitle.Name = "btnManageTitle";
            btnManageTitle.Size = new Size(92, 32);
            btnManageTitle.TabIndex = 1;
            btnManageTitle.Text = "제목 수정";
            btnManageTitle.UseVisualStyleBackColor = true;
            btnManageTitle.Click += btnManageTitle_Click;
            // 
            // btnManageDelete
            // 
            btnManageDelete.Location = new Point(391, 55);
            btnManageDelete.Name = "btnManageDelete";
            btnManageDelete.Size = new Size(92, 32);
            btnManageDelete.TabIndex = 2;
            btnManageDelete.Text = "삭제";
            btnManageDelete.UseVisualStyleBackColor = true;
            btnManageDelete.Click += btnManageDelete_Click;
            // 
            // ManageForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(497, 329);
            Controls.Add(btnManageDelete);
            Controls.Add(btnManageTitle);
            Controls.Add(lvPresetConf);
            Name = "ManageForm";
            Text = "ManageForm";
            Load += ManageForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private ListView lvPresetConf;
        private ColumnHeader numHeader;
        private ColumnHeader titleHeader;
        private ColumnHeader infoHeader;
        private Button btnManageTitle;
        private Button btnManageDelete;
    }
}