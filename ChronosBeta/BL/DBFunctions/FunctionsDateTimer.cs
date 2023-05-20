using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media;
using ChronosBeta.DB;
using ChronosBeta.Model;

namespace ChronosBeta.BL
{
    public class FunctionsDateTimer
    {
        private static bool     joobTime         = false;
        private static TimeSpan timeStart        = new TimeSpan();
        private static TimeSpan timeEnd          = new TimeSpan();
        private static string ContentButtonTrack = "Здраствуйте, включите таймер рабочего времени!";

        private static void AddDateTimer(TimeSpan timeStart, TimeSpan timeEnd)
        {
            DB.DateTimer time = new DB.DateTimer();
            try
            {
                time.Users         = FunctionsCurrentUser.GetIDUser();
                time.Day           = DateTime.Now;
                time.TimeStart     = timeStart;
                time.TimeEnd       = timeEnd;
                time.AllRunProgram = FunctionsJSON.GetJson();
            }
            catch 
            {
                FunctionsWindow.OpenErrorWindow("Ошибка добавление записи об ");
            }

            if (time == null)
                return;

            try
            {
                DB.CronosEntities entities = new DB.CronosEntities();
                entities.DateTimer.Add(time);
                entities.SaveChanges();
                FunctionsImage.CurrentDateTimer = time.ID_DateTimer;
                FunctionsImage.AddScreenshot();
                return;
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка добавление записи об рабочем таймере");
            }
        }

        public static void OffOnDateTimer()
        {
            if (joobTime)
            {
                ContentButtonTrack = "Здраствуйте, включите таймер рабочего времени!";
                joobTime = false;

                timeEnd = DateTime.Now.TimeOfDay;
                AddDateTimer(timeStart, timeEnd);
            }
            else
            {
                ContentButtonTrack = "Приятной работы!";
                joobTime = true;

                timeStart = DateTime.Now.TimeOfDay;
                FunctionsJSON.CreateJson();
                FunctionsImage.StartScreenshot();
            }
        }

        public static string GetContentButtonTrack()
        {
            return ContentButtonTrack;
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

        public static void DeleteDateTimer(DateTimer currentDate)
        {
            CronosEntities entities = new CronosEntities();
            var screenshot = entities.Screenshot.Where(x => x.DateTimer == currentDate.ID_DateTimer).ToList();
            foreach(var screen in screenshot)
            {
                entities.Screenshot.Remove(screen);
            }

            entities.DateTimer.Remove(entities.DateTimer.Find(currentDate.ID_DateTimer));
            entities.SaveChanges();
        }
    }
}