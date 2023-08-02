using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsAppTest
{
    public partial class PlotForm : Form
    {
        public PlotForm()
        {
            InitializeComponent();
            c_pnPlotSelection.isFill = true;
            c_pnPlotSelection.isBorder = false;
            c_pnPlotSelection.borderColor = Color.Honeydew;
            c_pnPlotData.isFill = true;
            c_pnPlotData.isBorder = false;
            c_pnPlotData.borderColor = Color.Honeydew;
        }

        private void btnPlotCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
