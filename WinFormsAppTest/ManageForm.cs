using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WinFormsAppTest.MainForm;

namespace WinFormsAppTest
{
    public partial class ManageForm : Form
    {
        internal customEventHandler mainPaint;

        public ManageForm()
        {
            InitializeComponent();
        }

        private void ManageForm_Load(object sender, EventArgs e)
        {
            preConfLoad();
        }


        private void preConfLoad()
        {
            //프리셋 콘피그 저장 장소
            string[] confCheck = Directory.GetFiles(Path.Combine(configPath, reqDi[(int)configFileType.Preset]), "PresetConfig*");
            Array.Sort(confCheck);

            lvPresetConf.Items.Clear();

            foreach (string conf in confCheck)
            {
                string json = File.ReadAllText(conf);

                var jsonArray = JsonConvert.DeserializeObject<dynamic>(json);
                dynamic JObject = jsonArray[0].FIleInfo;
                string confTitle = JObject.title;

                json = json.Replace(Environment.NewLine, "");
                json = json.Replace(" ", "");
                json = Regex.Replace(json, "[{}]", "");

                string fileName = conf.Substring(conf.IndexOf("presetConfig"), conf.Length - conf.IndexOf("presetConfig") - 5);
                ListViewItem confItem = new ListViewItem();
                confItem.Name = fileName;
                confItem.SubItems.Add(confTitle);
                confItem.SubItems.Add(json);
                lvPresetConf.Items.Add(confItem);
            }

            for (int i = 0; i < lvPresetConf.Items.Count; i++)
            {
                lvPresetConf.Items[i].Text = i.ToString();
            }
        }

        private void btnManageTitle_Click(object sender, EventArgs e)
        {
            string fileName = lvPresetConf.SelectedItems[0].Name;
            string[] confCheck = Directory.GetFiles(Path.Combine(configPath, reqDi[(int)configFileType.Preset]), "PresetConfig*");
            string? newTitle = Microsoft.VisualBasic.Interaction.InputBox("바꿀 제목을 입력해주세요.", "프리셋 제목 바꾸기");

            if (newTitle == null)
            {
                return;
            }

            foreach (string conf in confCheck)
            {
                if (conf.Contains(fileName))
                {
                    string json = File.ReadAllText(conf);

                    var jsonArray = JsonConvert.DeserializeObject<dynamic>(json);

                    dynamic JObject = jsonArray[0].FIleInfo;
                    JObject.title = newTitle;
                    File.WriteAllText(conf, jsonArray.ToString());
                }
            }
            preConfLoad();
            mainPaint();
        }

        private void btnManageDelete_Click(object sender, EventArgs e)
        {
            if (lvPresetConf.SelectedItems.Count < 1)
            {
                MessageBox.Show("삭제할 프리셋을 선택해주세요");
                return;
            }

            string[] confCheck = Directory.GetFiles(Path.Combine(configPath, reqDi[(int)configFileType.Preset]), "PresetConfig*");
            if (MessageBox.Show("선택한 프리셋을 삭제하시겠습니까?",
                "프리셋 삭제", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }

            foreach (string conf in confCheck)
            {
                string fileName = conf.Substring(conf.IndexOf("presetConfig"), conf.Length - conf.IndexOf("presetConfig") - 5);
                if (lvPresetConf.SelectedItems[0].Name == fileName)
                {
                    lvPresetConf.SelectedItems[0].Remove();
                    File.Delete(conf);

                    int oldIndex = int.Parse(fileName.Substring(12));

                    for (int i = oldIndex + 1; i < confCheck.Length; i++)
                    {
                        string oldPath = Path.Combine(Path.Combine(configPath, reqDi[(int)configFileType.Preset]) + @"\", $"presetConfig{i}.json");
                        string newPath = Path.Combine(Path.Combine(configPath, reqDi[(int)configFileType.Preset]), $"presetConfig{i - 1}.json");

                        if (File.Exists(oldPath) && File.Exists(newPath))
                        {
                            File.Delete(newPath);
                        }
                        File.Move(oldPath, newPath);
                    }
                    mainPaint();
                    preConfLoad();
                    break;
                }
            }
        }
    }
}
