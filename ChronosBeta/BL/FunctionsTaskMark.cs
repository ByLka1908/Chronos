using ChronosBeta.DB;
using ChronosBeta.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronosBeta.BL
{
    public class FunctionsTaskMark
    {
        public static List<ViewTaskTimer> GetTasksTimer()
        {
            try
            {
                CronosEntities entities = new CronosEntities();
                var task = entities.TaskTimer.ToList();
                List<ViewTaskTimer> view = new List<ViewTaskTimer>();
                foreach (var item in task)
                    view.Add(new ViewTaskTimer(item));
                return view;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
