using System.Windows.Media.Imaging;

namespace ChronosBeta.Model
{
    public class ViewCurrentUser
    {
        public string Username { get; set; } //ФИО Текущего пользователя
        public string DisplayName { get; set; }
        public BitmapImage ImageUser { get; set; } //Фото текущего пользователя 
    }
}