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
        List<ViewTask> contentProject = new List<ViewTask>();
        private bool joobTime = false;
        TimeSpan timeStart = new TimeSpan();
        TimeSpan timeEnd = new TimeSpan();

        public Desktop()
        {
            InitializeComponent();

            contentProject = BL.GetTask.Task();
            lbcontentProject.ItemsSource = contentProject;
            BL.CurrentUser.GetUser(lbName, lbSurname, lbJobTitle, ImageUser);
        }

        private void btStartTimer_Click(object sender, RoutedEventArgs e)
        {

            ImageBrush myImageBrush = new ImageBrush();

            if (joobTime)
            {
                myImageBrush.ImageSource = new BitmapImage(new Uri("F:\\Projects\\VisualStudioSource\\ChronosBeta\\ChronosBeta\\Image\\Off.png", UriKind.Relative));
                btStartTimer.Background = myImageBrush;
                joobTime = false;

                timeEnd = DateTime.Now.TimeOfDay;
                DateTimerAddAndChange.AddDateTimer(timeStart, timeEnd);
            }
            else
            {
                myImageBrush.ImageSource = new BitmapImage(new Uri("F:\\Projects\\VisualStudioSource\\ChronosBeta\\ChronosBeta\\Image\\On.png", UriKind.Relative));
                btStartTimer.Background = myImageBrush;
                joobTime = true;

                timeStart = DateTime.Now.TimeOfDay;
                BL.ListApplication.CreateJsonListApplication();
            }
        }


        private void btTest_Click(object sender, RoutedEventArgs e)
        {
            Tab.OpenTab("UserListAll", tcTabs);
        }

        private void btListApp_Click(object sender, RoutedEventArgs e)
        {
            Tab.OpenTab("ListApplication", tcTabs);
        }
    }
}