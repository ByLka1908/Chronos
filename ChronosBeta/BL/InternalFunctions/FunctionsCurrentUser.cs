using ChronosBeta.DB;
using ChronosBeta.Model;
using System.Windows.Media.Imaging;

namespace ChronosBeta.BL
{
    public class FunctionsCurrentUser
    {
        private static Users _user; //Текущий пользователь
        private static BitmapImage ImageUser { get; set; } //Фото текущего пользователя

        public static Users User
        {
            get { return _user; }
        }

        /// <summary>
        /// Установить текущего пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        public static void SetUser(Users user)
        {
            _user = user;

            if (user.ImageUser == null || user.ImageUser.Length == 0)
                return;
            ImageUser = FunctionsImage.ByteToBitmapImage(user.ImageUser);
        }

        /// <summary>
        /// Получить представление текущего пользователя
        /// </summary>
        /// <returns></returns>
        public static ViewCurrentUser GetViewUser()
        {
            ViewCurrentUser user = new ViewCurrentUser();
            user.Username        = _user.Name;
            user.DisplayName     = $"{_user.Name} {_user.MiddleName} {_user.Surname}";
            user.ImageUser       = ImageUser;
            return user;
        }
    }
}