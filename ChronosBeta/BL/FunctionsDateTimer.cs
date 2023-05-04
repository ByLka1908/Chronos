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
using ChronosBeta.ViewModels;
using FontAwesome.Sharp;
using ChronosBeta.Model;

namespace ChronosBeta.BL
{
    public class FunctionsDateTimer
    {
        private static bool joobTime = false;
        private static TimeSpan timeStart = new TimeSpan();
        private static TimeSpan timeEnd = new TimeSpan();

        private static bool AddDateTimer(TimeSpan timeStart, TimeSpan timeEnd)
        {
            DB.DateTimer time = new DB.DateTimer();
            try
            {
                time.Users = FunctionsCurrentUser.GetIDUser();
                time.Day = DateTime.Now;
                time.TimeStart = timeStart;
                time.TimeEnd = timeEnd;
                time.AllRunProgram = FunctionsJSON.GetJson();
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

        public static string OffOnDateTimer()
        {
            string contentLabel;
            if (joobTime)
            {
                contentLabel = "Здраствуйте, включите таймер рабочего времени!";
                joobTime = false;

                timeEnd = DateTime.Now.TimeOfDay;
                AddDateTimer(timeStart, timeEnd);
            }
            else
            {
                contentLabel = "Приятной работы!";
                joobTime = true;

                timeStart = DateTime.Now.TimeOfDay;
                FunctionsJSON.CreateJson();
            }
            return contentLabel;
        }
        public static SolidColorBrush GetColorBrushes()
        {
            if (joobTime)
                return Brushes.Green;
            else
                return Brushes.Red;
        }

        public static List<ViewDateTimer> GetDateTimer()
        {
            try
            {
                DB.CronosEntities entities = new DB.CronosEntities();
                var dateTimer = entities.DateTimer.ToList();
                List<ViewDateTimer> listDateTimer = new List<ViewDateTimer>();
                foreach (var date in dateTimer)
                    listDateTimer.Add(new ViewDateTimer(date));
                return listDateTimer;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
