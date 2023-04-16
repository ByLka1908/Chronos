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
    /// Логика взаимодействия для ProjectsView.xaml
    /// </summary>
    public partial class ProjectsView : UserControl
    {
        public ICollectionView CurrentProject { get; private set; }
        public ProjectsView()
        {
            InitializeComponent();
            List<ViewProject> currentProject = FunctionsProject.GetProject();
            CurrentProject = CollectionViewSource.GetDefaultView(currentProject);
            DataContext = this;
        }
    }
}
