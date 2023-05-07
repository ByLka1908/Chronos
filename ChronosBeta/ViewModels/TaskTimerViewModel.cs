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
    public class TaskTimerViewModel: ViewModelBase
    {
        private ICollectionView _markedTime;
        private static MainViewModel _currentMain;
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
        public ICommand RemoveTaskTimer { get; }
        public ICommand Search { get; }
        public ViewTaskTimer SelectedTaskTimer { get; set; }
        public string CurrentText { get; set; }

        private void UpdateView()
        {
            List<ViewTaskTimer> TaskTimer = FunctionsTaskMark.GetTasksTimer();
            MarkedTime = CollectionViewSource.GetDefaultView(TaskTimer);
        }

        public TaskTimerViewModel()
        {
            AddTaskTimer = new ViewModelCommand(ExecutedAddTaskTimerCommand);
            EditTaskTimer = new ViewModelCommand(ExecutedEditTaskTimerCommand);
            RemoveTaskTimer = new ViewModelCommand(ExecutedRemoveTaskTimerCommand);
            Search = new ViewModelCommand(ExecutedSearchCommand);

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
                return;
            }

            if (CurrentText == string.Empty)
            {
                UpdateView();
                return;
            }

            List<ViewTaskTimer> currentTaskTimer = FunctionsTaskMark.GetTasksTimer();
            List<ViewTaskTimer> findTaskTimer = currentTaskTimer.Where(x => x.Task.ToUpper().StartsWith(CurrentText.ToUpper())).ToList();

            if (findTaskTimer.Count < 1)
            {
                MessageBox.Show("Обьект не найден");
                CurrentText = string.Empty;
                UpdateView();
                return;
            }

            MarkedTime = CollectionViewSource.GetDefaultView(findTaskTimer);
        }

        private void ExecutedRemoveTaskTimerCommand(object obj)
        {
            FunctionsTaskMark.DeleteTaskTimer(SelectedTaskTimer.TaskTimer);
            UpdateView();
        }

        private void ExecutedAddTaskTimerCommand(object obj)
        {
            _currentMain.CurrentChildView = new TaskTimerObjViewModel(_currentMain);
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
            _currentMain.CurrentChildView = new TaskTimerObjViewModel(_currentMain, SelectedTaskTimer);
            _currentMain.Caption = "Редактирование отметки";
            _currentMain.Icon = IconChar.ThumbTack;
        }

    }
}
