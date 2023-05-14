using ChronosBeta.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronosBeta.Model
{
    public class ViewJobTitle
    {
        public JobTitles Job { get; set; }
        public string NameJobTitle { get; set; }

        public ViewJobTitle(JobTitles job)
        {
            Job = job;
            NameJobTitle = job.NameJobTitle;
        }
    }
}
