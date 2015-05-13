using DataAccessExamples.Core.Actions;
using DataAccessExamples.Core.ViewModels;

namespace DataAccessExamples.Core.Services.Employee
{
    /// <summary>
    ///   Service for adding and retrieving new <see cref="Data.Employee"/>s
    /// </summary>
    public interface IEmployeeService
    {
        EmployeeList ListRecentHires();
        void AddEmployee(AddEmployee action);
    }
}
