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
            string fileDi = Path.Combine(basePath, reqDi[(int)configFileType.Preset]);
            //프리셋 콘피그 저장 장소
            string[] confCheck = Directory.GetFiles(fileDi, "presetConfig*");
            Array.Sort(confCheck);

            lvPresetConf.Items.Clear();

            //config류 csv파일 구조는 0: Type, 1: Visibility, 2: Key, 3: Value, 4: 설명(개발자 전용)이다
            string title = "";
            StreamReader sr;
            List<string> plotData = new();
            foreach (string conf in confCheck)
            {
                sr = new StreamReader(conf);
                string csvLines;
                while ((csvLines = sr.ReadLine()) != null)
                {
                    //MessageBox.Show(csvLines);
                    if (csvLines.Contains("title"))
                    {
                        title = csvLines.Split(',')[3];
                    }
                    else if (csvLines.Contains(",circle,") || csvLines.Contains(",rectangle,"))
                    {
                        plotData.Add(csvLines);
                    }
                }

                string workInfo = "";
                foreach (string plots in plotData)
                {
                    workInfo += plots.Split(',')[3] + " ";
                }

                string fileName = Path.GetFileName(conf);
                ListViewItem confItem = new ListViewItem();
                confItem.Name = fileName;
                confItem.SubItems.Add(title);
                confItem.SubItems.Add(workInfo);
                lvPresetConf.Items.Add(confItem);

                sr.Close();
            }

            for (int i = 0; i < lvPresetConf.Items.Count; i++)
            {
                lvPresetConf.Items[i].Text = i.ToString();
            }
        }

        private void btnManageTitle_Click(object sender, EventArgs e)
        {
            string fileName = lvPresetConf.SelectedItems[0].Name;
            string oldTitle = lvPresetConf.SelectedItems[0].SubItems[1].Text;
            string fileDi = Path.Combine(basePath, reqDi[(int)configFileType.Preset]);
            string[] confCheck = Directory.GetFiles(fileDi, "presetConfig*");
            string? newTitle = Microsoft.VisualBasic.Interaction.InputBox("바꿀 제목을 입력해주세요.", "프리셋 제목 바꾸기");

            if (newTitle == null)
            {
                return;
            }

            string csv = File.ReadAllText(Path.Combine(fileDi, fileName));

            csv = csv.Replace(oldTitle, newTitle);

            File.WriteAllText(Path.Combine(fileDi, fileName), csv);

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

            string[] confCheck = Directory.GetFiles(Path.Combine(basePath, reqDi[(int)configFileType.Preset]), "PresetConfig*");
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
                        string oldPath = Path.Combine(Path.Combine(basePath, reqDi[(int)configFileType.Preset]) + @"\", $"presetConfig{i}.csv");
                        string newPath = Path.Combine(Path.Combine(basePath, reqDi[(int)configFileType.Preset]), $"presetConfig{i - 1}.csv");

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

        private void btnManageCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
