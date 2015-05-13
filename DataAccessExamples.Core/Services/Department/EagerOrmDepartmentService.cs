using System;
using System.Data.Entity;
using System.Linq;
using DataAccessExamples.Core.Data;
using DataAccessExamples.Core.ViewModels;

namespace DataAccessExamples.Core.Services.Department
{
    /// <summary>
    ///   Implementation of <see cref="IDepartmentService"/> using EF and eager-loading where possible
    /// </summary>
    public class EagerOrmDepartmentService : IDepartmentService
    {
        private readonly IEmployeesContext context;

        public EagerOrmDepartmentService(IEmployeesContext context)
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
                Departments = context.Departments
                    .Include(d => d.DepartmentEmployees.Select(e => e.Employee.Salaries))
                    .Select(d => new DepartmentSalary
                    {
                        Code = d.Code,
                        Name = d.Name,
                        AverageSalary =
                            (int) d.DepartmentEmployees.SelectMany(
                                e => e.Employee.Salaries
                                    .Where(s => s != null)
                                    .Where(s => s.ToDate > DateTime.Now)
                                    .Take(1))
                                .Average(s => s.Amount)
                    })
                    .OrderByDescending(d => d.AverageSalary)
            };
        }
    }
}
