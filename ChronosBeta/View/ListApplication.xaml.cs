using ChronosBeta.BL;
using ChronosBeta.InterfaceBL;
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
using System.Xml.Linq;

namespace ChronosBeta.View
{
    public partial class ListApplication : UserControl, ITab
    {
        public ListApplication()
        {
            InitializeComponent();
            string[] CurrentProcess = BL.FunctionsListApplication.GetRunningProcesses();
            lbCurrentRunApp.ItemsSource = CurrentProcess;
        }

        public void ShowTab(TabControl currentTab, TabItem newTabItem, Frame frame)
        {
            ListApplication currentApp = new ListApplication();

            newTabItem.Header = "Список приложений";
            currentApp.Height = frame.Height;
            currentApp.Width = frame.Width;
            frame.Navigate(currentApp);

            newTabItem.Content = frame;
            currentTab.Items.Add(newTabItem);
            currentTab.SelectedItem = newTabItem;
        }

        private void btGetListApplication_Click(object sender, RoutedEventArgs e)
        {
            FunctionsListApplication.CreateJsonListApplication();
        }
    }
}