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
    public class ListApplication
    {
        public string NameProcess { get; set; }
        public string MainWindowTitle { get; set; }
        public string StartProcessTime { get; set; }
        public string EndProcessTime { get; set; }

        public static void CreateJsonListApplication()
        {   
            List<ListApplication> listApplications = new List<ListApplication>();
            string[] process = GetRunningProcesses();

            foreach (string currentProcess in process)
            {
                ListApplication list = new ListApplication();
                string[] processes = currentProcess.Split(new char[] {'%'});

                for(int i = 0; i < processes.Length; i++)
                {   
                    list.NameProcess = processes[0];
                    list.MainWindowTitle = processes[1];
                    list.StartProcessTime = processes[2];           
                }
                listApplications.Add(list);
            }

            string json = JsonConvert.SerializeObject(listApplications, Formatting.Indented);
            //Надо автоматически получать путь до папаки Temp не прописывая его в ручную
            string path = @"F:\Projects\VisualStudioSource\ChronosBeta\ChronosBeta\Temp\ListProcess.json";

            File.WriteAllText(path, json);
        }

        public static string[] GetRunningProcesses()
        {
            var processes = Process.GetProcesses().Where(p => !string.IsNullOrEmpty(p.MainWindowTitle)).Select(
                p => p.ProcessName + "%" + p.MainWindowTitle + "%" + p.StartTime).ToArray();
            return processes;
        }
    }
}