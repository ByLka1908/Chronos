using ChronosBeta.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronosBeta.BL.InternalFunctions
{
    public class FunctionsSettingStart
    {
        public static void StartApp()
        {
            SettingView setting = new SettingView();
            setting.RememberUser = false;
            setting.CurrentConectName = "ChronosEntites";

            string settingToJson = JsonConvert.SerializeObject(setting, Formatting.Indented);

            string path = Directory.GetCurrentDirectory();
            path = path + @"\AppSetting.json";

            if (!File.Exists(path))
            {
                File.WriteAllText(path, settingToJson);
            }

            settingToJson = File.ReadAllText(path);
            setting = FunctionsJSON.GetDeserializeJsonToStartSetting(settingToJson);

            FunctionsConnection.UserConnectName = setting.CurrentConectName;
        }
    }
}
