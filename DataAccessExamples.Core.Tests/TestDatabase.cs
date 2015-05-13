using DataAccessExamples.Core.SqlUtils;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlServerCe;
using System.IO;

namespace DataAccessExamples.Core.Tests
{
    public class TestDatabase : IDbConnectionFactory, IDisposable
    {
        private readonly string filename;
        private readonly string connectionString;
        private SqlCeEngine engine;

        public TestDatabase()
        {
            filename = Guid.NewGuid() + ".sdf";

            connectionString = String.Format("Data Source={0}", filename);

            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            engine = new SqlCeEngine(connectionString);
            engine.CreateDatabase();
        }

        public void ExecuteScript(string sql)
        {
            using (var connection = new SqlCeConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateMultiQueryCommand();
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }

        public DbConnection CreateConnection()
        {
            var connection = new SqlCeConnection(connectionString);
            connection.Open();
            return connection;
        }

        IDbConnection IDbConnectionFactory.CreateConnection()
        {
            return CreateConnection();
        }

        public void Dispose()
        {
            File.Delete(filename);
        }
    }
}
