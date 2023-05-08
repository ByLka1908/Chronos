using System.Linq;
using System.Net;
using System.Windows;

namespace ChronosBeta.BL
{
    class FunctionsAuntificator
    {
        public static bool Auntification(NetworkCredential credential)
        {
            try
            {
                DB.CronosEntities entities = new DB.CronosEntities();
                DB.Users user = new DB.Users();
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
                MessageBox.Show("Ошибка аунтификации");
                return false;
            }
        }
    }
}