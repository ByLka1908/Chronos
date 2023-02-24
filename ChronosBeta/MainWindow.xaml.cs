using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using ChronosBeta.BL;

namespace ChronosBeta
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string[] ListProcess = GetRunningProcesses();
        }

        public static string[] GetRunningProcesses()
        {
            var processes = Process.GetProcesses().Where(p => !string.IsNullOrEmpty(p.MainWindowTitle)).Select(p => p.ProcessName + " - " + p.MainWindowTitle).ToArray();
            return processes;
        }

        private void btGo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL.Auntificator.Auntification(tbLogin.Text, tbPassword.Text);
                WindowOpen.OpenNewWindow(this, new View.Desktop());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        private void btExist_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
