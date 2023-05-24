using ChronosBeta.BL;
using ChronosBeta.BL.InternalFunctions;
using ChronosBeta.DB;
using ChronosBeta.Model;
using ChronosBeta.View;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
        private static string nameUserFilter;
        private static string nameCustomerFilter;
        private static string dayPickerFilter;
        private static bool isProjectOverFilter;
        private static bool FilterOn = false;

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
        public ICommand GoProjectEdit { get; }
        public ICommand RemoveProject { get; }
        public ICommand Search { get; }
        public ICommand Filter { get; }

        public ViewProject SelectedProject { get; set; }
        public string CurrentText { get; set; }

        public ProjectsViewMode() 
        {
            AddProject = new ViewModelCommand(ExecutedAddProjectCommand);
            EditProject = new ViewModelCommand(ExecutedEditProjectCommand);
            GoProjectEdit = new ViewModelCommand(ExecutedGoProjectEditCommand);
            RemoveProject = new ViewModelCommand(ExecutedRemoveProjectCommand);
            Search = new ViewModelCommand(ExecutedSearchCommand);
            Filter = new ViewModelCommand(ExecutedFilterCommand);

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
                if (!FilterOn)
                {
                    List<ViewProject> currentProject = FunctionsProject.GetProject();
                    CurrentProject = CollectionViewSource.GetDefaultView(currentProject);
                }
                else
                {
                    List<ViewProject> currentProject = FunctionsProject.GetProject();
                    currentProject = currentProject.Where(x => x.Project.ItsOver == isProjectOverFilter).ToList();

                    if(nameUserFilter != "Все")
                    {
                        int idUser = FunctionsUsers.GetUserId(nameUserFilter);
                        currentProject = currentProject.Where(x => x.Project.ResponsibleОfficer == idUser).ToList();
                    }


                    if (nameCustomerFilter != "Все")
                    {
                        int idCustomer = FunctionsCustomer.GetCustomerId(nameCustomerFilter);
                        currentProject = currentProject.Where(x => x.Project.ResponsibleСustomer == idCustomer).ToList();
                    }

                    if (dayPickerFilter != string.Empty && dayPickerFilter != "" && dayPickerFilter != null)
                    {
                        DateTime day = DateTime.ParseExact(dayPickerFilter, FunctionsSettingStart.Validformats, FunctionsSettingStart.Provider, DateTimeStyles.None);
                        currentProject = currentProject.Where(x => x.Project.Deadline <= day).ToList();
                    }

                    CurrentProject = CollectionViewSource.GetDefaultView(currentProject);
                }
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

        private void ExecutedFilterCommand(object obj)
        {
            try
            {
                if (FunctionsWindow.OpenFilterWindow(new ProjectFilterView()))
                {
                    nameUserFilter = ProjectFilterViewModel.UserSelected;
                    isProjectOverFilter = ProjectFilterViewModel.isProjectOver;
                    dayPickerFilter = ProjectFilterViewModel.DatePicker;
                    nameCustomerFilter = ProjectFilterViewModel.CustomerSelected;
                    FilterOn = true;
                    UpdateView();
                }
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Не удалось открыть фильтер!");
            }
        }

        private void ExecutedGoProjectEditCommand(object obj)
        {
            _currentMain.CurrentChildView = new ProjectObjViewModel(_currentMain, (ViewProject)obj, new ProjectsViewMode(), "Проекты", IconChar.Book);
            _currentMain.Caption = "Редактирование проекта";
            _currentMain.Icon = IconChar.Book;
        }
    }
}