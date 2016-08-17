using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessExamples.Core.SqlUtils;

namespace DataAccessExamples.Core.Tests
{
    class SqliteTestDatabase : IDbConnectionFactory, IDisposable
    {
        private readonly string connectionString;
        private readonly SQLiteConnection keepAliveConnection;

        public SqliteTestDatabase()
        {
            var filename = Guid.NewGuid().ToString();
            
            connectionString = new SQLiteConnectionStringBuilder()
            {
                // We could use file::memory:, but the GUID-based approach means that each instance
                // of this class creates an isolated DB, useful for running tests in parallel.
                FullUri = $"file:{filename}?mode=memory&cache=shared"
            }.ConnectionString;

            // SQLite in-memory DBs only live as long as there is at least one open connection,
            // so we create a connection to keep the database alive.
            // We could also acheive this by enabling connection pooling.
            keepAliveConnection = new SQLiteConnection(connectionString);
            keepAliveConnection.Open();
        }

        public void ExecuteScript(string sql)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateMultiQueryCommand();
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }

        public DbConnection CreateConnection()
        {
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            return connection;
        }

        IDbConnection IDbConnectionFactory.CreateConnection()
        {
            return CreateConnection();
        }

        public void Dispose()
        {
            keepAliveConnection.Dispose();
        }
    }
}