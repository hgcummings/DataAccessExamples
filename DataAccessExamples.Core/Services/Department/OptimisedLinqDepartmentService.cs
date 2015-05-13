using DataAccessExamples.Core.Data;
using DataAccessExamples.Core.ViewModels;
using System.Linq;

namespace DataAccessExamples.Core.Services.Department
{
    /// <summary>
    ///   Implementation of <see cref="IDepartmentService"/> using Entity Framework with LINQ written for optimal SQL
    /// </summary>
    public class OptimisedOrmDepartmentService : IDepartmentService
    {
        private readonly IEmployeesContext context;

        public OptimisedOrmDepartmentService(IEmployeesContext context)
        {
            this.context = context;
        }

        public DepartmentList ListDepartments()
        {
            return new DepartmentList
            {
                Departments = context.Departments.OrderBy(d => d.Code).Select(d => new DepartmentSummary
                {
                    Code = d.Code,
                    Name = d.Name
                })
            };
        }

        public DepartmentList ListAverageSalaryPerDepartment()
        {
            return new DepartmentList
            {
                Departments = context.Departments
                    .SelectMany(d => d.DepartmentEmployees)
                    .GroupBy(de => de.Department)
                    .Select(group =>
                    new {
                        Department = group.Key,
                        AverageSalary = group.SelectMany(de => de.Employee.Salaries).Where(s => s != null).Average(s => s.Amount)
                    }).OrderByDescending(d => d.AverageSalary)
                    .Select(x => new DepartmentSalary
                    {
                        Code = x.Department.Code,
                        Name = x.Department.Name,
                        AverageSalary = (int) x.AverageSalary
                    })
            };
        }
    }
}
