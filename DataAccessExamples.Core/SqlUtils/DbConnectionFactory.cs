using System.Configuration;
using System.Data;
using System.Data.Common;

namespace DataAccessExamples.Core.SqlUtils
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }

    public class DbConnectionFactory : IDbConnectionFactory
    {
        private const string ConnectionStringName = "EmployeesContext";

        public IDbConnection CreateConnection()
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[ConnectionStringName];
            DbProviderFactory factory = DbProviderFactories.GetFactory(connectionStringSettings.ProviderName);

            var connection = factory.CreateConnection();
            connection.ConnectionString = connectionStringSettings.ConnectionString;
            return connection;
        }
    }
}
