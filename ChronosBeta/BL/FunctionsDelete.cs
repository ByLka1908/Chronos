using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronosBeta.BL
{
    public class FunctionsDelete
    {
        public static void Delete(string path) 
        {
            File.Delete(path);
        }
    }
}
