using ChronosBeta.BL;
using ChronosBeta.BL.InternalFunctions;
using ChronosBeta.DB;
using ChronosBeta.Model;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace ChronosBeta.ViewModels
{
    public class TaskObjViewModel: ViewModelBase
    {   
        private static MainViewModel _currentMain;
        private static ViewModelBase _parentsView;
        private static ViewTask SelectedTask;
        private static string _nameParentsView;
        private static IconChar _iconParentsView;
        private static bool itEdit;

        public List<string> UserDoTask { get; set; }
        public string SelectedUserDoTask { get; set; }
        public List<string> UserCreateTask { get; set; }
        public string SelectedUserCreateTask { get; set; }
        public List<string> Project { get; set; }
        public string SelectedProject { get; set; }
        public List<string> ItsOver { get; set; }
        public string SelectedItsOver { get; set; }

        public string NameTask { get; set; }
        public string EstimatedTime { get; set; }
        public string AllSpentTime { get; set; }
        public string DeadLine { get; set; }
        public string Description { get; set; } 

        public ICommand Save { get; }
        public ICommand Back { get; }
        public ICommand GoUserDoTask { get; }
        public ICommand GoUserCreateTask { get; }
        public ICommand GoProject { get; }

        public TaskObjViewModel()
        {
            try
            {
                List<string> itsOver = new List<string>();
                itsOver.Add("Да");
                itsOver.Add("Нет");

                ItsOver = itsOver;
                UserCreateTask = FunctionsUsers.GetViewUser();
                UserDoTask = FunctionsUsers.GetViewUser();
                Project = FunctionsProject.GetViewProject();

                //Инициализация команд
                Save = new ViewModelCommand(ExecutedSaveCommand);
                Back = new ViewModelCommand(ExecutedBackCommand);
                GoUserDoTask = new ViewModelCommand(ExecutedGoUserDoTaskCommand);
                GoUserCreateTask = new ViewModelCommand(ExecutedGoUserCreateTaskCommand);
                GoProject = new ViewModelCommand(ExecutedGoProjectCommand);

                if (itEdit)
                    SetTask();
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка инициализации окна");
                return;
            }
        }

        public TaskObjViewModel(MainViewModel main, ViewModelBase parentsView, string nameParentsView, IconChar iconParentsView)
        {
            _iconParentsView = iconParentsView;
            _nameParentsView = nameParentsView;
            _parentsView = parentsView;
            _currentMain = main;
            itEdit = false;
        }

        public TaskObjViewModel(MainViewModel main, ViewTask selectedTask, ViewModelBase parentsView, string nameParentsView, IconChar iconParentsView)
        {
            _iconParentsView = iconParentsView;
            _nameParentsView = nameParentsView;
            _parentsView = parentsView;
            _currentMain = main;
            SelectedTask = selectedTask;
            itEdit = true;
        }

        private void SetTask()
        {
            NameTask = SelectedTask.Name;
            EstimatedTime = SelectedTask.EstimatedTime;
            AllSpentTime = SelectedTask.Task.AllSpentTime.ToString();
            SelectedUserDoTask = SelectedTask.UserDoTask;
            SelectedUserCreateTask = SelectedTask.UserCreateTask;
            SelectedProject = SelectedTask.Task.Project1.NameProject;
            DeadLine = SelectedTask.Deadline;
            Description = SelectedTask.Task.Description;

            if (SelectedTask.Task.ItsOver)
                SelectedItsOver = "Да";
            else
                SelectedItsOver = "Нет";
        }

        private void ExecutedSaveCommand(object obj)
        {
            DateTime time;
            if (SelectedItsOver == null || SelectedItsOver == "")
            {
                FunctionsWindow.OpenConfrumWindow("Укажите выполнена ли задача!");
                return;
            }
            if (NameTask == null || NameTask == "")
            {
                FunctionsWindow.OpenConfrumWindow("Укажите название задачи!");
                return;
            }
            if (SelectedUserDoTask == null || SelectedUserDoTask == "")
            {
                FunctionsWindow.OpenConfrumWindow("Укажите пользователя для выполнения задачи!");
                return;
            }
            if (SelectedUserCreateTask == null || SelectedUserCreateTask == "")
            {
                FunctionsWindow.OpenConfrumWindow("Укажите пользователя который создал задачу!");
                return;
            }
            if (SelectedProject == null || SelectedProject == "")
            {
                FunctionsWindow.OpenConfrumWindow("Выберите проект!");
                return;
            }
            try
            {
                Convert.ToDouble(EstimatedTime);
            }
            catch
            {
                FunctionsWindow.OpenConfrumWindow("Укажите правильный формат для\nожидаемого времени выполнения");
                return;
            }
            try
            {
                time = DateTime.ParseExact(DeadLine, FunctionsSettingStart.Validformats, FunctionsSettingStart.Provider, DateTimeStyles.None);
            }
            catch
            {
                FunctionsWindow.OpenConfrumWindow("Укажите правильный формат для\nдаты окончания задачи");
                return;
            }

            if (!itEdit)
            {
                try
                {
                    FunctionsTask.AddTask(FunctionsUsers.GetUserId(SelectedUserDoTask), 
                                          FunctionsUsers.GetUserId(SelectedUserCreateTask),
                                          NameTask,
                                          FunctionsProject.GetIdProject(SelectedProject),
                                          time, Description, SelectedItsOver,
                                          EstimatedTime, AllSpentTime);
                    FunctionsWindow.OpenGoodWindow("Задача добавлена");
                }
                catch
                {
                    FunctionsWindow.OpenErrorWindow("Задача не добавлена");
                }
            }
            else
            {
                try
                {
                    FunctionsTask.SaveEditTask(FunctionsUsers.GetUserId(SelectedUserDoTask),
                                               FunctionsUsers.GetUserId(SelectedUserCreateTask),
                                               NameTask, 
                                               FunctionsProject.GetIdProject(SelectedProject),
                                               time, Description,
                                               SelectedItsOver, SelectedTask.Task,
                                               EstimatedTime, AllSpentTime);
                    FunctionsWindow.OpenGoodWindow("Задача отредактирована");
                }
                catch
                {
                    FunctionsWindow.OpenErrorWindow("Задача не отредактирована");
                }
            }
        }

        private void ExecutedGoUserDoTaskCommand(object obj)
        {
            if (SelectedUserDoTask == null || SelectedUserDoTask == "")
            {
                FunctionsWindow.OpenConfrumWindow("Укажите исполнителя!");
                return;
            }

            if (!FunctionsWindow.OpenDialogWindow("При переходе к исполнителю все не сохраненые изменения будут утеряны\n" +
                                                  "Вы уверены что хотите перейти к исполнителю?"))
            {
                return;
            }

            try
            {
                TaskObjViewModel currentTask;
                string currentCaptionTask;
                if (itEdit)
                {
                    currentTask = new TaskObjViewModel(_currentMain, SelectedTask, _parentsView, _nameParentsView, _iconParentsView);
                    currentCaptionTask = "Редактирование задачи";
                }
                else
                {
                    currentTask = new TaskObjViewModel(_currentMain, _parentsView, _nameParentsView, _iconParentsView);
                    currentCaptionTask = "Добавление задачи";
                }
                _currentMain.CurrentChildView = new UserObjViewModel(_currentMain, FunctionsUsers.GetUserView(SelectedUserDoTask), 
                                                                     currentTask, currentCaptionTask, IconChar.ListCheck);
                _currentMain.Caption = "Редактирование пользователя";
                _currentMain.Icon = IconChar.UserEdit;
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Не удалось перейти к пользователю!");
            }
        }

        private void ExecutedGoUserCreateTaskCommand(object obj)
        {
            if (SelectedUserCreateTask == null || SelectedUserCreateTask == "")
            {
                FunctionsWindow.OpenConfrumWindow("Укажите поручителя!");
                return;
            }

            if (!FunctionsWindow.OpenDialogWindow("При переходе к поручителю все не сохраненые изменения будут утеряны\n" +
                                                  "Вы уверены что хотите перейти к поручителю?"))
            {
                return;
            }

            try
            {
                TaskObjViewModel currentTask;
                string currentCaptionTask;
                if (itEdit)
                {
                    currentTask = new TaskObjViewModel(_currentMain, SelectedTask, _parentsView, _nameParentsView, _iconParentsView);
                    currentCaptionTask = "Редактирование задачи";
                }
                else
                {
                    currentTask = new TaskObjViewModel(_currentMain, _parentsView, _nameParentsView, _iconParentsView);
                    currentCaptionTask = "Добавление задачи";
                }
                _currentMain.CurrentChildView = new UserObjViewModel(_currentMain, FunctionsUsers.GetUserView(SelectedUserCreateTask),
                                                                     currentTask, currentCaptionTask, IconChar.ListCheck);
                _currentMain.Caption = "Редактирование пользователя";
                _currentMain.Icon = IconChar.UserEdit;
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Не удалось перейти к поручителю!");
            }
        }

        private void ExecutedGoProjectCommand(object obj)
        {
            if (SelectedProject == null || SelectedProject == "")
            {
                FunctionsWindow.OpenConfrumWindow("Укажите проект!");
                return;
            }

            if (!FunctionsWindow.OpenDialogWindow("При переходе к проекту все не сохраненые изменения будут утеряны\n" +
                                                  "Вы уверены что хотите перейти к проекту?"))
            {
                return;
            }

            try
            {
                TaskObjViewModel currentTask;
                string currentCaptionTask;

                if (itEdit)
                {
                    currentTask = new TaskObjViewModel(_currentMain, SelectedTask, _parentsView, _nameParentsView, _iconParentsView);
                    currentCaptionTask = "Редактирование задачи";
                }
                else
                {
                    currentTask = new TaskObjViewModel(_currentMain, _parentsView, _nameParentsView, _iconParentsView);
                    currentCaptionTask = "Добавление задачи";
                }

                _currentMain.CurrentChildView = new ProjectObjViewModel(_currentMain, FunctionsProject.GetProjectView(SelectedProject),
                                                                     currentTask, currentCaptionTask, IconChar.ListCheck);
                _currentMain.Caption = "Редактирование проекта";
                _currentMain.Icon = IconChar.Book;
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Не удалось перейти к проекту!");
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
