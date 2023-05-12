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
    public class ListApplicationViewModel: ViewModelBase
    {
        private static MainViewModel _currentMain;
        public ICollectionView CurrentListApplication { get; private set; }

        public ICommand Back { get; }

        public ListApplicationViewModel() 
        {
            try
            {
                Back = new ViewModelCommand(ExecutedBackCommand);

                List<ViewListApplication> currentApp = FunctionsListApplication.GetListProcesses();
                CurrentListApplication = CollectionViewSource.GetDefaultView(currentApp);
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка инициализации окна");
            }
        }

        public ListApplicationViewModel(MainViewModel main)
        {
            _currentMain = main;
        }

        private void ExecutedBackCommand(object obj)
        {
            _currentMain.CurrentChildView = new SettingViewModel();
            _currentMain.Caption = "Настройки";
            _currentMain.Icon = IconChar.Gears;
        }
    }
}