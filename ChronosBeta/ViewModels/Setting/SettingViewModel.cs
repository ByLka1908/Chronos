using ChronosBeta.BL;
using ChronosBeta.BL.InternalFunctions;
using ChronosBeta.View;
using ChronosBeta.Views;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

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

        public string TimeScreenshot { get; set; }
        public string TimeUpdateListApp { get; set; }

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
        public ICommand ViewCustomers { get; }
        public ICommand ListApplication { get; }
        public ICommand TryConnection { get; }
        public ICommand ExitApp { get; }
        public ICommand OpenListConnection { get; }

        public SettingViewModel() 
        {
            NameConnected = FunctionsConnection.GetConnectList();
            TimeScreenshot = ConvertToStringSec(FunctionsImage.ScreenShotTiming);
            TimeUpdateListApp = ConvertToStringSec(FunctionsJSON.UpdateListAppTimer);


            Save = new ViewModelCommand(ExecutedSaveCommand);
            ViewCustomers = new ViewModelCommand(ExecutedViewCustomersCommand);
            ListApplication = new ViewModelCommand(ExecutedListApplicationCommand);
            TryConnection = new ViewModelCommand(ExecutedTryConnectionCommand);
            ExitApp = new ViewModelCommand(ExecutedExitAppCommand);
            OpenListConnection = new ViewModelCommand(ExecutedOpenListConnectionCommand);

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

        private string ConvertToStringSec(int time)
        {
            int timers = time / 1000;
            return timers.ToString();
        }

        private int ConvertToIntSec(string time)
        {
            int timers = Convert.ToInt32(time);
            timers = timers * 1000;
            return timers;
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

        private void ExecutedExitAppCommand(object obj)
        {
            FunctionsSettingStart.setting.RememberUser = false;
            FunctionsSettingStart.setting.NameUser = "";
            FunctionsSettingStart.setting.PasswordUser = "";
            FunctionsSettingStart.Update();

            var loginView = new LoginView();
            loginView.Show();
            MainView main = FunctionsSettingStart.MainView;
            main.Close();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    var mainView = new MainView();
                    FunctionsSettingStart.MainView = mainView;
                    mainView.Show();
                    loginView.Close();
                }
            };
        }

        private void ExecutedViewCustomersCommand(object obj)
        {
            _currentMain.CurrentChildView = new CustomerViewModel(_currentMain);
            _currentMain.Caption = "Заказчики";
            _currentMain.Icon = IconChar.AddressBook;
        }

        private void ExecutedOpenListConnectionCommand(object obj)
        {
            _currentMain.CurrentChildView = new ConnectionsViewModel(_currentMain);
            _currentMain.Caption = "Список подключений";
            _currentMain.Icon = IconChar.Database;
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
                Convert.ToInt32(TimeScreenshot);
            }
            catch
            {
                FunctionsWindow.OpenConfrumWindow("Укажите время скриншота в правильном формате");
                return;
            }
            try
            {
                Convert.ToInt32(TimeUpdateListApp);
            }
            catch
            {
                FunctionsWindow.OpenConfrumWindow("Укажите время обновления списка приложений в правильном формате");
                return;
            }

            try
            {
                FunctionsConnection.SaveConnection(SelectedNameConnected, AddresServer, NameDB, PasswordUser, NameUser, _checkedTrue);

                FunctionsImage.ScreenShotTiming = ConvertToIntSec(TimeScreenshot);
                FunctionsJSON.UpdateListAppTimer = ConvertToIntSec(TimeUpdateListApp);

                FunctionsSettingStart.setting.ScreenShotTimer = ConvertToIntSec(TimeScreenshot);
                FunctionsSettingStart.setting.UpdateListAppTimer = ConvertToIntSec(TimeUpdateListApp);
                FunctionsSettingStart.Update();

                FunctionsWindow.OpenGoodWindow("Насторйки сохранены");
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Насторйки не сохранены");
            }
        }

    }
}