using System;
using System.Net;
using System.Security.Principal;
using System.Security;
using System.Threading;
using System.Windows.Input;
using ChronosBeta.BL;
using ChronosBeta.BL.InternalFunctions;

namespace ChronosBeta.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        #region Параметры
        private string _username; //Логин
        private SecureString _password; //Пароль
        private string _errorMessage; //Сообщение об ошибке
        private bool _isViewVisible = true;
        #endregion

        #region Свойства
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public SecureString Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;
            }
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }
        public bool isRememberUser { get; set; } // Запомнить пользователя
        #endregion

        #region Команды
        public ICommand LoginCommand { get; } 
        public ICommand RecoverPasswordCommand { get; }
        #endregion

        #region Конструктор
        public LoginViewModel()
        {
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPassCommand("", ""));
        }
        #endregion

        #region Методы
        /// <summary>
        /// Определить минимальную длину логину и пароля
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 ||
                Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;
        }

        /// <summary>
        /// Аунтфикация в программу
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteLoginCommand(object obj)
        {
            try
            {
                var isValidUser = FunctionsAuntificator.Auntification(new NetworkCredential(Username, Password));
                if (isValidUser)
                {
                    if (isRememberUser)
                    {
                        NetworkCredential credential = new NetworkCredential(Username, Password);
                        FunctionsSettingStart.setting.RememberUser = true;
                        FunctionsSettingStart.setting.NameUser = credential.UserName;
                        FunctionsSettingStart.setting.PasswordUser = credential.Password;
                        FunctionsSettingStart.Update();
                    }

                    Thread.CurrentPrincipal = new GenericPrincipal(
                        new GenericIdentity(Username), null);
                    IsViewVisible = false;
                }
                else
                {
                    ErrorMessage = "* Неверный Логин или Пароль";
                }
            }
            catch
            {
                FunctionsWindow.OpenErrorWindow("Ошибка аунтификации");
            }
        }

        /// <summary>
        /// Ошибка
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}