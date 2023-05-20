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
        private static ViewModelBase _parentsView;
        private static string _nameParentsView;
        private static IconChar _iconParentsView;
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
        public ICommand GoResponsibleCustomer { get; }
        public ICommand GoResponsibleOfficer { get; }

        public ProjectObjViewModel()
        {
            try
            {
                ResponsibleCustomer = FunctionsCustomer.GetViewCustomer();
                ResponsibleOfficer = FunctionsUsers.GetViewUser();

                //Инициализация команд
                Save = new ViewModelCommand(ExecutedSaveCommand);
                Back = new ViewModelCommand(ExecutedBackCommand);
                GoResponsibleCustomer = new ViewModelCommand(ExecutedGoResponsibleCustomerCommand);
                GoResponsibleOfficer = new ViewModelCommand(ExecutedGoResponsibleOfficerCommand);

                if (itEdit)
                    SetProject();
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка инициализации окна проектов");
            }
        }

        public ProjectObjViewModel(MainViewModel main, ViewModelBase parentsView, string nameParentsView, IconChar iconParentsView)
        {
            _iconParentsView = iconParentsView;
            _nameParentsView = nameParentsView;
            _parentsView = parentsView;
            _currentMain = main;
            itEdit = false;
        }

        public ProjectObjViewModel(MainViewModel main, ViewProject selectedProject, ViewModelBase parentsView, string nameParentsView, IconChar iconParentsView)
        {
            _iconParentsView = iconParentsView;
            _nameParentsView = nameParentsView;
            _parentsView = parentsView;
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

        private void ExecutedGoResponsibleCustomerCommand(object obj)
        {
            if (SelectedResponsibleCustomer == null || SelectedResponsibleCustomer == "")
            {
                FunctionsWindow.OpenConfrumWindow("Укажите заказчика!");
                return;
            }

            if (!FunctionsWindow.OpenDialogWindow("При переходе к поручителю все не сохраненые изменения будут утеряны\n" +
                                                  "Вы уверены что хотите перейти к заказчику?"))
            {
                return;
            }

            try
            {
                ProjectObjViewModel currentProject;
                string currentCaptionProject;
                if (itEdit)
                {
                    currentProject = new ProjectObjViewModel(_currentMain, SelectedProject, _parentsView, _nameParentsView, _iconParentsView);
                    currentCaptionProject = "Редактирование проекта";
                }
                else
                {
                    currentProject = new ProjectObjViewModel(_currentMain, _parentsView, _nameParentsView, _iconParentsView);
                    currentCaptionProject = "Добавление проекта";
                }
                _currentMain.CurrentChildView = new CustomerObjViewModel(_currentMain, FunctionsCustomer.GetCustomerView(SelectedResponsibleCustomer),
                                                                         currentProject, currentCaptionProject, IconChar.Book);
                _currentMain.Caption = "Редактирование заказчика";
                _currentMain.Icon = IconChar.AddressBook;
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Не удалось перейти к заказчику!");
            }
        }

        private void ExecutedGoResponsibleOfficerCommand(object obj)
        {
            if (SelectedResponsibleOfficer == null || SelectedResponsibleOfficer == "")
            {
                FunctionsWindow.OpenConfrumWindow("Укажите ответственного!");
                return;
            }

            if (!FunctionsWindow.OpenDialogWindow("При переходе к поручителю все не сохраненые изменения будут утеряны\n" +
                                                  "Вы уверены что хотите перейти к ответственному?"))
            {
                return;
            }

            try
            {
                ProjectObjViewModel currentProject;
                string currentCaptionProject;
                if (itEdit)
                {
                    currentProject = new ProjectObjViewModel(_currentMain, SelectedProject, _parentsView, _nameParentsView, _iconParentsView);
                    currentCaptionProject = "Редактирование проекта";
                }
                else
                {
                    currentProject = new ProjectObjViewModel(_currentMain, _parentsView, _nameParentsView, _iconParentsView);
                    currentCaptionProject = "Добавление проекта";
                }
                _currentMain.CurrentChildView = new UserObjViewModel(_currentMain, FunctionsUsers.GetUserView(SelectedResponsibleOfficer),
                                                                     currentProject, currentCaptionProject, IconChar.Book);
                _currentMain.Caption = "Редактирование пользователя";
                _currentMain.Icon = IconChar.UserEdit;
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Не удалось перейти к ответственному!");
            }
        }

        private void ExecutedBackCommand(object obj)
        {
            _currentMain.CurrentChildView = _parentsView;
            _currentMain.Caption = _nameParentsView;
            _currentMain.Icon = _iconParentsView;
        }
    }
}