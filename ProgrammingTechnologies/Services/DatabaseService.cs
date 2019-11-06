using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ProgrammingTechnologies.Services
{
    public class DatabaseService
    {
        private string _connectionString;

        public DatabaseService()
        {
            string currentDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.FullName;
            string databaseDirectory = Path.Combine(projectDirectory, "ProgrammingTechnologies\\Database.mdf");
            _connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={databaseDirectory};Integrated Security=True";
        }

        public DataTable ExecuteQuery(string query)
        {
            DataTable result = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(result);
                    }
                }
            }

            return result;
        }

        public void ExecuteInsert(string instruction)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(instruction, connection))
                {
                    using (var adapter = new SqlDataAdapter())
                    {
                        adapter.InsertCommand = command;
                        connection.Open();
                        adapter.InsertCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }

        public void ExecuteDelete(string instruction)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(instruction, connection))
                {
                    using (var adapter = new SqlDataAdapter())
                    {
                        adapter.DeleteCommand = command;
                        connection.Open();
                        adapter.DeleteCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }

        public void ExecuteUpdate(string instruction)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(instruction, connection))
                {
                    using (var adapter = new SqlDataAdapter())
                    {
                        adapter.UpdateCommand = command;
                        connection.Open();
                        adapter.UpdateCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }
    }
}
