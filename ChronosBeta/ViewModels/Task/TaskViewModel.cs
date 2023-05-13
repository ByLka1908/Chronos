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
    public class TaskViewModel: ViewModelBase
    {        
        private static MainViewModel _currentMain;
        private ICollectionView _currentTask;

        public ICollectionView CurrentTask
        {
            get { return _currentTask; }
            set
            {
                _currentTask = value;
                OnPropertyChanged(nameof(CurrentTask));
            }
        }
        public ICommand AddTask { get; }
        public ICommand EditTask { get; }
        public ICommand Search { get; }
        public ICommand RemoveTask { get; }
        public ViewTask SelectedTask { get; set; }
        public string CurrentText { get; set; }

        public TaskViewModel()
        {
            AddTask = new ViewModelCommand(ExecutedAddTaskCommand);
            EditTask = new ViewModelCommand(ExecutedEditTaskCommand);
            Search = new ViewModelCommand(ExecutedSearchCommand);
            RemoveTask = new ViewModelCommand(ExecutedRemoveTaskCommand);

            UpdateView();
        }

        public TaskViewModel(MainViewModel main)
        {
            _currentMain = main;
        }

        private void UpdateView()
        {
            try
            {
                List<ViewTask> currentTask = FunctionsTask.GetTasks();
                CurrentTask = CollectionViewSource.GetDefaultView(currentTask);
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка обновления окна");
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
                List<ViewTask> currentTask = FunctionsTask.GetTasks();
                List<ViewTask> findTask = currentTask.Where(x => x.Name.ToUpper().StartsWith(CurrentText.ToUpper())).ToList();

                if (findTask.Count < 1)
                {
                    MessageBox.Show("Обьект не найден");
                    CurrentText = string.Empty;
                    UpdateView();
                    return;
                }
                CurrentTask = CollectionViewSource.GetDefaultView(findTask);
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка поиска");
            }
        }

        private void ExecutedAddTaskCommand(object obj)
        {
            _currentMain.CurrentChildView = new TaskObjViewModel(_currentMain);
            _currentMain.Caption = "Добавление задачи";
            _currentMain.Icon = IconChar.ListCheck;
        }

        private void ExecutedEditTaskCommand(object obj)
        {
            if(SelectedTask == null)
            {
                FunctionsWindow.OpenConfrumWindow("Задача не выбрана");
                return;
            }
            _currentMain.CurrentChildView = new TaskObjViewModel(_currentMain, SelectedTask);
            _currentMain.Caption = "Редактирование задачи";
            _currentMain.Icon = IconChar.ListCheck;
        }

        private void ExecutedRemoveTaskCommand(object obj)
        {
            if (!FunctionsWindow.OpenDialogWindow("Вы действиельно хотите удалить задачу?"))
                return;

            if (SelectedTask.Task == null)
            {
                FunctionsWindow.OpenConfrumWindow("Задача не выбрана");
                return;
            }
            try
            {
                FunctionsTask.DeleteTask(SelectedTask.Task);
                UpdateView();
                FunctionsWindow.OpenGoodWindow("Задача удалена!");
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка удаления отметки");
            }
        }
    }
}