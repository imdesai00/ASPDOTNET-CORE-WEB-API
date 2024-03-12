using adotoaspcorewebapi.Models;
using Npgsql;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace adotoaspcorewebapi.Data
{
    public class CityDataAccess
    {
        private readonly string? _connectionString;

        public CityDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("postgre");
        }
        public IEnumerable<CityMaster> getallCity()
        {
            var cityMasters = new List<CityMaster>();
            DataSet ds = new DataSet();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                using (var command = new NpgsqlCommand("select * from \"GetAllCityMasterData\"('ref001'); fetch all in \"ref001\";", connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    {
                        adapter.Fill(ds);
                        foreach (DataRow reader in ds.Tables[1].Rows)
                        {
                            var cityMaster = new CityMaster
                            {
                                CityId = Convert.ToInt32(reader["CityID"]),
                                CityName = Convert.ToString(reader["CityName"]),
                                StateId = Convert.ToInt32(reader["StateID"]),
                                CountryId = Convert.ToInt32(reader["CountryID"])
                            };
                            cityMasters.Add(cityMaster);
                        }
                    }
                }
            }
            return cityMasters;
        }

        public IEnumerable<CityMaster> getallCitybyid(int id)
        {
            var cityMasters = new List<CityMaster>();
            DataSet ds = new DataSet();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                using (var command = new NpgsqlCommand("select * from \"getCityByID\"(@ids,'ref0'); fetch all in \"ref0\";", connection))
                {
                    command.Parameters.AddWithValue("@ids", id);
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    {
                        adapter.Fill(ds);
                        foreach (DataRow reader in ds.Tables[1].Rows)
                        {
                            var cityMaster = new CityMaster
                            {
                                CityId = Convert.ToInt32(reader["CityID"]),
                                CityName = Convert.ToString(reader["CityName"]),
                                StateId = Convert.ToInt32(reader["StateID"]),
                                CountryId = Convert.ToInt32(reader["CountryID"])
                            };
                            cityMasters.Add(cityMaster);
                            return cityMasters;
                        }
                    }
                }
            }
           return null;
        }

        public void insertCitymaster(string cityname, int stateid, int countyid)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("call \"insertCityMaster\"(@cityname,@stateid,@countryid)", connection))
                {
                    command.Parameters.AddWithValue("@cityname", cityname);
                    command.Parameters.AddWithValue("@stateid", stateid);
                    command.Parameters.AddWithValue("@countryid", countyid);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void updateCitymaster(int cityid, string cityname)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("call \"updateCityMaster\"(@cid,@CityName)", connection))
                {
                    command.Parameters.AddWithValue("@cid", cityid);
                    command.Parameters.AddWithValue("@CityName", cityname);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void deleteCitymaster(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("call \"deleteCityMaster\"(@cityid)", connection))
                {
                    command.Parameters.AddWithValue("@cityid", DbType.Int16).Value = id;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
