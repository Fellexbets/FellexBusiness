using System;
using Microsoft.Data.Sqlite;
using Microsoft.Data.SqlClient;

namespace _5DataAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            // ConnectToSqlite();
            ConnectToSqlServer();
            
        }
 
        // 1. dotnet add package Microsoft.Data.Sqlite
        // 2. using Microsoft.Data.Sqlite;
        static void ConnectToSqlite()
        {
            String dbName = "db/Students.db";
            String connectionString = $"Filename={ dbName }";
            using(SqliteConnection db = new SqliteConnection(connectionString))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand("SELECT name from Students", db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    Console.WriteLine($"Hello {query.GetString(0)}!");
                }
                db.Close();
            }
        }

        // 1. dotnet add package Microsoft.Data.SqlClient
        // 2. using Microsoft.Data.SqlClient;
        static void ConnectToSqlServer()
        {
            String connectionString = "Server=.;Database=Northwind2016;User Id=sa;Password=tagus;";
            // String connectionString = "Server=.;Database=Northwind;User Id=sa;Password=UpSkill!12;";
            using(SqlConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                SqlCommand selectCommand = new SqlCommand("SELECT TitleOfCourtesy, FirstName, LastName from Employees", db);
                SqlDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    Console.WriteLine($"Hello {query.GetString(0)} {query.GetString(1)} {query.GetString(2)}!");
                    // De outra forma: Console.WriteLine($"Hello {query["LastName"]}!");
                }
                db.Close();
            }
        }
    }
}
