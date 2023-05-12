using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace ChronosBeta.BL
{
    public class FunctionsConnection
    {
        public static string UserConnectName { get; set; }
        private static string CurrentConnect;

        public static string test()
        {
            UserConnectName = "ChronosEntites";
            ConnectionView connect = new ConnectionView();

            connect.ConnectName = "ChronosEntites";
            connect.connectionString = @"metadata = res://*/DB.ModelDB.csdl|res://*/DB.ModelDB.ssdl|res://*/DB.ModelDB.msl;provider=System.Data.SqlClient;provider connection string=" + @"""data source=KOMPUTER\\SQLEXPRESS;initial catalog=Cronos;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework""";
            connect.metadata = "res://*/DB.ModelDB.csdl|res://*/DB.ModelDB.ssdl|res://*/DB.ModelDB.msl";
            connect.provider = "System.Data.SqlClient";
            connect.dataSource = @"KOMPUTER\SQLEXPRESS";
            connect.initialCatalog = "Cronos";
            connect.integratedSecurity = "True";
            connect.MultipleActiveResultSets = "True";
            connect.App = "EntityFramework";

            List<ConnectionView> connections = new List<ConnectionView>();
            connections.Add(connect);

            string ConnectionSetting = JsonConvert.SerializeObject(connections, Formatting.Indented);

            string path = Directory.GetCurrentDirectory();
            path = path + @"\AppSetting.json";

            //if (File.Exists(path))
            //{
            //    ConnectionSetting = File.ReadAllText(path);
            //}

            File.WriteAllText(path, ConnectionSetting);
            ConnectionSetting = File.ReadAllText(path);

            List<ConnectionView> connection = FunctionsJSON.GetDeserializeJsonToConnect(ConnectionSetting);
            foreach(var conect in connection)
            {
                if (conect.ConnectName != UserConnectName)
                    continue;

                CurrentConnect = $@"metadata = {conect.metadata};provider={conect.provider};provider connection string=" + $@"""data source={conect.dataSource};initial catalog={conect.initialCatalog};integrated security={conect.integratedSecurity};MultipleActiveResultSets={conect.MultipleActiveResultSets};App={conect.App}""";
            }

            return CurrentConnect;
        }
    }
}