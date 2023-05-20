using ChronosBeta.BL;
using ChronosBeta.Model;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ChronosBeta.ViewModels
{
    public class ProjectsViewMode: ViewModelBase
    {
        private static MainViewModel _currentMain;
        private ICollectionView _currentProject;

        public ICollectionView CurrentProject
    {
            get { return _currentProject; }
            set
            {
                _currentProject = value;
                OnPropertyChanged(nameof(CurrentProject));
            }
        }
        public ICommand AddProject { get; }
        public ICommand EditProject { get; }
        public ICommand RemoveProject { get; }
        public ICommand Search { get; }
        public ViewProject SelectedProject { get; set; }
        public string CurrentText { get; set; }

        public ProjectsViewMode() 
        {
            AddProject = new ViewModelCommand(ExecutedAddProjectCommand);
            EditProject = new ViewModelCommand(ExecutedEditProjectCommand);
            RemoveProject = new ViewModelCommand(ExecutedRemoveProjectCommand);
            Search = new ViewModelCommand(ExecutedSearchCommand);

            UpdateView();
        }

        public ProjectsViewMode(MainViewModel main)
        {
            _currentMain = main;
        }

        private void UpdateView()
        {
            try
            {
                List<ViewProject> currentProject = FunctionsProject.GetProject();
                CurrentProject = CollectionViewSource.GetDefaultView(currentProject);
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка обновления таблицы проектов");
            }
        }

        private void ExecutedSearchCommand(object obj)
        {
            try
            {
                if (CurrentText == null)
                {
                    return;
                }

                if (CurrentText == string.Empty)
                {
                    UpdateView();
                    return;
                }

                List<ViewProject> currentProjects = FunctionsProject.GetProject();
                List<ViewProject> findProjects = currentProjects.Where(x => x.NameProject.ToUpper().StartsWith(CurrentText.ToUpper())).ToList();

                if (findProjects.Count < 1)
                {
                    MessageBox.Show("Обьект не найден");
                    CurrentText = string.Empty;
                    UpdateView();
                    return;
                }
                CurrentProject = CollectionViewSource.GetDefaultView(findProjects);
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка поиска");
            }
        }

        private void ExecutedRemoveProjectCommand(object obj)
        {
            if (!FunctionsWindow.OpenDialogWindow("Вы дествительно хотите удалить проект?"))
                return;

            if (SelectedProject.Project == null)
            {
                FunctionsWindow.OpenConfrumWindow("Проект не выбрана");
                return;
            }

            try
            {
                FunctionsProject.DeleteProject(SelectedProject.Project);
                UpdateView();
                FunctionsWindow.OpenGoodWindow("Проект удален!");
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка удаления проекта");
            }
        }

        private void ExecutedAddProjectCommand(object obj)
        {
            _currentMain.CurrentChildView = new ProjectObjViewModel(_currentMain, new ProjectsViewMode(), "Проекты", IconChar.Book);
            _currentMain.Caption = "Добавление проекта";
            _currentMain.Icon = IconChar.Book;
        }

        private void ExecutedEditProjectCommand(object obj)
        {
            if (SelectedProject == null)
            {
                MessageBox.Show("Проект не выбран");
                return;
            }
            _currentMain.CurrentChildView = new ProjectObjViewModel(_currentMain, SelectedProject, new ProjectsViewMode(), "Проекты", IconChar.Book);
            _currentMain.Caption = "Редактирование проекта";
            _currentMain.Icon = IconChar.Book;
        }
    }
}