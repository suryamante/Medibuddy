using System.Data.Common;

namespace Medibuddy.DataAccess
{
    public interface IDbConnectionFactory
    {
        bool IsSqlite { get; }

        DbConnection CreateConnection();
    }
}
