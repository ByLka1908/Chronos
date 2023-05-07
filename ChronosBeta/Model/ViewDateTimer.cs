using ChronosBeta.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronosBeta.Model
{
    public class ViewDateTimer
    {
        public DateTimer DateTimer { get; set; }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserMiddleName { get; set; }
        public string Day { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }

        public ViewDateTimer(DateTimer date)
        {
            DateTimer = date;
            Id = date.ID_DateTimer;
            UserName = date.Users1.Name;
            UserSurname = date.Users1.Surname;
            UserMiddleName = date.Users1.MiddleName;
            Day = date.Day.ToShortDateString();
            TimeStart = date.TimeStart.ToString();
            TimeEnd = date.TimeEnd.ToString();
        }

    }
}
