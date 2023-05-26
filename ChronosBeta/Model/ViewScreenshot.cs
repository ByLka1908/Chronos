using ChronosBeta.BL;
using ChronosBeta.DB;
using System.Windows.Media.Imaging;

namespace ChronosBeta.Model
{
    public class ViewScreenshot
    {
        public Screenshot Screenshot { get; set; } //Снимок экрана
        public BitmapImage ImageScreenshot { get; set; } //Изображения снимка экрана
        public int DateTimer { get; set; } 
        public string Time { get; set; }

        /// <summary>
        /// Инициализация представления снимка экрана
        /// </summary>
        /// <param name="screenshot">Снимок экрана</param>
        public ViewScreenshot(Screenshot screenshot = null) 
        {
            if (screenshot != null)
            {
                Screenshot = screenshot;
                DateTimer  = screenshot.DateTimer;
                Time       = screenshot.Time.ToString();

                if (screenshot.ImageScreenshot == null || screenshot.ImageScreenshot.Length == 0)
                    return;

                ImageScreenshot = FunctionsImage.ByteToBitmapImage(screenshot.ImageScreenshot);
            }
        }
    }
}