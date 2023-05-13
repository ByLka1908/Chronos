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
            CronosEntities entities = new CronosEntities();
            var jobtitle = entities.JobTitles.Select(x => x.NameJobTitle).ToList();
            return jobtitle;
        }

        public static int GetId(string jobTitle)
        {
            CronosEntities entities = new CronosEntities();
            return entities.JobTitles.Where(x => x.NameJobTitle == jobTitle).First().ID_JobTitles;
        }

    }
}