using ChronosBeta.DB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using ChronosBeta.Model;
using Label = System.Windows.Controls.Label;
using System.Windows.Forms;

namespace ChronosBeta.BL
{
    public class FunctionsCurrentUser
    {
        private static Users User { get; set; }
        private static string Name { get; set; }
        private static string Surname { get; set; }
        private static string JobTitle { get; set; }


        //private static string ImageUser { get; set; }

        public static void SetUser(Users user)
        {
            User = user;
            Name = user.Name;
            Surname = user.Surname;
            JobTitle = $"Должность: {user.JobTitles.NameJobTitle}";
            //ImageUser = string.IsNullOrWhiteSpace(user.ImageUser) ? @"/ImageDef.jpg" : user.ImageUser;
        }

        public static void GetUser(Label name, Label surname, Label jobTitle, Image imageUser)
        {
            name.Content = Name;
            surname.Content = Surname;
            jobTitle.Content = JobTitle;
            //imageUser.Source = Image(ImageUser);
        }

        public static int GetUser()
        {
            return User.ID_Users;
        }

        public static ViewCurrentUser GetViewUser()
        {
            ViewCurrentUser user = new ViewCurrentUser();
            user.Username = Name;
            user.DisplayName = $"Welcome {Name} {Surname}";
            user.ProfilePicture = null;
            return user;
        }
        
    }
}