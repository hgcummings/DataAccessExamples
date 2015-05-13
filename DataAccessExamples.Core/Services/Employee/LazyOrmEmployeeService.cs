using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessExamples.Core.Actions;
using DataAccessExamples.Core.Data;
using DataAccessExamples.Core.ViewModels;

namespace DataAccessExamples.Core.Services.Employee
{
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
