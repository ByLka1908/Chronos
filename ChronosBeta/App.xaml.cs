using ChronosBeta.BL;
using ChronosBeta.BL.InternalFunctions;
using ChronosBeta.DB;
using ChronosBeta.View;
using ChronosBeta.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace ChronosBeta
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            FunctionsSettingStart.StartApp();

            if (FunctionsSettingStart.setting.RememberUser)
            {
                CronosEntities entities = new CronosEntities();
                Users user = entities.Users.Single(x => x.Login == FunctionsSettingStart.setting.NameUser && x.Password == FunctionsSettingStart.setting.PasswordUser);
                FunctionsCurrentUser.SetUser(user);

                var mainView = new MainView();
                FunctionsSettingStart.MainView = mainView;
                mainView.Show();
            }
            else
            {
                var loginView = new LoginView();
                loginView.Show();
                loginView.IsVisibleChanged += (s, ev) =>
                {
                    if (loginView.IsVisible == false && loginView.IsLoaded)
                    {
                        var mainView = new MainView();
                        FunctionsSettingStart.MainView = mainView;
                        mainView.Show();
                        loginView.Close();
                    }
                };
            }
        }
    }
}
