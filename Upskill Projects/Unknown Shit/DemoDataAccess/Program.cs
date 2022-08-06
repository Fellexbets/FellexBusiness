using System;
using Microsoft.Data.Sqlite;
using Microsoft.Data.SqlClient;

namespace DemoDataAccess
{
    public class Program
    {
        private static void ConnectToSqlite() 
        {
            // 1. dotnet add package Microsoft.Data.Sqlite
            // 2. using Microsoft.Data.Sqlite;
            string dbName = "db/Students.db";
            string connectionString = $"Data Source={ dbName }";

            using (SqliteConnection sqliteConnection = new SqliteConnection(connectionString))
            {
                sqliteConnection.Open();

                string commandText = "SELECT name FROM STUDENTS;";
                SqliteCommand sqliteCommand = new SqliteCommand(commandText, sqliteConnection);

                SqliteDataReader query = sqliteCommand.ExecuteReader();

                // Enquanto existirem linhas para ler
                while (query.Read())
                {
                    Console.WriteLine($"Hello { query["name"] }");
                }

                sqliteConnection.Close();
            }
        }

        static void Main(string[] args)
        {
            // ConnectToSqlite();
            ConnectToSql();
        }

        private static void ConnectToSql()
        {
            // 1. Microsoft.Data.SqlClient
            // 2. using Microsoft.Data.SqlClient
            string connectionString = $"Server=.;Database=SewRight;User Id=sa;Password=raga123;TrustServerCertificate=true;";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string commandText = "SELECT Nome FROM Cliente;";
                SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                SqlDataReader query = sqlCommand.ExecuteReader();
                while (query.Read()) 
                {
                   Console.WriteLine($"Hello { query["Nome"] }");
                }

                                
                sqlConnection.Close();
            }
        }
    }
}
