using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronosBeta.Model
{
    public class ViewProject
    {
        public DB.Project Project { get; set; }
        public int Id { get; set; }
        public string NameProject { get; set; }
        public string ResponsibleCustomer { get; set; }
        public string ResponsibleOfficer { get; set; }

        public ViewProject(DB.Project project)
        {
            Project = project;
            Id = project.id_Project;
            NameProject = project.NameProject;
            ResponsibleCustomer = project.ResponsibleСustomer;
            ResponsibleOfficer = project.Users.Name;
        }
    }
}
