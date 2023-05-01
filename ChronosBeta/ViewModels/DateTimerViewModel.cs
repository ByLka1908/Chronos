using ChronosBeta.BL;
using ChronosBeta.Model;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ChronosBeta.ViewModels
{
    public class DateTimerViewModel : ViewModelBase
    {
        public ICollectionView CurrentDateList { get; private set; }
        public ICommand OpenListDate { get; }
        public ViewDateTimer SelectedDate { get; set; }

        private static MainViewModel _currentMain;

        public DateTimerViewModel()
        {
            OpenListDate = new ViewModelCommand(ExecutedOpenListDateCommand);

            List<ViewDateTimer> currentDate = FunctionsDateTimer.GetDateTimer();
            CurrentDateList = CollectionViewSource.GetDefaultView(currentDate);
        }
        public DateTimerViewModel(MainViewModel main)
        {
            _currentMain = main;
        }

        private void ExecutedOpenListDateCommand(object obj)
        {
            if (SelectedDate == null)
            {
                MessageBox.Show("Пользователь не выбран");
                return;
            }
            _currentMain.CurrentChildView = new DateTimerObjViewModel(_currentMain, SelectedDate);
            _currentMain.Caption = "Редактирование пользователя";
            _currentMain.Icon = IconChar.UserEdit;
        }
    }
}
