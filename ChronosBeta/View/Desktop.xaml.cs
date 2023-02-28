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

namespace ChronosBeta.View
{
    /// <summary>
    /// Логика взаимодействия для Desktop.xaml
    /// </summary>
    public partial class Desktop : Window
    {
        List<BL.ViewProject> contentProject = new List<BL.ViewProject>();
        public Desktop()
        {
            InitializeComponent();
            contentProject = BL.GetProject.GetProjects();
            lbcontentProject.ItemsSource = contentProject;
            BL.CurrentUser.GetUser(lbName, lbSurname, lbJobTitle, ImageUser);
        }
    }
}
