using ChronosBeta.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosBeta.DB;
using System.Data.Entity.Migrations;
using System.Windows;

namespace ChronosBeta.BL
{
    public class FunctionsUsers
    {

        public static List<ViewUsers> GetUsers()
        {
            try
            {
                DB.CronosEntities entities = new DB.CronosEntities();
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

        public static bool AddUser(string name, string surname, string login,
                                   string password, string phone, string skype, string jobTitle)
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
                                        string phone, string skype, string jobTitle, ViewUsers SelectedUser)
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

        public static int GetIdUser(string user)
        {
            try
            {
                CronosEntities entities = new CronosEntities();
                return entities.Users.Where(x => x.Name == user).First().ID_Users;
            }
            catch
            {
                throw new Exception("Ошибка при получении отвественного");
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
