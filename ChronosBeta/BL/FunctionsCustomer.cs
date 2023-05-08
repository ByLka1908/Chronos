using ChronosBeta.DB;
using ChronosBeta.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChronosBeta.BL
{
    public class FunctionsCustomer
    {
        public static List<ViewCustomer> GetCustomers()
        {
            try
            {
                CronosEntities entities = new CronosEntities();
                var costomer = entities.Customers.ToList();
                List<ViewCustomer> view = new List<ViewCustomer>();
                foreach (var item in costomer)
                    view.Add(new ViewCustomer(item));
                return view;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static List<string> GetViewCustomer()
        {
            try
            {
                CronosEntities entities = new CronosEntities();
                var users = entities.Customers.Select(x => x.Name + " " + x.Surname + " " + x.MiddleName).ToList();
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

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

        public static int GetCustomerId(string customer)
        {
            try
            {
                string[] FIO = customer.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string Name = FIO[0];
                string Surname = FIO[1];
                string MiddleName = FIO[2];

                CronosEntities entities = new CronosEntities();
                return entities.Customers.Where(x => x.Name == Name && x.Surname == Surname 
                                                && x.MiddleName == MiddleName).First().Id_Customers;
            }
            catch
            {
                throw new Exception("Ошибка при получении пользователя");
            }
        }

        public static bool AddCustomer(string Name, string Surname,
                                       string MiddleName, string Phone, string Email)
        {
            Customers customer = new Customers();
            try
            {
                customer.Name = Name;
                customer.Surname = Surname;
                customer.MiddleName = MiddleName;
                customer.Phone = Phone;
                customer.Email = Email;
            }
            catch
            {
                throw new Exception("Ошибка иницилизации добавления");
            }
            if (customer == null)
            {
                return false;
            }
            try
            {
                CronosEntities entities = new CronosEntities();
                entities.Customers.Add(customer);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                throw new Exception("Ошибка добавлении пользователя");
            }
        }

        public static bool SaveEditCustomer(string Name, string Surname, string MiddleName, 
                                        string Phone, string Email, Customers customer)
        {
            try
            {
                customer.Name = Name;
                customer.Surname = Surname;
                customer.MiddleName = MiddleName;
                customer.Phone = Phone;
                customer.Email = Email;
            }
            catch
            {
                throw new Exception("Ошибка иницилизации добавления");
            }
            if (customer == null)
            {
                return false;
            }
            try
            {
                CronosEntities entities = new CronosEntities();
                entities.Customers.AddOrUpdate(customer);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                throw new Exception("Ошибка добавлении пользователя");
            }
        }

        public static void DeleteCustomer(Customers currentCustomer)
        {
            if (currentCustomer == null)
            {
                MessageBox.Show("Отметка не выбрана");
                return;
            }
            try
            {
                DB.CronosEntities entities = new CronosEntities();
                entities.Customers.Remove(entities.Customers.Find(currentCustomer.Id_Customers));
                entities.SaveChanges();
            }
            catch
            {
                throw new Exception("Ошибка удаления отметки по задаче");
            }
        }
    }
}