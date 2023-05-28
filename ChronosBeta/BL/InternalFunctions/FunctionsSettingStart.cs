using ChronosBeta.View;
using Newtonsoft.Json;
using System.Globalization;
using System.IO;
using SettingView = ChronosBeta.Model.SettingView;

namespace ChronosBeta.BL.InternalFunctions
{
    public class FunctionsSettingStart
    {
        public static MainView MainView { get; set; } //Основное рабоче окно
        public static SettingView setting { get; set; } //Представление параметров приложения
        public static string[] Validformats { get; set; } //Формат даты
        public static CultureInfo Provider { get; set; } //Язык


        private static string path; //Путь к файлу с настройками приложения

        //Запуск приложения, установка настроек
        public static void StartApp()
        {
            Validformats = new[] { "MM/dd/yyyy", "yyyy/MM/dd", "MM/dd/yyyy HH:mm:ss",
                                   "M/d/yyyy hh:mm:ss tt", "yyyy-MM-dd HH:mm:ss, fff" };
            Provider = new CultureInfo("en-US");

            setting = new SettingView();
            setting.RememberUser = false;
            setting.CurrentConectName = "ChronosEntites";
            setting.UpdateListAppTimer = 5000;
            setting.ScreenShotTimer = 10000;

            string settingToJson = JsonConvert.SerializeObject(setting, Formatting.Indented);

            path = Directory.GetCurrentDirectory();
            path = path + @"\AppSetting.json";

            if (!File.Exists(path))
            {
                File.WriteAllText(path, settingToJson);
            }

            settingToJson = File.ReadAllText(path);
            setting = FunctionsJSON.GetDeserializeJsonToStartSetting(settingToJson);

            FunctionsConnection.UserConnectName = setting.CurrentConectName;
            FunctionsImage.ScreenShotTiming = setting.ScreenShotTimer;
            FunctionsJSON.UpdateListAppTimer = setting.UpdateListAppTimer;
        }

        /// <summary>
        /// Изменить подключение к БД
        /// </summary>
        /// <param name="connectName">Название подключения</param>
        public static void ChangeConnect(string connectName)
        {
            setting.CurrentConectName = connectName;
            Update();
        }

        //Обновление настроек приложения
        public static void Update()
        {
            string settingToJson = JsonConvert.SerializeObject(setting, Formatting.Indented);
            File.WriteAllText(path, settingToJson);
        }
    }
}