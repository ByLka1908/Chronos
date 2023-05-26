using ChronosBeta.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ChronosBeta.BL
{
    public class FunctionsListApplication
    {
        /// <summary>
        /// Получить список запущенных приложений
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Получить запущенные процессы
        /// </summary>
        /// <returns></returns>
        private static string[] GetRunningProcesses()
        {
            var processes = Process.GetProcesses().Where(p => !string.IsNullOrEmpty(p.MainWindowTitle)).Select(
                p => p.ProcessName + "%" + p.MainWindowTitle + "%" + p.StartTime).ToArray();
            return processes;
        }
    }
}