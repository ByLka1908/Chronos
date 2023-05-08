﻿using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChronosBeta.ViewModels
{
    public class SettingViewModel : ViewModelBase
    {
        private static MainViewModel _currentMain;
        public ICommand ViewCustomers { get; }
        public ICommand ListApplication { get; }

        public SettingViewModel() 
        {
            ViewCustomers = new ViewModelCommand(ExecutedViewCustomersCommand);
            ListApplication = new ViewModelCommand(ExecutedListApplicationCommand);
        }
        public SettingViewModel(MainViewModel main)
        {
            _currentMain = main;
        }

        private void ExecutedViewCustomersCommand(object obj)
        {
            _currentMain.CurrentChildView = new CustomerViewModel(_currentMain);
            _currentMain.Caption = "Заказчики";
            _currentMain.Icon = IconChar.AddressBook;
        }

        private void ExecutedListApplicationCommand(object obj)
        {
            _currentMain.CurrentChildView = new ListApplicationViewModel(_currentMain);
            _currentMain.Caption = "Список программ";
            _currentMain.Icon = IconChar.Desktop;
        }
    }
}