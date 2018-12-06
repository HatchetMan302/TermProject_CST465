using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Term_Project.Models;

namespace Term_Project.Repositories.Powerup
{
    public class PowerupDBRepository : IPowerupRepository
    {
        private IConfiguration Configuration;
        private DatabaseSettings databaseSettings;
        public PowerupDBRepository(IOptionsSnapshot<DatabaseSettings> config, IConfiguration configuration)
        {
            Configuration = configuration;
            databaseSettings = config.Value;
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DatabaseConnection")))
            {
                using (SqlCommand command = new SqlCommand("Powerup_Delete", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    command.Parameters.AddWithValue("@ID", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<PowerupModel> GetList()
        {
            List<PowerupModel> powerupList = new List<PowerupModel>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DatabaseConnection")))
            {
                using (SqlCommand command = new SqlCommand("Powerup_GetList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PowerupModel powerup = new PowerupModel
                            {
                                ID = (int)reader["ID"],
                                Powerup = reader["Powerup"].ToString()
                            };
                            powerupList.Add(powerup);
                        }
                    }
                }
            }

            return powerupList;
        }

        public void Insert(PowerupModel powerup)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DatabaseConnection")))
            {
                using (SqlCommand command = new SqlCommand("Powerup_Insert", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    if (powerup.ID != 0)
                    {
                        command.Parameters.AddWithValue("@ID", powerup.ID);
                    }
                    command.Parameters.AddWithValue("@Powerup", powerup.Powerup);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
