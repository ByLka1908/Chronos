﻿using ChronosBeta.BL;
using ChronosBeta.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChronosBeta.View
{
    /// <summary>
    /// Логика взаимодействия для UsersView.xaml
    /// </summary>
    public partial class UsersView: UserControl
    {
        public ICollectionView CurrentUserList { get; private set; }
        public UsersView()
        {
            InitializeComponent();
            List<ViewUsers> currentUsers = FunctionsUsers.GetUsers();
            CurrentUserList = CollectionViewSource.GetDefaultView(currentUsers);
            DataContext = this;
        }
    }
}
