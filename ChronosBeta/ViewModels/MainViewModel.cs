using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChronosBeta.Model;
using ChronosBeta.BL;
using FontAwesome.Sharp;
using System.Windows.Input;
using ChronosBeta.View;
using System.Windows.Media;
using System.Windows.Controls;

namespace ChronosBeta.ViewModels
{
    public class MainViewModel: ViewModelBase
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
        public Image ImageUser { get; set; }

        // Commands
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowUserViewCommand { get; }
        public ICommand ShowTaskViewCommand { get; }
        public ICommand ShowTaskTimerViewCommand { get; }
        public ICommand ShowProjectViewCommand { get; }
        public ICommand ShowDateTimerCommand { get; }
        public ICommand ShowSettingViewCommand { get; }

        public MainViewModel() 
        {
            CurrentUser = new ViewCurrentUser();

            //Initialize commands
            ShowHomeViewCommand = new ViewModelCommand(ExecutedShowHomeCommand);
            ShowUserViewCommand = new ViewModelCommand(ExecutedShowUsersCommand);
            ShowTaskViewCommand = new ViewModelCommand(ExecutedShowTaskCommand);
            ShowTaskTimerViewCommand = new ViewModelCommand(ExecutedShowTaskTimerCommand);
            ShowProjectViewCommand = new ViewModelCommand(ExecutedShowProjectCommand);
            ShowDateTimerCommand = new ViewModelCommand(ExecutedShowDateTimerCommandCommand);
            ShowSettingViewCommand = new ViewModelCommand(ExecutedShowSettingCommandCommand);

            //Defoult view
            ExecutedShowHomeCommand(null);
            LoadCurrentUserData();
        }

        private void ExecutedShowSettingCommandCommand(object obj)
        {
            CurrentChildView = new SettingViewModel(this);
            Caption = "Настройки";
            Icon = IconChar.Gears;
        }
        
        private void ExecutedShowTaskTimerCommand(object obj)
        {
            CurrentChildView = new TaskTimerViewModel(this);
            Caption = "Отметки по задачам";
            Icon = IconChar.ThumbTack;
        }

        private void ExecutedShowHomeCommand(object obj)
        {
            CurrentChildView = new HomeViewModel(this);
            Caption = "Рабочий стол";
            Icon = IconChar.Home;
        }

        private void ExecutedShowTaskCommand(object obj)
        {
            CurrentChildView = new TaskViewModel(this);
            Caption = "Зaдачи";
            Icon = IconChar.ListCheck;
        }

        private void ExecutedShowDateTimerCommandCommand(object obj)
        {
            CurrentChildView = new DateTimerViewModel(this);
            Caption = "Рабочее время";
            Icon = IconChar.Clock;
        }

        private void ExecutedShowUsersCommand(object obj)
        {
            CurrentChildView = new UsersViewModel(this);
            Caption = "Пользователи";
            Icon = IconChar.UserGroup;
        }

        private void ExecutedShowProjectCommand(object obj)
        {
            CurrentChildView = new ProjectsViewMode(this);
            Caption = "Проекты";
            Icon = IconChar.Book;
        }

        private void LoadCurrentUserData()
        {
            CurrentUser = FunctionsCurrentUser.GetViewUser();
            if (CurrentUser.Username == null)
                CurrentUser.DisplayName = "Неопознаный пользователь";
        }
    }
}