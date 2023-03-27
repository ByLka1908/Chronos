using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ChronosBeta.BL
{
    public class DateTimerAddAndChange
    {
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
                Delete.DeleteFile(@"F:\Projects\VisualStudioSource\ChronosBeta\ChronosBeta\Temp\ListProcess.json");
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
    }
}
