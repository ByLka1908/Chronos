using ChronosBeta.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronosBeta.Model
{
    public class ViewCurrentUser
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public byte[] ProfilePicture { get; set; }
        //public static string ImageUser { get; set; }

    }
}
