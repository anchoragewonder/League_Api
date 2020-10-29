using League_Api.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using MySql;
using Renci.SshNet;

namespace League_Api.DbSchema
{
    class TableInterface
    {
        // formating is going to be wrong but general concept
        public void GetChamp()
        {
            
            try
            {
                MySqlConnection Connection = new MySqlConnection();//connection_string
                Connection.Open();
                MySqlCommand Cmd = Connection.CreateCommand();
                Cmd.CommandText = "SELECT * FROM leagueOfLegendsChampionData WHERE Name = Aatrox ";
                MySqlDataReader sqlRead = Cmd.ExecuteReader();

                while (sqlRead.Read())
                {
                    Console.WriteLine(sqlRead);
                }

                sqlRead.Close();
                Connection.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
           
        }
    }
}
