using ChronosBeta.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChronosBeta.BL
{
    public class ViewProject
    {
        public Projects Projects { get; set; }
        public string Name { get; set; }
        public string UserCreateProject { get; set; }
        public string UserDoProject { get; set; }

        public ViewProject(Projects projects) 
        {
            Projects = projects;
            Name = projects.NameProject;
            UserCreateProject = projects.Users.Name;
            UserDoProject = projects.Users.Name;
        }
    }
}