using System.Linq;
using System.Net;
using ChronosBeta.DB;
using System.Windows;
using System.Configuration;

namespace ChronosBeta.BL
{
    class FunctionsAuntificator
    {
        public static bool Auntification(NetworkCredential credential)
        {
            try
            {
                CronosEntities entities = new CronosEntities();
                Users user = new Users();
                user = entities.Users.Single(x => x.Login == credential.UserName && x.Password == credential.Password);

                if (user != null)
                {
                    FunctionsCurrentUser.SetUser(user);
                    return true;
                }
                else
                    return false; 
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка аунтефикации.\nНе найден пользователь");
                return false;
            }
        }
    }
}