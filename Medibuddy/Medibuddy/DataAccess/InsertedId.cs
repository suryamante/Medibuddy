using System.Data.Common;

namespace Medibuddy.DataAccess
{
    /// <summary>
    /// Reads the database-generated identity of the row just inserted on the given open command,
    /// provider-aware (SQLite vs SQL Server). Must be called on the same connection as the INSERT.
    /// </summary>
    internal static class InsertedId
    {
        public static async Task<int> ReadAsync(IDbConnectionFactory connectionFactory, DbCommand command)
        {
            command.CommandText = connectionFactory.IsSqlite ? "SELECT last_insert_rowid();" : "SELECT SCOPE_IDENTITY();";
            object? result = await command.ExecuteScalarAsync();
            return result is null || result is DBNull ? 0 : Convert.ToInt32(result);
        }
    }
}
