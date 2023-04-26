﻿using ChronosBeta.DB;
using ChronosBeta.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static bool AddProject(string NameProject, string ResponsibleCustomer,
                                      string ResponsibleOfficer, string Budget,
                                      string DeadLine, string Description)
        {
            Project project = new Project();
            try
            {
                project.NameProject = NameProject;
                project.ResponsibleСustomer = ResponsibleCustomer;
                project.ResponsibleОfficer = GetIdResponsibleOfficer(ResponsibleOfficer);
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

        public static bool EditProject(string NameProject, string ResponsibleCustomer,
                              string ResponsibleOfficer, string Budget,
                              string DeadLine, string Description, Project currentProject)
        {
            try
            {
                currentProject.NameProject = NameProject;
                currentProject.ResponsibleСustomer = ResponsibleCustomer;
                currentProject.ResponsibleОfficer = GetIdResponsibleOfficer(ResponsibleOfficer);
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

        private static int GetIdResponsibleOfficer(string responsibleOfficer)
        {
            try
            {
                CronosEntities entities = new CronosEntities();
                return entities.Users.Where(x => x.Name == responsibleOfficer).First().ID_Users;
            }
            catch
            {
                throw new Exception("Ошибка при получении Епик геймс");
            }
        }
    }
}
