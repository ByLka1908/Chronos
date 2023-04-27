using ChronosBeta.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ChronosBeta.BL
{
    public class FunctionsListApplication
    {
        public static void CreateJsonListApplication()
        {   
            string json = JsonConvert.SerializeObject(GetListProcesses(), Formatting.Indented);

            //Надо автоматически получать путь до папаки Temp не прописывая его в ручную
            string path = @"F:\GitProject\Chronos\ChronosBeta\Temp\ListProcess.json";
            File.WriteAllText(path, json);
        }

        public static List<ViewListApplication> GetListProcesses()
        {
            List<ViewListApplication> listApplications = new List<ViewListApplication>();
            string[] process = GetRunningProcesses();

            foreach (string currentProcess in process)
            {
                ViewListApplication list = new ViewListApplication();
                string[] processes = currentProcess.Split(new char[] { '%' });

                for (int i = 0; i < processes.Length; i++)
                {
                    list.NameProcess = processes[0];
                    list.MainWindowTitle = processes[1];
                    list.StartProcessTime = processes[2];
                }
                listApplications.Add(list);
            }
            return listApplications;
        }

        private static string[] GetRunningProcesses()
        {
            var processes = Process.GetProcesses().Where(p => !string.IsNullOrEmpty(p.MainWindowTitle)).Select(
                p => p.ProcessName + "%" + p.MainWindowTitle + "%" + p.StartTime).ToArray();
            return processes;
        }
    }
}