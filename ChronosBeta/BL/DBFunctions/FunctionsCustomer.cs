using ChronosBeta.DB;
using ChronosBeta.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace ChronosBeta.BL
{
    public class FunctionsCustomer
    {
        /// <summary>
        /// Получить представление заказчика
        /// </summary>
        /// <param name="customer">ФИО заказчика</param>
        /// <returns></returns>
        public static ViewCustomer GetCustomerView(string customer)
        {
            string[] FIO      = customer.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string Name       = FIO[0];
            string Surname    = FIO[1];
            string MiddleName = FIO[2];

            CronosEntities entities = new CronosEntities();
            Customers currentCustomer = entities.Customers.Where(x => x.Name == Name && x.Surname == Surname && x.MiddleName == MiddleName).First();
            return new ViewCustomer(currentCustomer);
        }

        /// <summary>
        /// Получить список заказчиков
        /// </summary>
        /// <returns></returns>
        public static List<ViewCustomer> GetCustomers()
        {
            CronosEntities entities = new CronosEntities();
            var customers = entities.Customers.ToList();
            List<ViewCustomer> viewCustomers = new List<ViewCustomer>();
            foreach (var currentCustomer in customers)
                viewCustomers.Add(new ViewCustomer(currentCustomer));
            return viewCustomers;
        }

        /// <summary>
        /// Получить список заказчиков
        /// </summary>
        /// <returns></returns>
        public static List<string> GetListCustomers()
        {
            CronosEntities entities = new CronosEntities();
            var customers = entities.Customers.Select(x => x.Name + " " + x.Surname + " " + x.MiddleName).ToList();
            return customers;
        }

        /// <summary>
        /// Получить id заказчика
        /// </summary>
        /// <param name="customer">ФИО заказчика</param>
        /// <returns></returns>
        public static int GetCustomerId(string customer)
        {
            string[] FIO      = customer.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string Name       = FIO[0];
            string Surname    = FIO[1];
            string MiddleName = FIO[2];

            CronosEntities entities = new CronosEntities();
            return entities.Customers.Where(x => x.Name == Name && x.Surname == Surname
                                            && x.MiddleName == MiddleName).First().Id_Customers;
        }

        /// <summary>
        /// Добавить заказчика
        /// </summary>
        /// <param name="Name">Имя</param>
        /// <param name="Surname">Фамилия</param>
        /// <param name="MiddleName">Отчество</param>
        /// <param name="Phone">Телефон</param>
        /// <param name="Email">Электронная почта</param>
        public static void AddCustomer(string Name,       string Surname,
                                       string MiddleName, string Phone, string Email)
        {
            Customers customer = new Customers();

            customer.Name = Name;
            customer.Surname = Surname;
            customer.MiddleName = MiddleName;
            customer.Phone = Phone;
            customer.Email = Email;

            if (customer == null)
            {
                return;
            }

            CronosEntities entities = new CronosEntities();
            entities.Customers.Add(customer);
            entities.SaveChanges();
        }

        /// <summary>
        /// Изменить заказчика
        /// </summary>
        /// <param name="Name">Имя</param>
        /// <param name="Surname">Фамилия</param>
        /// <param name="MiddleName">Отчество</param>
        /// <param name="Phone">Телефон</param>
        /// <param name="Email">Электронная почта</param>
        /// <param name="customer">Заказчик</param>
        public static void EditCustomer(string Name,  string Surname, string MiddleName, 
                                            string Phone, string Email,   Customers customer)
        {
            customer.Name = Name;
            customer.Surname = Surname;
            customer.MiddleName = MiddleName;
            customer.Phone = Phone;
            customer.Email = Email;

            if (customer == null)
            {
                FunctionsWindow.OpenErrorWindow("Заказчик не заполнен");
                return;
            }

            CronosEntities entities = new CronosEntities();
            entities.Customers.AddOrUpdate(customer);
            entities.SaveChanges();
        }

        /// <summary>
        /// Удалить заказчика
        /// </summary>
        /// <param name="currentCustomer">Заказчик</param>
        public static void DeleteCustomer(Customers customer)
        {
            CronosEntities entities = new CronosEntities();

            //Проверка на связи в проектах
            var projects = entities.Project.Where(x => x.ResponsibleСustomer == customer.Id_Customers).ToList();
            foreach (var project in projects)
            {
                entities.Project.Remove(project);
            }

            entities.Customers.Remove(entities.Customers.Find(customer.Id_Customers));
            entities.SaveChanges();
        }
    }
}