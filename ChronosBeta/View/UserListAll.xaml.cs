using ChronosBeta.BL;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для UserListAll.xaml
    /// </summary>
    public partial class UserListAll : UserControl, ITab
    {
        public UserListAll()
        {
            InitializeComponent();
        }

        public void ShowTab(TabControl currentTab, TabItem newTabItem, Frame frame)
        {
            UserListAll currentUser = new UserListAll();
            
            newTabItem.Header = "Список пользователей";
            currentUser.Height = frame.Height;
            currentUser.Width = frame.Width;
            frame.Navigate(currentUser);

            newTabItem.Content = frame;
            currentTab.Items.Add(newTabItem);
            currentTab.SelectedItem = newTabItem;
        }
    }
}
