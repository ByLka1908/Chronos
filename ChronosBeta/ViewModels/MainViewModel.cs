using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChronosBeta.Model;
using ChronosBeta.BL;
using ChronosBeta.InterfaceBL;
using FontAwesome.Sharp;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using ChronosBeta.View;

namespace ChronosBeta.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewCurrentUser _currentUser;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

        // Parametrs
        public ViewCurrentUser CurrentUser
        {
            get { return _currentUser; }

            set
            {   
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        public ViewModelBase CurrentChildView
        {
            get { return _currentChildView; }

            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        public string Caption
        {
            get { return _caption; }

            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public IconChar Icon
        {
            get { return _icon; }

            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        // Commands
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowListApplicationViewCommand { get; }
        public ICommand ShowUserViewCommand { get; }
        public ICommand ShowTaskViewCommand { get; }
        public ICommand ShowProjectViewCommand { get; }

        public MainViewModel() 
        {
            CurrentUser = new ViewCurrentUser();

            //Initialize commands
            ShowHomeViewCommand = new ViewModelCommand(ExecutedShowHomeCommand);
            ShowListApplicationViewCommand = new ViewModelCommand(ExecutedShowListApplicationViewCommand);
            ShowUserViewCommand = new ViewModelCommand(ExecutedShowUsersCommand);
            ShowTaskViewCommand = new ViewModelCommand(ExecutedShowTaskCommand);
            ShowProjectViewCommand = new ViewModelCommand(ExecutedShowProjectCommand);

            //Defoult view
            ExecutedShowHomeCommand(null);

            LoadCurrentUserData();
        }

        private void ExecutedShowListApplicationViewCommand(object obj)
        {
            CurrentChildView = new ListApplicationViewModel();
            Caption = "Список приложений";
            Icon = IconChar.Desktop;
        }

        private void ExecutedShowHomeCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Рабочий стол";
            Icon = IconChar.Home;
        }

        private void ExecutedShowTaskCommand(object obj)
        {
            CurrentChildView = new TaskViewModel();
            Caption = "Зaдачи";
            Icon = IconChar.Home;
        }

        private void ExecutedShowUsersCommand(object obj)
        {
            CurrentChildView = new UsersViewModel(this);
            Caption = "Пользователи";
            Icon = IconChar.Users;
        }
        private void ExecutedShowProjectCommand(object obj)
        {
            CurrentChildView = new ProjectsViewMode();
            Caption = "Проекты";
            Icon = IconChar.Users;
        }

        private void LoadCurrentUserData()
        {
            CurrentUser = FunctionsCurrentUser.GetViewUser();
            if (CurrentUser.Username == null)
                CurrentUser.DisplayName = "Invalid user, not logged in";
        }

    }
}
