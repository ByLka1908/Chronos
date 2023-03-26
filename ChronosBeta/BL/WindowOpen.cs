using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChronosBeta.BL
{
     class WindowOpen
     {
        public static void OpenNewWindow(Window currentWindow, Window windowToOpen)
        {
            windowToOpen.Show();
            currentWindow.Close();
        }
     }
}