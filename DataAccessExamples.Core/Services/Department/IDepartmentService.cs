using DataAccessExamples.Core.ViewModels;

namespace DataAccessExamples.Core.Services.Department
{
    /// <summary>
    ///   Service for retrieving department information
    /// </summary>
    public interface IDepartmentService
    {
        DepartmentList ListDepartments();
        DepartmentList ListAverageSalaryPerDepartment();
    }
}