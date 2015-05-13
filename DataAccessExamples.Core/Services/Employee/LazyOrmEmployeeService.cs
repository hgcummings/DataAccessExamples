using AutoMapper;
using DataAccessExamples.Core.Actions;
using DataAccessExamples.Core.Data;
using DataAccessExamples.Core.ViewModels;
using System;
using System.Linq;

namespace DataAccessExamples.Core.Services.Employee
{
    /// <summary>
    ///   Naive and (intentionally) broken implementation of <see cref="IEmployeeService"/> using EF
    ///   Presents two pitfalls: Unintentional Lazy-Loading and use of unsupported expression in Linq2Entities
    /// </summary>
    public class LazyOrmEmployeeService : IEmployeeService
    {
        private readonly IEmployeesContext context;

        public LazyOrmEmployeeService(IEmployeesContext context)
        {
            this.context = context;
        }

        public EmployeeList ListRecentHires()
        {
            return new EmployeeList
            {
                Employees = context.Employees
                    .Where(e => e.HireDate > DateTime.Now.AddDays(-7))
                    .OrderByDescending(e => e.HireDate)
            };
        }

        public void AddEmployee(AddEmployee action)
        {
            var employee = Mapper.Map<Data.Employee>(action);
            employee.DepartmentEmployees.Add(new DepartmentEmployee
            {
                Employee = employee,
                DepartmentCode = action.DepartmentCode,
                FromDate = action.HireDate
            });
            context.Employees.Add(employee);
            context.SaveChanges();
        }
    }
}
