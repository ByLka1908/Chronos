using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace ChronosBeta.BL
{
    public class FunctionsDateTimer
    {
        private static bool joobTime = false;
        private static TimeSpan timeStart = new TimeSpan();
        private static TimeSpan timeEnd = new TimeSpan();

        public static bool AddDateTimer(TimeSpan timeStart, TimeSpan timeEnd)
        {
            DB.DateTimer time = new DB.DateTimer();
            try
            {
                time.Users = CurrentUser.User.ID_Users;
                time.Day = DateTime.Now;
                time.TimeStart = timeStart;
                time.TimeEnd = timeEnd;
                time.AllRunProgram = File.ReadAllText(@"F:\Projects\VisualStudioSource\ChronosBeta\ChronosBeta\Temp\ListProcess.json");
                DeleteFile.Delete(@"F:\Projects\VisualStudioSource\ChronosBeta\ChronosBeta\Temp\ListProcess.json");
            }
            catch 
            {
                throw new Exception("Ошибка иницилизации добавления");
            }

            if (time == null)
                return false;

            try
            {
                DB.CronosEntities entities = new DB.CronosEntities();
                entities.DateTimer.Add(time);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                throw new Exception("Ошибка добавления");
            }
        }

        public static void OffOnDateTimer(Button currentButton)
        {
            ImageBrush myImageBrush = new ImageBrush();

            if (joobTime)
            {
                myImageBrush.ImageSource = new BitmapImage(new Uri("F:\\Projects\\VisualStudioSource\\ChronosBeta\\ChronosBeta\\Image\\Off.png", UriKind.Relative));
                currentButton.Background = myImageBrush;
                joobTime = false;

                timeEnd = DateTime.Now.TimeOfDay;
                AddDateTimer(timeStart, timeEnd);
            }
            else
            {
                myImageBrush.ImageSource = new BitmapImage(new Uri("F:\\Projects\\VisualStudioSource\\ChronosBeta\\ChronosBeta\\Image\\On.png", UriKind.Relative));
                currentButton.Background = myImageBrush;
                joobTime = true;

                timeStart = DateTime.Now.TimeOfDay;
                FunctionsListApplication.CreateJsonListApplication();
            }
        }
    }
}
