using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Drawing;
using System.Timers;
using System.Collections.Generic;
using ChronosBeta.Model;
using System.Drawing.Imaging;
using Point = System.Drawing.Point;
using ChronosBeta.DB;
using System.Linq;
using System.Diagnostics;
using Size = System.Drawing.Size;

namespace ChronosBeta.BL
{
    public static class FunctionsImage
    {
        private static List<ViewScreenshot> Screenshots { get; set; }
        private static System.Timers.Timer myTimer;

        public static int CurrentDateTimer;

        public static int ScreenShotTiming { get; set; }

        public static List<ViewScreenshot> GetScreenshot(int dateTimer)
        {
            CronosEntities entities = new CronosEntities();
            var screnshot = entities.Screenshot.Where(x => x.DateTimer == dateTimer).ToList();
            List<ViewScreenshot> listScreenshot = new List<ViewScreenshot>();
            foreach (var scren in screnshot)
                listScreenshot.Add(new ViewScreenshot(scren));
            return listScreenshot;
        }

        public static void StartScreenshot()
        {
            myTimer = new System.Timers.Timer(ScreenShotTiming); //5000 это 5 сек
            myTimer.Elapsed += Screenshot;
            myTimer.Enabled = true;
            Screenshots = new List<ViewScreenshot>();
        }

        public static void AddScreenshot()
        {
            foreach (var screenshot in Screenshots)
            {
                Screenshot currentscren = new Screenshot();

                currentscren.ImageScreenshot = PushImage(screenshot.ImageScreenshot);
                currentscren.DateTimer = CurrentDateTimer;
                currentscren.Time = StringToTimeSpan(screenshot.Time);

                if (currentscren == null)
                    return;

                CronosEntities entities = new CronosEntities();
                entities.Screenshot.Add(currentscren);
                entities.SaveChanges();
                return;
            }
        }

        public static BitmapImage GetImage(string path = null)
        {
            if (path == null)
                return new BitmapImage(new Uri("pack://application:,,/Images/no_image.png"));

            return new BitmapImage(new Uri(path));
        }

        public static BitmapImage SetImage()
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return GetImage(openFileDialog.FileName);
                }
                return null;
            }
        }

        public static byte[] PushImage(ImageSource ImageUser)
        {
            var image = (BitmapImage)ImageUser;

            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        public static BitmapImage ByteToBitmapImage(byte[] ImageUser)
        {
            var memoryStream = new MemoryStream(ImageUser);
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memoryStream;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();
            bitmapImage.Freeze();

            return bitmapImage;
        }

        private async static void Screenshot(Object source, ElapsedEventArgs e)
        {
            Size monitorSize = GetMonitorSize();
            Bitmap bmp = new Bitmap(monitorSize.Width, monitorSize.Height);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.CopyFromScreen(Point.Empty, Point.Empty, bmp.Size);

            ViewScreenshot currentscren = new ViewScreenshot();
            currentscren.Screenshot = new DB.Screenshot();
            currentscren.ImageScreenshot = BitmapToBitmapImage(bmp);
            currentscren.Time = DateTime.Now.ToLongTimeString();

            Screenshots.Add(currentscren);

            await System.Threading.Tasks.Task.Delay(1000);
        }

        private static Size GetMonitorSize()
        {
            IntPtr hwnd = Process.GetCurrentProcess().MainWindowHandle;
            Graphics g = Graphics.FromHwnd(hwnd);

            return new Size((int)g.VisibleClipBounds.Width, (int)g.VisibleClipBounds.Height);
        }

        private static TimeSpan StringToTimeSpan(string str)
        {
            string[] time = str.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            string hours = time[0];
            string min = time[1];
            string sec = time[2];

            return new TimeSpan(Convert.ToInt32(hours), Convert.ToInt32(min), Convert.ToInt32(sec));
        }

        private static BitmapImage BitmapToBitmapImage(this Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
}