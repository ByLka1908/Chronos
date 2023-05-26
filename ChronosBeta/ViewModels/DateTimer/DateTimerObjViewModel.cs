﻿using ChronosBeta.BL;
using ChronosBeta.Model;
using FontAwesome.Sharp;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace ChronosBeta.ViewModels
{
    public class DateTimerObjViewModel: ViewModelBase
    {        
        private static MainViewModel _currentMain;
        private static ViewDateTimer DateTimer;

        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string Day { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }

        public ICollectionView CurrentAppList { get; private set; }
        public ICommand Back { get; }
        public ICommand OpenListScreentshot { get; }

        public DateTimerObjViewModel() 
        {
            Back = new ViewModelCommand(ExecutedBackCommand);
            OpenListScreentshot = new ViewModelCommand(ExecutedOpenListScreentshotCommand);
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
            _currentMain.Caption = "Рабочее время";
            _currentMain.Icon = IconChar.Clock;
        } 

        private void ExecutedOpenListScreentshotCommand(object obj)
        {
            _currentMain.CurrentChildView = new ScreenshotViewModel(_currentMain, DateTimer);
            _currentMain.Caption = "Снимки экрана";
            _currentMain.Icon = IconChar.Images;
        }

        private void SetDateTimer()
        {
            try
            {
                if (DateTimer == null)
                    return;

                List<ViewListApplication> list = FunctionsJSON.GetDeserializeJson(DateTimer.DateTimer.AllRunProgram);
                CurrentAppList = CollectionViewSource.GetDefaultView(list);

                UserName = DateTimer.UserName;
                UserSurname = DateTimer.UserSurname;
                Day = DateTimer.Day;
                TimeStart = DateTimer.TimeStart;
                TimeEnd = DateTimer.TimeEnd;
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка запуска рабочего таймера");
            }  
        }
    }
}