using DataAccessExamples.Core.ViewModels;

namespace DataAccessExamples.Core.Services
{
    public interface IDepartmentService
    {
        DepartmentList ListDepartments();
        DepartmentList ListAverageSalaryPerDepartment();
    }
}