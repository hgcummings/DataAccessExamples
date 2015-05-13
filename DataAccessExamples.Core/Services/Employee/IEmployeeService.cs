using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessExamples.Core.Actions;
using DataAccessExamples.Core.ViewModels;

namespace DataAccessExamples.Core.Services.Employee
{
    public interface IEmployeeService
    {
        void AddEmployee(AddEmployee action);
        EmployeeList ListRecentHires();
    }
}
