using System;
using System.Collections.Generic;
using System.Linq;
using ChronosBeta.Model;
using ChronosBeta.DB;
using System.Data.Entity.Migrations;

namespace ChronosBeta.BL
{
    class FunctionsTask
    {
        /// <summary>
        /// Получить представление задачи
        /// </summary>
        /// <param name="task">Название задачи</param>
        /// <returns></returns>
        public static ViewTask GetTaskView(string task)
        {
            CronosEntities entities = new CronosEntities();
            DB.Task currentTask = entities.Task.Where(x => x.NameTask == task).First();
            return new ViewTask(currentTask);
        }

        /// <summary>
        /// Получить список задач
        /// </summary>
        /// <returns></returns>
        public static List<ViewTask> GetTasks()
        {
            CronosEntities entities = new CronosEntities();
            var tasks = entities.Task.ToList();
            List<ViewTask> viewTasks = new List<ViewTask>();
            foreach (var task in tasks)
                viewTasks.Add(new ViewTask(task));
            return viewTasks;
        }

        /// <summary>
        /// Получить список задач
        /// </summary>
        /// <returns></returns>
        public static List<string> GetListTasks()
        {
            CronosEntities entities = new CronosEntities();
            return entities.Task.Select(x => x.NameTask).ToList();
        }

        /// <summary>
        /// Получить id задачи
        /// </summary>
        /// <param name="task">Название задачи</param>
        /// <returns></returns>
        public static int GetIdTask(string task)
        {
            CronosEntities entities = new CronosEntities();
            return entities.Task.Where(x => x.NameTask == task).First().ID_Task;
        }

        /// <summary>
        /// Добавить задачу
        /// </summary>
        /// <param name="UserDoTask">Пользователь выполняющий задачу</param>
        /// <param name="UserCreateTask">Пользователь создавший задачу</param>
        /// <param name="NameTask">Название задачи</param>
        /// <param name="Project">Проект по задаче</param>
        /// <param name="DeadLine">Срок выполения</param>
        /// <param name="Description">Описание</param>
        /// <param name="SelectedItsOver">Закончена ли задача</param>
        /// <param name="EstimatedTime">Ожидаемое время выполения (в часах)</param>
        /// <param name="AllSpentTime">Общее время выполнения (в часах)</param>
        public static void AddTask( int      UserDoTask,      int UserCreateTask ,
                                    string   NameTask,        int Project, 
                                    DateTime DeadLine,        string Description, 
                                    string   SelectedItsOver, string EstimatedTime, 
                                    string   AllSpentTime)
        {
            DB.Task task = new DB.Task();

            task.NameTask       = NameTask;
            task.UserDoTask     = UserDoTask;
            task.UserCreateTask = UserCreateTask;
            task.Project        = Project;
            task.Deadline       = DeadLine;
            task.Description    = Description;
            task.EstimatedTime  = Convert.ToDouble(EstimatedTime);

            if (AllSpentTime == "")
                task.AllSpentTime = 0;
            else
                task.AllSpentTime = Convert.ToDouble(AllSpentTime);

            if (SelectedItsOver == "Да")
                task.ItsOver = true;
            else
                task.ItsOver = false;

            if (task == null)
            {
                return;
            }

            CronosEntities entities = new CronosEntities();
            entities.Task.Add(task);
            entities.SaveChanges();
        }

        /// <summary>
        /// Изменить задачу
        /// </summary>
        /// <param name="UserDoTask">Пользователь выполняющий задачу</param>
        /// <param name="UserCreateTask">Пользователь создавший задачу</param>
        /// <param name="NameTask">Название задачи</param>
        /// <param name="Project">Проект по задаче</param>
        /// <param name="DeadLine">Срок выполения</param>
        /// <param name="Description">Описание</param>
        /// <param name="SelectedItsOver">Закончена ли задача</param>
        /// <param name="EstimatedTime">Ожидаемое время выполения (в часах)</param>
        /// <param name="AllSpentTime">Общее время выполнения (в часах)</param>
        /// <param name="task">Задача</param>
        public static void EditTask(int UserDoTask,           int UserCreateTask,
                                    string   NameTask,        int Project,
                                    DateTime DeadLine,        string Description,
                                    string   SelectedItsOver, string   EstimatedTime,
                                    string AllSpentTime,      DB.Task task)
        {
            task.NameTask       = NameTask;
            task.UserDoTask     = UserDoTask;
            task.UserCreateTask = UserCreateTask;
            task.Project        = Project;
            task.Deadline       = DeadLine;
            task.Description    = Description;
            task.EstimatedTime  = Convert.ToDouble(EstimatedTime);
            if (AllSpentTime == "")
                task.AllSpentTime = 0;
            else
                task.AllSpentTime   = Convert.ToDouble(AllSpentTime);

            if (SelectedItsOver == "Да")
                task.ItsOver = true;
            else
                task.ItsOver = false;

            if (task == null)
            {
                return;
            }

            CronosEntities entities = new CronosEntities();
            entities.Task.AddOrUpdate(task);
            entities.SaveChanges();
        }

        /// <summary>
        /// Удалить задачу
        /// </summary>
        /// <param name="currentTask">Задача</param>
        public static void DeleteTask(DB.Task task)
        {
            CronosEntities entities = new CronosEntities();
            
            //Проверяем отметки по задаче
            var tasksTimer = entities.TaskTimer.Where(x => x.Task == task.ID_Task).ToList();
            foreach (var taskTimer in tasksTimer)
            {
                entities.TaskTimer.Remove(taskTimer);
            }

            entities.Task.Remove(entities.Task.Find(task.ID_Task));
            entities.SaveChanges();
        }
    }
}