using DataAccessExamples.Core.ViewModels;

namespace DataAccessExamples.Core.Services.Department
{
    public interface IDepartmentService
    {
        DepartmentList ListDepartments();
        DepartmentList ListAverageSalaryPerDepartment();
    }
}