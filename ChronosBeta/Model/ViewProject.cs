using ChronosBeta.DB;

namespace ChronosBeta.Model
{
    public class ViewProject
    {
        public Project Project { get; set; } //Проект
        public int Id { get; set; }
        public string NameProject { get; set; } //Название проекта
        public string ResponsibleCustomer { get; set; } //ФИО заказчика
        public string ResponsibleOfficer { get; set; } //ФИО ответственного

        /// <summary>
        /// Инициализация представления проекта
        /// </summary>
        /// <param name="project">Проект</param>
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