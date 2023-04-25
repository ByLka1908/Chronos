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
    public class TaskViewModel: ViewModelBase
    {
        public ICollectionView CurrentTask { get; private set; }
        public ICommand AddTask { get; }
        public ICommand EditTask { get; }
        public ViewTask SelectedTask { get; set; }

        private static MainViewModel _currentMain;

        public TaskViewModel()
        {
            AddTask = new ViewModelCommand(ExecutedAddTaskCommand);
            EditTask = new ViewModelCommand(ExecutedEditTaskCommand);

            List<ViewTask> currentTask = FunctionsTask.GetTasks();
            CurrentTask = CollectionViewSource.GetDefaultView(currentTask);
        }

        public TaskViewModel(MainViewModel main)
        {
            _currentMain = main;
        }

        private void ExecutedAddTaskCommand(object obj)
        {
            _currentMain.CurrentChildView = new TaskObjViewModel(_currentMain);
            _currentMain.Caption = "Добавление задачи";
            _currentMain.Icon = IconChar.UserPlus;
        }
        private void ExecutedEditTaskCommand(object obj)
        {
            _currentMain.CurrentChildView = new TaskObjViewModel(_currentMain, SelectedTask);
            _currentMain.Caption = "Редактирование задачи";
            _currentMain.Icon = IconChar.UserPlus;
        }

    }
}
