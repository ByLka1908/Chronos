using ChronosBeta.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace ChronosBeta.BL
{
    public class FunctionsJSON
    {
        private static List<ViewListApplication> JsonAppList { get; set; }
        private static Timer myTimer;

        public static int UpdateListAppTimer { get; set; }

        public static void CreateJson()
        {
            JsonAppList = FunctionsListApplication.GetListProcesses();
            myTimer = new Timer(UpdateListAppTimer); //5000 = 5 сек
            myTimer.Elapsed += UpdateJson;
            myTimer.Enabled = true;
        }

        public static string GetJson()
        {
            myTimer.Enabled = false;
            return JsonConvert.SerializeObject(JsonAppList, Formatting.Indented);
        }

        public static List<ViewListApplication> GetDeserializeJson(string JsonlistApp)
        {
            List<ViewListApplication> list = JsonConvert.DeserializeObject<List<ViewListApplication>>(JsonlistApp);
            return list;
        }

        public static List<ConnectionView> GetDeserializeJsonToConnect(string ConnectSetting)
        {
            List<ConnectionView> connect = JsonConvert.DeserializeObject<List<ConnectionView>>(ConnectSetting);
            return connect;
        }

        public static SettingView GetDeserializeJsonToStartSetting(string ConnectSetting)
        {
            SettingView setting = JsonConvert.DeserializeObject<SettingView>(ConnectSetting);
            return setting;
        }

        public async static void UpdateJson(Object source, ElapsedEventArgs e)
        {
            List<ViewListApplication> newList = FunctionsListApplication.GetListProcesses();

            foreach (ViewListApplication oldApp in JsonAppList)
            {
                var viewList = newList.Where(x => x.NameProcess.ToUpper().StartsWith(oldApp.NameProcess.ToUpper())).ToList();
                if(viewList.Count < 1 & oldApp.EndProcessTime == null)
                {
                    oldApp.EndProcessTime = DateTime.Now.ToString();
                }
            }

            foreach (ViewListApplication newApp in newList)
            {
                var viewList = JsonAppList.Where(x => x.NameProcess.ToUpper().StartsWith(newApp.NameProcess.ToUpper())).ToList();
                if(viewList.Count < 1)
                {
                    JsonAppList.Add(newApp);
                }
            }
            await Task.Delay(1000);
        }
    }
}