using ChronosBeta.DB;
using ChronosBeta.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace ChronosBeta.BL
{
    public class FunctionsTaskMark
    {
        /// <summary>
        /// Получить список отметок по задачам
        /// </summary>
        /// <returns></returns>
        public static List<ViewTaskTimer> GetTasksTimer()
        {
            CronosEntities entities = new CronosEntities();
            var taskTimers = entities.TaskTimer.ToList();
            List<ViewTaskTimer> viewTaskTimers = new List<ViewTaskTimer>();
            foreach (var taskTimer in taskTimers)
                viewTaskTimers.Add(new ViewTaskTimer(taskTimer));
            return viewTaskTimers;
        }

        /// <summary>
        /// Добавить отметку по задаче
        /// </summary>
        /// <param name="User">Пользователь</param>
        /// <param name="Task">Задача</param>
        /// <param name="SpentTime">Потраченное время</param>
        /// <param name="Description">Описание</param>
        /// <param name="Day">День отметки</param>
        /// <exception cref="Exception"></exception>
        public static void AddTaskTimer(int User, int Task, string SpentTime, 
                                        string Description, DateTime Day)
        {
            TaskTimer taskTimer = new TaskTimer();

            taskTimer.Users       = User; 
            taskTimer.Task        = Task;
            taskTimer.SpentTime   = Convert.ToDouble(SpentTime);
            taskTimer.Description = Description;
            taskTimer.Day         = Day;

            if (taskTimer == null)
            {
                throw new Exception();
            }

            CronosEntities entities = new CronosEntities();
            entities.TaskTimer.Add(taskTimer);
            entities.SaveChanges();
        }

        /// <summary>
        /// Изменить отметку по задаче
        /// </summary>
        /// <param name="User">Пользователь</param>
        /// <param name="Task">Задача</param>
        /// <param name="SpentTime">Потраченное время</param>
        /// <param name="Description">Описание</param>
        /// <param name="Day">День отметки</param>
        /// <param name="taskTimer">Отметка по задаче</param>
        /// <exception cref="Exception"></exception>
        public static void EditTaskTimer(int User, int Task, string SpentTime, 
                                         string Description, DateTime Day, TaskTimer taskTimer)
        {
            taskTimer.Users       = User; 
            taskTimer.Task        = Task;
            taskTimer.SpentTime   = Convert.ToDouble(SpentTime);
            taskTimer.Description = Description;
            taskTimer.Day         = Day;

            if (taskTimer == null)
            {
                throw new Exception();
            }

            CronosEntities entities = new CronosEntities();
            entities.TaskTimer.AddOrUpdate(taskTimer);
            entities.SaveChanges();
        }

        /// <summary>
        /// Удалить отметку по задаче
        /// </summary>
        /// <param name="taskTimer">Отметка по задаче</param>
        public static void DeleteTaskTimer(TaskTimer taskTimer)
        {
            CronosEntities entities = new CronosEntities();
            entities.TaskTimer.Remove(entities.TaskTimer.Find(taskTimer.ID_TaskTimer));
            entities.SaveChanges();
        }
    }
}