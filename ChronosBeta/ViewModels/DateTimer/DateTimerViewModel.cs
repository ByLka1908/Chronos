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
        private ICollectionView _currentDateList;
        private static MainViewModel _currentMain;

        public ICollectionView CurrentDateList
        {
            get { return _currentDateList; }
            set
            {
                _currentDateList = value;
                OnPropertyChanged(nameof(CurrentDateList));
            }
        }
        public ICommand OpenListDate { get; }
        public ICommand Search { get; }
        public ViewDateTimer SelectedDate { get; set; }
        public string DatePicker { get; set; }
        public string CurrentText { get; set; }

        public DateTimerViewModel()
        {
            OpenListDate = new ViewModelCommand(ExecutedOpenListDateCommand);
            Search = new ViewModelCommand(ExecutedSearchCommand);

            UpdateView();
        }

        public DateTimerViewModel(MainViewModel main)
        {
            _currentMain = main;
        }

        private void UpdateView()
        {
            try
            {
                List<ViewDateTimer> currentDate = FunctionsDateTimer.GetDateTimer();
                CurrentDateList = CollectionViewSource.GetDefaultView(currentDate);
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка обновления таблицы");
            }
        }

        private void ExecutedSearchCommand(object obj)
        {
            try
            {
                DateTime date;
                if (CurrentText == null & DatePicker == null)
                {
                    return;
                }

                if (CurrentText == string.Empty & DatePicker == string.Empty)
                {
                    UpdateView();
                    return;
                }

                List<ViewDateTimer> currentDate = FunctionsDateTimer.GetDateTimer();
                List<ViewDateTimer> findDate;
                if (CurrentText != null & DatePicker != null)
                {
                    string[] words = DatePicker.Split(new char[] { '/' });
                    string[] year = words[2].Split(new char[] { ' ' });
                    date = new DateTime(Convert.ToInt32(year[0]), Convert.ToInt32(words[0]), Convert.ToInt32(words[1]));
                    findDate = currentDate.Where(x => x.UserSurname.ToUpper().StartsWith(CurrentText.ToUpper())
                                                 & x.Day.ToUpper().StartsWith(date.ToShortDateString().ToUpper())).ToList();
                }
                else if (CurrentText != null)
                {
                    findDate = currentDate.Where(x => x.UserSurname.ToUpper().StartsWith(CurrentText.ToUpper())).ToList();
                }
                else
                {
                    string[] words = DatePicker.Split(new char[] { '/' });
                    string[] year = words[2].Split(new char[] { ' ' });
                    date = new DateTime(Convert.ToInt32(year[0]), Convert.ToInt32(words[0]), Convert.ToInt32(words[1]));
                    findDate = currentDate.Where(x => x.Day.ToUpper().StartsWith(date.ToShortDateString().ToUpper())).ToList();
                }

                if (findDate.Count < 1)
                {
                    MessageBox.Show("Обьект не найден");
                    CurrentText = string.Empty;
                    UpdateView();
                    return;
                }

                CurrentDateList = CollectionViewSource.GetDefaultView(findDate);
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка поиска");
            }
        }

        private void ExecutedOpenListDateCommand(object obj)
        {
            if (SelectedDate == null)
            {
                MessageBox.Show("Пользователь не выбран");
                return;
            }
            _currentMain.CurrentChildView = new DateTimerObjViewModel(_currentMain, SelectedDate);
            _currentMain.Caption = "Список запущеный программ";
            _currentMain.Icon = IconChar.UserClock;
        }
    }
}