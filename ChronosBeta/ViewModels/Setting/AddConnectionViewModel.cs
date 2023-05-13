using ChronosBeta.BL;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChronosBeta.ViewModels
{
    public class AddConnectionViewModel: ViewModelBase
    {
        private static MainViewModel _currentMain;

        public string NameConnection { get; set; }
        public string AddresServer { get; set; }
        public string NameDB { get; set; }
        public string NameUser { get; set; }
        public string PasswordUser { get; set; }

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
        }

        public AddConnectionViewModel(MainViewModel main)
        {
            _currentMain = main;
        }

        private void ExecutedBackCommand(object obj)
        {
            _currentMain.CurrentChildView = new SettingViewModel();
            _currentMain.Caption = "Настройки";
            _currentMain.Icon = IconChar.Gears;
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
    }
}
