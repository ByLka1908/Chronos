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
    public class TaskTimerViewModel: ViewModelBase
    {
        private static string nameUserFilter;
        private static string dayPickerFilter;
        private static bool isTaskOverFilter;
        private static bool FilterOn = false;

        private ICollectionView _markedTime;
        private static MainViewModel _currentMain;

        public ViewTaskTimer SelectedTaskTimer { get; set; }
        public string CurrentText { get; set; }
        public ICollectionView MarkedTime
        {
            get { return _markedTime; }
            set
            {
                _markedTime = value;
                OnPropertyChanged(nameof(MarkedTime));
            }
        }

        public ICommand AddTaskTimer { get; }
        public ICommand EditTaskTimer { get; }
        public ICommand GoTaskTimerEdit { get; }
        public ICommand RemoveTaskTimer { get; }
        public ICommand Search { get; }
        public ICommand Filter { get; }

        private void UpdateView()
        {
            try
            {
                if (!FilterOn)
                {
                    List<ViewTaskTimer> TaskTimer = FunctionsTaskMark.GetTasksTimer();
                    MarkedTime = CollectionViewSource.GetDefaultView(TaskTimer);
                }
                else
                {
                    List<ViewTaskTimer> TaskTimer = FunctionsTaskMark.GetTasksTimer();
                    TaskTimer = TaskTimer.Where(x => x.TaskTimer.Task1.ItsOver == isTaskOverFilter).ToList();

                    if(nameUserFilter != "Все")
                    {
                        int idUser = FunctionsUsers.GetUserId(nameUserFilter);
                        TaskTimer = TaskTimer.Where(x => x.TaskTimer.Users == idUser).ToList();
                    }

                    if(dayPickerFilter != string.Empty && dayPickerFilter != "" && dayPickerFilter != null)
                    {
                        DateTime day = DateTime.ParseExact(dayPickerFilter, FunctionsSettingStart.Validformats, FunctionsSettingStart.Provider, DateTimeStyles.None);
                        TaskTimer = TaskTimer.Where(x => x.TaskTimer.Day == day).ToList();
                    }

                    MarkedTime = CollectionViewSource.GetDefaultView(TaskTimer);
                }
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка обновления таблицы");
            }
        }

        public TaskTimerViewModel()
        {
            AddTaskTimer    = new ViewModelCommand(ExecutedAddTaskTimerCommand);
            EditTaskTimer   = new ViewModelCommand(ExecutedEditTaskTimerCommand);
            GoTaskTimerEdit = new ViewModelCommand(ExecutedGoTaskTimerEditCommand);
            RemoveTaskTimer = new ViewModelCommand(ExecutedRemoveTaskTimerCommand);
            Search          = new ViewModelCommand(ExecutedSearchCommand);
            Filter          = new ViewModelCommand(ExecutedFilterCommand);

            UpdateView();
        }

        public TaskTimerViewModel(MainViewModel main)
        {
            _currentMain = main;
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
                List<ViewTaskTimer> currentTaskTimer = FunctionsTaskMark.GetTasksTimer();
                List<ViewTaskTimer> findTaskTimer = currentTaskTimer.Where(x => x.Task.ToUpper().StartsWith(CurrentText.ToUpper())).ToList();

                if (findTaskTimer.Count < 1)
                {
                    FunctionsWindow.OpenConfrumWindow("Обьект не найден");
                    CurrentText = string.Empty;
                    UpdateView();
                    return;
                }

                MarkedTime = CollectionViewSource.GetDefaultView(findTaskTimer);
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка поиска");
            }
        }

        private void ExecutedRemoveTaskTimerCommand(object obj)
        {
            if (SelectedTaskTimer.TaskTimer == null)
            {
                FunctionsWindow.OpenConfrumWindow("Отметка не выбрана");
                return;
            }

            if (!FunctionsWindow.OpenDialogWindow("Вы действиельно хотите удалить задачу?"))
                return;

            try
            {
                FunctionsTaskMark.DeleteTaskTimer(SelectedTaskTimer.TaskTimer);
                UpdateView();
                FunctionsWindow.OpenGoodWindow("Отметка удалена");
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка удаления");
            }
        }

        private void ExecutedAddTaskTimerCommand(object obj)
        {
            _currentMain.CurrentChildView = new TaskTimerObjViewModel(_currentMain, new TaskTimerViewModel(), "Отметка по задачам", IconChar.ThumbTack);
            _currentMain.Caption = "Добавление отметки";
            _currentMain.Icon = IconChar.ThumbTack;
        }

        private void ExecutedEditTaskTimerCommand(object obj)
        {
            if(SelectedTaskTimer == null)
            {
                MessageBox.Show("Задача не выбрана");
                return;
            }
            _currentMain.CurrentChildView = new TaskTimerObjViewModel(_currentMain, SelectedTaskTimer, new TaskTimerViewModel(), "Отметка по задачам", IconChar.ThumbTack);
            _currentMain.Caption = "Редактирование отметки";
            _currentMain.Icon = IconChar.ThumbTack;
        }

        private void ExecutedFilterCommand(object obj)
        {
            try
            {
                if (FunctionsWindow.OpenFilterWindow(new TaskTimerFilterView()))
                {
                    nameUserFilter   = TaskTimerFilterViewModel.UserSelected;
                    isTaskOverFilter = TaskTimerFilterViewModel.isTaskOver;
                    dayPickerFilter  = TaskTimerFilterViewModel.DatePicker;
                    FilterOn = true;
                    UpdateView();
                }
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Не удалось открыть фильтер!");
            }
        }

        private void ExecutedGoTaskTimerEditCommand(object obj)
        {
            _currentMain.CurrentChildView = new TaskTimerObjViewModel(_currentMain, (ViewTaskTimer)obj, new TaskTimerViewModel(), "Отметка по задачам", IconChar.ThumbTack);
            _currentMain.Caption = "Редактирование отметки";
            _currentMain.Icon = IconChar.ThumbTack;
        }

    }
}
