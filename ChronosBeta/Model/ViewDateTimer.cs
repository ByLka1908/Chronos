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
        public string User { get; set; }
        public string Day { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }

        public ViewDateTimer(DateTimer date)
        {
            DateTimer = date;
            Id = date.ID_DateTimer;
            User = date.Users1.Surname;
            Day = date.Day.ToString();
            TimeStart = date.TimeStart.ToString();
            TimeEnd = date.TimeEnd.ToString();
        }

    }
}
