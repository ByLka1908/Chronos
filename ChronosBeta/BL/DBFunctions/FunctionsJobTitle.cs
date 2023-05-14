using ChronosBeta.DB;
using ChronosBeta.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronosBeta.BL
{
    public class FunctionsJobTitle
    {
        public static List<ViewJobTitle> GetJobTitles()
        {
            CronosEntities entities = new CronosEntities();
            var jobTitles = entities.JobTitles.ToList();
            List<ViewJobTitle> listJobTitle = new List<ViewJobTitle>();
            foreach (var job in jobTitles)
                listJobTitle.Add(new ViewJobTitle(job));
            return listJobTitle;
        }

        public static List<string> GetJobTitle()
        {
            CronosEntities entities = new CronosEntities();
            var jobtitle = entities.JobTitles.Select(x => x.NameJobTitle).ToList();
            return jobtitle;
        }

        public static int GetId(string jobTitle)
        {
            CronosEntities entities = new CronosEntities();
            return entities.JobTitles.Where(x => x.NameJobTitle == jobTitle).First().ID_JobTitles;
        }

        public static void DeleteJobTitle(JobTitles job)
        {
            CronosEntities entities = new CronosEntities();
            var users = entities.Users.Where(x => x.JobTitle == job.ID_JobTitles).ToList();
            foreach (var user in users)
            {
                entities.Users.Remove(user);
            }

            entities.JobTitles.Remove(entities.JobTitles.Find(job.ID_JobTitles));
            entities.SaveChanges();
        }

        public static void AddJobTitle(string NameJobTitle)
        {
            JobTitles jobtitle = new JobTitles();
            
            jobtitle.NameJobTitle = NameJobTitle;

            if (jobtitle == null)
                return;

            CronosEntities entities = new CronosEntities();
            entities.JobTitles.Add(jobtitle);
            entities.SaveChanges();
        }

        public static void EditJobTitle(string NameJobTitle, JobTitles selectedJob)
        {
            JobTitles jobtitle = selectedJob;

            jobtitle.NameJobTitle = NameJobTitle;

            if (jobtitle == null)
                return;

            CronosEntities entities = new CronosEntities();
            entities.JobTitles.AddOrUpdate(jobtitle);
            entities.SaveChanges();
        }

    }
}