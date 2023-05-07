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

namespace ChronosBeta.ViewModels
{
    public class ProjectObjViewModel: ViewModelBase
    {
        private static MainViewModel _currentMain;
        private static ViewProject SelectedProject;
        private static bool itEdit;

        public string NameProject { get; set; }
        public string ResponsibleCustomer { get; set; }
        public string ResponsibleOfficer { get; set; }
        public string Budget { get; set; }
        public string Deadline { get; set; }
        public string Description { get; set; }

        public ICommand Save { get; }
        public ICommand Back { get; }

        public ProjectObjViewModel()
        {
            //Инициализация команд
            Save = new ViewModelCommand(ExecutedSaveCommand);
            Back = new ViewModelCommand(ExecutedBackCommand);

            if (itEdit)
                SetProject();
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
            NameProject = SelectedProject.NameProject;
            ResponsibleCustomer = SelectedProject.ResponsibleCustomer;
            ResponsibleOfficer = SelectedProject.ResponsibleOfficer;
            Budget = SelectedProject.Project.Budget.ToString();
            Deadline = SelectedProject.Project.Deadline.ToString();
            Description = SelectedProject.Project.Description;
        }

        private void ExecutedSaveCommand(object obj)
        {
            if (!itEdit)
            {
                try
                {
                    FunctionsProject.AddProject(NameProject, ResponsibleCustomer, ResponsibleOfficer, 
                                                Budget, Deadline, Description);
                    MessageBox.Show("Проект добавлена");
                }
                catch
                {
                    MessageBox.Show("Проект не добавлена");
                }
            }
            else
            {
                try
                {
                    FunctionsProject.EditProject(NameProject, ResponsibleCustomer, ResponsibleOfficer,
                                                 Budget, Deadline, Description, SelectedProject.Project);
                    MessageBox.Show("Проект отредактирована");
                }
                catch
                {
                    MessageBox.Show("Проект не отредактирована");
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