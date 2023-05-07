using ChronosBeta.BL;
using ChronosBeta.DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChronosBeta.Model
{
    public class ViewUsers 
    {
        public Users User { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Skype { get; set; }
        public string JobTitle { get; set; }
        public BitmapImage ImageUser { get; set; }

        public ViewUsers(Users user)
        {
            Id = user.ID_Users;
            User = user;
            Name = user.Name;
            Surname = user.Surname;
            Phone = user.Phone;
            Skype = user.Skype;
            JobTitle = user.JobTitles.NameJobTitle;

            if (user.ImageUser == null || user.ImageUser.Length == 0)
                return;
            ImageUser = FunctionsImage.ByteToBitmapImage(user.ImageUser);
        }
    }
}