using ChronosBeta.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronosBeta.BL
{
    public class FunctionsCustomer
    {
        public static int GetIdCustomer(string customer)
        {
            try
            {
                CronosEntities entities = new CronosEntities();
                return entities.Customers.Where(x => x.Surname == customer).First().Id_Customers;
            }
            catch
            {
                throw new Exception("Ошибка при получении отвественного");
            }
        }
    }
}
