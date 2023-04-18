using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronosBeta.BL
{
    public class FunctionsJobTitle
    {
        public static List<string> GetJobTitle()
        {
            try
            {
                DB.CronosEntities entities = new DB.CronosEntities();
                var jobtitle = entities.JobTitles.Select(x => x.NameJobTitle).ToList();
                return jobtitle;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static bool Add(string name, string surname, string login,
                               string password, string phone, string skype, string jobTitle)
        {
            DB.Users user = new DB.Users();
            try
            {
                user.Name = name;
                user.Surname = surname;
                user.Login = login;
                user.Password = password;
                user.Phone = phone;
                user.Skype = skype;
                user.JobTitle = GetId(jobTitle);
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

        private static int GetId(string jobTitle)
        {
            try
            {
                DB.CronosEntities entities = new DB.CronosEntities();
                return entities.JobTitles.Where(x => x.NameJobTitle == jobTitle).First().ID_JobTitles;
            }
            catch
            {
                throw new Exception("Ошибка при получении должности");
            }
        }
    }
}
