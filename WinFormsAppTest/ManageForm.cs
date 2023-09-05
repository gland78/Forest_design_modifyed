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
        internal presetReflectHandler presetReflect;

        internal MainForm mainForm;

        public ManageForm()
        {
            InitializeComponent();
        }

        private void ManageForm_Load(object sender, EventArgs e)
        {
            //폼 생성 위치 지정
            Point screenSize = ((Point)Screen.PrimaryScreen.Bounds.Size);
            this.Location = new Point((screenSize.X - this.Width) / 2, (screenSize.Y - this.Height) / 2);

            //리스트 뷰에 들어갈 사용자 설정값 로드
            preConfLoad();
            //리스트 뷰 각 열 사이즈 조절(지금은 info 열만 조정)
            lvPresetConf_SizeChanged(sender, e);
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
            string date = "";
            string workInfo = "";
            StreamReader sr;
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
                    else if (csvLines.Contains("date"))
                    {
                        date = csvLines.Split(',')[3];
                    }
                    else if (csvLines.Contains("info"))
                    {
                        workInfo = csvLines.Split(",")[3];
                    }
                }

                string fileName = Path.GetFileName(conf);
                ListViewItem confItem = new ListViewItem(title);
                confItem.Name = fileName;
                confItem.SubItems.Add(date);
                confItem.SubItems.Add(workInfo);
                lvPresetConf.Items.Add(confItem);

                sr.Close();
            }
        }

        private void btnManageTitle_Click(object sender, EventArgs e)
        {
            if (lvPresetConf.SelectedItems.Count < 1)
            {
                MessageBox.Show("제목을 변경할 사용자 설정을 선택해주세요");
                return;
            }

            string fileName = lvPresetConf.SelectedItems[0].Name;
            string fileDi = Path.Combine(basePath, reqDi[(int)configFileType.Preset]);
            string[] confCheck = Directory.GetFiles(fileDi, "presetConfig*");
            string oldTitle = lvPresetConf.SelectedItems[0].SubItems[0].Text;
            string? newTitle = Microsoft.VisualBasic.Interaction.InputBox("바꿀 제목을 입력해주세요.", "사용자 설정 제목 바꾸기");

            if (newTitle == "")
            {
                return;
            }

            //string csv = File.ReadAllText(Path.Combine(fileDi, fileName));
            //csv = csv.Replace(oldTitle, newTitle);
            //File.WriteAllText(Path.Combine(fileDi, fileName), csv, Encoding.UTF8);

            //사용자가 설명에 title 문자를 포함시켰을 경우를 대비해 다시 코딩했음
            string csv = "";
            using(StreamReader sr = new StreamReader(Path.Combine(fileDi, fileName)))
            {
                string csvLines = "";
                while ( (csvLines = sr.ReadLine()) != null )
                {
                    string[] csvParams = csvLines.Split(',');
                    if (csvParams[2].Equals("title"))
                    {
                        csvParams[3] = newTitle;
                        csvLines = "";

                        foreach (string param in csvParams)
                        {
                            csvLines += param + ',';
                        }
                        csvLines.TrimEnd(',');
                    }
                    csv += csvLines + Environment.NewLine;
                }
            }
            File.WriteAllText(Path.Combine(fileDi, fileName), csv.TrimEnd('\r', '\n'), Encoding.UTF8);

            preConfLoad();
            mainPaint();
        }

        private void btnManageDelete_Click(object sender, EventArgs e)
        {
            if (lvPresetConf.SelectedItems.Count < 1)
            {
                MessageBox.Show("삭제할 사용자 설정을 선택해주세요");
                return;
            }

            string[] confCheck = Directory.GetFiles(Path.Combine(basePath, reqDi[(int)configFileType.Preset]), "presetConfig*");
            if (MessageBox.Show("선택한 사용자 설정을 삭제하시겠습니까?",
                "사용자 설정 삭제", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }

            foreach (string conf in confCheck)
            {
                string fileName = Path.GetFileName(conf);
                if (lvPresetConf.SelectedItems[0].Name == fileName)
                {
                    lvPresetConf.SelectedItems[0].Remove();
                    File.Delete(conf);

                    //int oldIndex = int.Parse(fileName.Substring(12));
                    int oldIndex = int.Parse(Regex.Replace(fileName, @"\D", ""));

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

        //ManageForm 크기 조절 시 info 열 크기 자동 맞춤
        private void lvPresetConf_SizeChanged(object sender, EventArgs e)
        {
            lvPresetConf.Columns[2].Width = lvPresetConf.Width - lvPresetConf.Columns[0].Width - lvPresetConf.Columns[1].Width;
        }

        //선택된 행의 config 파일 설명 수정
        private void btnManageInfo_Click(object sender, EventArgs e)
        {
            if (lvPresetConf.SelectedItems.Count < 1)
            {
                MessageBox.Show("수정할 사용자 설정을 선택해주세요");
                return;
            }

            string fileName = lvPresetConf.SelectedItems[0].Name;
            string oldInfo = lvPresetConf.SelectedItems[0].SubItems[2].Text;
            string fileDi = Path.Combine(basePath, reqDi[(int)configFileType.Preset]);
            string? newInfo = Microsoft.VisualBasic.Interaction.InputBox("설정값에 붙일 설명을 입력해주세요", "사용자 설정값 설명 바꾸기");

            if (newInfo == "")
            {
                return;
            }

            string? csvLines;
            StringBuilder sb = new StringBuilder();

            using (StreamReader reader = new StreamReader(Path.Combine(fileDi, fileName)))
            {
                while ((csvLines = reader.ReadLine()) != null)
                {
                    if (csvLines.Contains("info"))
                    {
                        csvLines = csvLines.Replace("," + oldInfo + ",", "," + newInfo + ",");
                    }
                    sb.AppendLine(csvLines);
                }
            }

            File.WriteAllText(Path.Combine(fileDi, fileName), sb.ToString(), Encoding.UTF8);

            preConfLoad();
        }

        private void lvPresetConf_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvPresetConf.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewHitTestInfo hitTestInfo = lvPresetConf.HitTest(e.Location);

            if(hitTestInfo == null)
            {
                MessageBox.Show("Error: Fail to load Users Setting Manage", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
                return;
            }

            int clickedColumn = hitTestInfo.Item.SubItems.IndexOf(hitTestInfo.SubItem);

            switch(clickedColumn)
            {
                case 0:
                    btnManageTitle_Click(sender, e);
                    break;
                case 1:
                    string fileDi = Path.Combine(basePath, reqDi[(int)configFileType.Preset]);
                    string fileName = lvPresetConf.SelectedItems[0].Name;
                    presetReflect(fileDi, fileName);
                    this.Close();
                    break;
                case 2:
                    btnManageInfo_Click(sender, e);
                    break;
            }
        }
    }
}
