using ChronosBeta.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using ChronosBeta.DB;
using System.Data.Entity.Migrations;
using System.Windows;
using System.Windows.Media;
using System.Threading.Tasks;

namespace ChronosBeta.BL
{
    public class FunctionsUsers
    {
        public static List<ViewUsers> GetUsers()
        {
            CronosEntities entities = new CronosEntities();
            var users = entities.Users.ToList();
            List<ViewUsers> listUser = new List<ViewUsers>();
            foreach (var user in users)
                listUser.Add(new ViewUsers(user));
            return listUser;
        }

        public static List<string> GetViewUser()
        {
            CronosEntities entities = new CronosEntities();
            var users = entities.Users.Select(x => x.Name + " " + x.Surname + " " + x.MiddleName).ToList();
            return users;
        }

        public static void AddUser(string name,     string surname, string middleName,
                                   string login,    string password, string phone,
                                   string skype, string jobTitle, ImageSource imageUser)
        {
            Users user = new Users();

            user.Name       = name;
            user.Surname    = surname;
            user.MiddleName = middleName;
            user.Login      = login;
            user.Password   = password;
            user.Phone      = phone;
            user.Skype      = skype;
            user.ImageUser  = FunctionsImage.PushImage(imageUser);
            user.JobTitle   = FunctionsJobTitle.GetId(jobTitle);

            if (user == null)
            {
                return;
            }

            CronosEntities entities = new CronosEntities();
            entities.Users.Add(user);
            entities.SaveChanges();
        }

        public static void SaveEditUser(string name, string surname, string middleName,
                                        string login, string password, string phone,
                                        string skype, string jobTitle, ViewUsers SelectedUser,
                                        ImageSource imageUser)
        {
            Users user = SelectedUser.User;

            user.Name       = name;
            user.Surname    = surname;
            user.MiddleName = middleName;
            user.Login      = login;
            user.Password   = password;
            user.Phone      = phone;
            user.Skype      = skype;
            user.ImageUser  = FunctionsImage.PushImage(imageUser);
            user.JobTitle   = FunctionsJobTitle.GetId(jobTitle);

            if (user == null)
            {
                return;
            }

            CronosEntities entities = new CronosEntities();
            entities.Users.AddOrUpdate(user);
            entities.SaveChanges();
        }

        public static int GetUserId(string user)
        {
            string[] FIO = user.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string Name = FIO[0];
            string Surname = FIO[1];
            string MiddleName = FIO[2];

            CronosEntities entities = new CronosEntities();
            return entities.Users.Where(x => x.Name == Name && x.Surname == Surname && x.MiddleName == MiddleName).First().ID_Users;
        }

        public static ViewUsers GetUserView(string user)
        {
            string[] FIO = user.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string Name = FIO[0];
            string Surname = FIO[1];
            string MiddleName = FIO[2];

            CronosEntities entities = new CronosEntities();
            Users currentUser  = entities.Users.Where(x => x.Name == Name && x.Surname == Surname && x.MiddleName == MiddleName).First();
            return new ViewUsers(currentUser);
        }

        public static void DeleteUser(Users currentUser)
        {
            CronosEntities entities = new CronosEntities();

            var projects = entities.Project.Where(x => x.ResponsibleОfficer == currentUser.ID_Users).ToList();
            foreach (var project in projects)
            {
                entities.Project.Remove(project);
            }

            var tasks = entities.Task.Where(x => x.UserDoTask == currentUser.ID_Users || x.UserCreateTask == currentUser.ID_Users).ToList();
            foreach (var task in tasks)
            {
                var tasksTimer1 = entities.TaskTimer.Where(x => x.Task == task.ID_Task).ToList();
                foreach(var taskTimer in tasksTimer1)
                {
                    entities.TaskTimer.Remove(taskTimer);
                }
                entities.Task.Remove(task);
            }

            var tasksTimer = entities.TaskTimer.Where(x => x.Users == currentUser.ID_Users).ToList();
            foreach (var taskTimer in tasksTimer)
            {
                entities.TaskTimer.Remove(taskTimer);
            }

            var dateTimers = entities.DateTimer.Where(x => x.Users == currentUser.ID_Users).ToList();
            foreach (var date in dateTimers)
            {
                entities.DateTimer.Remove(date);
            }

            entities.Users.Remove(entities.Users.Find(currentUser.ID_Users));
            entities.SaveChanges();
        }
    }
}