using ChronosBeta.BL;
using ChronosBeta.Model;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace ChronosBeta.ViewModels
{
    public class ConnectionsViewModel: ViewModelBase
    {
        private static MainViewModel _currentMain;
        private ICollectionView _connectionList;

        public ICommand AddConnect { get; }
        public ICommand EditConnect { get; }
        public ICommand GoConnectEdit { get; }
        public ICommand Search { get; }
        public ICommand RemoveConnect { get; }
        public ICommand Back { get; }

        public ICollectionView ConnectionList
        {
            get { return _connectionList; }
            set
            {
                _connectionList = value;
                OnPropertyChanged(nameof(ConnectionList));
            }
        }
        public string CurrentText { get; set; }
        public ConnectionView SelectedConnection { get; set; }

        public ConnectionsViewModel() 
        {
            AddConnect = new ViewModelCommand(ExecutedAddConnectCommand);
            EditConnect = new ViewModelCommand(ExecutedEditConnectCommand);
            GoConnectEdit = new ViewModelCommand(ExecutedGoConnectEditCommand);
            Search = new ViewModelCommand(ExecutedSearchCommand);
            RemoveConnect = new ViewModelCommand(ExecutedRemoveConnectCommand);
            Back = new ViewModelCommand(ExecutedBackCommand);

            UpdateView();
        }

        public ConnectionsViewModel(MainViewModel main)
        {
            _currentMain = main;
        }

        public void UpdateView()
        {
            try
            {
                ConnectionList = CollectionViewSource.GetDefaultView(FunctionsConnection.ConnectionViews);
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка обновления таблицы");
            }
        }

        private void ExecutedSearchCommand(object obj)
        {
            try
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

                List<ConnectionView> findCustomer = FunctionsConnection.ConnectionViews.Where(x => x.ConnectName.ToUpper().StartsWith(CurrentText.ToUpper())).ToList();

                if (findCustomer.Count < 1)
                {
                    FunctionsWindow.OpenConfrumWindow("Обьект не найден");
                    CurrentText = string.Empty;
                    UpdateView();
                    return;
                }
                ConnectionList = CollectionViewSource.GetDefaultView(findCustomer);
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка поиска");
            }
        }

        private void ExecutedAddConnectCommand(object obj)
        {
            _currentMain.CurrentChildView = new AddConnectionViewModel(_currentMain);
            _currentMain.Caption = "Добавить подключение к серверу";
            _currentMain.Icon = IconChar.Database;
        }

        private void ExecutedEditConnectCommand(object obj)
        {
            if (SelectedConnection == null)
            {
                FunctionsWindow.OpenConfrumWindow("Подключение не выбрана");
                return;
            }
            _currentMain.CurrentChildView = new AddConnectionViewModel(_currentMain, SelectedConnection);
            _currentMain.Caption = "Редактирование подключение к серверу";
            _currentMain.Icon = IconChar.Database;
        }

        private void ExecutedGoConnectEditCommand(object obj)
        {
            _currentMain.CurrentChildView = new AddConnectionViewModel(_currentMain, (ConnectionView)obj);
            _currentMain.Caption = "Редактирование подключение к серверу";
            _currentMain.Icon = IconChar.Database;
        }

        private void ExecutedRemoveConnectCommand(object obj)
        {

            if (SelectedConnection.ConnectName == null)
            {
                FunctionsWindow.OpenConfrumWindow("Подключение не выбрана");
                return;
            }

            if (!FunctionsWindow.OpenDialogWindow("Вы дествительно хотите удалить подключение?"))
                return;

            if (FunctionsConnection.ConnectionViews.Count == 1)
            {
                FunctionsWindow.OpenConfrumWindow("Вы не можете удалить последнее поключение!");
                return;
            }

            try
            {
                FunctionsConnection.DeleteConnection(SelectedConnection);
                UpdateView();
                FunctionsWindow.OpenGoodWindow("Подключение удалено!");
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка удаление подключения");
            }
        }

        private void ExecutedBackCommand(object obj)
        {
            _currentMain.CurrentChildView = new SettingViewModel();
            _currentMain.Caption = "Настройки";
            _currentMain.Icon = IconChar.Gears;
        }
    }
}
