using ChronosBeta.DB;
using ChronosBeta.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;

namespace ChronosBeta.BL
{
    public class FunctionsCustomer
    {
        public static List<ViewCustomer> GetCustomers()
        {
            CronosEntities entities = new CronosEntities();
            var costomer = entities.Customers.ToList();
            List<ViewCustomer> view = new List<ViewCustomer>();
            foreach (var item in costomer)
                view.Add(new ViewCustomer(item));
            return view;
        }

        public static List<string> GetViewCustomer()
        {
            CronosEntities entities = new CronosEntities();
            var users = entities.Customers.Select(x => x.Name + " " + x.Surname + " " + x.MiddleName).ToList();
            return users;
        }

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

        public static void SaveEditCustomer(string Name,  string Surname, string MiddleName, 
                                            string Phone, string Email,   Customers customer)
        {
            customer.Name = Name;
            customer.Surname = Surname;
            customer.MiddleName = MiddleName;
            customer.Phone = Phone;
            customer.Email = Email;

            if (customer == null)
            {
                FunctionsWindow.OpenErrorWindow("Пользователь не заполнен");
                return;
            }

            CronosEntities entities = new CronosEntities();
            entities.Customers.AddOrUpdate(customer);
            entities.SaveChanges();
        }

        public static void DeleteCustomer(Customers currentCustomer)
        {
            CronosEntities entities = new CronosEntities();
            entities.Customers.Remove(entities.Customers.Find(currentCustomer.Id_Customers));
            entities.SaveChanges();
        }
    }
}