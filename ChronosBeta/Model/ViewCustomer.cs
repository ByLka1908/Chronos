using ChronosBeta.DB;

namespace ChronosBeta.Model
{
    public class ViewCustomer
    {
        public Customers Customer { get; set; } //Заказчик
        public int Id { get; set; }
        public string Name { get; set; } //Имя
        public string Surname { get; set; } //Фамилия
        public string MiddleName { get; set; } //Отчество
        public string Phone { get; set; } //Телефон

        /// <summary>
        /// Инициализация представления заказчика
        /// </summary>
        /// <param name="customer">Закачик</param>
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