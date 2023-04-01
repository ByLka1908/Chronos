using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    public partial class MainWindow : Window
    {
        public event EventHandler Auntificator;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btGo_Click(object sender, RoutedEventArgs e)
        {
            // Login: gurzhi Password: warcraft3
            try
            {
                FunctionsAuntificator.Auntification(tbLogin.Text, tbPassword.Text);
               FunctionsWindow.OpenNewWindow(this, new View.Desktop());
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
