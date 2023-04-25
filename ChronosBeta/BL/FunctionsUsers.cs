using ChronosBeta.InterfaceBL;
using ChronosBeta.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosBeta.DB;
using System.Data.Entity.Migrations;

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
                DB.CronosEntities entities = new DB.CronosEntities();
                entities.Users.Add(user);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                throw new Exception("Ошибка добавлении пользователя");
            }
        }

        public static void Set(string name, string surname, string login, string password, 
                               string phone, string skype, string jobTitle, ViewUsers SelectedUser)
        {
            name = SelectedUser.Name;
            surname = SelectedUser.Surname;
            login = SelectedUser.User.Login;
            password = SelectedUser.User.Password;
            phone = SelectedUser.Phone;
            skype = SelectedUser.Skype;
            jobTitle = SelectedUser.JobTitle;
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
    }
}
