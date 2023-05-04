using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronosBeta.Model
{
    public class ViewTaskTimer
    {
        public DB.TaskTimer TaskTimer { get; set; }
        public int Id { get; set; }
        public string Task { get; set; }
        public string SpentTime { get; set; }

        public ViewTaskTimer(DB.TaskTimer task)
        {
            TaskTimer = task;
            Id = task.ID_TaskTimer;
            Task = task.Task1.NameTask;
            SpentTime = task.SpentTime.ToString();
        }
    }
}
