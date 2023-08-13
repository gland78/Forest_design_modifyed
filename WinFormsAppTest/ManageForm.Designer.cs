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
            listView1 = new ListView();
            numHeader = new ColumnHeader();
            titleHeader = new ColumnHeader();
            infoHeader = new ColumnHeader();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { numHeader, titleHeader, infoHeader });
            listView1.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            listView1.GridLines = true;
            listView1.Location = new Point(12, 12);
            listView1.Name = "listView1";
            listView1.Size = new Size(363, 305);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
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
            // button1
            // 
            button1.Location = new Point(391, 12);
            button1.Name = "button1";
            button1.Size = new Size(92, 32);
            button1.TabIndex = 1;
            button1.Text = "제목 수정";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(391, 55);
            button2.Name = "button2";
            button2.Size = new Size(92, 32);
            button2.TabIndex = 2;
            button2.Text = "삭제";
            button2.UseVisualStyleBackColor = true;
            // 
            // ManageForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(497, 329);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(listView1);
            Name = "ManageForm";
            Text = "ManageForm";
            Load += ManageForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private ListView listView1;
        private ColumnHeader numHeader;
        private ColumnHeader titleHeader;
        private ColumnHeader infoHeader;
        private Button button1;
        private Button button2;
    }
}