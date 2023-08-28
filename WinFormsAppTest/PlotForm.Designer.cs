namespace WinFormsAppTest
{
    partial class PlotForm
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
            tbPlotCircleR = new TextBox();
            lbPlotCircleR = new Label();
            tbPlotCircleY = new TextBox();
            lbPlotCircleY = new Label();
            tbPlotCircleX = new TextBox();
            lbPlotCircleX = new Label();
            tbPlotRecYmax = new TextBox();
            lbPlotRecYmax = new Label();
            tbPlotRecXmax = new TextBox();
            lbPlotRecXmax = new Label();
            tbPlotRecYmin = new TextBox();
            lbPlotRecYmin = new Label();
            tbPlotRecXmin = new TextBox();
            lbPlotRecXmin = new Label();
            tbPlotPolySet = new TextBox();
            lbPlotPolySet = new Label();
            btnPlotPolySet = new CustomBtn();
            btnPlotOK = new Button();
            btnPlotCancel = new Button();
            tcPlot = new CustomTabControl();
            tpPlotCircle = new TabPage();
            lbPlotCircle = new Label();
            tpPlotRec = new TabPage();
            lbPlotRec = new Label();
            tpPlotPoly = new TabPage();
            lbPlotPoly = new Label();
            pnPlotSelection = new CustomPanel();
            lbPlotSelection = new Label();
            cbPlotShape = new ComboBox();
            pnPlotData = new CustomPanel();
            btnPlotData = new CustomBtn();
            tbPlotData = new TextBox();
            lbPlotData = new Label();
            tcPlot.SuspendLayout();
            tpPlotCircle.SuspendLayout();
            tpPlotRec.SuspendLayout();
            tpPlotPoly.SuspendLayout();
            pnPlotSelection.SuspendLayout();
            pnPlotData.SuspendLayout();
            SuspendLayout();
            // 
            // tbPlotCircleR
            // 
            tbPlotCircleR.Location = new Point(135, 132);
            tbPlotCircleR.Name = "tbPlotCircleR";
            tbPlotCircleR.Size = new Size(107, 23);
            tbPlotCircleR.TabIndex = 7;
            tbPlotCircleR.Leave += tbPlotCircleR_Leave;
            // 
            // lbPlotCircleR
            // 
            lbPlotCircleR.AutoSize = true;
            lbPlotCircleR.Location = new Point(67, 136);
            lbPlotCircleR.Name = "lbPlotCircleR";
            lbPlotCircleR.Size = new Size(61, 15);
            lbPlotCircleR.TabIndex = 6;
            lbPlotCircleR.Text = "Radius(M)";
            // 
            // tbPlotCircleY
            // 
            tbPlotCircleY.Location = new Point(135, 90);
            tbPlotCircleY.Name = "tbPlotCircleY";
            tbPlotCircleY.Size = new Size(107, 23);
            tbPlotCircleY.TabIndex = 5;
            tbPlotCircleY.Leave += tbPlotCircleY_Leave;
            // 
            // lbPlotCircleY
            // 
            lbPlotCircleY.AutoSize = true;
            lbPlotCircleY.Location = new Point(67, 94);
            lbPlotCircleY.Name = "lbPlotCircleY";
            lbPlotCircleY.Size = new Size(14, 15);
            lbPlotCircleY.TabIndex = 4;
            lbPlotCircleY.Text = "Y";
            // 
            // tbPlotCircleX
            // 
            tbPlotCircleX.Location = new Point(135, 48);
            tbPlotCircleX.Name = "tbPlotCircleX";
            tbPlotCircleX.Size = new Size(107, 23);
            tbPlotCircleX.TabIndex = 3;
            tbPlotCircleX.Leave += tbPlotCircleX_Leave;
            // 
            // lbPlotCircleX
            // 
            lbPlotCircleX.AutoSize = true;
            lbPlotCircleX.Location = new Point(67, 52);
            lbPlotCircleX.Name = "lbPlotCircleX";
            lbPlotCircleX.Size = new Size(14, 15);
            lbPlotCircleX.TabIndex = 2;
            lbPlotCircleX.Text = "X";
            // 
            // tbPlotRecYmax
            // 
            tbPlotRecYmax.Location = new Point(304, 90);
            tbPlotRecYmax.Name = "tbPlotRecYmax";
            tbPlotRecYmax.Size = new Size(107, 23);
            tbPlotRecYmax.TabIndex = 17;
            tbPlotRecYmax.Leave += tbPlotRecYmax_Leave;
            // 
            // lbPlotRecYmax
            // 
            lbPlotRecYmax.AutoSize = true;
            lbPlotRecYmax.Location = new Point(261, 93);
            lbPlotRecYmax.Name = "lbPlotRecYmax";
            lbPlotRecYmax.Size = new Size(37, 15);
            lbPlotRecYmax.TabIndex = 16;
            lbPlotRecYmax.Text = "Ymax";
            // 
            // tbPlotRecXmax
            // 
            tbPlotRecXmax.Location = new Point(304, 48);
            tbPlotRecXmax.Name = "tbPlotRecXmax";
            tbPlotRecXmax.Size = new Size(107, 23);
            tbPlotRecXmax.TabIndex = 15;
            tbPlotRecXmax.Leave += tbPlotRecXmax_Leave;
            // 
            // lbPlotRecXmax
            // 
            lbPlotRecXmax.AutoSize = true;
            lbPlotRecXmax.Location = new Point(261, 51);
            lbPlotRecXmax.Name = "lbPlotRecXmax";
            lbPlotRecXmax.Size = new Size(37, 15);
            lbPlotRecXmax.TabIndex = 14;
            lbPlotRecXmax.Text = "Xmax";
            // 
            // tbPlotRecYmin
            // 
            tbPlotRecYmin.Location = new Point(135, 90);
            tbPlotRecYmin.Name = "tbPlotRecYmin";
            tbPlotRecYmin.Size = new Size(107, 23);
            tbPlotRecYmin.TabIndex = 11;
            tbPlotRecYmin.Leave += tbPlotRecYmin_Leave;
            // 
            // lbPlotRecYmin
            // 
            lbPlotRecYmin.AutoSize = true;
            lbPlotRecYmin.Location = new Point(67, 94);
            lbPlotRecYmin.Name = "lbPlotRecYmin";
            lbPlotRecYmin.Size = new Size(35, 15);
            lbPlotRecYmin.TabIndex = 10;
            lbPlotRecYmin.Text = "Ymin";
            // 
            // tbPlotRecXmin
            // 
            tbPlotRecXmin.Location = new Point(135, 48);
            tbPlotRecXmin.Name = "tbPlotRecXmin";
            tbPlotRecXmin.Size = new Size(107, 23);
            tbPlotRecXmin.TabIndex = 9;
            tbPlotRecXmin.Leave += tbPlotRecXmin_Leave;
            // 
            // lbPlotRecXmin
            // 
            lbPlotRecXmin.AutoSize = true;
            lbPlotRecXmin.Location = new Point(67, 52);
            lbPlotRecXmin.Name = "lbPlotRecXmin";
            lbPlotRecXmin.Size = new Size(35, 15);
            lbPlotRecXmin.TabIndex = 8;
            lbPlotRecXmin.Text = "Xmin";
            // 
            // tbPlotPolySet
            // 
            tbPlotPolySet.Location = new Point(154, 48);
            tbPlotPolySet.Name = "tbPlotPolySet";
            tbPlotPolySet.Size = new Size(216, 23);
            tbPlotPolySet.TabIndex = 5;
            // 
            // lbPlotPolySet
            // 
            lbPlotPolySet.AutoSize = true;
            lbPlotPolySet.Location = new Point(67, 52);
            lbPlotPolySet.Name = "lbPlotPolySet";
            lbPlotPolySet.Size = new Size(72, 15);
            lbPlotPolySet.TabIndex = 4;
            lbPlotPolySet.Text = "Polygon Set";
            // 
            // btnPlotPolySet
            // 
            btnPlotPolySet.BackColor = Color.Green;
            btnPlotPolySet.BackgroundColor = Color.Green;
            btnPlotPolySet.BackgroundImageLayout = ImageLayout.None;
            btnPlotPolySet.BorderColor = Color.PaleVioletRed;
            btnPlotPolySet.BorderRadius = 8;
            btnPlotPolySet.BorderSize = 0;
            btnPlotPolySet.FlatStyle = FlatStyle.Flat;
            btnPlotPolySet.ForeColor = Color.White;
            btnPlotPolySet.Location = new Point(376, 46);
            btnPlotPolySet.Name = "btnPlotPolySet";
            btnPlotPolySet.Size = new Size(97, 27);
            btnPlotPolySet.TabIndex = 10;
            btnPlotPolySet.Text = "File Search";
            btnPlotPolySet.TextColor = Color.White;
            btnPlotPolySet.UseVisualStyleBackColor = false;
            btnPlotPolySet.Click += btnPlotPolySet_Click;
            // 
            // btnPlotOK
            // 
            btnPlotOK.BackColor = Color.Honeydew;
            btnPlotOK.FlatAppearance.BorderSize = 0;
            btnPlotOK.FlatStyle = FlatStyle.Flat;
            btnPlotOK.Location = new Point(375, 334);
            btnPlotOK.Name = "btnPlotOK";
            btnPlotOK.Size = new Size(64, 24);
            btnPlotOK.TabIndex = 7;
            btnPlotOK.Text = "확인";
            btnPlotOK.UseVisualStyleBackColor = false;
            btnPlotOK.Click += btnPlotOK_Click;
            // 
            // btnPlotCancel
            // 
            btnPlotCancel.BackColor = Color.Honeydew;
            btnPlotCancel.FlatAppearance.BorderSize = 0;
            btnPlotCancel.FlatStyle = FlatStyle.Flat;
            btnPlotCancel.Location = new Point(445, 334);
            btnPlotCancel.Name = "btnPlotCancel";
            btnPlotCancel.Size = new Size(64, 24);
            btnPlotCancel.TabIndex = 8;
            btnPlotCancel.Text = "취소";
            btnPlotCancel.UseVisualStyleBackColor = false;
            btnPlotCancel.Click += btnPlotCancel_Click;
            // 
            // tcPlot
            // 
            tcPlot.Controls.Add(tpPlotCircle);
            tcPlot.Controls.Add(tpPlotRec);
            tcPlot.Controls.Add(tpPlotPoly);
            tcPlot.ItemSize = new Size(0, 5);
            tcPlot.Location = new Point(13, 129);
            tcPlot.Multiline = true;
            tcPlot.Name = "tcPlot";
            tcPlot.SelectedIndex = 0;
            tcPlot.Size = new Size(495, 198);
            tcPlot.TabIndex = 9;
            // 
            // tpPlotCircle
            // 
            tpPlotCircle.BackColor = Color.YellowGreen;
            tpPlotCircle.Controls.Add(lbPlotCircle);
            tpPlotCircle.Controls.Add(tbPlotCircleR);
            tpPlotCircle.Controls.Add(tbPlotCircleY);
            tpPlotCircle.Controls.Add(lbPlotCircleR);
            tpPlotCircle.Controls.Add(lbPlotCircleX);
            tpPlotCircle.Controls.Add(tbPlotCircleX);
            tpPlotCircle.Controls.Add(lbPlotCircleY);
            tpPlotCircle.Location = new Point(4, 9);
            tpPlotCircle.Name = "tpPlotCircle";
            tpPlotCircle.Padding = new Padding(3);
            tpPlotCircle.Size = new Size(487, 185);
            tpPlotCircle.TabIndex = 0;
            tpPlotCircle.MouseDown += PlotForm_MouseDown;
            tpPlotCircle.MouseMove += PlotForm_MouseMove;
            // 
            // lbPlotCircle
            // 
            lbPlotCircle.AutoSize = true;
            lbPlotCircle.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lbPlotCircle.Location = new Point(22, 13);
            lbPlotCircle.Name = "lbPlotCircle";
            lbPlotCircle.Size = new Size(87, 21);
            lbPlotCircle.TabIndex = 8;
            lbPlotCircle.Text = "Circle Plot";
            // 
            // tpPlotRec
            // 
            tpPlotRec.BackColor = Color.YellowGreen;
            tpPlotRec.Controls.Add(lbPlotRec);
            tpPlotRec.Controls.Add(tbPlotRecYmax);
            tpPlotRec.Controls.Add(lbPlotRecYmax);
            tpPlotRec.Controls.Add(lbPlotRecXmin);
            tpPlotRec.Controls.Add(tbPlotRecXmax);
            tpPlotRec.Controls.Add(tbPlotRecXmin);
            tpPlotRec.Controls.Add(lbPlotRecXmax);
            tpPlotRec.Controls.Add(lbPlotRecYmin);
            tpPlotRec.Controls.Add(tbPlotRecYmin);
            tpPlotRec.Location = new Point(4, 9);
            tpPlotRec.Name = "tpPlotRec";
            tpPlotRec.Padding = new Padding(3);
            tpPlotRec.Size = new Size(487, 185);
            tpPlotRec.TabIndex = 1;
            // 
            // lbPlotRec
            // 
            lbPlotRec.AutoSize = true;
            lbPlotRec.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lbPlotRec.Location = new Point(22, 13);
            lbPlotRec.Name = "lbPlotRec";
            lbPlotRec.Size = new Size(121, 21);
            lbPlotRec.TabIndex = 18;
            lbPlotRec.Text = "Rectangle Plot";
            // 
            // tpPlotPoly
            // 
            tpPlotPoly.BackColor = Color.YellowGreen;
            tpPlotPoly.Controls.Add(lbPlotPoly);
            tpPlotPoly.Controls.Add(tbPlotPolySet);
            tpPlotPoly.Controls.Add(btnPlotPolySet);
            tpPlotPoly.Controls.Add(lbPlotPolySet);
            tpPlotPoly.Location = new Point(4, 9);
            tpPlotPoly.Name = "tpPlotPoly";
            tpPlotPoly.Padding = new Padding(3);
            tpPlotPoly.Size = new Size(487, 185);
            tpPlotPoly.TabIndex = 2;
            // 
            // lbPlotPoly
            // 
            lbPlotPoly.AutoSize = true;
            lbPlotPoly.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lbPlotPoly.Location = new Point(22, 13);
            lbPlotPoly.Name = "lbPlotPoly";
            lbPlotPoly.Size = new Size(109, 21);
            lbPlotPoly.TabIndex = 11;
            lbPlotPoly.Text = "Polygon Plot";
            // 
            // pnPlotSelection
            // 
            pnPlotSelection.BackColor = Color.Transparent;
            pnPlotSelection.Controls.Add(lbPlotSelection);
            pnPlotSelection.Controls.Add(cbPlotShape);
            pnPlotSelection.Location = new Point(13, 73);
            pnPlotSelection.Name = "pnPlotSelection";
            pnPlotSelection.Size = new Size(494, 60);
            pnPlotSelection.TabIndex = 10;
            // 
            // lbPlotSelection
            // 
            lbPlotSelection.AutoSize = true;
            lbPlotSelection.BackColor = Color.Transparent;
            lbPlotSelection.Location = new Point(19, 22);
            lbPlotSelection.Name = "lbPlotSelection";
            lbPlotSelection.Size = new Size(56, 15);
            lbPlotSelection.TabIndex = 12;
            lbPlotSelection.Text = "Selection";
            // 
            // cbPlotShape
            // 
            cbPlotShape.FormattingEnabled = true;
            cbPlotShape.Items.AddRange(new object[] { "Circle", "Rectangle", "Polygon" });
            cbPlotShape.Location = new Point(89, 18);
            cbPlotShape.Name = "cbPlotShape";
            cbPlotShape.Size = new Size(109, 23);
            cbPlotShape.TabIndex = 11;
            cbPlotShape.SelectedIndexChanged += cbPlotShape_SelectedIndexChanged;
            // 
            // pnPlotData
            // 
            pnPlotData.BackColor = Color.Transparent;
            pnPlotData.Controls.Add(btnPlotData);
            pnPlotData.Controls.Add(tbPlotData);
            pnPlotData.Controls.Add(lbPlotData);
            pnPlotData.Location = new Point(13, 16);
            pnPlotData.Name = "pnPlotData";
            pnPlotData.Size = new Size(494, 60);
            pnPlotData.TabIndex = 11;
            // 
            // btnPlotData
            // 
            btnPlotData.BackColor = Color.Green;
            btnPlotData.BackgroundColor = Color.Green;
            btnPlotData.BackgroundImageLayout = ImageLayout.None;
            btnPlotData.BorderColor = Color.PaleVioletRed;
            btnPlotData.BorderRadius = 8;
            btnPlotData.BorderSize = 0;
            btnPlotData.FlatStyle = FlatStyle.Flat;
            btnPlotData.ForeColor = Color.White;
            btnPlotData.Location = new Point(380, 17);
            btnPlotData.Name = "btnPlotData";
            btnPlotData.Size = new Size(97, 27);
            btnPlotData.TabIndex = 12;
            btnPlotData.Text = "File Search";
            btnPlotData.TextColor = Color.White;
            btnPlotData.UseVisualStyleBackColor = false;
            btnPlotData.Click += btnPlotData_Click;
            // 
            // tbPlotData
            // 
            tbPlotData.Enabled = false;
            tbPlotData.Location = new Point(89, 19);
            tbPlotData.Name = "tbPlotData";
            tbPlotData.ReadOnly = true;
            tbPlotData.Size = new Size(285, 23);
            tbPlotData.TabIndex = 9;
            tbPlotData.TextChanged += tbPlotData_TextChanged;
            // 
            // lbPlotData
            // 
            lbPlotData.AutoSize = true;
            lbPlotData.BackColor = Color.Transparent;
            lbPlotData.Location = new Point(19, 23);
            lbPlotData.Name = "lbPlotData";
            lbPlotData.Size = new Size(62, 15);
            lbPlotData.TabIndex = 12;
            lbPlotData.Text = "Data Load";
            // 
            // PlotForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.YellowGreen;
            ClientSize = new Size(521, 370);
            Controls.Add(pnPlotSelection);
            Controls.Add(tcPlot);
            Controls.Add(pnPlotData);
            Controls.Add(btnPlotCancel);
            Controls.Add(btnPlotOK);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PlotForm";
            Text = "Form2";
            Load += PlotForm_Load;
            MouseDown += PlotForm_MouseDown;
            MouseMove += PlotForm_MouseMove;
            tcPlot.ResumeLayout(false);
            tpPlotCircle.ResumeLayout(false);
            tpPlotCircle.PerformLayout();
            tpPlotRec.ResumeLayout(false);
            tpPlotRec.PerformLayout();
            tpPlotPoly.ResumeLayout(false);
            tpPlotPoly.PerformLayout();
            pnPlotSelection.ResumeLayout(false);
            pnPlotSelection.PerformLayout();
            pnPlotData.ResumeLayout(false);
            pnPlotData.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TextBox tbPlotCircleR;
        private Label lbPlotCircleR;
        private TextBox tbPlotCircleY;
        private Label lbPlotCircleY;
        private TextBox tbPlotCircleX;
        private Label lbPlotCircleX;
        private TextBox tbPlotRecYmin;
        private Label lbPlotRecYmin;
        private TextBox tbPlotRecXmin;
        private Label lbPlotRecXmin;
        private TextBox tbPlotRecYmax;
        private Label lbPlotRecYmax;
        private TextBox tbPlotRecXmax;
        private Label lbPlotRecXmax;
        private TextBox tbPlotPolySet;
        private Label lbPlotPolySet;
        private CustomBtn btnPlotPolySet;
        private Button btnPlotOK;
        private Button btnPlotCancel;
        private CustomTabControl tcPlot;
        private TabPage tpPlotCircle;
        private TabPage tpPlotRec;
        private TabPage tpPlotPoly;
        private Label lbPlotCircle;
        private Label lbPlotRec;
        private Label lbPlotPoly;
        private CustomPanel pnPlotSelection;
        private Label lbPlotSelection;
        private ComboBox cbPlotShape;
        private CustomPanel pnPlotData;
        private CustomBtn btnPlotData;
        private TextBox tbPlotData;
        private Label lbPlotData;
    }
}