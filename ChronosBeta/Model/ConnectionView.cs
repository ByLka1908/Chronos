namespace ChronosBeta.BL
{
    public class ConnectionView
    {
        public string ConnectName { get; set; }// Имя подключения
        public string connectionString { get; set; }// Итоговая строка подключения
        public string metadata { get; set; }
        public string provider { get; set; }
        public string dataSource { get; set; }//Адрес сервера
        public string initialCatalog { get; set; } //Имя БД
        public bool integratedSecurity { get; set; } //Аунтификация Windows
        public bool MultipleActiveResultSets { get; set; }
        public string App { get; set; }
        public string UserId { get; set; }//Имя пользователя
        public string Password { get; set; }//Пароль
    }
}