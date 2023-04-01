using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronosBeta.BL
{
    class FunctionsTask
    {
        public static List<ViewTask> GetTasks()
        {
            try
            {
                DB.CronosEntities entities = new DB.CronosEntities();
                var project = entities.Task.ToList();
                List<ViewTask> view = new List<ViewTask>();
                foreach (var item in project)
                    view.Add(new ViewTask(item));
                return view;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}