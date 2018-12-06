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

namespace Term_Project.Repositories.Level
{
    public class LevelDBRepository : ILevelRepository
    {
        private DatabaseSettings databaseSettings;
        public LevelDBRepository(IOptionsSnapshot<DatabaseSettings> config)
        {
            databaseSettings = config.Value;
        }

        public string GetConnectionString()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            var configuration = builder.Build();

            return configuration.GetConnectionString("DatabaseConnection");

        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("Level_Delete", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    command.Parameters.AddWithValue("@ID", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<LevelModel> GetList()
        {
            List<LevelModel> levelList = new List<LevelModel>();
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("Level_GetList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LevelModel level = new LevelModel
                            {
                                ID = (int)reader["ID"],
                                LevelName = reader["LevelName"].ToString()
                            };
                            levelList.Add(level);
                        }
                    }
                }
            }

            return levelList;
        }

        public void Insert(LevelModel level)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("Level_Insert", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    if (level.ID != 0)
                    {
                        command.Parameters.AddWithValue("@ID", level.ID);
                    }
                    command.Parameters.AddWithValue("@LevelName", level.LevelName);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
