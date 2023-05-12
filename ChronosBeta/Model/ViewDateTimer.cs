using ChronosBeta.DB;

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
            DateTimer      = date;
            Id             = date.ID_DateTimer;
            Day            = date.Day.ToShortDateString();
            TimeEnd        = date.TimeEnd.ToString();
            TimeStart      = date.TimeStart.ToString();
            UserName       = date.Users1.Name;
            UserSurname    = date.Users1.Surname;
            UserMiddleName = date.Users1.MiddleName;
        }
    }
}