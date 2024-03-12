using adotoaspcorewebapi.Models;
using Npgsql;

namespace adotoaspcorewebapi.Data
{
    public class CountryDataAccess
    {
        private readonly string? _connectionString;

        public CountryDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("postgre");
        }

        public IEnumerable<CountryMaster> GetAllCountry()
        {
            var countryMasters = new List<CountryMaster>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM \"CountryMaster\"", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var countryMaster = new CountryMaster
                        {
                            CountryId = Convert.ToInt32(reader["CountryID"]),
                            CountryName = Convert.ToString(reader["CountryName"])
                        };
                        countryMasters.Add(countryMaster);
                    }
                }
            }

            return countryMasters;
        }
        public CountryMaster GetCountryById(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM \"CountryMaster\" WHERE \"CountryID\" = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CountryMaster
                            {
                                CountryId = Convert.ToInt32(reader["CountryID"]),
                                CountryName = Convert.ToString(reader["CountryName"])
                            };
                        }
                    }
                }
            }

            return null;
        }
        public void AddCountry(CountryMaster countryMaster)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO \"CountryMaster\" (\"CountryName\") VALUES (@countryname);", connection))
                {
                    command.Parameters.AddWithValue("@countryname", countryMaster.CountryName);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void UpdateCountry(CountryMaster countryMaster)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("UPDATE \"CountryMaster\" SET \"CountryName\"=@countryname WHERE \"CountryID\" = @countryid", connection))
                {
                    command.Parameters.AddWithValue("@countryname", countryMaster.CountryName);
                    command.Parameters.AddWithValue("@countryid", countryMaster.CountryId);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void Deletecountry(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("DELETE FROM \"CountryMaster\" WHERE \"CountryID\" = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
