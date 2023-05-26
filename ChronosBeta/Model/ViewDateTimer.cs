using ChronosBeta.DB;

namespace ChronosBeta.Model
{
    public class ViewDateTimer
    {
        public DateTimer DateTimer { get; set; } //Отметка рабочего дня
        public int Id { get; set; }
        public string UserName { get; set; } //Имя пользователя
        public string UserSurname { get; set; } //Фамилия пользователя
        public string UserMiddleName { get; set; } //Отчетсво пользователя
        public string Day { get; set; } 
        public string TimeStart { get; set; } //Время старта рабочего дня
        public string TimeEnd { get; set; } //Время окончания рабочего дня

        /// <summary>
        /// Инициализация представления рабочего дня
        /// </summary>
        /// <param name="dateTimer">Отметка рабочего дня</param>
        public ViewDateTimer(DateTimer dateTimer)
        {
            DateTimer      = dateTimer;
            Id             = dateTimer.ID_DateTimer;
            Day            = dateTimer.Day.ToShortDateString();
            TimeEnd        = dateTimer.TimeEnd.ToString();
            TimeStart      = dateTimer.TimeStart.ToString();
            UserName       = dateTimer.Users1.Name;
            UserSurname    = dateTimer.Users1.Surname;
            UserMiddleName = dateTimer.Users1.MiddleName;
        }
    }
}