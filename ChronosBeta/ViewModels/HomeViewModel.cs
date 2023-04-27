using ChronosBeta.BL;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace ChronosBeta.ViewModels
{
    public class HomeViewModel: ViewModelBase
    {
        public ICommand OnOffTimer { get; }
        public ICollectionView OverdueTask { get; private set; }
        public ICollectionView CurrentTask { get; private set; }
        public ICollectionView FutureTask { get; private set; }
        public ICollectionView MarkedTime { get; private set; }
        public SolidColorBrush ForegroundButton { get; set; }
        public string DatePicker { get; set; }
        public string ContentLabel { get; set; }

        private static MainViewModel _currentMain;

        public HomeViewModel() 
        {
            ContentLabel = "Здраствуйте, включите таймер рабочего времени!";
            ForegroundButton = Brushes.Red;
            OnOffTimer = new ViewModelCommand(ExecutedOnOffTimerCommand);
        }
        public HomeViewModel(MainViewModel main)
        {
            _currentMain = main;
        }

        private void ExecutedOnOffTimerCommand(object obj)
        {
            ContentLabel = FunctionsDateTimer.OffOnDateTimer();
        }

    }
}
