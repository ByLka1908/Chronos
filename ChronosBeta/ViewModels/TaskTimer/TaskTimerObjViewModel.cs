using ChronosBeta.BL;
using ChronosBeta.Model;
using ChronosBeta.DB;
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
        private static Users _currentUser;
        private static MainViewModel _currentMain;
        private static ViewTaskTimer SelectedTaskTimer;
        private static bool itEdit;

        public List<string> Users { get; set; }
        public string SelectedUser { get; set; }

        public List<string> Task { get; set; }
        public string SelectedTask { get; set; }


        public string SpentTime { get; set; }
        public string Description { get; set; }

        public ICommand Save { get; }
        public ICommand Back { get; }

        public TaskTimerObjViewModel()
        {
            try
            {
                Users = FunctionsUsers.GetViewUser();
                Task = FunctionsTask.GetViewTask();

                _currentUser = FunctionsCurrentUser.User;
                SelectedUser = _currentUser.Name + " " + _currentUser.Surname + " " + _currentUser.MiddleName;

                //Инициализация команд
                Save = new ViewModelCommand(ExecutedSaveCommand);
                Back = new ViewModelCommand(ExecutedBackCommand);

                if (itEdit)
                    SetTaskTimer();
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка инициализации окна");
            }
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
            _currentUser = SelectedTaskTimer.TaskTimer.Users1;
            SelectedUser = _currentUser.Name + " " + _currentUser.Surname + " " + _currentUser.MiddleName;

            SelectedTask = SelectedTaskTimer.Task;
            SpentTime    = SelectedTaskTimer.SpentTime;
            Description  = SelectedTaskTimer.TaskTimer.Description;
        }
        private void ExecutedSaveCommand(object obj)
        {
            if (SelectedUser == null)
            {
                FunctionsWindow.OpenConfrumWindow("Выберите пользователя!");
                return;
            }
            if (SelectedTask == null)
            {
                FunctionsWindow.OpenConfrumWindow("Укажите задачу!");
                return;
            }
            try
            {
                Convert.ToDouble(SpentTime);
            }
            catch
            {
                FunctionsWindow.OpenConfrumWindow("Укажите правильный формат для\nзатраченного времени выполнения");
                return;
            }

            if (!itEdit)
            {
                try
                {
                    FunctionsTaskMark.AddTaskTimer(FunctionsUsers.GetUserId(SelectedUser), FunctionsTask.GetIdTask(SelectedTask), SpentTime, Description);
                    FunctionsWindow.OpenGoodWindow("Отметка добавлена");
                }
                catch
                {
                    FunctionsWindow.OpenErrorWindow("Отметка не добавлена");
                }
            }
            else
            {
                try
                {
                    FunctionsTaskMark.EditTaskTimer(FunctionsUsers.GetUserId(SelectedUser), FunctionsTask.GetIdTask(SelectedTask), SpentTime, Description, SelectedTaskTimer.TaskTimer);
                    FunctionsWindow.OpenGoodWindow("Отметка отредактирована");
                }
                catch
                {
                    FunctionsWindow.OpenErrorWindow("Отметка не отредактирована");
                }
            }
        }

        private void ExecutedBackCommand(object obj)
        {
            _currentMain.CurrentChildView = new TaskTimerViewModel();
            _currentMain.Caption = "Отметка по задачам";
            _currentMain.Icon = IconChar.ThumbTack;
        }
    }
}
