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
        private static List<ViewScreenshot> Screenshots { get; set; } //Список снимков рабочего стола
        private static System.Timers.Timer myTimer; //Таймер снятия снимков

        public static int CurrentDateTimer;

        public static int ScreenShotTiming { get; set; } // Интервал снятия снимков

        /// <summary>
        /// Получить список снимков экрана
        /// </summary>
        /// <param name="dateTimer"></param>
        /// <returns></returns>
        public static List<ViewScreenshot> GetScreenshots(int dateTimer)
        {
            CronosEntities entities = new CronosEntities();
            var screnshots = entities.Screenshot.Where(x => x.DateTimer == dateTimer).ToList();
            List<ViewScreenshot> viewScreenshots = new List<ViewScreenshot>();
            foreach (var screnshot in screnshots)
                viewScreenshots.Add(new ViewScreenshot(screnshot));
            return viewScreenshots;
        }

        /// <summary>
        /// Начать снятие снимков экрана
        /// </summary>
        public static void StartScreenshot()
        {
            myTimer = new System.Timers.Timer(ScreenShotTiming); //5000 это 5 сек
            myTimer.Elapsed += Screenshot;
            myTimer.Enabled = true;
            Screenshots = new List<ViewScreenshot>();
        }

        /// <summary>
        /// Добавить снимки экранов
        /// </summary>
        public static void AddScreenshots()
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

        /// <summary>
        /// Получить изображение из указанного пути
        /// </summary>
        /// <param name="path">Путь к изображению</param>
        /// <returns></returns>
        public static BitmapImage GetImage(string path = null)
        {
            if (path == null)
                return new BitmapImage(new Uri("pack://application:,,/Images/no_image.png"));

            return new BitmapImage(new Uri(path));
        }

        /// <summary>
        /// Выбрать изображение из папки
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Подготовить изображение к загрузке в БД
        /// </summary>
        /// <param name="ImageUser">Изображение</param>
        /// <returns></returns>
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

        /// <summary>
        /// Преобразовать изображение из БД
        /// </summary>
        /// <param name="ImageUser">Байтовое предстволение изображения</param>
        /// <returns></returns>
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

        #region Приватные методы
        /// <summary>
        /// Снятие снимка экрана
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Получить размер монитора пользователя
        /// </summary>
        /// <returns></returns>
        private static Size GetMonitorSize()
        {
            IntPtr hwnd = Process.GetCurrentProcess().MainWindowHandle;
            Graphics g = Graphics.FromHwnd(hwnd);

            return new Size((int)g.VisibleClipBounds.Width, (int)g.VisibleClipBounds.Height);
        }

        /// <summary>
        /// Время снятия снимка рабочего стола
        /// </summary>
        /// <param name="textTime">Строчное представление времени</param>
        /// <returns></returns>
        private static TimeSpan StringToTimeSpan(string textTime)
        {
            string[] time = textTime.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            string hours = time[0];
            string min = time[1];
            string sec = time[2];

            return new TimeSpan(Convert.ToInt32(hours), Convert.ToInt32(min), Convert.ToInt32(sec));
        }

        /// <summary>
        /// Конвертация изображения из Bitmap в BitmapImage
        /// </summary>
        /// <param name="bitmap">Изображение</param>
        /// <returns></returns>
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
        #endregion
    }
}