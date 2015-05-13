using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessExamples.Core.ViewModels;
using Simple.Data;

namespace DataAccessExamples.Core.Services.Department
{
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
