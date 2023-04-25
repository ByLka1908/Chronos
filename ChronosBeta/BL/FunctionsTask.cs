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

        public static DB.Task GetCurrentTask(ViewTask currentTask)
        {
            CronosEntities ent = new CronosEntities();
            DB.Task task = ent.Task.Find(currentTask.Id);
            return task;
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
                task.UserDoTask = GetIdUser(UserDoTask);
                task.UserCreateTask = GetIdUser(UserCreateTask);
                task.Project = GetIdProject(Project);
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
                currentTask.UserDoTask = GetIdUser(UserDoTask);
                currentTask.UserCreateTask = GetIdUser(UserCreateTask);
                currentTask.Project = GetIdProject(Project);
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

        private static int GetIdUser(string user)
        {
            try
            {
                CronosEntities entities = new CronosEntities();
                return entities.Users.Where(x => x.Name == user).First().ID_Users;
            }
            catch
            {
                throw new Exception("Ошибка при получении Епик геймс");
            }
        }

        private static int GetIdProject(string project)
        {
            try
            {
                DB.CronosEntities entities = new DB.CronosEntities();
                return entities.Project.Where(x => x.NameProject == project).First().id_Project;
            }
            catch
            {
                throw new Exception("Ошибка при получении Епик геймс");
            }
        }
    }
}