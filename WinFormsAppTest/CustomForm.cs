using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppTest
{
    public class CustomForm : Form
    {
        // 명령의 출처 중 윈도우를 가리키는 코드
        private const int WM_SYSCOMMAND = 0x0112;
        // 윈폼 최소화 명령의 코드
        private const int SC_MINIMIZE = 0xF020;

        // 윈폼이 외부로부터 받는 명령을 실행하는 메서드 일부분 오버라이드
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SYSCOMMAND)
            {
                if (m.WParam.ToInt32() == SC_MINIMIZE)
                {
                    this.WindowState = FormWindowState.Minimized;
                }
            }
            base.WndProc(ref m);
        }
    }
}
