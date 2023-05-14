using ChronosBeta.BL;
using ChronosBeta.DB;
using System.Windows.Media.Imaging;

namespace ChronosBeta.Model
{
    public class ViewUsers 
    {
        public Users User { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }
        public string Skype { get; set; }
        public string JobTitle { get; set; }
        public BitmapImage ImageUser { get; set; }

        public ViewUsers(Users user)
        {
            Id       = user.ID_Users;
            User     = user;
            Name     = user.Name;
            MiddleName = user.MiddleName;
            Surname  = user.Surname;
            Phone    = user.Phone;
            Skype    = user.Skype;
            JobTitle = user.JobTitles.NameJobTitle;

            if (user.ImageUser == null || user.ImageUser.Length == 0)
            {
                ImageUser = FunctionsImage.GetImage();
                return;
            }

            ImageUser = FunctionsImage.ByteToBitmapImage(user.ImageUser);
        }
    }
}