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
    public class JobTitleViewModel: ViewModelBase
    {
        private ICollectionView _currentJobTitleList;
        private static MainViewModel _currentMain;

        public ICollectionView CurrentJobTitleList
        {
            get { return _currentJobTitleList; }
            set
            {
                _currentJobTitleList = value;
                OnPropertyChanged(nameof(CurrentJobTitleList));
            }
        }

        public ICommand AddJobTitle { get; }
        public ICommand EditJobTitle { get; }
        public ICommand RemoveJobTitle { get; }
        public ICommand Search { get; }
        public ICommand Back { get; }
        public ViewJobTitle SelectedJobTitle { get; set; }
        public string CurrentText { get; set; }

        public JobTitleViewModel()
        {
            AddJobTitle = new ViewModelCommand(ExecutedAddJobTitleCommand);
            EditJobTitle = new ViewModelCommand(ExecutedEditJobTitleCommand);
            RemoveJobTitle = new ViewModelCommand(ExecutedRemoveJobTitleCommand);
            Search = new ViewModelCommand(ExecutedSearchCommand);
            Back = new ViewModelCommand(ExecutedBackCommand);

            UpdateView();
        }

        public JobTitleViewModel(MainViewModel main)
        {
            _currentMain = main;
        }

        private void UpdateView()
        {
            try
            {
                List<ViewJobTitle> currentJobTitles = FunctionsJobTitle.GetJobTitles();
                CurrentJobTitleList = CollectionViewSource.GetDefaultView(currentJobTitles);
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка обновления таблицы");
            }
        }

        private void ExecutedSearchCommand(object obj)
        {
            if (CurrentText == null)
            {
                FunctionsWindow.OpenConfrumWindow("Поле поиска пустое");
                return;
            }

            if (CurrentText == string.Empty)
            {
                UpdateView();
                return;
            }

            try
            {
                List<ViewJobTitle> currentJobTitles = FunctionsJobTitle.GetJobTitles();
                List<ViewJobTitle> findJob = currentJobTitles.Where(x => x.NameJobTitle.ToUpper().StartsWith(CurrentText.ToUpper())).ToList();

                if (findJob.Count < 1)
                {
                    FunctionsWindow.OpenConfrumWindow("Обьект не найден");
                    CurrentText = string.Empty;
                    UpdateView();
                    return;
                }

                CurrentJobTitleList = CollectionViewSource.GetDefaultView(findJob);
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка поиска");
            }
        }

        private void ExecutedAddJobTitleCommand(object obj)
        {
            _currentMain.CurrentChildView = new JobTitleObjViewModel(_currentMain);
            _currentMain.Caption = "Добавление должности";
            _currentMain.Icon = IconChar.UserTag;
        }

        private void ExecutedEditJobTitleCommand(object obj)
        {
            if (SelectedJobTitle == null)
            {
                FunctionsWindow.OpenConfrumWindow("Пользователь не выбран");
                return;
            }
            _currentMain.CurrentChildView = new JobTitleObjViewModel(_currentMain, SelectedJobTitle);
            _currentMain.Caption = "Редактирование должности";
            _currentMain.Icon = IconChar.UserTag;
        }

        private void ExecutedRemoveJobTitleCommand(object obj)
        {

            if (SelectedJobTitle.Job == null)
            {
                FunctionsWindow.OpenConfrumWindow("Должность не выбрана");
                return;
            }

            try
            {
                List<ViewJobTitle> currentJobTitles = FunctionsJobTitle.GetJobTitles();
                if (currentJobTitles.Count == 1)
                {
                    FunctionsWindow.OpenConfrumWindow("Вы не можете удалить последнию должность!");
                    return;
                }
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка получения текущих должностей!");
                return;
            }

            if (!FunctionsWindow.OpenDialogWindow("Вы действиельно хотите удалить должность?\n" +
                                                  "Будут удалены все связанные пользователи!!!"))
                return;

            try
            {
                FunctionsJobTitle.DeleteJobTitle(SelectedJobTitle.Job);
                UpdateView();
                FunctionsWindow.OpenGoodWindow("Должность удалена");
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка удаления должности!");
            }
        }

        private void ExecutedBackCommand(object obj)
        {
            _currentMain.CurrentChildView = new SettingViewModel();
            _currentMain.Caption = "Настройки";
            _currentMain.Icon = IconChar.Gears;
        }
    }
}