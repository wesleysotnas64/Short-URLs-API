using DotNetEnv;
using Npgsql;

namespace Short_URLs_API.Services
{
    public class ConnectionService
    {
        private readonly string? connectionString;
        public NpgsqlConnection? connection;

        public ConnectionService()
        {
            Env.Load();

            var host = Environment.GetEnvironmentVariable("PGHOST");
            var database = Environment.GetEnvironmentVariable("PGDATABASE");
            var username = Environment.GetEnvironmentVariable("PGUSER");
            var password = Environment.GetEnvironmentVariable("PGPASSWORD");

            connectionString = $"Host={host};Database={database};Username={username};Password={password}";

        }

        public void OpenConnection()
        {
            if (connection == null || connection.State == System.Data.ConnectionState.Closed)
            {
                connection = new NpgsqlConnection(connectionString);
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
