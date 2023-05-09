using ChronosBeta.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronosBeta.BL
{
    public class FunctionsJobTitle
    {
        public static List<string> GetJobTitle()
        {
            try
            {
                DB.CronosEntities entities = new DB.CronosEntities();
                var jobtitle = entities.JobTitles.Select(x => x.NameJobTitle).ToList();
                return jobtitle;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static int GetId(string jobTitle)
        {
            try
            {
                CronosEntities entities = new CronosEntities();
                return entities.JobTitles.Where(x => x.NameJobTitle == jobTitle).First().ID_JobTitles;
            }
            catch
            {
                throw new Exception("Ошибка при получении должности");
            }
        }
    }
}