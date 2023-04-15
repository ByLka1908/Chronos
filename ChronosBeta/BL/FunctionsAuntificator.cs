using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ChronosBeta.InterfaceBL;

namespace ChronosBeta.BL
{
    class FunctionsAuntificator : IFunctionsAuntificator
    {
        public bool Auntification(NetworkCredential credential)
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
    }
}