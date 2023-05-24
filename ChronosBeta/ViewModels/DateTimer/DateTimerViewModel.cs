using ChronosBeta.BL;
using ChronosBeta.BL.InternalFunctions;
using ChronosBeta.Model;
using ChronosBeta.View;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
        private static string dayPickerFilter;
        private static bool FilterOn = false;

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
        public ICommand RemoveDateTimer { get; }
        public ICommand GoDateTimer { get; }
        public ICommand Filter { get; }
        public ViewDateTimer SelectedDate { get; set; }
        public string DatePicker { get; set; }
        public string CurrentText { get; set; }

        public DateTimerViewModel()
        {
            OpenListDate = new ViewModelCommand(ExecutedOpenListDateCommand);
            Search = new ViewModelCommand(ExecutedSearchCommand);
            RemoveDateTimer = new ViewModelCommand(ExecutedRemoveDateTimerCommand);
            GoDateTimer = new ViewModelCommand(ExecutedGoDateTimerCommand);
            Filter = new ViewModelCommand(ExecutedFilterCommand);

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
                if (!FilterOn)
                {
                    List<ViewDateTimer> currentDate = FunctionsDateTimer.GetDateTimer();
                    CurrentDateList = CollectionViewSource.GetDefaultView(currentDate);
                }
                else
                {
                    List<ViewDateTimer> currentDate = FunctionsDateTimer.GetDateTimer();
                    if(dayPickerFilter != string.Empty && dayPickerFilter != "" && dayPickerFilter != null)
                    {
                        DateTime day = DateTime.ParseExact(dayPickerFilter, FunctionsSettingStart.Validformats, FunctionsSettingStart.Provider, DateTimeStyles.None);
                        currentDate = currentDate.Where(x => x.DateTimer.Day == day).ToList();
                    }
                    CurrentDateList = CollectionViewSource.GetDefaultView(currentDate);
                }
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка обновления таблицы");
            }
        }

        private void ExecutedFilterCommand(object obj)
        {
            try
            {
                if (FunctionsWindow.OpenFilterWindow(new DateTimerFilterView()))
                {
                    dayPickerFilter = DateTimerFilterViewModel.DatePicker;
                    FilterOn = true;
                    UpdateView();
                }
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Не удалось открыть фильтер!");
            }
        }

        private void ExecutedSearchCommand(object obj)
        {
            if (CurrentText == null)
            {
                FunctionsWindow.OpenConfrumWindow("Поле поиска пустое");
                return;
            }

            if (CurrentText == string.Empty)
            {
                UpdateView();
                return;
            }

            try
            {
                List<ViewDateTimer> currentDate = FunctionsDateTimer.GetDateTimer();
                List<ViewDateTimer> findDate = currentDate.Where(x => x.UserName.ToUpper().StartsWith(CurrentText.ToUpper()) ||
                                                                      x.UserSurname.ToUpper().StartsWith(CurrentText.ToUpper())).ToList();

                if (findDate.Count < 1)
                {
                    FunctionsWindow.OpenErrorWindow("Обьект не найден");
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
                FunctionsWindow.OpenConfrumWindow("Рабочее время не выбрано");
                return;
            }
            _currentMain.CurrentChildView = new DateTimerObjViewModel(_currentMain, SelectedDate);
            _currentMain.Caption = "Список запущеный программ";
            _currentMain.Icon = IconChar.UserClock;
        }

        private void ExecutedGoDateTimerCommand(object obj)
        {
            _currentMain.CurrentChildView = new DateTimerObjViewModel(_currentMain, (ViewDateTimer)obj);
            _currentMain.Caption = "Список запущеный программ";
            _currentMain.Icon = IconChar.UserClock;
        }

        private void ExecutedRemoveDateTimerCommand(object obj)
        {
            if (SelectedDate == null)
            {
                FunctionsWindow.OpenConfrumWindow("Рабочее время не выбрана");
                return;
            }

            if (!FunctionsWindow.OpenDialogWindow("Вы действиельно хотите удалить отмеченное рабочее время?"))
                return;

            try
            {
                FunctionsDateTimer.DeleteDateTimer(SelectedDate.DateTimer);
                UpdateView();
                FunctionsWindow.OpenGoodWindow("Рабочее время удалена!");
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка удаления отметки о рабочем времени!");
            }
        }

    }
}