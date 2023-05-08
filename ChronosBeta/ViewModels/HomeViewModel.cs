using ChronosBeta.BL;
using ChronosBeta.Model;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            ContentLabel = "Здраствуйте, включите таймер рабочего времени!";
            ForegroundButton = Brushes.Red;
            OnOffTimer = new ViewModelCommand(ExecutedOnOffTimerCommand);

            DateTime currentTime = DateTime.Today;


            List<ViewTask> currentTasks = FunctionsTask.GetTasks();
            List<ViewTask> _overdueTask = currentTasks.Where(x => DateTime.Parse(x.Deadline) < currentTime).ToList();
            List<ViewTask> _currentTask = currentTasks.Where(x => DateTime.Parse(x.Deadline) >= currentTime 
                                                             && DateTime.Parse(x.Deadline) < currentTime.AddDays(3)).ToList();
            List<ViewTask> _futureTask  = currentTasks.Where(x => DateTime.Parse(x.Deadline) > currentTime.AddDays(4)).ToList();


            OverdueTask = CollectionViewSource.GetDefaultView(_overdueTask);
            CurrentTask = CollectionViewSource.GetDefaultView(_currentTask);
            FutureTask = CollectionViewSource.GetDefaultView(_futureTask);
        }

        public HomeViewModel(MainViewModel main)
        {
            _currentMain = main;
        }

        private void ExecutedOnOffTimerCommand(object obj)
        {
            ContentLabel = FunctionsDateTimer.OffOnDateTimer();
            ForegroundButton = FunctionsDateTimer.GetColorBrushes();
        }
    }
}