using ChronosBeta.Model;
using ChronosBeta.BL;
using FontAwesome.Sharp;
using System.Windows.Input;
using System.Windows.Controls;

namespace ChronosBeta.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        #region Параметры
        private ViewCurrentUser _currentUser; //Текущий пользователь
        private ViewModelBase _currentChildView; //Дочернее окно
        private string _caption; //Заголовок окна
        private IconChar _icon; //Иконка окна
        #endregion

        #region Свойства
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
        public Image ImageUser { get; set; } //Фото пользователя
        #endregion

        #region Команды
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowUserViewCommand { get; }
        public ICommand ShowTaskViewCommand { get; }
        public ICommand ShowTaskTimerViewCommand { get; }
        public ICommand ShowProjectViewCommand { get; }
        public ICommand ShowDateTimerCommand { get; }
        public ICommand ShowSettingViewCommand { get; }
        #endregion

        #region Конструктор
        public MainViewModel() 
        {
            CurrentUser = new ViewCurrentUser();
            
            ShowHomeViewCommand = new ViewModelCommand(ExecutedShowHomeCommand);
            ShowUserViewCommand = new ViewModelCommand(ExecutedShowUsersCommand);
            ShowTaskViewCommand = new ViewModelCommand(ExecutedShowTaskCommand);
            ShowTaskTimerViewCommand = new ViewModelCommand(ExecutedShowTaskTimerCommand);
            ShowProjectViewCommand = new ViewModelCommand(ExecutedShowProjectCommand);
            ShowDateTimerCommand = new ViewModelCommand(ExecutedShowDateTimerCommandCommand);
            ShowSettingViewCommand = new ViewModelCommand(ExecutedShowSettingCommandCommand);

            ExecutedShowHomeCommand(null);
            LoadCurrentUserData();
        }
        #endregion

        #region Методы
        /// <summary>
        /// Открыть окно "Настройки"
        /// </summary>
        /// <param name="obj"></param>
        private void ExecutedShowSettingCommandCommand(object obj)
        {
            CurrentChildView = new SettingViewModel(this);
            Caption = "Настройки";
            Icon = IconChar.Gears;
        }
        
        /// <summary>
        /// Открыть окно "Отметки по задачам"
        /// </summary>
        /// <param name="obj"></param>
        private void ExecutedShowTaskTimerCommand(object obj)
        {
            CurrentChildView = new TaskTimerViewModel(this);
            Caption = "Отметки по задачам";
            Icon = IconChar.ThumbTack;
        }

        /// <summary>
        /// Открыть окно "Рабочий стол"
        /// </summary>
        /// <param name="obj"></param>
        private void ExecutedShowHomeCommand(object obj)
        {
            CurrentChildView = new HomeViewModel(this);
            Caption = "Рабочий стол";
            Icon = IconChar.Home;
        }

        /// <summary>
        /// Открыть окно "Задачи"
        /// </summary>
        /// <param name="obj"></param>
        private void ExecutedShowTaskCommand(object obj)
        {
            CurrentChildView = new TaskViewModel(this);
            Caption = "Зaдачи";
            Icon = IconChar.ListCheck;
        }

        /// <summary>
        /// Открыть окно "Рабочее время"
        /// </summary>
        /// <param name="obj"></param>
        private void ExecutedShowDateTimerCommandCommand(object obj)
        {
            CurrentChildView = new DateTimerViewModel(this);
            Caption = "Рабочее время";
            Icon = IconChar.Clock;
        }

        /// <summary>
        /// Открыть окно "Пользователи"
        /// </summary>
        /// <param name="obj"></param>
        private void ExecutedShowUsersCommand(object obj)
        {
            CurrentChildView = new UsersViewModel(this);
            Caption = "Пользователи";
            Icon = IconChar.UserGroup;
        }

        /// <summary>
        /// Открыть окно "Проекты"
        /// </summary>
        /// <param name="obj"></param>
        private void ExecutedShowProjectCommand(object obj)
        {
            CurrentChildView = new ProjectsViewMode(this);
            Caption = "Проекты";
            Icon = IconChar.Book;
        }

        /// <summary>
        /// Загрузить текущего пользователя
        /// </summary>
        private void LoadCurrentUserData()
        {
            CurrentUser = FunctionsCurrentUser.GetViewUser();
            if (CurrentUser.Username == null)
                CurrentUser.DisplayName = "Неопознаный пользователь";
        }
        #endregion
    }
}