﻿using ChronosBeta.BL;
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

        private void UpdateView()
        {
            List<ViewProject> currentProject = FunctionsProject.GetProject();
            CurrentProject = CollectionViewSource.GetDefaultView(currentProject);
        }

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
        private void ExecutedSearchCommand(object obj)
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

        private void ExecutedRemoveProjectCommand(object obj)
        {
            FunctionsProject.DeleteProject(SelectedProject.Project);
            UpdateView();
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
