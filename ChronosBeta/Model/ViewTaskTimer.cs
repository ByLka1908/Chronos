using ChronosBeta.DB;

namespace ChronosBeta.Model
{
    public class ViewTaskTimer
    {
        public TaskTimer TaskTimer { get; set; } //Отметка по задаче
        public int Id { get; set; }
        public string Task { get; set; } //Название задачи
        public string SpentTime { get; set; } //Затреченое время
        public string Day { get; set; } 

        /// <summary>
        /// Инициализация представления отметки по задаче
        /// </summary>
        /// <param name="task">Отметка по задаче</param>
        public ViewTaskTimer(TaskTimer task)
        {
            TaskTimer = task;
            Id        = task.ID_TaskTimer;
            Task      = task.Task1.NameTask;
            SpentTime = task.SpentTime.ToString();
            Day       = task.Day?.ToString("M/d/yyyy hh:mm:ss tt");
        }
    }
}