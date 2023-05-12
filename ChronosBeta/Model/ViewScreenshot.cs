using ChronosBeta.BL;
using ChronosBeta.DB;
using System.Windows.Media.Imaging;

namespace ChronosBeta.Model
{
    public class ViewScreenshot
    {
        public Screenshot Screenshot { get; set; }
        public BitmapImage ImageScreenshot { get; set; }
        public int DateTimer { get; set; }
        public string Time { get; set; }

        public ViewScreenshot()
        {

        }

        public ViewScreenshot(Screenshot screenshot) 
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