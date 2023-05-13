using ChronosBeta.BL;
using ChronosBeta.Model;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChronosBeta.ViewModels
{
    public class CustomerObjViewModel : ViewModelBase
    {
        private static MainViewModel _currentMain;
        private static ViewCustomer SelectedCustomer;
        private static bool itEdit;

        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public ICommand Save { get; }
        public ICommand Back { get; }

        public CustomerObjViewModel() 
        {
            //Инициализация команд
            Save = new ViewModelCommand(ExecutedSaveCommand);
            Back = new ViewModelCommand(ExecutedBackCommand);

            if (itEdit)
                SetTask();
        }

        public CustomerObjViewModel(MainViewModel main)
        {
            _currentMain = main;
            itEdit = false;
        }

        public CustomerObjViewModel(MainViewModel main, ViewCustomer selectedCustomer)
        {
            _currentMain = main;
            SelectedCustomer = selectedCustomer;
            itEdit = true;
        }

        private void SetTask()
        {
            Name       = SelectedCustomer.Name;
            Surname    = SelectedCustomer.Surname;
            MiddleName = SelectedCustomer.MiddleName;
            Phone      = SelectedCustomer.Phone;
            Email      = SelectedCustomer.Customer.Email;
        }

        private void ExecutedSaveCommand(object obj)
        {
            if (!itEdit)
            {
                try
                {
                    FunctionsCustomer.AddCustomer(Name, Surname, MiddleName, Phone, Email);
                    FunctionsWindow.OpenGoodWindow("Заказчик добавлен(а)");
                }
                catch
                {
                    FunctionsWindow.OpenErrorWindow("Заказчик не добавлен(а)");
                }
            }
            else
            {
                try
                {
                    FunctionsCustomer.SaveEditCustomer(Name, Surname, MiddleName, Phone, Email, SelectedCustomer.Customer);
                    FunctionsWindow.OpenGoodWindow("Заказчик отредактирован(а)");
                }
                catch
                {
                    FunctionsWindow.OpenErrorWindow("Заказчик не отредактирован(а)");
                }
            }
        }

        private void ExecutedBackCommand(object obj)
        {
            _currentMain.CurrentChildView = new CustomerViewModel();
            _currentMain.Caption = "Заказчики";
            _currentMain.Icon = IconChar.AddressBook;
        }
    }
}