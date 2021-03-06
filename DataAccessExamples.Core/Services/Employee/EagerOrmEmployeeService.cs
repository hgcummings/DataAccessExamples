﻿using AutoMapper;
using DataAccessExamples.Core.Actions;
using DataAccessExamples.Core.Data;
using DataAccessExamples.Core.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;

namespace DataAccessExamples.Core.Services.Employee
{
    /// <summary>
    ///   Implementation of <see cref="IEmployeeService"/> using EF and eager-loading where possible
    /// </summary>
    public class EagerOrmEmployeeService : IEmployeeService
    {
        private readonly IEmployeesContext context;

        public EagerOrmEmployeeService(IEmployeesContext context)
        {
            this.context = context;
        }

        public EmployeeList ListRecentHires()
        {
            var minDate = DateTime.Now.AddDays(-7);
            return new EmployeeList
            {
                Employees = context.Employees
                    .Include(e => e.DepartmentEmployees.Select(de => de.Department))
                    .Where(e => e.HireDate > minDate)
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
