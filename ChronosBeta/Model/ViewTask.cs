namespace ChronosBeta.Model
{
    public class ViewTask
    {
        public DB.Task Task { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string EstimatedTime { get; set; }
        public string Deadline { get; set; }
        public string UserCreateTask { get; set; }
        public string UserDoTask { get; set; }

        public ViewTask(DB.Task task) 
        {
            Task           = task;
            Id             = task.ID_Task;
            Name           = task.NameTask;
            EstimatedTime  = task.EstimatedTime.ToString();
            Deadline       = task.Deadline.ToString("M/d/yyyy hh:mm:ss tt");
            UserCreateTask = task.Users.Name + " " + task.Users.Surname + " " + task.Users.MiddleName;
            UserDoTask     = task.Users1.Name + " " + task.Users1.Surname + " " + task.Users1.MiddleName;
        }
    }
}