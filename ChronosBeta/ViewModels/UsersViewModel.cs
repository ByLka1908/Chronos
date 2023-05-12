using ChronosBeta.BL;
using ChronosBeta.Model;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace ChronosBeta.ViewModels
{
    public class UsersViewModel : ViewModelBase
    {
        private ICollectionView _currentUserList;
        private static MainViewModel _currentMain;

        public ICollectionView CurrentUserList
        {
            get { return _currentUserList; }
            set
            {
                _currentUserList = value;
                OnPropertyChanged(nameof(CurrentUserList));
            }
        }

        public ICommand AddUser { get; }
        public ICommand EditUser { get; }
        public ICommand RemoveUser { get; }
        public ICommand Search { get; }
        public ViewUsers SelectedUser { get; set; }
        public string CurrentText { get; set; }

        public void UpdateView()
        {
            try
            {
                List<ViewUsers> currentUsers = FunctionsUsers.GetUsers();
                CurrentUserList = CollectionViewSource.GetDefaultView(currentUsers);
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка обновления таблицы");
            }
        }

        public UsersViewModel()
        {
            AddUser = new ViewModelCommand(ExecutedAddUserCommand);
            EditUser = new ViewModelCommand(ExecutedEditUserCommand);
            RemoveUser = new ViewModelCommand(ExecutedRemoveUserCommand);
            Search = new ViewModelCommand(ExecutedSearchCommand);

            UpdateView();
        }

        public UsersViewModel(MainViewModel main)
        {
            _currentMain = main;
        }

        private void ExecutedSearchCommand(object obj)
        {    
            if(CurrentText == null)
            {
                FunctionsWindow.OpenConfrumWindow("Поле поиска пустое");
                return;
            }

            if (CurrentText == string.Empty)
            {
                UpdateView();
                return;
            }

            try
            {
                List<ViewUsers> currentUsers = FunctionsUsers.GetUsers();
                List<ViewUsers> findUsers = currentUsers.Where(x => x.Name.ToUpper().StartsWith(CurrentText.ToUpper())
                                            || x.Surname.ToUpper().StartsWith(CurrentText.ToUpper())).ToList();

                if (findUsers.Count < 1)
                {
                    MessageBox.Show("Обьект не найден");
                    CurrentText = string.Empty;
                    UpdateView();
                    return;
                }

                CurrentUserList = CollectionViewSource.GetDefaultView(findUsers);
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка поиска");
            }
        }

        private void ExecutedAddUserCommand(object obj)
        {
            _currentMain.CurrentChildView = new UserObjViewModel(_currentMain);
            _currentMain.Caption = "Добавление пользователя";
            _currentMain.Icon = IconChar.UserPlus;
        }

        private void ExecutedEditUserCommand(object obj)
        {
            if (SelectedUser == null)
            {
                FunctionsWindow.OpenConfrumWindow("Пользователь не выбран");
                return;
            }
            _currentMain.CurrentChildView = new UserObjViewModel(_currentMain, SelectedUser);
            _currentMain.Caption = "Редактирование пользователя";
            _currentMain.Icon = IconChar.UserEdit;
        }
        private void ExecutedRemoveUserCommand(object obj)
        {
            if (SelectedUser.User == null)
            {
                FunctionsWindow.OpenConfrumWindow("Пользователь не выбран(а)");
                return;
            }

            if (!FunctionsWindow.OpenDialogWindow("Вы действиельно хотите удалить пользователя?"))
                return;

            try
            {
                FunctionsUsers.DeleteUser(SelectedUser.User);
                UpdateView();
                FunctionsWindow.OpenGoodWindow("Пользователь удален");
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка удаления пользователя!");
            }
        }
    }
}