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
        private static ViewModelBase _parentsView;
        private static string _nameParentsView;
        private static IconChar _iconParentsView;
        private static bool itEdit;

        public List<string> Users { get; set; }
        public string SelectedUser { get; set; }

        public List<string> Task { get; set; }
        public string SelectedTask { get; set; }


        public string SpentTime { get; set; }
        public string Description { get; set; }

        public ICommand Save { get; }
        public ICommand Back { get; }
        public ICommand GoUser { get; }
        public ICommand GoTask { get; }

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
                GoUser = new ViewModelCommand(ExecutedGoUserCommand);
                GoTask = new ViewModelCommand(ExecutedGoTaskCommand);

                if (itEdit)
                    SetTaskTimer();
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка инициализации окна");
            }
        }

        public TaskTimerObjViewModel(MainViewModel main, ViewModelBase parentsView, string nameParentsView, IconChar iconParentsView)
        {
            _iconParentsView = iconParentsView;
            _nameParentsView = nameParentsView;
            _parentsView = parentsView;
            _currentMain = main;
            itEdit = false;
        }

        public TaskTimerObjViewModel(MainViewModel main, ViewTaskTimer selectedTaskTimer, ViewModelBase parentsView, string nameParentsView, IconChar iconParentsView)
        {
            _iconParentsView = iconParentsView;
            _nameParentsView = nameParentsView;
            _parentsView = parentsView;
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
            if (SelectedUser == null || SelectedUser == "")
            {
                FunctionsWindow.OpenConfrumWindow("Выберите пользователя!");
                return;
            }
            if (SelectedTask == null || SelectedTask == "")
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

        private void ExecutedGoUserCommand(object obj)
        {
            if (SelectedUser == null || SelectedUser == "")
            {
                FunctionsWindow.OpenConfrumWindow("Укажите пользователя!");
                return;
            }

            if (!FunctionsWindow.OpenDialogWindow("При переходе к поручителю все не сохраненые изменения будут утеряны\n" +
                                                  "Вы уверены что хотите перейти к пользователю?"))
            {
                return;
            }

            try
            {
                TaskTimerObjViewModel currentTaskTimer;
                string currentCaptionTaskTimer;
                if (itEdit)
                {
                    currentTaskTimer = new TaskTimerObjViewModel(_currentMain, SelectedTaskTimer, _parentsView, _nameParentsView, _iconParentsView);
                    currentCaptionTaskTimer = "Редактирование отметки";
                }
                else
                {
                    currentTaskTimer = new TaskTimerObjViewModel(_currentMain, _parentsView, _nameParentsView, _iconParentsView);
                    currentCaptionTaskTimer = "Добавление отметки";
                }

                _currentMain.CurrentChildView = new UserObjViewModel(_currentMain, FunctionsUsers.GetUserView(SelectedUser),
                                                                     currentTaskTimer, currentCaptionTaskTimer, IconChar.ThumbTack);
                _currentMain.Caption = "Редактирование пользователя";
                _currentMain.Icon = IconChar.UserEdit;
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Не удалось перейти к пользователю!");
            }

        }

        private void ExecutedGoTaskCommand(object obj)
        {
            if (SelectedTask == null || SelectedTask == "")
            {
                FunctionsWindow.OpenConfrumWindow("Укажите задачу!");
                return;
            }

            if (!FunctionsWindow.OpenDialogWindow("При переходе к поручителю все не сохраненые изменения будут утеряны\n" +
                                                  "Вы уверены что хотите перейти к задаче?"))
            {
                return;
            }

            try
            {
                TaskTimerObjViewModel currentTaskTimer;
                string currentCaptionTaskTimer;
                if (itEdit)
                {
                    currentTaskTimer = new TaskTimerObjViewModel(_currentMain, SelectedTaskTimer, _parentsView, _nameParentsView, _iconParentsView);
                    currentCaptionTaskTimer = "Редактирование отметки";
                }
                else
                {
                    currentTaskTimer = new TaskTimerObjViewModel(_currentMain, _parentsView, _nameParentsView, _iconParentsView);
                    currentCaptionTaskTimer = "Добавление отметки";
                }
                _currentMain.CurrentChildView = new TaskObjViewModel(_currentMain, FunctionsTask.GetTaskView(SelectedTask),
                                                                     currentTaskTimer, currentCaptionTaskTimer, IconChar.ThumbTack);
                _currentMain.Caption = "Редактирование задачи";
                _currentMain.Icon = IconChar.ListCheck;
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Не удалось перейти к задаче!");
            }
        }

        private void ExecutedBackCommand(object obj)
        {
            _currentMain.CurrentChildView = _parentsView;
            _currentMain.Caption = _nameParentsView;
            _currentMain.Icon = _iconParentsView;
        }

    }
}
