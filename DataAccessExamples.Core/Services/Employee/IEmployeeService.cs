using DataAccessExamples.Core.Actions;
using DataAccessExamples.Core.ViewModels;

namespace DataAccessExamples.Core.Services.Employee
{
    public interface IEmployeeService
    {
        EmployeeList ListRecentHires();
        void AddEmployee(AddEmployee action);
    }
}
