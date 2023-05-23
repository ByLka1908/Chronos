using ChronosBeta.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChronosBeta.ViewModels
{
    public  class TaskTimerFilterViewModel: ViewModelBase
    {
        private bool? _dialogResult;
        private static bool isNewFiler = true;

        public bool? DialogResult
        {
            get { return _dialogResult; }
            protected set
            {
                _dialogResult = value;
                OnPropertyChanged("DialogResult");
            }
        }
        public static List<string> UsersSource { get; set; }
        public static string UserSelected { get; set; }
        public static string DatePicker { get; set; }
        public static bool isTaskOver { get; set; }

        public ICommand OK { get; }
        public ICommand Cansel { get; }
        public ICommand Close { get; }

        public TaskTimerFilterViewModel() 
        {
            OK = new ViewModelCommand(ExecutedOKCommand);
            Cansel = new ViewModelCommand(ExecutedCanselCommand);
            Close = new ViewModelCommand(ExecutedCloseCommand);


            List<string> Users = FunctionsUsers.GetViewUser();
            Users.Add("Все");

            if (isNewFiler)
            {
                UsersSource = Users;
                UserSelected = "Все";
            }
        }

        private void ExecutedOKCommand(object obj)
        {
            isNewFiler = false;
            DialogResult = true;
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
