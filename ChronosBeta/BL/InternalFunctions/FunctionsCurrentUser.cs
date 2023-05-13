using ChronosBeta.DB;
using ChronosBeta.Model;
using System.Windows.Media.Imaging;

namespace ChronosBeta.BL
{
    public class FunctionsCurrentUser
    {
        private static Users _user;
        public static Users User
        {
            get { return _user; }
        } 

        private static string Name { get; set; }
        private static string Surname { get; set; }
        private static string MiddleName { get; set; }
        private static string JobTitle { get; set; }
        private static BitmapImage ImageUser { get; set; }

        public static void SetUser(Users user)
        {
            _user = user;
            Name = user.Name;
            Surname = user.Surname;
            MiddleName = user.MiddleName;
            JobTitle = $"Должность: {user.JobTitles.NameJobTitle}";

            if (user.ImageUser == null || user.ImageUser.Length == 0)
                return;
            ImageUser = FunctionsImage.ByteToBitmapImage(user.ImageUser);
        }

        public static int GetIDUser()
        {
            return User.ID_Users;
        }

        public static ViewCurrentUser GetViewUser()
        {
            ViewCurrentUser user = new ViewCurrentUser();
            user.Username        = Name;
            user.DisplayName     = $"{Name} {MiddleName} {Surname}";
            user.ImageUser       = ImageUser;
            return user;
        }
    }
}