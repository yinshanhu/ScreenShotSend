using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShotSend.Helper
{
    public class ScreenHelper
    {
        public static Bitmap GetScreen()
        {
            ////获取整个屏幕图像,不包括任务栏
            //Point point = new Point(point.X, point.Y);
            //Rectangle ScreenArea = Screen.GetWorkingArea(point);
            //Bitmap bmp = new Bitmap(ScreenArea.Width, ScreenArea.Height);
            //using (Graphics g = Graphics.FromImage(bmp))
            //{
            //    g.CopyFromScreen(0, 0, 0, 0, new Size(ScreenArea.Width, ScreenArea.Height));
            //}
            //return bmp;

            int width = Screen.PrimaryScreen.Bounds.Width;
            int height = Screen.PrimaryScreen.Bounds.Height;
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(0, 0, 0, 0, new Size(width, height));
            }
            return bmp;
        }
    }
}
