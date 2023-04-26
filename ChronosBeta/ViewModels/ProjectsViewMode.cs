using ChronosBeta.BL;
using ChronosBeta.Model;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace ChronosBeta.ViewModels
{
    public class ProjectsViewMode: ViewModelBase
    {
        public ICollectionView CurrentProject { get; private set; }
        public ICommand AddProject { get; }
        public ICommand EditProject { get; }
        public ViewProject SelectedProject { get; set; }

        private static MainViewModel _currentMain;

        public ProjectsViewMode() 
        {
            AddProject = new ViewModelCommand(ExecutedAddProjectCommand);
            EditProject = new ViewModelCommand(ExecutedEditProjectCommand);

            List<ViewProject> currentProject = FunctionsProject.GetProject();
            CurrentProject = CollectionViewSource.GetDefaultView(currentProject);
        }

        public ProjectsViewMode(MainViewModel main)
        {
            _currentMain = main;
        }

        private void ExecutedAddProjectCommand(object obj)
        {
            _currentMain.CurrentChildView = new ProjectObjViewModel(_currentMain);
            _currentMain.Caption = "Добавление проекта";
            _currentMain.Icon = IconChar.UserPlus;
        }
        private void ExecutedEditProjectCommand(object obj)
        {
            _currentMain.CurrentChildView = new ProjectObjViewModel(_currentMain, SelectedProject);
            _currentMain.Caption = "Редактирование проекта";
            _currentMain.Icon = IconChar.UserPlus;
        }
    }
}
