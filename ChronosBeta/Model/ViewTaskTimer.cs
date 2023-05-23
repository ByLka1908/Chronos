using ChronosBeta.DB;

namespace ChronosBeta.Model
{
    public class ViewTaskTimer
    {
        public TaskTimer TaskTimer { get; set; }
        public int Id { get; set; }
        public string Task { get; set; }
        public string SpentTime { get; set; }
        public string Day { get; set; }

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