using ChronosBeta.View;
using Newtonsoft.Json;
using System.IO;
using SettingView = ChronosBeta.Model.SettingView;

namespace ChronosBeta.BL.InternalFunctions
{
    public class FunctionsSettingStart
    {
        public static MainView MainView { get; set; }
        public static SettingView setting { get; set; }

        private static string path;

        public static void StartApp()
        {
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

        public static void Update()
        {
            string settingToJson = JsonConvert.SerializeObject(setting, Formatting.Indented);
            File.WriteAllText(path, settingToJson);
        }

    }
}