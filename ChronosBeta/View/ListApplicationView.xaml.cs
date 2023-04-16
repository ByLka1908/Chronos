using ChronosBeta.BL;
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
    /// Логика взаимодействия для ListApplicationView.xaml
    /// </summary>
    public partial class ListApplicationView : UserControl
    {
        public ICollectionView CurrentListApplication { get; private set; }
        public ListApplicationView()
        {
            InitializeComponent();
            List<ViewListApplication> currentApp = FunctionsListApplication.GetListProcesses();
            CurrentListApplication = CollectionViewSource.GetDefaultView(currentApp);
            DataContext = this;
        }
    }
}
