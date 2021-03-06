﻿using DataAccessExamples.Core.Data;
using DataAccessExamples.Core.ViewModels;
using System;
using System.Linq;

namespace DataAccessExamples.Core.Services.Department
{
    /// <summary>
    ///   Implementation of <see cref="IDepartmentService"/> using Entity Framework naively with Lazy Loading
    /// </summary>
    public class LazyOrmDepartmentService : IDepartmentService
    {
        private readonly IEmployeesContext context;

        public LazyOrmDepartmentService(IEmployeesContext context)
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
                }).OrderByDescending(d => d.AverageSalary)
            };
        }
    }
}
