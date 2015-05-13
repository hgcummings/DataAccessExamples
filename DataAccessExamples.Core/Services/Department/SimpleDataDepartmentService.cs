using DataAccessExamples.Core.ViewModels;
using Simple.Data;
using System;

namespace DataAccessExamples.Core.Services.Department
{
    /// <summary>
    ///   Implementation of <see cref="IDepartmentService"/> using Simple.Data
    /// </summary>
    public class SimpleDataDepartmentService : IDepartmentService
    {
        public DepartmentList ListDepartments()
        {
            var db = Database.OpenNamedConnection("EmployeesContext");
            return new DepartmentList
            {
                Departments = db.Departments.All().OrderBy(db.Departments.Code)
            };
        }

        public DepartmentList ListAverageSalaryPerDepartment()
        {
            throw new NotImplementedException();
        }
    }
}
