using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessExamples.Core.Data;
using DataAccessExamples.Core.SqlUtils;
using DataAccessExamples.Core.ViewModels;

namespace DataAccessExamples.Core.Services
{
    public class DapperDepartmentService : IDepartmentService
    {
        private readonly DbConnectionFactory connectionFactory;

        public DapperDepartmentService(DbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public DepartmentList ListDepartments()
        {
            using (var connection = connectionFactory.GetConnection())
            {
                return new DepartmentList
                {
                    Departments = connection.Query<DepartmentSummary>("SELECT * FROM Department")
                };
            }
        }
    }
}
