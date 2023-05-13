using ChronosBeta.BL;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace ChronosBeta.ViewModels
{
    public class SettingViewModel : ViewModelBase
    {
        private static MainViewModel _currentMain;

        private Timer myTimer;
        private string _currentSelectedNameConnected;

        private string _adresServer;
        private string _nameDB;
        private string _nameUser;
        private string _passwordUser;

        private bool _checkedFalse;
        private bool _checkedTrue;

        public List<string> NameConnected { get; set; }
        public string SelectedNameConnected { get; set; }

        public string AddresServer
        {
            get { return _adresServer; }
            set
            {
                _adresServer = value;
                OnPropertyChanged(nameof(AddresServer));
            }
        }
        public string NameDB
        {
            get { return _nameDB; }
            set
            {
                _nameDB = value;
                OnPropertyChanged(nameof(NameDB));
            }
        }
        public string NameUser
        {
            get { return _nameUser; }
            set
            {
                _nameUser = value;
                OnPropertyChanged(nameof(NameUser));
            }
        }
        public string PasswordUser
        {
            get { return _passwordUser; }
            set
            {
                _passwordUser = value;
                OnPropertyChanged(nameof(PasswordUser));
            }
        }

        public bool CheckedFalse
        {
            get { return _checkedFalse; }
            set
            {
                _checkedFalse = value;
                OnPropertyChanged(nameof(CheckedFalse));
            }
        }
        public bool CheckedTrue
        {
            get { return _checkedTrue; }
            set
            {
                _checkedTrue = value;
                OnPropertyChanged(nameof(CheckedTrue));
            }
        }


        public ICommand Save { get; }
        public ICommand AddConnect { get; }
        public ICommand ViewCustomers { get; }
        public ICommand ListApplication { get; }
        public ICommand TryConnection { get; }

        public SettingViewModel() 
        {
            NameConnected = FunctionsConnection.GetConnectList();

            Save = new ViewModelCommand(ExecutedSaveCommand);
            AddConnect = new ViewModelCommand(ExecutedAddConnectCommand);
            ViewCustomers = new ViewModelCommand(ExecutedViewCustomersCommand);
            ListApplication = new ViewModelCommand(ExecutedListApplicationCommand);
            TryConnection = new ViewModelCommand(ExecutedTryConnectionCommand);

            SetConnection(FunctionsConnection.CurrentConnect);

            myTimer = new Timer(500); //0.5 сек
            myTimer.Elapsed += UpdateConnection;
            myTimer.Enabled = true;
        }

        public SettingViewModel(MainViewModel main)
        {
            _currentMain = main;
        }

        private void SetConnection(ConnectionView connect)
        {
            AddresServer = connect.dataSource;
            NameDB = connect.initialCatalog;
            SelectedNameConnected = connect.ConnectName;
            _currentSelectedNameConnected = SelectedNameConnected;

            if (connect.integratedSecurity)
            {
                CheckedFalse = false;
                CheckedTrue = true;

                NameUser = "";
                PasswordUser = "";
            }
            else
            {
                CheckedFalse = true;
                CheckedTrue = false;

                NameUser = connect.UserId;
                PasswordUser = connect.Password;
            }
        }

        private async void UpdateConnection(Object source, ElapsedEventArgs e)
        {
            
            if (SelectedNameConnected == _currentSelectedNameConnected)
                return;

            SetConnection(FunctionsConnection.GetConnect(SelectedNameConnected));
            await Task.Delay(400);
        }

        private void ExecutedTryConnectionCommand(object obj)
        {
            try
            {
                bool resul = FunctionsConnection.TryConnection(AddresServer, NameDB, CheckedTrue, NameUser, PasswordUser);
                if (resul)
                    FunctionsWindow.OpenGoodWindow("Подключение успешно");
                else
                    FunctionsWindow.OpenErrorWindow("Подключение провалено");
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка подключения");
            }
        }

        private void ExecutedAddConnectCommand(object obj)
        {
            _currentMain.CurrentChildView = new AddConnectionViewModel(_currentMain);
            _currentMain.Caption = "Добавить подключение к серверу";
            _currentMain.Icon = IconChar.Database;
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

        private void ExecutedSaveCommand(object obj)
        {
            try
            {
                FunctionsConnection.SaveConnection(SelectedNameConnected, AddresServer, NameDB, PasswordUser, NameUser, _checkedTrue);

                FunctionsWindow.OpenGoodWindow("Насторйки сохранены");
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Насторйки не сохранены");
            }
        }
    }
}