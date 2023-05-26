using ChronosBeta.DB;
using ChronosBeta.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace ChronosBeta.BL
{
    public class FunctionsProject
    {
        /// <summary>
        /// Получить представление проекта
        /// </summary>
        /// <param name="project">Название проекта</param>
        /// <returns></returns>
        public static ViewProject GetProjectView(string project)
        {
            CronosEntities entities = new CronosEntities();
            Project projects = entities.Project.Where(x => x.NameProject == project).First();
            return new ViewProject(projects);
        }

        /// <summary>
        /// Получить список проектов
        /// </summary>
        /// <returns></returns>
        public static List<ViewProject> GetProjects()
        {
            CronosEntities entities = new CronosEntities();
            var projects = entities.Project.ToList();
            List<ViewProject> viewProjects = new List<ViewProject>();
            foreach (var project in projects)
                viewProjects.Add(new ViewProject(project));
            return viewProjects;
        }

        /// <summary>
        /// Получить список проектов
        /// </summary>
        /// <returns></returns>
        public static List<string> GetListProjects()
        {
            CronosEntities entities = new CronosEntities();
            return entities.Project.Select(x => x.NameProject).ToList();
        }

        /// <summary>
        /// Получить id проекта
        /// </summary>
        /// <param name="project">Название проекта</param>
        /// <returns></returns>
        public static int GetProjectId(string project)
        {
            CronosEntities entities = new CronosEntities();
            return entities.Project.Where(x => x.NameProject == project).First().id_Project;
        }

        /// <summary>
        /// Добавить проект
        /// </summary>
        /// <param name="NameProject">Название проекта</param>
        /// <param name="ResponsibleCustomer">Id заказчика</param>
        /// <param name="ResponsibleOfficer">Id ответственного</param>
        /// <param name="Budget">Бюджед проекта</param>
        /// <param name="DeadLine">Срок сдачи</param>
        /// <param name="Description">Описание</param>
        /// <param name="SelectedItsOver">Закончена ли задача</param>
        public static void AddProject(string NameProject,     int ResponsibleCustomer,
                                      int ResponsibleOfficer, string Budget,
                                      DateTime DeadLine,      string Description,
                                      string SelectedItsOver)
        {
            Project project = new Project();

            project.NameProject = NameProject;
            project.ResponsibleСustomer = ResponsibleCustomer;
            project.ResponsibleОfficer = ResponsibleOfficer;
            project.Budget = Convert.ToInt32(Budget);
            project.Deadline = DeadLine;
            project.Description = Description;

            if (SelectedItsOver == "Да")
                project.ItsOver = true;
            else
                project.ItsOver = false;

            if (project == null)
            {
                return;
            }

            CronosEntities entities = new CronosEntities();
            entities.Project.Add(project);
            entities.SaveChanges();
        }

        /// <summary>
        /// Изменить проект
        /// </summary>
        /// <param name="NameProject">Название проекта</param>
        /// <param name="ResponsibleCustomer">Id заказчика</param>
        /// <param name="ResponsibleOfficer">Id ответственного</param>
        /// <param name="Budget">Бюджед проекта</param>
        /// <param name="DeadLine">Срок сдачи</param>
        /// <param name="Description">Описание</param>
        /// <param name="SelectedItsOver">Закончена ли задача</param>
        /// <param name="project">Проект</param>
        public static void EditProject(string NameProject,     int ResponsibleCustomer,
                                       int ResponsibleOfficer, string Budget,
                                       DateTime DeadLine,      string Description,
                                       string SelectedItsOver, Project project)
        {
            project.NameProject = NameProject;
            project.ResponsibleСustomer = ResponsibleCustomer;
            project.ResponsibleОfficer = ResponsibleOfficer;
            project.Budget = Convert.ToInt32(Budget);
            project.Deadline = DeadLine;
            project.Description = Description;

            if (SelectedItsOver == "Да")
                project.ItsOver = true;
            else
                project.ItsOver = false;

            if (project == null)
            {
                return;
            }

            CronosEntities entities = new CronosEntities();
            entities.Project.AddOrUpdate(project);
            entities.SaveChanges();
        }

        /// <summary>
        /// Удалить проект
        /// </summary>
        /// <param name="project">Проект</param>
        public static void DeleteProject(Project project)
        {
            CronosEntities entities = new CronosEntities();
            entities.Project.Remove(entities.Project.Find(project.id_Project));
            entities.SaveChanges();
        }
    }
}