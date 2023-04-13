using ChronosBeta.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
