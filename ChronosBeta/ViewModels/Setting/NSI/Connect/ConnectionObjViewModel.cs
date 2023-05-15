using ChronosBeta.BL;
using ChronosBeta.Model;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;

namespace ChronosBeta.ViewModels
{
    public class AddConnectionViewModel: ViewModelBase
    {
        private static MainViewModel _currentMain;
        private static ConnectionView SelectedConnect;
        private static bool itEdit;

        public string NameConnection { get; set; }
        public string AddresServer { get; set; }
        public string NameDB { get; set; }
        public string NameUser { get; set; }
        public string PasswordUser { get; set; }
        public bool isReadOnlyName { get; set; }

        public bool CheckedFalse { get; set; }
        public bool CheckedTrue { get; set; }

        public ICommand Save { get; }
        public ICommand Back { get; }
        public ICommand TryConnection { get; }

        public AddConnectionViewModel() 
        {
            Save = new ViewModelCommand(ExecutedSaveCommand);
            Back = new ViewModelCommand(ExecutedBackCommand);
            TryConnection = new ViewModelCommand(ExecutedTryConnectionCommand);

            CheckedFalse = true;
            if (itEdit)
            {
                SetConnect();
                isReadOnlyName = true;
            }
            isReadOnlyName = false;
                
        }

        public AddConnectionViewModel(MainViewModel main)
        {
            _currentMain = main;
            itEdit = false;
        }

        public AddConnectionViewModel(MainViewModel main, ConnectionView selectedConnect)
        {
            _currentMain = main;
            SelectedConnect = selectedConnect;
            itEdit = true;
        }

        private void SetConnect()
        {
            NameConnection = SelectedConnect.ConnectName;
            AddresServer = SelectedConnect.dataSource;
            NameDB = SelectedConnect.initialCatalog;
            NameUser = SelectedConnect.UserId;
            PasswordUser = SelectedConnect.Password;
        }

        private void ExecutedBackCommand(object obj)
        {
            _currentMain.CurrentChildView = new ConnectionsViewModel();
            _currentMain.Caption = "Список подключений";
            _currentMain.Icon = IconChar.Database;
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

        private void ExecutedSaveCommand(object obj)
        {
            if (NameConnection == null || NameConnection == "")
            {
                FunctionsWindow.OpenConfrumWindow("Укажите навазние подключения!");
                return;
            }
            if (AddresServer == null || AddresServer == "")
            {
                FunctionsWindow.OpenConfrumWindow("Укажите адрес сервера!");
                return;
            }
            if (NameDB == null || NameDB == "")
            {
                FunctionsWindow.OpenConfrumWindow("Укажите название базы данных!");
                return;
            }

            if (CheckedTrue)
            {
                if (NameUser == null || NameUser == "")
                {
                    FunctionsWindow.OpenConfrumWindow("Укажите имя пользователя!");
                    return;
                }
                if (PasswordUser == null || PasswordUser == "")
                {
                    FunctionsWindow.OpenConfrumWindow("Укажите пароль пользователя!");
                    return;
                }
            }

            if (!itEdit)
            {
                try
                {
                    FunctionsConnection.AddConnection(NameConnection, AddresServer, NameDB, PasswordUser, NameUser, CheckedTrue);

                    FunctionsWindow.OpenGoodWindow("Подключение добавленно");
                }
                catch
                {
                    FunctionsWindow.OpenErrorWindow("Подключение не добавленно");
                }
            }
            else
            {
                try
                {
                    FunctionsConnection.EditConnection(SelectedConnect);

                    FunctionsWindow.OpenGoodWindow("Подключение добавленно");
                }
                catch
                {
                    FunctionsWindow.OpenErrorWindow("Подключение не добавленно");
                }
            }
        }
    }
}