﻿using ChronosBeta.BL;
using ChronosBeta.DB;
using ChronosBeta.Model;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace ChronosBeta.ViewModels
{
    public class UserObjViewModel: ViewModelBase
    {   
        private static MainViewModel _currentMain;
        private static ViewUsers SelectedUser;
        private static ImageSource _imageUser;
        private static bool itEdit;

        //Параметры
        public ImageSource ImageUser
        {
            get { return _imageUser; }
            set
            {
                _imageUser = value;
                OnPropertyChanged(nameof(ImageUser));
            }
        }

        public string Name     { get; set; } 
        public string Surname  { get; set; }
        public string Login    { get; set; }
        public string Password { get; set; }
        public string Phone    { get; set; }
        public string Skype    { get; set; }

        public List<string> JobTitle  { get; set; }
        public string SelectedJobTitle { get; set; }

        //Команды
        public ICommand Save { get; }
        public ICommand Back { get; }
        public ICommand SelectedUserImage { get; }

        public UserObjViewModel() 
        {
            JobTitle = FunctionsJobTitle.GetJobTitle();

            //Инициализация команд
            Save = new ViewModelCommand(ExecutedSaveCommand);
            Back = new ViewModelCommand(ExecutedBackCommand);
            SelectedUserImage = new ViewModelCommand(ExecutedSelectedUserImageCommand);

            if (itEdit)
                SetUser();
        }
        public UserObjViewModel(MainViewModel main)
        {
            _currentMain = main;
            itEdit = false;
        }

        public UserObjViewModel(MainViewModel main, ViewUsers selectedUser)
        {
            _currentMain = main;
            SelectedUser = selectedUser;
            itEdit = true;
        }

        private void SetUser()
        {
            Name = SelectedUser.Name;
            Surname = SelectedUser.Surname;
            Login = SelectedUser.User.Login;
            Password = SelectedUser.User.Password;
            Phone = SelectedUser.Phone;
            Skype = SelectedUser.Skype;
            SelectedJobTitle = SelectedUser.JobTitle;

            if (SelectedUser.ImageUser != null)
                ImageUser = SelectedUser.ImageUser;
            else
                ImageUser = FunctionsImage.GetImage();
        }

        private void ExecutedSaveCommand(object obj)
        {
            if(!itEdit)
            {
                try
                {
                    FunctionsUsers.AddUser(Name, Surname, Login, Password, Phone, Skype,
                                           SelectedJobTitle, ImageUser);
                    MessageBox.Show("Пользователь добавлен");
                }
                catch
                {
                    MessageBox.Show("Пользователь не добавлен");
                }
            }
            else
            {
                try
                {
                    FunctionsUsers.SaveEditUser(Name, Surname, Login, Password, Phone, Skype,
                                                SelectedJobTitle, SelectedUser, ImageUser);
                    MessageBox.Show("Пользователь отредактирован");
                }
                catch
                {
                    MessageBox.Show("Пользователь не отредактирован");
                }
            }
        }

        private void ExecutedSelectedUserImageCommand(object obj)
        {
            var result = FunctionsImage.SetImage();
            if (result == null)
                return;

            ImageUser = result;
        }

        private void ExecutedBackCommand(object obj)
        {
            _currentMain.CurrentChildView = new UsersViewModel();
            _currentMain.Caption = "Пользователи";
            _currentMain.Icon = IconChar.Users;
        }
    }
}