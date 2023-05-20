using ChronosBeta.DB;
using ChronosBeta.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace ChronosBeta.BL
{
    public class FunctionsProject
    {
        public static List<ViewProject> GetProject()
        {
            CronosEntities entities = new CronosEntities();
            var project = entities.Project.ToList();
            List<ViewProject> view = new List<ViewProject>();
            foreach (var item in project)
                view.Add(new ViewProject(item));
            return view;
        }

        public static List<string> GetViewProject()
        {
            CronosEntities entities = new CronosEntities();
            var users = entities.Project.Select(x => x.NameProject).ToList();
            return users;
        }

        public static void AddProject(string NameProject,     int ResponsibleCustomer,
                                      int ResponsibleOfficer, string Budget,
                                      DateTime DeadLine,        string Description)
        {
            Project project = new Project();

            project.NameProject = NameProject;
            project.ResponsibleСustomer = ResponsibleCustomer;
            project.ResponsibleОfficer = ResponsibleOfficer;
            project.Budget = Convert.ToInt32(Budget);
            project.Deadline = DeadLine;
            project.Description = Description;

            if (project == null)
            {
                return;
            }

            CronosEntities entities = new CronosEntities();
            entities.Project.Add(project);
            entities.SaveChanges();
        }

        public static void EditProject(string NameProject,     int ResponsibleCustomer,
                                       int ResponsibleOfficer, string Budget,
                                       DateTime DeadLine,        string Description, 
                                       Project currentProject)
        {
            currentProject.NameProject = NameProject;
            currentProject.ResponsibleСustomer = ResponsibleCustomer;
            currentProject.ResponsibleОfficer = ResponsibleOfficer;
            currentProject.Budget = Convert.ToInt32(Budget);
            currentProject.Deadline = DeadLine;
            currentProject.Description = Description;

            if (currentProject == null)
            {
                return;
            }

            CronosEntities entities = new CronosEntities();
            entities.Project.AddOrUpdate(currentProject);
            entities.SaveChanges();
        }

        public static int GetIdProject(string project)
        {
            CronosEntities entities = new CronosEntities();
            return entities.Project.Where(x => x.NameProject == project).First().id_Project;
        }

        public static ViewProject GetProjectView(string project)
        {
            CronosEntities entities = new CronosEntities();
            Project currentProject = entities.Project.Where(x => x.NameProject == project).First();
            return new ViewProject(currentProject);
        }

        public static void DeleteProject(Project currentProject)
        {
            CronosEntities entities = new CronosEntities();
            entities.Project.Remove(entities.Project.Find(currentProject.id_Project));
            entities.SaveChanges();
        }
    }
}