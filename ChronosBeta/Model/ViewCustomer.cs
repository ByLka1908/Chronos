using ChronosBeta.DB;

namespace ChronosBeta.Model
{
    public class ViewCustomer
    {
        public Customers Customer { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }

        public ViewCustomer(Customers customer)
        {
            Customer   = customer;
            Id         = customer.Id_Customers;
            Name       = customer.Name;
            Surname    = customer.Surname;
            MiddleName = customer.MiddleName;
            Phone      = customer.Phone;
        }
    }
}