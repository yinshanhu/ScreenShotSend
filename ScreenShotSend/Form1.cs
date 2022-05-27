using ScreenShotSend.Helper;
using ScreenShotSend.win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShotSend
{
    public partial class Form1 : Form
    {
        private bool ShowFlag = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // uapnxbavgdzvcacj
            // https://www.cnblogs.com/lyn928/p/6598379.html
            // EmailHelper.SendEmail("smtp.qq.com", 25, "378736635@qq.com", "uapnxbavgdzvcacj", "378736635@qq.com", "test", "test", "");
        }

        private void ScreenShot()
        {
            try
            {
                Bitmap bitmap = ScreenHelper.GetScreen();
                var fileName = AppContext.BaseDirectory + "Resources\\" + DateTime.Now.ToString("yyyyMMddHHmmsssss") + ".png";
                bitmap.Save(fileName, ImageFormat.Png);

                EmailHelper.SendEmail(tbxSmtpServer.Text, Convert.ToInt32(tbxMailPort.Text),
                    tbxMailForm.Text, tbxMailPwd.Text, tbxMailTo.Text, tbxMailSubject.Text, tbxMailBody.Text, fileName);
            }
            catch (Exception ex)
            {
                tbxMessage.Text = ex.Message;
            }
        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            //注销Id号为100的热键设定
            HotKey.UnregisterHotKey(Handle, 100);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //注册热键Shift+S，Id号为100。HotKey.KeyModifiers.Shift也可以直接使用数字4来表示。
            HotKey.RegisterHotKey(Handle, 100, HotKey.KeyModifiers.Shift, Keys.S);
        }

        /// 
        /// 监视Windows消息
        /// 重载WndProc方法，用于实现热键响应
        ///
        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            //按快捷键 
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 100:    //按下的是Shift+S
                            ScreenShot();
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("您确定要退出程序吗？否最小化托盘", "确认退出", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            e.Cancel = true;
            if (result == DialogResult.No)
            {
                HideMainForm();
            }
            else if (result == DialogResult.Yes)
            {
                e.Cancel = false;
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                HideMainForm();
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (ShowFlag)
            {
                this.WindowState = FormWindowState.Minimized;

                HideMainForm();
            }
            else
            {
                ShowMainForm();
            }
        }

        #region 右键菜单处理，显示　隐藏　退出

        private void menuItem_Show_Click(object sender, EventArgs e)
        {
            ShowMainForm();
        }

        private void menuItem_Hide_Click(object sender, EventArgs e)
        {
            HideMainForm();
        }

        private void menuItem_Exit_Click(object sender, EventArgs e)
        {
            ExitMainForm();
        }

        #endregion

        #region 私有方法　处理窗体的　显示　隐藏　关闭(退出)

        private void ExitMainForm()
        {
            Application.Exit();
        }

        private void HideMainForm()
        {
            ShowFlag = false;
            this.notifyIcon.Visible = true;
            this.Hide();
        }

        private void ShowMainForm()
        {
            ShowFlag = true;
            this.notifyIcon.Visible = false;
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        #endregion
    }
}
