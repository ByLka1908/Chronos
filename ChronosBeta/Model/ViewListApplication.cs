namespace ChronosBeta.Model
{
    public class ViewListApplication
    {
        public string NameProcess { get; set; } //Название запущенного приложения
        public string MainWindowTitle { get; set; } //Название основного окна приложения
        public string StartProcessTime { get; set; } //Время запуска приложения
        public string EndProcessTime { get; set; } //Время закрытия приложения
    }
}