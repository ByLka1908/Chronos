using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using ChronosBeta.DB;
using ChronosBeta.Model;

namespace ChronosBeta.BL
{
    public class FunctionsDateTimer
    {
        private static bool     joobTime         = false; //Начало работы
        private static TimeSpan timeStart        = new TimeSpan(); //Время старта работы
        private static TimeSpan timeEnd          = new TimeSpan(); //Время окончания работы
        private static string ContentButtonTrack = "Здраствуйте, включите таймер рабочего времени!"; //Текст перед кнопкой

        /// <summary>
        /// Получить список рабочего времени
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">Текст ошибки</exception>
        public static List<ViewDateTimer> GetDateTimer()
        {
            try
            {
                CronosEntities entities = new CronosEntities();
                var dateTimers = entities.DateTimer.ToList();
                List<ViewDateTimer> viewDateTimer = new List<ViewDateTimer>();
                foreach (var dateTimer in dateTimers)
                    viewDateTimer.Add(new ViewDateTimer(dateTimer));
                return viewDateTimer;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// Возвращаем текст рабочего времени
        /// </summary>
        /// <returns></returns>
        public static string GetContentButtonTrack()
        {
            return ContentButtonTrack;
        }

        /// <summary>
        /// Возвращаем цвет кнопки Вкл/Выкл рабочего времени
        /// </summary>
        /// <returns>Цвет кнопки</returns>
        public static SolidColorBrush GetColorBrushes()
        {
            if (joobTime)
                return Brushes.Green;
            else
                return Brushes.Red;
        }

        /// <summary>
        /// Включение и выключения трекера рабочего времени
        /// </summary>
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

        /// <summary>
        /// Добавление записи о времени рабочего дня
        /// </summary>
        /// <param name="timeStart">Время начало</param>
        /// <param name="timeEnd">Время окончания</param>
        private static void AddDateTimer(TimeSpan timeStart, TimeSpan timeEnd)
        {
            DateTimer dateTimer = new DateTimer();
            try
            {
                dateTimer.Users = FunctionsCurrentUser.GetIDUser();
                dateTimer.Day = DateTime.Now;
                dateTimer.TimeStart = timeStart;
                dateTimer.TimeEnd = timeEnd;
                dateTimer.AllRunProgram = FunctionsJSON.GetJson();
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка добавление записи об ");
            }

            if (dateTimer == null)
                return;

            try
            {
                CronosEntities entities = new CronosEntities();
                entities.DateTimer.Add(dateTimer);
                entities.SaveChanges();
                FunctionsImage.CurrentDateTimer = dateTimer.ID_DateTimer;
                FunctionsImage.AddScreenshot();
                return;
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка добавление записи об рабочем таймере");
            }
        }

        /// <summary>
        /// Удалить отметку рабочего времени
        /// </summary>
        /// <param name="currentDate">Отметка рабочего времени</param>
        public static void DeleteDateTimer(DateTimer dateTimer)
        {
            CronosEntities entities = new CronosEntities();

            //Проверяем, есть ли связанные снимки рабочего стола
            var screenshot = entities.Screenshot.Where(x => x.DateTimer == dateTimer.ID_DateTimer).ToList();
            foreach(var screen in screenshot)
            {
                entities.Screenshot.Remove(screen);
            }

            entities.DateTimer.Remove(entities.DateTimer.Find(dateTimer.ID_DateTimer));
            entities.SaveChanges();
        }
    }
}