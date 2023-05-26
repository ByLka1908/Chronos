namespace ChronosBeta.Model
{
    public class SettingView
    {
        public string NameUser { get; set; } //Имя пользователя 
        public string PasswordUser { get; set; } //Пароль пользователя 
        public bool RememberUser { get; set; } //Запомнить пользователя
        public string CurrentConectName { get; set; } //Название текущего подключения к БД
        public int ScreenShotTimer { get; set; } //Интервал снятий снимков экрана
        public int UpdateListAppTimer { get; set; } //Интервал обновления списка запущенных приложений
    }
}