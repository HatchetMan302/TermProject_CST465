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

namespace Term_Project.Repositories.Marble
{
    public class MarbleDBRepository : IMarbleRepository
    {
        private DatabaseSettings databaseSettings;
        public MarbleDBRepository(IOptionsSnapshot<DatabaseSettings> config)
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
                using (SqlCommand command = new SqlCommand("Marble_Delete", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    command.Parameters.AddWithValue("@ID", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<MarbleModel> GetList()
        {
            List<MarbleModel> marbleList = new List<MarbleModel>();
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("Marble_GetList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MarbleModel marble = new MarbleModel
                            {
                                ID = (int)reader["ID"],
                                MarbleName = reader["MarbleName"].ToString()
                            };
                            marbleList.Add(marble);
                        }
                    }
                }
            }

            return marbleList;
        }

        public void Insert(MarbleModel marble)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("Marble_Insert", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    if (marble.ID != 0)
                    {
                        command.Parameters.AddWithValue("@ID", marble.ID);
                    }
                    command.Parameters.AddWithValue("@MarbleName", marble.MarbleName);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
