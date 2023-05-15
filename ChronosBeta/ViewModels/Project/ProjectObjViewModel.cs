using ChronosBeta.BL;
using ChronosBeta.BL.InternalFunctions;
using ChronosBeta.DB;
using ChronosBeta.Model;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChronosBeta.ViewModels
{
    public class ProjectObjViewModel: ViewModelBase
    {
        private static MainViewModel _currentMain;
        private static ViewProject SelectedProject;
        private static bool itEdit;

        public List<string> ResponsibleCustomer { get; set; }
        public string SelectedResponsibleCustomer { get; set; }

        public List<string> ResponsibleOfficer { get; set; }
        public string SelectedResponsibleOfficer { get; set; }

        public string NameProject { get; set; }
        public string Budget { get; set; }
        public string Deadline { get; set; }
        public string Description { get; set; }

        public ICommand Save { get; }
        public ICommand Back { get; }

        public ProjectObjViewModel()
        {
            try
            {
                ResponsibleCustomer = FunctionsCustomer.GetViewCustomer();
                ResponsibleOfficer = FunctionsUsers.GetViewUser();

                //Инициализация команд
                Save = new ViewModelCommand(ExecutedSaveCommand);
                Back = new ViewModelCommand(ExecutedBackCommand);

                if (itEdit)
                    SetProject();
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка инициализации окна проектов");
            }
        }

        public ProjectObjViewModel(MainViewModel main)
        {
            _currentMain = main;
            itEdit = false;
        }

        public ProjectObjViewModel(MainViewModel main, ViewProject selectedProject)
        {
            _currentMain = main;
            SelectedProject = selectedProject;
            itEdit = true;
        }

        private void SetProject()
        {
            SelectedResponsibleCustomer = SelectedProject.ResponsibleCustomer;
            SelectedResponsibleOfficer = SelectedProject.ResponsibleOfficer;

            NameProject = SelectedProject.NameProject;
            Budget = SelectedProject.Project.Budget.ToString();
            Deadline = SelectedProject.Project.Deadline.ToString("M/d/yyyy hh:mm:ss tt");
            Description = SelectedProject.Project.Description;
        }

        private void ExecutedSaveCommand(object obj)
        {
            DateTime time;
            if (NameProject == null || NameProject == "")
            {
                FunctionsWindow.OpenConfrumWindow("Укажите название проекта!");
                return;
            }
            if (SelectedResponsibleOfficer == null || SelectedResponsibleOfficer == "")
            {
                FunctionsWindow.OpenConfrumWindow("Укажите ответственного пользователя!");
                return;
            }
            if (SelectedResponsibleCustomer == null || SelectedResponsibleCustomer == "")
            {
                FunctionsWindow.OpenConfrumWindow("Укажите ответственного заказчика!");
                return;
            }
            try
            {
                Convert.ToInt32(Budget);
            }
            catch
            {
                FunctionsWindow.OpenConfrumWindow("Укажите бюджет в правильном формате!");
                return;
            }
            try
            {
                time = DateTime.ParseExact(Deadline, FunctionsSettingStart.Validformats, FunctionsSettingStart.Provider, DateTimeStyles.None);
            }
            catch
            {
                FunctionsWindow.OpenConfrumWindow("Укажите дату в правильном формате!");
                return;
            }

            if (!itEdit)
            {
                try
                {
                    FunctionsProject.AddProject(NameProject, FunctionsCustomer.GetCustomerId(SelectedResponsibleCustomer),
                                                FunctionsUsers.GetUserId(SelectedResponsibleOfficer), 
                                                Budget, time, Description);
                    FunctionsWindow.OpenGoodWindow("Проект добавлен!");
                }
                catch
                {
                    FunctionsWindow.OpenErrorWindow("Проект не добавлен!!!");
                }
            }
            else
            {
                try
                {
                    FunctionsProject.EditProject(NameProject, FunctionsCustomer.GetCustomerId(SelectedResponsibleCustomer),
                                                 FunctionsUsers.GetUserId(SelectedResponsibleOfficer),
                                                 Budget, time, Description, SelectedProject.Project);
                    FunctionsWindow.OpenGoodWindow("Проект отредактирован!");
                }
                catch
                {
                    FunctionsWindow.OpenErrorWindow("Проект не отредактирован!!!");
                }
            }

        }

        private void ExecutedBackCommand(object obj)
        {
            _currentMain.CurrentChildView = new ProjectsViewMode();
            _currentMain.Caption = "Проекты";
            _currentMain.Icon = IconChar.Book;
        }
    }
}