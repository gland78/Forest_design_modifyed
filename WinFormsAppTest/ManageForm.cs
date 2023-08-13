using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WinFormsAppTest.MainForm;

namespace WinFormsAppTest
{
    public partial class ManageForm : Form
    {
        public ManageForm()
        {
            InitializeComponent();
        }

        private void ManageForm_Load(object sender, EventArgs e)
        {
            //configPath = Directory.GetParent(System.Environment.CurrentDirectory) + @"\bin\"
            string[] confCheck = Directory.GetFiles(Path.Combine(configPath, reqDi[(int)configFileType.Preset]), "preConfig*");

            foreach (string conf in confCheck)
            {
                string json = File.ReadAllText(configPath + @"PresetConfig\" + conf);
                json.Replace(Environment.NewLine, "");
                MessageBox.Show(json);
                var jsonArray = JsonConvert.DeserializeObject<dynamic>(json);

                dynamic JObject = jsonArray[0].GUI;

                ListViewItem confItem = new ListViewItem();
                confItem.SubItems.Add(conf);
            }
        }
    }
}
