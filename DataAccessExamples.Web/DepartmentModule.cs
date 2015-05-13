using DataAccessExamples.Core.Services.Department;
using Nancy;

namespace DataAccessExamples.Web
{
    /// <summary>
    ///   Web interface for retrieving Department data
    /// </summary>
    public class DepartmentModule : NancyModule
    {
        public DepartmentModule(IDepartmentService service) : base("departments")
        {
            Get["/list"] = parameters => View["List.cshtml", service.ListDepartments()];
            Get["/salaries"] = parameters => View["Salaries.cshtml", service.ListAverageSalaryPerDepartment()];
        }
    }
}