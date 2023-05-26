using ChronosBeta.DB;

namespace ChronosBeta.Model
{
    public class ViewJobTitle
    {
        public JobTitles Job { get; set; } //Должность
        public string NameJobTitle { get; set; } //Название должности

        /// <summary>
        /// Инициализация должности
        /// </summary>
        /// <param name="jobTitle">Должность</param>
        public ViewJobTitle(JobTitles jobTitle)
        {
            Job = jobTitle;
            NameJobTitle = jobTitle.NameJobTitle;
        }
    }
}