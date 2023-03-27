using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronosBeta.BL
{
    public class Delete
    {
        public static void DeleteFile(string path) 
        {
            File.Delete(path);
        }
    }
}
