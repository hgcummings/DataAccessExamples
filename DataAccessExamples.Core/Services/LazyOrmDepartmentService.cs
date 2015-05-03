using System;
using System.Linq;
using DataAccessExamples.Core.Data;
using DataAccessExamples.Core.ViewModels;

namespace DataAccessExamples.Core.Services
{
    public class LazyOrmDepartmentService : IDepartmentService
    {
        private readonly EmployeesContext context;

        public LazyOrmDepartmentService(EmployeesContext context)
        {
            this.context = context;
        }

        public DepartmentList ListDepartments()
        {
            return new DepartmentList {
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
                Departments = context.Departments.AsEnumerable().Select(d => new DepartmentSalary
                {
                    Code = d.Code,
                    Name = d.Name,
                    AverageSalary =
                        (int) d.DepartmentEmployees.Select(
                            e => e.Employee.Salaries.LastOrDefault(s => s.ToDate > DateTime.Now))
                            .Where(s => s != null)
                            .Average(s => s.Amount)
                })
            };
        }
    }
}
