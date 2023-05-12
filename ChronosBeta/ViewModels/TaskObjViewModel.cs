using ChronosBeta.BL;
using ChronosBeta.DB;
using ChronosBeta.Model;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public string NameTask { get; set; }
        public string EstimatedTime { get; set; }
        public string AllSpentTime { get; set; }
        public string DeadLine { get; set; }
        public string Description { get; set; }
        public string SelectedItsOver { get; set; }
        public List<string> ItsOver { get; set; }

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
            if (!itEdit)
            {
                try
                {
                    FunctionsTask.AddTask(FunctionsUsers.GetUserId(SelectedUserDoTask), 
                                          FunctionsUsers.GetUserId(SelectedUserCreateTask),
                                          NameTask,
                                          FunctionsProject.GetIdProject(SelectedProject),
                                          DeadLine, Description, SelectedItsOver,
                                          EstimatedTime, AllSpentTime);
                    MessageBox.Show("Задача добавлена");
                }
                catch
                {
                    MessageBox.Show("Задача не добавлена");
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
                                               DeadLine, Description,
                                               SelectedItsOver, SelectedTask.Task,
                                               EstimatedTime, AllSpentTime);
                    MessageBox.Show("Задача отредактирована");
                }
                catch
                {
                    MessageBox.Show("Задача не отредактирована");
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
