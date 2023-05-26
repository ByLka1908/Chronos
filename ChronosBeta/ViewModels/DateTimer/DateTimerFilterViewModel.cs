using ChronosBeta.BL;
using System.Windows.Input;

namespace ChronosBeta.ViewModels
{
    public class DateTimerFilterViewModel: ViewModelBase
    {
        private bool? _dialogResult;

        public bool? DialogResult
        {
            get { return _dialogResult; }
            protected set
            {
                _dialogResult = value;
                OnPropertyChanged("DialogResult");
            }
        }
        public static string DatePicker { get; set; }

        public ICommand OK { get; }
        public ICommand Cansel { get; }
        public ICommand Close { get; }
        public ICommand DeleteDate { get; }

        public DateTimerFilterViewModel() 
        {
            OK = new ViewModelCommand(ExecutedOKCommand);
            Cansel = new ViewModelCommand(ExecutedCanselCommand);
            Close = new ViewModelCommand(ExecutedCloseCommand);
            DeleteDate = new ViewModelCommand(ExecutedDeleteDateCommand);
        }

        private void ExecutedOKCommand(object obj)
        {
            DialogResult = true;
        }

        private void ExecutedDeleteDateCommand(object obj)
        {
            DatePicker = string.Empty;
            FunctionsWindow.OpenGoodWindow("Дата очищена!");
        }

        private void ExecutedCanselCommand(object obj)
        {
            DialogResult = false;
        }

        private void ExecutedCloseCommand(object obj)
        {
            DialogResult = false;
        }
    }
}
