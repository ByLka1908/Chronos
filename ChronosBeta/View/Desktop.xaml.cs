using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Shapes;
using System.Diagnostics;
using ChronosBeta.BL;

namespace ChronosBeta.View
{
    /// <summary>
    /// Логика взаимодействия для Desktop.xaml
    /// </summary>
    public partial class Desktop : Window
    {
        List<ViewTask> contentTask = new List<ViewTask>();

        public Desktop()
        {
            InitializeComponent();

            contentTask = FunctionsTask.GetTasks();
            lbcontentTask.ItemsSource = contentTask;
            FunctionsCurrentUser.GetUser(lbName, lbSurname, lbJobTitle, ImageUser);
        }

        private void btStartTimer_Click(object sender, RoutedEventArgs e)
        {
            FunctionsDateTimer.OffOnDateTimer(btStartTimer);
        }


        private void btTest_Click(object sender, RoutedEventArgs e)
        {
            FunctionsTab.SetTab(new UserListAll());
            FunctionsTab.OpenTab(tcTabs);
        }

        private void btListApp_Click(object sender, RoutedEventArgs e)
        {
            FunctionsTab.SetTab(new ListApplication());
            FunctionsTab.OpenTab(tcTabs);
        }
    }
}