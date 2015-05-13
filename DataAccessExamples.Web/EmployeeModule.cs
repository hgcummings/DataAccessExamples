using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessExamples.Core.Actions;
using DataAccessExamples.Core.Services;
using DataAccessExamples.Core.Services.Department;
using DataAccessExamples.Core.Services.Employee;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;

namespace DataAccessExamples.Web
{
    public class EmployeeModule : NancyModule
    {
        public EmployeeModule(IEmployeeService employeeService,
            IDepartmentService departmentService) : base("employee")
        {
            Get["/add"] = parameters => View["Create.cshtml", departmentService.ListDepartments()];
            Post["/add"] = parameters =>
            {
                employeeService.AddEmployee(this.Bind<AddEmployee>(new BindingConfig {BodyOnly = true}));
                return new RedirectResponse("/employee/recent");
            };
            Get["/recent"] = parameters => View["Recent.cshtml", employeeService.ListRecentHires()];
        }
    }
}