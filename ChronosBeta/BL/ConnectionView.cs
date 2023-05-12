using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronosBeta.BL
{
    public class ConnectionView
    {
        public string ConnectName { get; set; }
        public string connectionString { get; set; }
        public string metadata { get; set; }
        public string provider { get; set; }
        public string dataSource { get; set; }
        public string initialCatalog { get; set; }
        public string integratedSecurity { get; set; }
        public string MultipleActiveResultSets { get; set; }
        public string App { get; set; }
    }
}
