using ChronosBeta.BL;
using ChronosBeta.DB;
using System.Windows.Media.Imaging;

namespace ChronosBeta.Model
{
    public class ViewUsers 
    {
        public Users User { get; set; } //Пользователя
        public int Id { get; set; }
        public string Name { get; set; } //Имя пользователя
        public string Surname { get; set; } //Фамилия пользователя
        public string MiddleName { get; set; } //Отчетсво пользователя
        public string Phone { get; set; } //Телефон
        public string Skype { get; set; } //Скайп
        public string JobTitle { get; set; } //Название должности
        public BitmapImage ImageUser { get; set; } //Фото пользователя

        /// <summary>
        /// Инициализация представления пользователя
        /// </summary>
        /// <param name="user"></param>
        public ViewUsers(Users user)
        {
            Id         = user.ID_Users;
            User       = user;
            Name       = user.Name;
            MiddleName = user.MiddleName;
            Surname    = user.Surname;
            Phone      = user.Phone;
            Skype      = user.Skype;
            JobTitle   = user.JobTitles.NameJobTitle;

            if (user.ImageUser == null || user.ImageUser.Length == 0)
            {
                ImageUser = FunctionsImage.GetImage();
                return;
            }

            ImageUser = FunctionsImage.ByteToBitmapImage(user.ImageUser);
        }
    }
}