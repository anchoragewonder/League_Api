using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using MySql;
using MySql.Data.MySqlClient;

using League_Api.Extensions;
using League_Api.TableModels;

namespace League_Api.DbSchema
{
    public class TableInterface
    {
        private const string TABLE = "champions";

        // formating is going to be wrong but general concept
        public async Task<ChampModel> GetChamp(string name)
        {
            DbConnector connection = new DbConnector();
            if (!(await connection.IsConnected()))
            {
                throw new Exception();
            }
            try
            {
                string commandText = $"SELECT * FROM {TABLE} WHERE Name=@name;";
                MySqlCommand cmd = new MySqlCommand(commandText, connection.Connection);
                cmd.Parameters.AddWithValue("@name", name);
                MySqlDataReader reader = cmd.ExecuteReader();

                List<ChampModel> champModels = new List<ChampModel>();
                while (reader.Read())
                {
                    ChampModel champ = MySqlDataReaderToChampModel(reader);
                    champModels.Add(champ);
                }

                reader.Close();
                await connection.Disconnect();
                return champModels.FirstOrDefault();
            }

            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception Caught", ex);
                throw ex;
            }
        }

        public async Task<ChampModel> QuizChamp(int damage, int defense, int mobility, int crowdControl)
        {
            DbConnector connection = new DbConnector();
            if (!(await connection.IsConnected()))
            {
                throw new Exception();
            }
            try
            {
                string commandText = $"SELECT * FROM {TABLE} WHERE Damage=@damage AND Defense=@defense AND Mobility=@mobility AND CrowdControl=@crowdControl;";
                MySqlCommand cmd = new MySqlCommand(commandText, connection.Connection);
                cmd.Parameters.AddWithValue("@damage", damage);
                cmd.Parameters.AddWithValue("@defense", defense);
                cmd.Parameters.AddWithValue("@mobility", mobility);
                cmd.Parameters.AddWithValue("@crowdControl", crowdControl);
                MySqlDataReader reader = cmd.ExecuteReader();

                List<ChampModel> champModels = new List<ChampModel>();
                while (reader.Read())
                {
                    ChampModel champ = MySqlDataReaderToChampModel(reader);
                    champModels.Add(champ);
                }

                reader.Close();
                await connection.Disconnect();
                return champModels.FirstOrDefault();
            }

            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception Caught", ex);
                throw ex;
            }
        }


        private ChampModel MySqlDataReaderToChampModel(MySqlDataReader reader)
        {
            int id = Int32.Parse(reader["id"].ToString());
            string name = reader["name"].ToString();
            string _class = reader["class"].ToString();
            int style = Int32.Parse(reader["style"].ToString());
            int difficulty = Int32.Parse(reader["difficulty"].ToString());
            string damageType = reader["damageType"].ToString();
            int damage = Int32.Parse(reader["damage"].ToString());
            int sturdiness = Int32.Parse(reader["sturdiness"].ToString());
            int crowdControl = Int32.Parse(reader["crowdControl"].ToString());
            int mobility = Int32.Parse(reader["mobility"].ToString());
            int functionality = Int32.Parse(reader["functionality"].ToString());

            return new ChampModel(id, name, _class, style, difficulty, damageType, damage, 
                sturdiness, crowdControl, mobility, functionality);
        }
    }
}
