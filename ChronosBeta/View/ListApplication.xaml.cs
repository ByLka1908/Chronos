using ChronosBeta.BL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace ChronosBeta.View
{
    public partial class ListApplication : UserControl
    {
        public ListApplication()
        {
            InitializeComponent();
            string[] CurrentProcess = BL.ListApplication.GetRunningProcesses();
            lbCurrentRunApp.ItemsSource = CurrentProcess;
        }

        private void btGetListApplication_Click(object sender, RoutedEventArgs e)
        {
            BL.ListApplication.CreateJsonListApplication();
        }
    }
}