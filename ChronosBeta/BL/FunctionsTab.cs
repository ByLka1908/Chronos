using ChronosBeta.View;
using ChronosBeta.InterfaceBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChronosBeta.BL
{
    public class FunctionsTab 
    {
        private static ITab _tab;

        public static void SetTab(ITab tab)
        {
            _tab = tab;
        }

        public static void OpenTab(TabControl currentTab)
        {
            Frame frame = new Frame();
            TabItem newTabItem = new TabItem();
            _tab.ShowTab(currentTab, newTabItem, frame);
        }

    }
}
