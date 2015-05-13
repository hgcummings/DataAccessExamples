using Dapper;
using DataAccessExamples.Core.SqlUtils;
using DataAccessExamples.Core.ViewModels;

namespace DataAccessExamples.Core.Services.Department
{
    /// <summary>
    ///  Implementation of <see cref="IDepartmentService"/> using raw SQL queries with Dapper
    /// </summary>
    public class DapperDepartmentService : IDepartmentService
    {
        private readonly IDbConnectionFactory connectionFactory;

        public DapperDepartmentService(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public DepartmentList ListDepartments()
        {
            using (var connection = connectionFactory.CreateConnection())
            {
                return new DepartmentList
                {
                    Departments = connection
                        .Query<DepartmentSummary>("SELECT Code, Name FROM Department ORDER BY Department.Code")
                };
            }
        }

        public DepartmentList ListAverageSalaryPerDepartment()
        {
            using (var connection = connectionFactory.CreateConnection())
            {
                return new DepartmentList
                {
                    Departments = connection.Query<DepartmentSalary>(@"
SELECT Department.Code, Department.Name, AVG(CAST(Amount AS bigint)) AS 'AverageSalary'
FROM Department
JOIN DepartmentEmployee ON Department.Code = DepartmentCode
JOIN Salary ON DepartmentEmployee.EmployeeNumber = Salary.EmployeeNumber
WHERE Salary.ToDate > GETDATE()
GROUP BY Department.Code, Department.Name
ORDER BY AverageSalary DESC")
                };
            }
        }
    }
}
