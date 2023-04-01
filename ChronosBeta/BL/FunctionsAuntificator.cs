using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronosBeta.BL
{
    class FunctionsAuntificator
    {
        public static DB.Users Auntification(string login, string password)
        {
            try
            {
                DB.CronosEntities entities = new DB.CronosEntities();
                DB.Users user = new DB.Users();
                user = entities.Users.Single(x => x.Login == login && x.Password == password);
                if (user != null)
                {
                    FunctionsCurrentUser.SetUser(user);
                    return user;
                } 
                else
                    throw new Exception();
            }
            catch(Exception)
            {
                throw new Exception("Проблемы с аунтификацией");
            }
        }
    }
}