using ChronosBeta.BL;
using ChronosBeta.Model;
using FontAwesome.Sharp;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace ChronosBeta.ViewModels
{
    public class ScreenshotViewModel: ViewModelBase
    {
        #region Параметры
        private ICollectionView      _currentScreenshot; //Таблица снимкой экрана
        private static MainViewModel _currentMain; //Основное окно
        private static ViewDateTimer _currentDateTimer; //Представления отметки рабочего дня
        #endregion

        #region Свойства
        public ICollectionView CurrentScreenshot
        {
            get { return _currentScreenshot; }
            set
            {
                _currentScreenshot = value;
                OnPropertyChanged(nameof(CurrentScreenshot));
            }
        }
        #endregion

        #region Команды
        public ICommand Back { get; }
        #endregion

        #region Конструкторы
        public ScreenshotViewModel()
        {
            try
            {
                Back = new ViewModelCommand(ExecutedBackCommand);

                UpdateView();
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка инициализации окна списка рабочего");
            }
        }

        /// <summary>
        /// Инизиализация окна снимков экрана
        /// </summary>
        /// <param name="main">Основное окно</param>
        /// <param name="selectedDateTimer">Выбранная отметка рабочего дня</param>
        public ScreenshotViewModel(MainViewModel main, ViewDateTimer selectedDateTimer)
        {
            _currentDateTimer = selectedDateTimer;
            _currentMain = main;
        }
        #endregion

        #region Методы
        /// <summary>
        /// Обновить таблицу окна
        /// </summary>
        private void UpdateView()
        {
            List<ViewScreenshot> currentScren = FunctionsImage.GetScreenshots(_currentDateTimer.Id);
            CurrentScreenshot = CollectionViewSource.GetDefaultView(currentScren);
        }

        /// <summary>
        /// Вернуться в прошлое окно
        /// </summary>
        /// <param name="obj"></param>
        private void ExecutedBackCommand(object obj)
        {
            _currentMain.CurrentChildView = new DateTimerObjViewModel();
            _currentMain.Caption = "Список запущеный программ";
            _currentMain.Icon = IconChar.UserClock;
        }
        #endregion
    }
}