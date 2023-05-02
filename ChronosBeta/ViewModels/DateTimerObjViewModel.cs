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
    public class DateTimerObjViewModel: ViewModelBase
    {
        public string User { get; set; }
        public string Day { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public ICollectionView CurrentAppList { get; private set; }

        private static MainViewModel _currentMain;
        private static ViewDateTimer DateTimer;
        public ICommand Back { get; }

        public DateTimerObjViewModel() 
        {
            Back = new ViewModelCommand(ExecutedBackCommand);
            SetDateTimer();
        }
        public DateTimerObjViewModel(MainViewModel main, ViewDateTimer dateTimer)
        {
            _currentMain = main;
            DateTimer = dateTimer;
        }
        private void ExecutedBackCommand(object obj)
        {
            _currentMain.CurrentChildView = new DateTimerViewModel();
            _currentMain.Caption = "Пользователи";
            _currentMain.Icon = IconChar.Users;
        }
        private void SetDateTimer()
        {
            if (DateTimer == null)
                return;

            List<ViewListApplication> list = FunctionsJSON.GetDeserializeJson(DateTimer.DateTimer.AllRunProgram);
            CurrentAppList = CollectionViewSource.GetDefaultView(list);

            User = DateTimer.User;
            Day = DateTimer.Day;
            TimeStart = DateTimer.TimeStart;
            TimeEnd = DateTimer.TimeEnd;   
        }
    }
}
