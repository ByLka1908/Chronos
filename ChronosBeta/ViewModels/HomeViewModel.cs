using ChronosBeta.BL;
using ChronosBeta.Model;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace ChronosBeta.ViewModels
{
    public class HomeViewModel: ViewModelBase
    {
        #region Параметры
        private string _contentLabel; //Текст рабочего времени
        private SolidColorBrush _foregroundButton; //Цвет кнопки Вкл/Выкл трекера рабочего времени
        private static MainViewModel _currentMain; //Основное окно
        #endregion

        #region Свойства
        public ICollectionView OverdueTask { get; private set; } //Таблица прошедших задач
        public ICollectionView CurrentTask { get; private set; } //Таблица текущих задач
        public ICollectionView FutureTask { get; private set; } //Таблица будущих задач
        public string ContentLabel
        {
            get { return _contentLabel; }
            set
            {
                _contentLabel = value;
                OnPropertyChanged(nameof(ContentLabel));
            }
        }
        public SolidColorBrush ForegroundButton
        {
            get { return _foregroundButton; }
            set
            {
                _foregroundButton = value;
                OnPropertyChanged(nameof(ForegroundButton));
            }
        }
        #endregion

        #region Команды
        public ICommand OnOffTimer { get; } //Вкл/Выкл рабочего времени
        public ICommand GoTask { get; } //Перейти к задаче
        #endregion

        #region Конструкторы
        public HomeViewModel() 
        {
            try
            {
                UpdateButton();

                OnOffTimer = new ViewModelCommand(ExecutedOnOffTimerCommand);
                GoTask = new ViewModelCommand(ExecutedGoTaskCommand);


                DateTime currentTime = DateTime.Today;


                List<ViewTask> currentTasks = FunctionsTask.GetTasks();
                List<ViewTask> _overdueTask = currentTasks.Where(x => (x.Task.Users.ID_Users == FunctionsCurrentUser.User.ID_Users || x.Task.Users1.ID_Users == FunctionsCurrentUser.User.ID_Users)
                                                                      && x.Task.Deadline < currentTime).ToList();
                List<ViewTask> _currentTask = currentTasks.Where(x => (x.Task.Users.ID_Users == FunctionsCurrentUser.User.ID_Users || x.Task.Users1.ID_Users == FunctionsCurrentUser.User.ID_Users)
                                                                      && x.Task.Deadline >= currentTime
                                                                      && x.Task.Deadline <= currentTime.AddDays(3)).ToList();
                List<ViewTask> _futureTask = currentTasks.Where(x => (x.Task.Users.ID_Users == FunctionsCurrentUser.User.ID_Users || x.Task.Users1.ID_Users == FunctionsCurrentUser.User.ID_Users)
                                                                      && x.Task.Deadline >= currentTime.AddDays(4)).ToList();


                OverdueTask = CollectionViewSource.GetDefaultView(_overdueTask);
                CurrentTask = CollectionViewSource.GetDefaultView(_currentTask);
                FutureTask = CollectionViewSource.GetDefaultView(_futureTask);
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка инициализации окна");
            }
        }

        /// <summary>
        /// Инициализация окна
        /// </summary>
        /// <param name="main">Основное окно</param>
        public HomeViewModel(MainViewModel main)
        {
            _currentMain = main;
        }
        #endregion

        #region Методы
        /// <summary>
        /// Вкл/Выкл рабочего времени
        /// </summary>
        /// <param name="obj"></param>
        private void ExecutedOnOffTimerCommand(object obj)
        {
            try
            {
                FunctionsDateTimer.OffOnDateTimer();
                UpdateButton();
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка таймер не запущен");
            }
        }

        /// <summary>
        /// Быстро перейти к задаче
        /// </summary>
        /// <param name="obj"></param>
        private void ExecutedGoTaskCommand(object obj)
        {
            _currentMain.CurrentChildView = new TaskObjViewModel(_currentMain, (ViewTask)obj, new HomeViewModel(), "Рабочий стол", IconChar.House);
            _currentMain.Caption = "Редактирование задачи";
            _currentMain.Icon = IconChar.ListCheck;
        }

        /// <summary>
        /// Обновить кнопку
        /// </summary>
        private void UpdateButton()
        {
            ContentLabel = FunctionsDateTimer.GetContentButtonTrack();
            ForegroundButton = FunctionsDateTimer.GetColorBrushes();
        }
        #endregion
    }
}