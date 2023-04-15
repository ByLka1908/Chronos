using ChronosBeta.DB;
using ChronosBeta.InterfaceBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronosBeta.Model
{
    public class ViewUsers 
    {
        public Users User { get; set; }
        public string Photo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Skype { get; set; }
        public string JobTitle { get; set; }

        public ViewUsers(Users user)
        {
            User = user;
            Photo = user.Phone;
            Name = user.Name;
            Surname = user.Surname;
            Phone = user.Phone;
            Skype = user.Skype;
            JobTitle = user.JobTitles.NameJobTitle;
        }

    }
}
