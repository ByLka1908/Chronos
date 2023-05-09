using ChronosBeta.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using ChronosBeta.DB;
using System.Data.Entity.Migrations;
using System.Windows;
using System.Windows.Media;

namespace ChronosBeta.BL
{
    public class FunctionsUsers
    {
        public static List<ViewUsers> GetUsers()
        {
            try
            {
                CronosEntities entities = new CronosEntities();
                var users = entities.Users.ToList();
                List<ViewUsers> listUser = new List<ViewUsers>();
                foreach (var user in users)
                    listUser.Add(new ViewUsers(user));
                return listUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static List<string> GetViewUser()
        {
            try
            {
                CronosEntities entities = new CronosEntities();
                var users = entities.Users.Select(x => x.Name + " " + x.Surname + " " + x.MiddleName).ToList();
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static bool AddUser(string name, string surname, string login,
                                   string password, string phone, string skype, string jobTitle, ImageSource imageUser)
        {
            Users user = new Users();
            try
            {
                user.Name = name;
                user.Surname = surname;
                user.Login = login;
                user.Password = password;
                user.Phone = phone;
                user.Skype = skype;
                user.ImageUser = FunctionsImage.PushImage(imageUser);
                user.JobTitle = FunctionsJobTitle.GetId(jobTitle);
            }
            catch
            {
                throw new Exception("Ошибка иницилизации добавления");
            }
            if (user == null)
            {
                return false;
            }
            try
            {
                CronosEntities entities = new CronosEntities();
                entities.Users.Add(user);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                throw new Exception("Ошибка добавлении пользователя");
            }
        }

        public static bool SaveEditUser(string name, string surname, string login, string password,
                                        string phone, string skype, string jobTitle, ViewUsers SelectedUser, ImageSource imageUser)
        {
            Users user = SelectedUser.User;
            try
            {
                user.Name = name;
                user.Surname = surname;
                user.Login = login;
                user.Password = password;
                user.Phone = phone;
                user.Skype = skype;
                user.ImageUser = FunctionsImage.PushImage(imageUser);
                user.JobTitle = FunctionsJobTitle.GetId(jobTitle);
            }
            catch
            {
                throw new Exception("Ошибка иницилизации редактирования");
            }
            if (user == null)
            {
                return false;
            }
            try
            {
                CronosEntities entities = new CronosEntities();
                entities.Users.AddOrUpdate(user);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                throw new Exception("Ошибка редактирования пользователя");
            }
        }

        public static int GetUserId(string user)
        {
            try
            {
                string[] FIO = user.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string Name = FIO[0];
                string Surname = FIO[1];
                string MiddleName = FIO[2];

                CronosEntities entities = new CronosEntities();
                return entities.Users.Where(x => x.Name == Name && x.Surname == Surname && x.MiddleName == MiddleName).First().ID_Users;
            }
            catch
            {
                throw new Exception("Ошибка при получении пользователя");
            }
        }

        public static void DeleteUser(Users currentUser)
        {
            if (currentUser == null)
            {
                MessageBox.Show("Отметка не выбрана");
                return;
            }
            try
            {
                CronosEntities entities = new CronosEntities();
                entities.Users.Remove(entities.Users.Find(currentUser.ID_Users));
                entities.SaveChanges();
            }
            catch
            {
                throw new Exception("Ошибка удаления отметки по задаче");
            }
        }
    }
}