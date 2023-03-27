using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronosBeta.BL
{
    class GetProject
    {
        public static List<ViewProject> GetProjects()
        {
            try
            {
                DB.CronosEntities entities = new DB.CronosEntities();
                var project = entities.Projects.ToList();
                List<ViewProject> view = new List<ViewProject>();
                foreach (var item in project)
                    view.Add(new ViewProject(item));
                return view;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}