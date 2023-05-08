using ChronosBeta.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronosBeta.Model
{
    public class ViewProject
    {
        public Project Project { get; set; }
        public int Id { get; set; }
        public string NameProject { get; set; }
        public string ResponsibleCustomer { get; set; }
        public string ResponsibleOfficer { get; set; }

        public ViewProject(Project project)
        {
            Customers customer = project.Customers;
            Users officer = project.Users;

            Project = project;
            Id = project.id_Project;
            NameProject = project.NameProject;

            ResponsibleCustomer = customer.Name + " " + customer.Surname + " " + customer.MiddleName;
            ResponsibleOfficer = officer.Name + " " + officer.Surname + " " + officer.MiddleName;
        }
    }
}
