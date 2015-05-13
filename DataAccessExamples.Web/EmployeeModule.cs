using DataAccessExamples.Core.Actions;
using DataAccessExamples.Core.Services.Department;
using DataAccessExamples.Core.Services.Employee;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;

namespace DataAccessExamples.Web
{
    /// <summary>
    ///   Web interface for adding and retrieving new Employees
    /// </summary>
    public class EmployeeModule : NancyModule
    {
        public EmployeeModule(IEmployeeService employeeService,
            IDepartmentService departmentService) : base("employees")
        {
            Get["/add"] = parameters => View["Create.cshtml", departmentService.ListDepartments()];
            Post["/add"] = parameters =>
            {
                employeeService.AddEmployee(this.Bind<AddEmployee>());
                return new RedirectResponse("/employees/recent");
            };
            Get["/recent"] = parameters => View["Recent.cshtml", employeeService.ListRecentHires()];
        }
    }
}