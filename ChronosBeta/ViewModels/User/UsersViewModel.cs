using ChronosBeta.BL;
using ChronosBeta.Model;
using FontAwesome.Sharp;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace ChronosBeta.ViewModels
{
    public class UsersViewModel : ViewModelBase
    {
        #region Параметры
        private ICollectionView _currentUserList;
        private static MainViewModel _currentMain;
        #endregion

        #region Свойства
        public ICollectionView CurrentUserList
        {
            get { return _currentUserList; }
            set
            {
                _currentUserList = value;
                OnPropertyChanged(nameof(CurrentUserList));
            }
        }
        public ViewUsers SelectedUser { get; set; }
        public string CurrentText { get; set; }
        #endregion

        #region Команды
        public ICommand AddUser { get; }
        public ICommand EditUser { get; }
        public ICommand GoUserEdit { get; }
        public ICommand RemoveUser { get; }
        public ICommand Search { get; }
        #endregion

        #region Конструктор
        public UsersViewModel()
        {
            AddUser = new ViewModelCommand(ExecutedAddUserCommand);
            EditUser = new ViewModelCommand(ExecutedEditUserCommand);
            GoUserEdit = new ViewModelCommand(ExecutedGoUserEditCommand);
            RemoveUser = new ViewModelCommand(ExecutedRemoveUserCommand);
            Search = new ViewModelCommand(ExecutedSearchCommand);

            UpdateView();
        }

        public UsersViewModel(MainViewModel main)
        {
            _currentMain = main;
        }
        #endregion

        #region Методы
        /// <summary>
        /// Обновить таблицу
        /// </summary>
        private void UpdateView()
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

        /// <summary>
        /// Поиск
        /// </summary>
        /// <param name="obj"></param>
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
                    FunctionsWindow.OpenConfrumWindow("Обьект не найден");
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        private void ExecutedAddUserCommand(object obj)
        {
            _currentMain.CurrentChildView = new UserObjViewModel(_currentMain, new UsersViewModel(), "Пользователи", IconChar.Users);
            _currentMain.Caption = "Добавление пользователя";
            _currentMain.Icon = IconChar.UserPlus;
        }

        /// <summary>
        /// Быстро перейти к редактированию пользователю
        /// </summary>
        /// <param name="obj"></param>
        private void ExecutedGoUserEditCommand(object obj)
        {
            _currentMain.CurrentChildView = new UserObjViewModel(_currentMain, (ViewUsers)obj, new UsersViewModel(), "Пользователи", IconChar.Users);
            _currentMain.Caption = "Редактирование пользователя";
            _currentMain.Icon = IconChar.UserEdit;
        }

        /// <summary>
        /// Редактирование пользователя
        /// </summary>
        /// <param name="obj"></param>
        private void ExecutedEditUserCommand(object obj)
        {
            if (SelectedUser == null)
            {
                FunctionsWindow.OpenConfrumWindow("Пользователь не выбран");
                return;
            }
            _currentMain.CurrentChildView = new UserObjViewModel(_currentMain, SelectedUser, new UsersViewModel(), "Пользователи", IconChar.Users);
            _currentMain.Caption = "Редактирование пользователя";
            _currentMain.Icon = IconChar.UserEdit;
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <param name="obj"></param>
        private void ExecutedRemoveUserCommand(object obj)
        {

            if (SelectedUser.User == null)
            {
                FunctionsWindow.OpenConfrumWindow("Пользователь не выбран(а)");
                return;
            }

            try
            {
                List<ViewUsers> currentUsers = FunctionsUsers.GetUsers();
                if (currentUsers.Count == 1)
                {
                    FunctionsWindow.OpenConfrumWindow("Вы не можете удалить послденего пользователя!");
                    return;
                }
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка получения текущий пользователей!");
                return;
            }

            if (!FunctionsWindow.OpenDialogWindow("Вы действиельно хотите удалить пользователя?\n" +
                                                  "Будут удалены связаные ПРОЕКТЫ, ЗАДАЧИ, ОТМЕТКИ ПО ЗАДАЧАМ, ОТМЕТКИ ПО РАБОЧЕМУ ВРЕМЕНИ!!!"))
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
        #endregion
    }
}