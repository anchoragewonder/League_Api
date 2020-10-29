using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using MySql;
using MySql.Data.MySqlClient;

namespace League_Api.Extensions
{
    public class DbConnector
    {
        protected static string Password = "Blackdog2020$";
        protected static string Username = "Golohas";
        protected static int Port = 3306;
        protected static string Endpoint = "rds-mysql-lol.chzcbnejugbt.us-east-1.rds.amazonaws.com";
        protected static string Database = "leagueOfLegendsChampionData";

        protected static string ConnectionString = $"Server={Endpoint}; database={Database}; UID={Username}; password={Password}";

        // experimetnting with a public strring
        public static string Conn = $"Server={Endpoint}; database={Database}; UID={Username}; password={Password}";

        public MySqlConnection Connection;

        public async Task<bool> IsConnected()
        {
            string connection_string = $"Server={Endpoint}; database={Database}; UID={Username}; password={Password}";
            Connection = new MySqlConnection(connection_string);
            await Connection.OpenAsync();
            return Connection.State == System.Data.ConnectionState.Open;
        }

        public async Task Disconnect()
        {
            await Connection.CloseAsync();
        }
    }
}
