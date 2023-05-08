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

        public string UserDoTask { get; set; }
        public string UserCreateTask { get; set; }
        public string NameTask { get; set; }
        public string Project { get; set; }
        public string DeadLine { get; set; }
        public string Description { get; set; }
        public string SelectedItsOver { get; set; }
        public List<string> ItsOver { get; set; }

        public ICommand Save { get; }
        public ICommand Back { get; }

        public TaskObjViewModel()
        {
            //Инициализация команд
            Save = new ViewModelCommand(ExecutedSaveCommand);
            Back = new ViewModelCommand(ExecutedBackCommand);

            if (itEdit)
                SetTask();
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
            UserDoTask = SelectedTask.UserDoTask;
            UserCreateTask = SelectedTask.UserCreateTask;
            Project = SelectedTask.Task.Project1.NameProject;
            DeadLine = SelectedTask.Task.Deadline.ToString();
            Description = SelectedTask.Task.Description;
            ItsOver = itsOver;   
        }

        private void ExecutedSaveCommand(object obj)
        {
            if (!itEdit)
            {
                try
                {
                    FunctionsTask.AddTask(UserDoTask, UserCreateTask, NameTask, Project,
                                          DeadLine, Description, SelectedItsOver);
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
                    FunctionsTask.SaveEditTask(UserDoTask, UserCreateTask, NameTask,
                                               Project, DeadLine, Description,
                                               SelectedItsOver, SelectedTask.Task);
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
