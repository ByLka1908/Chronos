using ChronosBeta.BL;
using ChronosBeta.DB;
using ChronosBeta.Model;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace ChronosBeta.ViewModels
{
    public class TaskObjViewModel: ViewModelBase
    {
        private static MainViewModel _currentMain;

        public string UserDoTask { get; set; }
        public string UserCreateTask { get; set; }
        public string NameTask { get; set; }
        public string Project { get; set; }
        public string DeadLine { get; set; }
        public string Description { get; set; }
        public string SelectedItsOver { get; set; }
        public List<string> ItsOver { get; set; }
        private ViewTask SelectedTask;

        public ICommand Save { get; }
        public ICommand Back { get; }

        public TaskObjViewModel()
        {

            //Инициализация команд
            Save = new ViewModelCommand(ExecutedSaveCommand);
            Back = new ViewModelCommand(ExecutedBackCommand);
        }
        public TaskObjViewModel(MainViewModel main)
        {
            _currentMain = main;
        }
        public TaskObjViewModel(MainViewModel main, ViewTask selectedTask)
        {
            _currentMain = main;
            SelectedTask = selectedTask;
        }

        private void ExecutedSaveCommand(object obj)
        {

        }

        private void ExecutedBackCommand(object obj)
        {
            _currentMain.CurrentChildView = new TaskViewModel();
            _currentMain.Caption = "Задачи";
            _currentMain.Icon = IconChar.Users;
        }

    }
}
