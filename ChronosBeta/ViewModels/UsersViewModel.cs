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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace ChronosBeta.ViewModels
{
    public class UsersViewModel : ViewModelBase
    {
        public ICollectionView CurrentUserList { get; private set; }
        public ICommand AddUser { get; }

        private static MainViewModel _currentMain;

        public UsersViewModel()
        {
            AddUser = new ViewModelCommand(ExecutedAddUserCommand);
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
    }
}
