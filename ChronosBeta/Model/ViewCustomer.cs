using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronosBeta.Model
{
    public class ViewCustomer
    {
        public DB.Customers Customer { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }

        public ViewCustomer(DB.Customers customer)
        {
            Customer = customer;
            Id = customer.Id_Customers;
            Name = customer.Name;
            Surname = customer.Surname;
            MiddleName = customer.MiddleName;
            Phone = customer.Phone;
        }
    }
}