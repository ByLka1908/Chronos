using ChronosBeta.DB;
using ChronosBeta.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;

namespace ChronosBeta.BL
{
    public class FunctionsTaskMark
    {
        public static List<ViewTaskTimer> GetTasksTimer()
        {
            CronosEntities entities = new CronosEntities();
            var task = entities.TaskTimer.ToList();
            List<ViewTaskTimer> view = new List<ViewTaskTimer>();
            foreach (var item in task)
                view.Add(new ViewTaskTimer(item));
            return view;
        }

        public static void AddTaskTimer(int User, int Task, string SpentTime, string Description)
        {
            TaskTimer taskTimer = new TaskTimer();

            taskTimer.Users       = User; 
            taskTimer.Task        = Task;
            taskTimer.SpentTime   = Convert.ToDouble(SpentTime);
            taskTimer.Description = Description;

            if (taskTimer == null)
            {
                throw new Exception();
            }

            CronosEntities entities = new CronosEntities();
            entities.TaskTimer.Add(taskTimer);
            entities.SaveChanges();
        }

        public static void EditTaskTimer(int User, int Task, string SpentTime, string Description, TaskTimer taskTimer)
        {
            taskTimer.Users = User; 
            taskTimer.Task = Task;
            taskTimer.SpentTime = Convert.ToDouble(SpentTime);
            taskTimer.Description = Description;

            if (taskTimer == null)
            {
                throw new Exception();
            }

            CronosEntities entities = new CronosEntities();
            entities.TaskTimer.AddOrUpdate(taskTimer);
            entities.SaveChanges();
        }

        public static void DeleteTaskTimer(TaskTimer currentTask)
        {
            CronosEntities entities = new CronosEntities();
            entities.TaskTimer.Remove(entities.TaskTimer.Find(currentTask.ID_TaskTimer));
            entities.SaveChanges();
        }
    }
}