using ChronosBeta.BL;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace ChronosBeta.ViewModels
{
    public class UserObjViewModel: ViewModelBase
    {
        //Параметры
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Skype { get; set; }
        public string SelectedJobTitle { get; set; }
        public List<string> JobTitle  { get; set; }
        private static MainViewModel _currentMain;

        //Команды
        public ICommand Save { get; }
        public ICommand Back { get; }

        public UserObjViewModel() 
        {
            JobTitle = FunctionsJobTitle.GetJobTitle();

            //Инициализация команд
            Save = new ViewModelCommand(ExecutedSaveCommand);
            Back = new ViewModelCommand(ExecutedBackCommand);
        }
        public UserObjViewModel(MainViewModel main)
        {
            _currentMain = main;
        }

        private void ExecutedSaveCommand(object obj)
        {
            try
            {
                FunctionsJobTitle.Add(Name, Surname, Login, Password, Phone, Skype, SelectedJobTitle);
            }
            catch
            {
                MessageBox.Show("Пользователь добавлен");
            }
            
        }
        private void ExecutedBackCommand(object obj)
        {
            _currentMain.CurrentChildView = new UsersViewModel();
            _currentMain.Caption = "Пользователи";
            _currentMain.Icon = IconChar.Users;
        }
    }
}
