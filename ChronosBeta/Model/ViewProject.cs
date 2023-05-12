using ChronosBeta.DB;

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
            Users officer      = project.Users;
            Customers customer = project.Customers;

            Id          = project.id_Project;
            Project     = project;
            NameProject = project.NameProject;

            ResponsibleCustomer = customer.Name + " " + customer.Surname + " " + customer.MiddleName;
            ResponsibleOfficer  = officer.Name + " " + officer.Surname + " " + officer.MiddleName;
        }
    }
}
