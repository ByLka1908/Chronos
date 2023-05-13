using FontAwesome.Sharp;
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
using System.Windows.Shapes;

namespace ChronosBeta.View
{
    /// <summary>
    /// Логика взаимодействия для ErrorView.xaml
    /// </summary>
    public partial class InfoView : Window
    {
        public InfoView(string currText, string TextLabel, string TextTitle, IconChar icon, Brush colorIcon)
        {
            InitializeComponent();
            CurrentText.Text = currText;
            textLabel.Text = TextLabel;
            textTitle.Text = TextTitle;
            ImageWindow.Icon = icon;
            ImageWindow.Foreground = colorIcon;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}