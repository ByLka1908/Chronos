using ChronosBeta.BL;
using ChronosBeta.BL.InternalFunctions;
using ChronosBeta.Model;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace ChronosBeta.ViewModels
{
    public class HomeViewModel: ViewModelBase
    {
        private string _contentLabel;
        private SolidColorBrush _foregroundButton;
        private static MainViewModel _currentMain;

        public ICollectionView OverdueTask { get; private set; }
        public ICollectionView CurrentTask { get; private set; }
        public ICollectionView FutureTask { get; private set; }
        public ICommand OnOffTimer { get; }
        public ICommand GoTask { get; }

        public string DatePicker { get; set; }
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

        public HomeViewModel(MainViewModel main)
        {
            _currentMain = main;
        }

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

        private void ExecutedGoTaskCommand(object obj)
        {
            _currentMain.CurrentChildView = new TaskObjViewModel(_currentMain, (ViewTask)obj, new HomeViewModel(), "Рабочий стол", IconChar.House);
            _currentMain.Caption = "Редактирование задачи";
            _currentMain.Icon = IconChar.ListCheck;
        }

        private void UpdateButton()
        {
            ContentLabel = FunctionsDateTimer.GetContentButtonTrack();
            ForegroundButton = FunctionsDateTimer.GetColorBrushes();
        }

    }
}