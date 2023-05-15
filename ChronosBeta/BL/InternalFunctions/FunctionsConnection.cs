using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChronosBeta.BL
{
    public class FunctionsConnection
    {
        private static string CurrentConnectString;
        private static string path;

        public static List<ConnectionView> ConnectionViews { get; set; }
        public static string UserConnectName { get; set; }
        public static ConnectionView CurrentConnect { get; set; }

        public static string GetConnectionString()
        {
            ConnectionView connect = new ConnectionView();
            List<ConnectionView> connections = new List<ConnectionView>();

            connect.ConnectName = "ChronosEntites";
            connect.connectionString = @"metadata = res://*/DB.ModelDB.csdl|res://*/DB.ModelDB.ssdl|res://*/DB.ModelDB.msl;provider=System.Data.SqlClient;provider connection string=" + @"""data source=KOMPUTER\\SQLEXPRESS;initial catalog=Cronos;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework""";
            connect.metadata = "res://*/DB.ModelDB.csdl|res://*/DB.ModelDB.ssdl|res://*/DB.ModelDB.msl";
            connect.provider = "System.Data.SqlClient";
            connect.dataSource = @"KOMPUTER\SQLEXPRESS";
            connect.initialCatalog = "Cronos";
            connect.integratedSecurity = true;
            connect.MultipleActiveResultSets = true;
            connect.App = "EntityFramework";
            connections.Add(connect);

            string ConnectionSetting = JsonConvert.SerializeObject(connections, Formatting.Indented);

            path = Directory.GetCurrentDirectory();
            path = path + @"\AppConnectionSetting.json";

            if (!File.Exists(path))
            {
                File.WriteAllText(path, ConnectionSetting);
            }

            ConnectionSetting = File.ReadAllText(path);

            ConnectionViews = FunctionsJSON.GetDeserializeJsonToConnect(ConnectionSetting);

            foreach (var conect in ConnectionViews)
            {
                if (conect.ConnectName != UserConnectName)
                    continue;

                CurrentConnect = conect;

                CurrentConnectString = GetConnectionString(conect);
            }

            return CurrentConnectString;
        }

        public static ConnectionView GetConnect(string NameConnect)
        {
            return ConnectionViews.Where(x => x.ConnectName == NameConnect).First();
        }

        public static List<string> GetConnectList()
        {
            return ConnectionViews.Select(x => x.ConnectName).ToList();
        }

        public static void SaveConnection(string NameConnect, string AdressServer, string NameDB,
                                          string PasswordUser,string NameUser ,    bool IsAuntifucationWindows)
        {
            ConnectionView editConnect = GetConnect(NameConnect);
            ConnectionViews.Remove(editConnect);

            editConnect.ConnectName = NameConnect;
            editConnect.dataSource = AdressServer;
            editConnect.initialCatalog = NameDB;
            editConnect.Password = PasswordUser;
            editConnect.UserId = NameUser;
            editConnect.integratedSecurity = IsAuntifucationWindows;

            ConnectionViews.Add(editConnect);
            SaveJsonConnect();
        }

        public static void EditConnection(ConnectionView connect)
        {
            ConnectionView editConnect = GetConnect(connect.ConnectName);
            ConnectionViews.Remove(editConnect);

            ConnectionViews.Add(connect);
            SaveJsonConnect();
        }

        public static void AddConnection(string NameConnect,  string AdressServer, string NameDB,
                                         string PasswordUser, string NameUser,     bool IsAuntifucationWindows)
        {
            ConnectionView addConnect = new ConnectionView();

            addConnect.ConnectName = NameConnect;
            addConnect.dataSource = AdressServer;
            addConnect.initialCatalog = NameDB;
            addConnect.Password = PasswordUser;
            addConnect.UserId = NameUser;
            addConnect.integratedSecurity = IsAuntifucationWindows;
            addConnect.metadata = "res://*/DB.ModelDB.csdl|res://*/DB.ModelDB.ssdl|res://*/DB.ModelDB.msl";
            addConnect.provider = "System.Data.SqlClient";
            addConnect.MultipleActiveResultSets = true;
            addConnect.App = "EntityFramework";
            addConnect.connectionString = GetConnectionString(addConnect);

            ConnectionViews.Add(addConnect);
            SaveJsonConnect();
        }

        public static bool TryConnection(string AdressServer, string NameDB, bool IsAuntifucationWindows,
                                         string NameUser,     string PasswordUser)
        {
            string connectionString;

            if (IsAuntifucationWindows)
            {
                connectionString = $@"Data Source={AdressServer};Initial Catalog={NameDB};Integrated Security={IsAuntifucationWindows}";
            }
            else
            {
                connectionString = $@"Data Source={AdressServer};Initial Catalog={NameDB};User ID={NameUser};Password={PasswordUser}";
            }
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool TryDefaultConnection()
        {
            string connectionString;

            if (CurrentConnect.integratedSecurity)
            {
                connectionString = $@"Data Source={CurrentConnect.dataSource};Initial Catalog={CurrentConnect.initialCatalog};Integrated Security={CurrentConnect.integratedSecurity}";
            }
            else
            {
                connectionString = $@"Data Source={CurrentConnect.dataSource};Initial Catalog={CurrentConnect.initialCatalog};User ID={CurrentConnect.UserId};Password={CurrentConnect.Password}";
            }
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void DeleteConnection(ConnectionView Connect)
        {
            ConnectionViews.Remove(Connect);
            SaveJsonConnect();
        }

        private static string GetConnectionString(ConnectionView conect)
        {
            string connectString;

            if (conect.integratedSecurity)
            {
                connectString = $@"metadata = {conect.metadata};provider={conect.provider};provider connection string=" +
                                 $@"""data source={conect.dataSource};initial catalog={conect.initialCatalog};integrated security={conect.integratedSecurity};MultipleActiveResultSets={conect.MultipleActiveResultSets};App={conect.App}""";
            }
            else
            {
                connectString = $@"metadata = {conect.metadata};provider={conect.provider};provider connection string=" +
                                 $@"""data source={conect.dataSource};initial catalog={conect.initialCatalog};integrated security={conect.integratedSecurity};User ID={conect.UserId};Password={conect.Password};MultipleActiveResultSets={conect.MultipleActiveResultSets};App={conect.App}""";
            }
            return connectString;
        }

        private static void SaveJsonConnect()
        {
            string ConnectionSetting = JsonConvert.SerializeObject(ConnectionViews, Formatting.Indented);
            File.WriteAllText(path, ConnectionSetting);
        }
    }
}