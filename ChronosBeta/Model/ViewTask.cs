namespace ChronosBeta.Model
{
    public class ViewTask
    {
        public DB.Task Task { get; set; } //Задача
        public int Id { get; set; }
        public string Name { get; set; } //Название задачи
        public string EstimatedTime { get; set; } //Ожидаемое время выполнения задачи
        public string Deadline { get; set; } //Дата сдачи задачи
        public string UserCreateTask { get; set; } //ФИО пользователя создавшего задачу
        public string UserDoTask { get; set; } //ФИО пользователя выполняющего задачу

        /// <summary>
        /// Инициализация представления задачи
        /// </summary>
        /// <param name="task">Задача</param>
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