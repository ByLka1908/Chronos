using ChronosBeta.DB;
using ChronosBeta.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChronosBeta.BL
{
    public class FunctionsTaskMark
    {
        public static List<ViewTaskTimer> GetTasksTimer()
        {
            try
            {
                CronosEntities entities = new CronosEntities();
                var task = entities.TaskTimer.ToList();
                List<ViewTaskTimer> view = new List<ViewTaskTimer>();
                foreach (var item in task)
                    view.Add(new ViewTaskTimer(item));
                return view;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static bool AddTaskTimer(string User, string Task,
                              string SpentTime, string Description)
        {
            TaskTimer taskTimer = new TaskTimer();
            try
            {
                taskTimer.Users = FunctionsUsers.GetIdUser(User); 
                taskTimer.Task = FunctionsTask.GetIdTask(Task);
                taskTimer.SpentTime = Convert.ToDouble(SpentTime);
                taskTimer.Description = Description;
            }
            catch
            {
                throw new Exception("Ошибка иницилизации добавления");
            }
            if (taskTimer == null)
            {
                return false;
            }
            try
            {
                CronosEntities entities = new CronosEntities();
                entities.TaskTimer.Add(taskTimer);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                throw new Exception("Ошибка добавлении отметки по задаче");
            }
        }

        public static bool EditTaskTimer(string User, string Task,
                              string SpentTime, string Description, TaskTimer taskTimer)
        {
            try
            {
                taskTimer.Users = FunctionsUsers.GetIdUser(User); 
                taskTimer.Task = FunctionsTask.GetIdTask(Task);
                taskTimer.SpentTime = Convert.ToDouble(SpentTime);
                taskTimer.Description = Description;
            }
            catch
            {
                throw new Exception("Ошибка иницилизации добавления");
            }
            if (taskTimer == null)
            {
                return false;
            }
            try
            {
                CronosEntities entities = new CronosEntities();
                entities.TaskTimer.AddOrUpdate(taskTimer);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                throw new Exception("Ошибка редактирование отметки по задаче");
            }
        }

        public static void DeleteTaskTimer(TaskTimer currentTask)
        {
            if(currentTask == null)
            {
                MessageBox.Show("Отметка не выбрана");
                return;
            }
            try
            {
                DB.CronosEntities entities = new CronosEntities();
                entities.TaskTimer.Remove(entities.TaskTimer.Find(currentTask.ID_TaskTimer));
                entities.SaveChanges();
            }
            catch
            {
                throw new Exception("Ошибка удаления отметки по задаче");
            }
        }
    }
}
