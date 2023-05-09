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

namespace ChronosBeta.ViewModels
{
    public class ScreenshotViewModel: ViewModelBase
    {
        private ICollectionView _currentScreenshot;
        private static MainViewModel _currentMain;
        private static ViewDateTimer _currentDateTimer;

        public ICollectionView CurrentScreenshot
        {
            get { return _currentScreenshot; }
            set
            {
                _currentScreenshot = value;
                OnPropertyChanged(nameof(CurrentScreenshot));
            }
        }

        public ICommand Back { get; }

        public ScreenshotViewModel()
        {
            Back = new ViewModelCommand(ExecutedBackCommand);

            List<ViewScreenshot> currentScren = FunctionsImage.GetScreenshot(_currentDateTimer.Id);
            CurrentScreenshot = CollectionViewSource.GetDefaultView(currentScren);
        }

        public ScreenshotViewModel(MainViewModel main, ViewDateTimer selectedDateTimer)
        {
            _currentDateTimer = selectedDateTimer;
            _currentMain = main;
        }

        private void ExecutedBackCommand(object obj)
        {
            _currentMain.CurrentChildView = new DateTimerObjViewModel();
            _currentMain.Caption = "Список запущеный программ";
            _currentMain.Icon = IconChar.UserClock;
        }
    }
}