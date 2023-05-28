using ChronosBeta.BL;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Timer = System.Timers.Timer;
using ChronosBeta.BL.InternalFunctions;

namespace ChronosBeta.ViewModels
{
    public class ConnectionFastChangeViewModel : ViewModelBase
    {
        private bool? _dialogResult;

        private Timer myTimer;
        private string _currentSelectedNameConnected;

        private string _adresServer;
        private string _nameDB;
        private string _nameUser;
        private string _passwordUser;

        private bool _checkedFalse;
        private bool _checkedTrue;

        public bool? DialogResult
        {
            get { return _dialogResult; }
            protected set
            {
                _dialogResult = value;
                OnPropertyChanged("DialogResult");
            }
        }

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

        public ICommand TryConnection { get; }
        public ICommand OK { get; }
        public ICommand Cansel { get; }
        public ICommand Close { get; }

        public ConnectionFastChangeViewModel() 
        {
            NameConnected = FunctionsConnection.GetConnectList();

            OK = new ViewModelCommand(ExecutedOKCommand);
            Cansel = new ViewModelCommand(ExecutedCanselCommand);
            Close = new ViewModelCommand(ExecutedCloseCommand);
            TryConnection = new ViewModelCommand(ExecutedTryConnectionCommand);

            SetConnection(FunctionsConnection.CurrentConnect);

            myTimer = new Timer(500); //0.5 сек
            myTimer.Elapsed += UpdateConnection;
            myTimer.Enabled = true;
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

        private async void UpdateConnection(Object source, ElapsedEventArgs e)
        {

            if (SelectedNameConnected == _currentSelectedNameConnected)
                return;

            SetConnection(FunctionsConnection.GetConnect(SelectedNameConnected));
            await Task.Delay(400);
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

        private void ExecutedOKCommand(object obj)
        {
            if (AddresServer == null || AddresServer == "")
            {
                FunctionsWindow.OpenConfrumWindow("Укажите адрес подключения!");
                return;
            }
            if (NameDB == null || NameDB == "")
            {
                FunctionsWindow.OpenConfrumWindow("Укажите имя базы данных!");
                return;
            }
            if (CheckedFalse)
            {
                if (NameUser == null || NameUser == "")
                {
                    FunctionsWindow.OpenConfrumWindow("Укажите имя пользователя базы даных!");
                    return;
                }
                if (PasswordUser == null || PasswordUser == "")
                {
                    FunctionsWindow.OpenConfrumWindow("Укажите пароль пользователя базы даных!");
                    return;
                }
            }

            try
            {
                FunctionsConnection.SaveConnection(SelectedNameConnected, AddresServer, NameDB, PasswordUser, NameUser, _checkedTrue);
                DialogResult = true;
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Настройки не сохранены");
            }
        }

        private void ExecutedCanselCommand(object obj)
        {
            DialogResult = false;
        }

        private void ExecutedCloseCommand(object obj)
        {
            DialogResult = false;
        }
    }
}
