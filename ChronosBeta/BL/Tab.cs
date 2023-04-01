using ChronosBeta.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChronosBeta.BL
{
    public class Tab
    {
        public static void OpenTab(string NameWindow, TabControl currentTab) 
        {
            //Добавление вкладки с окном
            Frame frame = new Frame();
            TabItem newTabItem = new TabItem();

            switch (NameWindow)
            {
                case "ListApplication":
                    View.ListApplication currentApp = new View.ListApplication();
                    newTabItem.Header = "Список приложений";

                    currentApp.Height = frame.Height;
                    currentApp.Width = frame.Width;
                    frame.Navigate(currentApp);
                    break;

                case "UserListAll":
                    UserListAll currentUser = new UserListAll();
                    newTabItem.Header = "Список пользователей";

                    currentUser.Height = frame.Height;
                    currentUser.Width = frame.Width;
                    frame.Navigate(currentUser);
                    break;

                default:
                   
                break;
            }

            currentTab.Items.Add(newTabItem);
            currentTab.SelectedItem = newTabItem;
            newTabItem.Content = frame;

        }

    }
}
