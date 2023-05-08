using ChronosBeta.BL;
using ChronosBeta.Model;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ChronosBeta.ViewModels
{
    public class CustomerViewModel: ViewModelBase
    {
        private static MainViewModel _currentMain;
        private ICollectionView _currentCustomer;

        public ICommand AddCustomer { get; }
        public ICommand EditCustomer { get; }
        public ICommand Search { get; }
        public ICommand RemoveCustomer { get; }
        public ICommand Back { get; }

        public ICollectionView CurrentCustomer
        {
            get { return _currentCustomer; }
            set
            {
                _currentCustomer = value;
                OnPropertyChanged(nameof(CurrentCustomer));
            }
        }
        public string CurrentText { get; set; }
        public ViewCustomer SelectedCustomer { get; set; }

        public CustomerViewModel() 
        {
            AddCustomer = new ViewModelCommand(ExecutedAddCustomerCommand);
            EditCustomer = new ViewModelCommand(ExecutedEditCustomerCommand);
            Search = new ViewModelCommand(ExecutedSearchCommand);
            RemoveCustomer = new ViewModelCommand(ExecutedRemoveCustomerCommand);
            Back = new ViewModelCommand(ExecutedBackCommand);

            UpdateView();
        }

        public CustomerViewModel(MainViewModel main)
        {
            _currentMain = main;
        }

        public void UpdateView()
        {
            List<ViewCustomer> currentCustomer = FunctionsCustomer.GetCustomers();
            CurrentCustomer = CollectionViewSource.GetDefaultView(currentCustomer);
        }

        private void ExecutedSearchCommand(object obj)
        {
            if (CurrentText == null)
            {
                return;
            }

            if (CurrentText == string.Empty)
            {
                UpdateView();
                return;
            }

            List<ViewCustomer> currentCustomer = FunctionsCustomer.GetCustomers();
            List<ViewCustomer> findCustomer = currentCustomer.Where(x => x.Surname.ToUpper().StartsWith(CurrentText.ToUpper())).ToList();

            if (findCustomer.Count < 1)
            {
                MessageBox.Show("Обьект не найден");
                CurrentText = string.Empty;
                UpdateView();
                return;
            }
            CurrentCustomer = CollectionViewSource.GetDefaultView(findCustomer);
        }

        private void ExecutedAddCustomerCommand(object obj)
        {
            _currentMain.CurrentChildView = new CustomerObjViewModel(_currentMain);
            _currentMain.Caption = "Добавление заказчика";
            _currentMain.Icon = IconChar.AddressBook;
        }

        private void ExecutedEditCustomerCommand(object obj)
        {
            if (SelectedCustomer == null)
            {
                MessageBox.Show("Заказчик не выбрана");
                return;
            }
            _currentMain.CurrentChildView = new CustomerObjViewModel(_currentMain, SelectedCustomer);
            _currentMain.Caption = "Редактирование заказчика";
            _currentMain.Icon = IconChar.AddressBook;
        }

        private void ExecutedRemoveCustomerCommand(object obj)
        {
            FunctionsCustomer.DeleteCustomer(SelectedCustomer.Customer);
            UpdateView();
        }

        private void ExecutedBackCommand(object obj)
        {
            _currentMain.CurrentChildView = new SettingViewModel();
            _currentMain.Caption = "Настройки";
            _currentMain.Icon = IconChar.Gears;
        }
    }
}