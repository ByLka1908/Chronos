using ChronosBeta.DB;
using ChronosBeta.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;

namespace ChronosBeta.BL
{
    public class FunctionsProject
    {
        public static List<ViewProject> GetProject()
        {
            try
            {
                DB.CronosEntities entities = new DB.CronosEntities();
                var project = entities.Project.ToList();
                List<ViewProject> view = new List<ViewProject>();
                foreach (var item in project)
                    view.Add(new ViewProject(item));
                return view;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static List<string> GetViewProject()
        {
            try
            {
                CronosEntities entities = new CronosEntities();
                var users = entities.Project.Select(x => x.NameProject).ToList();
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static bool AddProject(string NameProject,     int ResponsibleCustomer,
                                      int ResponsibleOfficer, string Budget,
                                      string DeadLine,        string Description)
        {
            Project project = new Project();
            try
            {
                project.NameProject = NameProject;
                project.ResponsibleСustomer = ResponsibleCustomer;
                project.ResponsibleОfficer = ResponsibleOfficer;
                project.Budget = Convert.ToInt32(Budget);
                project.Deadline = DateTime.Parse(DeadLine);
                project.Description = Description;
            }
            catch
            {
                throw new Exception("Ошибка иницилизации добавления");
            }
            if (project == null)
            {
                return false;
            }
            try
            {
                CronosEntities entities = new CronosEntities();
                entities.Project.Add(project);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                throw new Exception("Ошибка добавлении проекта");
            }
        }

        public static bool EditProject(string NameProject,     int ResponsibleCustomer,
                                       int ResponsibleOfficer, string Budget,
                                       string DeadLine,        string Description, 
                                       Project currentProject)
        {
            try
            {
                currentProject.NameProject = NameProject;
                currentProject.ResponsibleСustomer = ResponsibleCustomer;
                currentProject.ResponsibleОfficer = ResponsibleOfficer;
                currentProject.Budget = Convert.ToInt32(Budget);
                currentProject.Deadline = DateTime.Parse(DeadLine);
                currentProject.Description = Description;
            }
            catch
            {
                throw new Exception("Ошибка иницилизации добавления");
            }
            if (currentProject == null)
            {
                return false;
            }
            try
            {
                CronosEntities entities = new CronosEntities();
                entities.Project.AddOrUpdate(currentProject);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                throw new Exception("Ошибка редактирование проекта");
            }
        }

        public static int GetIdProject(string project)
        {
            try
            {
                CronosEntities entities = new CronosEntities();
                return entities.Project.Where(x => x.NameProject == project).First().id_Project;
            }
            catch
            {
                throw new Exception("Ошибка при получении проекта");
            }
        }

        public static void DeleteProject(Project currentProject)
        {
            if (currentProject == null)
            {
                MessageBox.Show("Отметка не выбрана");
                return;
            }
            try
            {
                DB.CronosEntities entities = new CronosEntities();
                entities.Project.Remove(entities.Project.Find(currentProject.id_Project));
                entities.SaveChanges();
            }
            catch
            {
                throw new Exception("Ошибка удаления проекта");
            }
        }
    }
}