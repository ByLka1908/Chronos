using ChronosBeta.BL;
using ChronosBeta.Model;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace ChronosBeta.ViewModels
{
    public class JobTitleObjViewModel: ViewModelBase
    {
        private static MainViewModel _currentMain;
        private static ViewJobTitle SelectedJobTitle;
        private static bool itEdit;

        //Параметры

        public string NameJobTitle { get; set; }

        //Команды
        public ICommand Save { get; }
        public ICommand Back { get; }

        public JobTitleObjViewModel()
        {
            try
            {
                //Инициализация команд
                Save = new ViewModelCommand(ExecutedSaveCommand);
                Back = new ViewModelCommand(ExecutedBackCommand);

                if (itEdit)
                    SetJobTitle();
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка инициализация окна");
            }
        }
        public JobTitleObjViewModel(MainViewModel main)
        {
            _currentMain = main;
            itEdit = false;
        }

        public JobTitleObjViewModel(MainViewModel main, ViewJobTitle selectedJobTitle)
        {
            _currentMain = main;
            SelectedJobTitle = selectedJobTitle;
            itEdit = true;
        }

        private void SetJobTitle()
        {
            NameJobTitle = SelectedJobTitle.NameJobTitle;
        }

        private void ExecutedSaveCommand(object obj)
        {
            if (NameJobTitle == null || NameJobTitle == "")
            {
                FunctionsWindow.OpenConfrumWindow("Укажите навазние должности!");
                return;
            }

            if (!itEdit)
            {
                try
                {
                    FunctionsJobTitle.AddJobTitle(NameJobTitle);
                    FunctionsWindow.OpenGoodWindow("Должность добавлена!");
                }
                catch
                {
                    FunctionsWindow.OpenErrorWindow("Должность не добавлена");
                }
            }
            else
            {
                try
                {
                    FunctionsJobTitle.EditJobTitle(NameJobTitle, SelectedJobTitle.Job);
                    FunctionsWindow.OpenGoodWindow("Должность отредактирована!");
                }
                catch
                {
                    FunctionsWindow.OpenErrorWindow("Должность не отредактирована");
                }
            }
        }

        private void ExecutedBackCommand(object obj)
        {
            _currentMain.CurrentChildView = new JobTitleViewModel();
            _currentMain.Caption = "Список должностей";
            _currentMain.Icon = IconChar.UserTag;
        }
    }
}
