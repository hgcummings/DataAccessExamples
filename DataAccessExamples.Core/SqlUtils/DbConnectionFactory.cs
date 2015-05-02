using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessExamples.Core.SqlUtils
{
    public class DbConnectionFactory
    {
        private const string ConnectionStringName = "EmployeesContext";

        public IDbConnection GetConnection()
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[ConnectionStringName];
            DbProviderFactory factory = DbProviderFactories.GetFactory(connectionStringSettings.ProviderName);

            var connection = factory.CreateConnection();
            connection.ConnectionString = connectionStringSettings.ConnectionString;
            return connection;
        }
    }
}
