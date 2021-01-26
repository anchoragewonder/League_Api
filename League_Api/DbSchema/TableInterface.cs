using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using MySql;
using MySql.Data.MySqlClient;

using League_Api.Extensions;
using League_Api.TableModels;
using League_Api.ResponseModels;
using League_Api.RequestModels;

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

        //int damage, int defense, int mobility, int crowdControl variable in quiz champ holdover
        public async Task<List<ChampModel>> QuizChamp(QuizRequestModel request)
        {
            int dmg = request.Damage;
            int def = request.Sturdiness;
            int mob = request.Mobility;
            int cc = request.CrowdControl;

            DbConnector connection = new DbConnector();
            if (!(await connection.IsConnected()))
            {
                throw new Exception();
            }
            try
            {
                string commandText = $"SELECT * FROM (select *, (CASE WHEN Damage=@dmg then 1 else 0 END) + (CASE WHEN Sturdiness=@def then 1 else 0 END) " +
                    $"+ (CASE WHEN Mobility=@mob then 1 else 0 END) + (CASE WHEN CrowdControl=@cc then 1 else 0 END) AS factors from {TABLE}) " +
                    $"aliasname WHERE factors > 2";
                MySqlCommand cmd = new MySqlCommand(commandText, connection.Connection);
                cmd.Parameters.AddWithValue("@dmg", dmg);
                cmd.Parameters.AddWithValue("@def", def);
                cmd.Parameters.AddWithValue("@mob", mob);
                cmd.Parameters.AddWithValue("@cc", cc);
                MySqlDataReader reader = cmd.ExecuteReader();

                List<ChampModel> champModels = new List<ChampModel>();
                while (reader.Read())
                {
                    ChampModel champ = MySqlDataReaderToChampModel(reader);
                    champModels.Add(champ);
                }

                reader.Close();
                await connection.Disconnect();
                return champModels;
            }

            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception Caught", ex);
                throw ex;
            }
        }

        public Dictionary<string, object> SerializeReader(MySqlDataReader reader)
        {
            var results = new Dictionary<string, object>();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                results.Add(reader.GetName(i), reader.GetValue(i));
            }
            return results;
        }

        private ChampModel MySqlDataReaderToChampModel(MySqlDataReader reader)
        {
            Dictionary<string, object> dict = SerializeReader(reader);

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

            _ = dict.TryGetValue("factors", out object factor_obj);

            int? factor = null;
            if(factor_obj != null)
            {
                factor = Int32.Parse(factor_obj.ToString());
            }

            return new ChampModel(id, name, _class, style, difficulty, damageType, damage, 
                sturdiness, crowdControl, mobility, functionality, factor);
        }
    }
}
