using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChronosBeta.BL
{
    public interface ITab
    {
        void ShowTab(TabControl currentTab, TabItem newTabItem, Frame frame);
    }
}
