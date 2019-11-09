using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ProgrammingTechnologies.Services
{
    /// <summary>
    /// Service thath allows to communicate with local database.
    /// </summary>
    public class DatabaseService
    {
        private string _connectionString;

        /// <summary>
        /// Creates instance of DatabaseService establishing connection string relative to projects directory.
        /// </summary>
        public DatabaseService()
        {
            string currentDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(currentDirectory).Parent.FullName;
            string databaseDirectory = Path.Combine(projectDirectory, "Database.mdf");
            _connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={databaseDirectory};Integrated Security=True";
        }

        /// <summary>
        /// Executes passed query and returns DataTable object with the result.
        /// </summary>
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

        /// <summary>
        /// Executes passed insert, update, delete instructions.
        /// </summary>
        /// <param name="instruction"></param>
        public void ExecuteInstruction(string instruction)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(instruction, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}
