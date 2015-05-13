using DataAccessExamples.Core.Services.Department;
using Nancy;

namespace DataAccessExamples.Web
{
    public class DepartmentModule : NancyModule
    {
        public DepartmentModule(IDepartmentService service) : base("departments")
        {
            Get["/list"] = parameters => View["List.cshtml", service.ListDepartments()];
            Get["/salaries"] = parameters => View["Salaries.cshtml", service.ListAverageSalaryPerDepartment()];
        }
    }
}