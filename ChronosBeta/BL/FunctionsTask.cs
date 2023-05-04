using System;
using System.Collections.Generic;
using System.Linq;
using ChronosBeta.Model;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;
using ChronosBeta.DB;
using System.Xml.Linq;
using System.Data.Entity.Migrations;
using System.Windows;

namespace ChronosBeta.BL
{
    class FunctionsTask
    {
        public static List<ViewTask> GetTasks()
        {
            try
            {
                CronosEntities entities = new CronosEntities();
                var project = entities.Task.ToList();
                List<ViewTask> view = new List<ViewTask>();
                foreach (var item in project)
                    view.Add(new ViewTask(item));
                return view;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static bool AddTask( string UserDoTask, string UserCreateTask ,
                                    string NameTask, string Project, 
                                    string DeadLine, string Description, 
                                    string SelectedItsOver)
        {
            DB.Task task = new DB.Task();
            try
            {
                task.NameTask = NameTask;
                task.UserDoTask = FunctionsUsers.GetIdUser(UserDoTask);
                task.UserCreateTask = FunctionsUsers.GetIdUser(UserCreateTask);
                task.Project = FunctionsProject.GetIdProject(Project);
                task.Deadline = DateTime.Parse(DeadLine);
                task.Description = Description;
                if (SelectedItsOver == "Да")
                    task.ItsOver = true;
                else
                    task.ItsOver = false;
            }
            catch
            {
                throw new Exception("Ошибка иницилизации добавления");
            }
            if (task == null)
            {
                return false;
            }
            try
            {
                CronosEntities entities = new CronosEntities();
                entities.Task.Add(task);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                throw new Exception("Ошибка добавлении пользователя");
            }
        }

        public static bool SaveEditTask(string UserDoTask, string UserCreateTask,
                            string NameTask, string Project,
                            string DeadLine, string Description,
                            string SelectedItsOver, DB.Task currentTask)
        {
            try
            {
                currentTask.NameTask = NameTask;
                currentTask.UserDoTask = FunctionsUsers.GetIdUser(UserDoTask);
                currentTask.UserCreateTask = FunctionsUsers.GetIdUser(UserCreateTask);
                currentTask.Project = FunctionsProject.GetIdProject(Project);
                currentTask.Deadline = DateTime.Parse(DeadLine);
                currentTask.Description = Description;
                if (SelectedItsOver == "Да")
                    currentTask.ItsOver = true;
                else
                    currentTask.ItsOver = false;
            }
            catch
            {
                throw new Exception("Ошибка иницилизации добавления");
            }
            if (currentTask == null)
            {
                return false;
            }
            try
            {
                CronosEntities entities = new CronosEntities();
                entities.Task.AddOrUpdate(currentTask);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                throw new Exception("Ошибка добавлении пользователя");
            }
        }

        public static int GetIdTask(string task)
        {
            try
            {
                DB.CronosEntities entities = new DB.CronosEntities();
                return entities.Task.Where(x => x.NameTask == task).First().ID_Task;
            }
            catch
            {
                throw new Exception("Ошибка при получении Епик геймс");
            }
        }

        public static List<string> GetViewTask()
        {
            try
            {
                DB.CronosEntities entities = new DB.CronosEntities();
                var task = entities.Task.Select(x => x.NameTask).ToList();
                return task;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static void DeleteTask(DB.Task currentTask)
        {
            if (currentTask == null)
            {
                MessageBox.Show("Отметка не выбрана");
                return;
            }
            try
            {
                DB.CronosEntities entities = new CronosEntities();
                entities.Task.Remove(entities.Task.Find(currentTask.ID_Task));
                entities.SaveChanges();
            }
            catch
            {
                throw new Exception("Ошибка удаления отметки по задаче");
            }
        }
    }
}