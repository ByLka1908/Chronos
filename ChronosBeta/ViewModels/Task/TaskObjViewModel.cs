using ChronosBeta.BL;
using ChronosBeta.BL.InternalFunctions;
using ChronosBeta.DB;
using ChronosBeta.Model;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        private static ViewTask SelectedTask;
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

        public TaskObjViewModel()
        {
            try
            {
                UserCreateTask = FunctionsUsers.GetViewUser();
                UserDoTask = FunctionsUsers.GetViewUser();
                Project = FunctionsProject.GetViewProject();

                //Инициализация команд
                Save = new ViewModelCommand(ExecutedSaveCommand);
                Back = new ViewModelCommand(ExecutedBackCommand);

                if (itEdit)
                    SetTask();
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка инициализации окна");
                return;
            }
        }

        public TaskObjViewModel(MainViewModel main)
        {
            _currentMain = main;
            itEdit = false;
        }

        public TaskObjViewModel(MainViewModel main, ViewTask selectedTask)
        {
            _currentMain = main;
            SelectedTask = selectedTask;
            itEdit = true;
        }

        private void SetTask()
        {
            List<string> itsOver = new List<string>();
            itsOver.Add("Да");
            itsOver.Add("Нет");

            NameTask = SelectedTask.Name;
            EstimatedTime = SelectedTask.EstimatedTime;
            AllSpentTime = SelectedTask.Task.AllSpentTime.ToString();
            SelectedUserDoTask = SelectedTask.UserDoTask;
            SelectedUserCreateTask = SelectedTask.UserCreateTask;
            SelectedProject = SelectedTask.Task.Project1.NameProject;
            DeadLine = SelectedTask.Deadline;
            Description = SelectedTask.Task.Description;
            ItsOver = itsOver;   
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

        private void ExecutedBackCommand(object obj)
        {
            _currentMain.CurrentChildView = new TaskViewModel();
            _currentMain.Caption = "Задачи";
            _currentMain.Icon = IconChar.ListCheck;
        }
    }
}
