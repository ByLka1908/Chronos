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
        public ICollectionView MarkedTime { get; private set; }
        public ICommand AddTaskTimer { get; }
        public ICommand EditTaskTimer { get; }
        public ViewTaskTimer SelectedTaskTimer { get; set; }

        private static MainViewModel _currentMain;

        public TaskTimerViewModel()
        {
            AddTaskTimer = new ViewModelCommand(ExecutedAddTaskTimerCommand);
            EditTaskTimer = new ViewModelCommand(ExecutedEditTaskTimerCommand);

            List<ViewTaskTimer> TaskTimer = FunctionsTaskMark.GetTasksTimer();
            MarkedTime = CollectionViewSource.GetDefaultView(TaskTimer);
        }

        public TaskTimerViewModel(MainViewModel main)
        {
            _currentMain = main;
        }

        private void ExecutedAddTaskTimerCommand(object obj)
        {

        }
        private void ExecutedEditTaskTimerCommand(object obj)
        {

        }

    }
}
