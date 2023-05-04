using ChronosBeta.BL;
using ChronosBeta.Model;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChronosBeta.ViewModels
{
    public class TaskTimerObjViewModel: ViewModelBase
    {
        public string User { get; set; }
        public List<string> Task { get; set; }
        public string SelectedTask { get; set; }
        public string SpentTime { get; set; }
        public string Description { get; set; }


        private static MainViewModel _currentMain;
        private static ViewTaskTimer SelectedTaskTimer;
        private static bool itEdit;

        public ICommand Save { get; }
        public ICommand Back { get; }

        public TaskTimerObjViewModel()
        {
            User = FunctionsCurrentUser.GetNameUser();
            Task = FunctionsTask.GetViewTask();
            //Инициализация команд
            Save = new ViewModelCommand(ExecutedSaveCommand);
            Back = new ViewModelCommand(ExecutedBackCommand);

            if (itEdit)
                SetTaskTimer();
        }

        public TaskTimerObjViewModel(MainViewModel main)
        {
            _currentMain = main;
            itEdit = false;
        }

        public TaskTimerObjViewModel(MainViewModel main, ViewTaskTimer selectedTaskTimer)
        {
            _currentMain = main;
            SelectedTaskTimer = selectedTaskTimer;
            itEdit = true;
        }

        private void SetTaskTimer()
        {
            User = SelectedTaskTimer.TaskTimer.Users1.Name;
            SelectedTask = SelectedTaskTimer.Task;
            SpentTime = SelectedTaskTimer.SpentTime;
            Description = SelectedTaskTimer.TaskTimer.Description;
        }
        private void ExecutedSaveCommand(object obj)
        {
            if (!itEdit)
            {
                try
                {
                    FunctionsTaskMark.AddTaskTimer(User, SelectedTask, SpentTime, Description);
                    MessageBox.Show("Отметка добавлена");
                }
                catch
                {
                    MessageBox.Show("Отметка не добавлена");
                }
            }
            else
            {
                try
                {
                    FunctionsTaskMark.EditTaskTimer(User, SelectedTask, SpentTime, Description, SelectedTaskTimer.TaskTimer);
                    MessageBox.Show("Отметка отредактирована");
                }
                catch
                {
                    MessageBox.Show("Отметка не отредактирована");
                }
            }

        }

        private void ExecutedBackCommand(object obj)
        {
            _currentMain.CurrentChildView = new TaskTimerViewModel();
            _currentMain.Caption = "Отметка по задачам";
            _currentMain.Icon = IconChar.Users;
        }
    }
}
