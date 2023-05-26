using ChronosBeta.DB;
using ChronosBeta.Model;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace ChronosBeta.BL
{
    public class FunctionsJobTitle
    {
        /// <summary>
        /// Получить список должностей
        /// </summary>
        /// <returns></returns>
        public static List<ViewJobTitle> GetJobTitles()
        {
            CronosEntities entities = new CronosEntities();
            var jobTitles = entities.JobTitles.ToList();
            List<ViewJobTitle> viewJobTitles = new List<ViewJobTitle>();
            foreach (var jobTitle in jobTitles)
                viewJobTitles.Add(new ViewJobTitle(jobTitle));
            return viewJobTitles;
        }

        /// <summary>
        /// Получить Список должностей
        /// </summary>
        /// <returns></returns>
        public static List<string> GetListJobTitles()
        {
            CronosEntities entities = new CronosEntities();
            return entities.JobTitles.Select(x => x.NameJobTitle).ToList();
        }

        /// <summary>
        /// Получить id должности
        /// </summary>
        /// <param name="jobTitle">Название должности</param>
        /// <returns></returns>
        public static int GetJobTitleId(string jobTitle)
        {
            CronosEntities entities = new CronosEntities();
            return entities.JobTitles.Where(x => x.NameJobTitle == jobTitle).First().ID_JobTitles;
        }

        /// <summary>
        /// Добавить должность
        /// </summary>
        /// <param name="NameJobTitle">Название должности</param>
        public static void AddJobTitle(string NameJobTitle)
        {
            JobTitles jobTitle = new JobTitles();

            jobTitle.NameJobTitle = NameJobTitle;

            if (jobTitle == null)
                return;

            CronosEntities entities = new CronosEntities();
            entities.JobTitles.Add(jobTitle);
            entities.SaveChanges();
        }

        /// <summary>
        /// Изменить должность
        /// </summary>
        /// <param name="NameJobTitle">Название должности</param>
        /// <param name="selectedJob">Должность</param>
        public static void EditJobTitle(string NameJobTitle, JobTitles jobTitle)
        {
            jobTitle.NameJobTitle = NameJobTitle;

            if (jobTitle == null)
                return;

            CronosEntities entities = new CronosEntities();
            entities.JobTitles.AddOrUpdate(jobTitle);
            entities.SaveChanges();
        }

        /// <summary>
        /// Удалить должность
        /// </summary>
        /// <param name="job">Должность</param>
        public static void DeleteJobTitle(JobTitles jobTitle)
        {
            CronosEntities entities = new CronosEntities();

            //Проверка на связи с пользователями
            var users = entities.Users.Where(x => x.JobTitle == jobTitle.ID_JobTitles).ToList();
            foreach (var user in users)
            {
                entities.Users.Remove(user);
            }

            entities.JobTitles.Remove(entities.JobTitles.Find(jobTitle.ID_JobTitles));
            entities.SaveChanges();
        }
    }
}