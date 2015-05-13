using DataAccessExamples.Core.Data;
using DataAccessExamples.Core.ViewModels;

namespace DataAccessExamples.Core.Services.Department
{
    public class RawOrmDepartmentService : IDepartmentService
    {
        private EmployeesContext context;

        public RawOrmDepartmentService(EmployeesContext context)
        {
            this.context = context;
        }

        public DepartmentList ListDepartments()
        {
            return new DepartmentList
            {
                Departments = context.Database.SqlQuery<DepartmentSummary>("SELECT Code, Name FROM Department ORDER BY Department.Code")
            };
        }

        public DepartmentList ListAverageSalaryPerDepartment()
        {
            return new DepartmentList
            {
                Departments = context.Database.SqlQuery<DepartmentSalary>(@"
SELECT Department.Code, Department.Name, CAST(AVG(CAST(Amount AS bigint)) AS int) AS 'AverageSalary'
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
