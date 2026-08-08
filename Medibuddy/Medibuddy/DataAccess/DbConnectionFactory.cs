using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace Medibuddy.DataAccess
{
    /// <summary>
    /// Creates database connections for the configured provider (SqlServer or Sqlite),
    /// letting the app run against an in-memory SQLite database for testing without
    /// changing any DataAccess code.
    /// </summary>
    public sealed class DbConnectionFactory : IDbConnectionFactory, IDisposable
    {
        private readonly bool _isSqlite;
        private readonly string _connectionString;

        // Keeps a shared in-memory SQLite database alive for the app's lifetime.
        // Not needed for a file-based (persistent) SQLite database.
        private readonly DbConnection? _keepAlive;

        public DbConnectionFactory(IConfiguration configuration)
        {
            string provider = configuration.GetValue<string>("DatabaseProvider") ?? "SqlServer";
            _isSqlite = provider.Equals("Sqlite", StringComparison.OrdinalIgnoreCase);

            _connectionString = _isSqlite
                ? configuration.GetConnectionString("SqliteConnectionString")
                    ?? "Data Source=medibuddy.db"
                : configuration.GetConnectionString("DbConnectionString") ?? string.Empty;

            if (_isSqlite && _connectionString.Contains("Mode=Memory", StringComparison.OrdinalIgnoreCase))
            {
                _keepAlive = new SqliteConnection(_connectionString);
                _keepAlive.Open();
            }
        }

        public bool IsSqlite => _isSqlite;

        public DbConnection CreateConnection() =>
            _isSqlite ? new SqliteConnection(_connectionString) : new SqlConnection(_connectionString);

        public void Dispose() => _keepAlive?.Dispose();
    }
}
