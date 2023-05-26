using ChronosBeta.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using ChronosBeta.DB;
using System.Data.Entity.Migrations;
using System.Windows.Media;

namespace ChronosBeta.BL
{
    public class FunctionsUsers
    {
        /// <summary>
        /// Получить представление пользователя
        /// </summary>
        /// <param name="user">ФИО пользователя</param>
        /// <returns></returns>
        public static ViewUsers GetUserView(string user)
        {
            string[] FIO      = user.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string Name       = FIO[0];
            string Surname    = FIO[1];
            string MiddleName = FIO[2];

            CronosEntities entities = new CronosEntities();
            Users currentUser = entities.Users.Where(x => x.Name == Name && x.Surname == Surname && x.MiddleName == MiddleName).First();
            return new ViewUsers(currentUser);
        }

        /// <summary>
        /// Получить список пользователей
        /// </summary>
        /// <returns></returns>
        public static List<ViewUsers> GetUsers()
        {
            CronosEntities entities = new CronosEntities();
            var users = entities.Users.ToList();
            List<ViewUsers> listUsers = new List<ViewUsers>();
            foreach (var user in users)
                listUsers.Add(new ViewUsers(user));
            return listUsers;
        }

        /// <summary>
        /// Получить список пользователей
        /// </summary>
        /// <returns></returns>
        public static List<string> GetViewUser()
        {
            CronosEntities entities = new CronosEntities();
            return entities.Users.Select(x => x.Name + " " + x.Surname + " " + x.MiddleName).ToList();
        }

        /// <summary>
        /// Получить id пользователя
        /// </summary>
        /// <param name="user">ФИО пользователя</param>
        /// <returns></returns>
        public static int GetUserId(string user)
        {
            string[] FIO      = user.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string Name       = FIO[0];
            string Surname    = FIO[1];
            string MiddleName = FIO[2];

            CronosEntities entities = new CronosEntities();
            return entities.Users.Where(x => x.Name == Name && x.Surname == Surname && x.MiddleName == MiddleName).First().ID_Users;
        }

        /// <summary>
        /// Добавить пользователя
        /// </summary>
        /// <param name="Name">Имя</param>
        /// <param name="Surname">Фамилия</param>
        /// <param name="MiddleName">Отчетство</param>
        /// <param name="Login">Логин</param>
        /// <param name="Password">Пароль</param>
        /// <param name="Phone">Телефон</param>
        /// <param name="Skype">Скайп</param>
        /// <param name="JobTitle">Id должность</param>
        /// <param name="ImageUser">Фото пользователя</param>
        public static void AddUser(string Name,     string Surname, string MiddleName,
                                   string Login,    string Password, string Phone,
                                   string Skype, int JobTitle, ImageSource ImageUser)
        {
            Users user = new Users();

            user.Name       = Name;
            user.Surname    = Surname;
            user.MiddleName = MiddleName;
            user.Login      = Login;
            user.Password   = Password;
            user.Phone      = Phone;
            user.Skype      = Skype;
            user.JobTitle   = JobTitle;
            user.ImageUser  = FunctionsImage.PushImage(ImageUser);

            if (user == null)
            {
                return;
            }

            CronosEntities entities = new CronosEntities();
            entities.Users.Add(user);
            entities.SaveChanges();
        }

        /// <summary>
        /// Изменить пользователя
        /// </summary>
        /// <param name="Name">Имя</param>
        /// <param name="Surname">Фамилия</param>
        /// <param name="MiddleName">Отчетство</param>
        /// <param name="Login">Логин</param>
        /// <param name="Password">Пароль</param>
        /// <param name="Phone">Телефон</param>
        /// <param name="Skype">Скайп</param>
        /// <param name="JobTitle">Id должность</param>
        /// <param name="ImageUser">Фото пользователя</param>
        /// <param name="user">Пользователь</param>
        public static void EditUser(string Name, string Surname, string MiddleName,
                                        string Login, string Password, string Phone,
                                        string Skype, int JobTitle,
                                        ImageSource ImageUser, Users user)
        {
            user.Name       = Name;
            user.Surname    = Surname;
            user.MiddleName = MiddleName;
            user.Login      = Login;
            user.Password   = Password;
            user.Phone      = Phone;
            user.Skype      = Skype;
            user.JobTitle   = JobTitle;
            user.ImageUser  = FunctionsImage.PushImage(ImageUser);

            if (user == null)
            {
                return;
            }

            CronosEntities entities = new CronosEntities();
            entities.Users.AddOrUpdate(user);
            entities.SaveChanges();
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <param name="currentUser">Пользователь</param>
        public static void DeleteUser(Users user)
        {
            CronosEntities entities = new CronosEntities();

            //Проверяем на связи в проектах
            var projects = entities.Project.Where(x => x.ResponsibleОfficer == user.ID_Users).ToList();
            foreach (var project in projects)
            {
                entities.Project.Remove(project);
            }

            //Проверяем на связи в задачах
            var tasks = entities.Task.Where(x => x.UserDoTask == user.ID_Users || x.UserCreateTask == user.ID_Users).ToList();
            foreach (var task in tasks)
            {
                var tasksTimer1 = entities.TaskTimer.Where(x => x.Task == task.ID_Task).ToList();
                foreach(var taskTimer in tasksTimer1)
                {
                    entities.TaskTimer.Remove(taskTimer);
                }
                entities.Task.Remove(task);
            }

            //Проверяем на связи в отметках по задачам
            var tasksTimer = entities.TaskTimer.Where(x => x.Users == user.ID_Users).ToList();
            foreach (var taskTimer in tasksTimer)
            {
                entities.TaskTimer.Remove(taskTimer);
            }

            //Проверяем на связи в отметках по рабочему времени
            var dateTimers = entities.DateTimer.Where(x => x.Users == user.ID_Users).ToList();
            foreach (var date in dateTimers)
            {
                entities.DateTimer.Remove(date);
            }

            entities.Users.Remove(entities.Users.Find(user.ID_Users));
            entities.SaveChanges();
        }
    }
}