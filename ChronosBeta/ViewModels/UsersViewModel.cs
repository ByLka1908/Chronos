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
using System.Windows.Forms;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace ChronosBeta.ViewModels
{
    public class UsersViewModel : ViewModelBase
    {
        public ICollectionView CurrentUserList { get; private set; }
        public ICommand AddUser { get; }
        public ICommand EditUser { get; }
        public ViewUsers SelectedUser { get; set; }

        private static MainViewModel _currentMain;

        public UsersViewModel()
        {
            AddUser = new ViewModelCommand(ExecutedAddUserCommand);
            EditUser = new ViewModelCommand(ExecutedEditUserCommand);

            List<ViewUsers> currentUsers = FunctionsUsers.GetUsers();
            CurrentUserList = CollectionViewSource.GetDefaultView(currentUsers);
        }

        public UsersViewModel(MainViewModel main)
        {
            _currentMain = main;
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
                MessageBox.Show("Пользователь не выбран");
                return;
            }
            _currentMain.CurrentChildView = new UserObjViewModel(_currentMain, SelectedUser);
            _currentMain.Caption = "Редактирование пользователя";
            _currentMain.Icon = IconChar.UserEdit;
        }
    }
}
