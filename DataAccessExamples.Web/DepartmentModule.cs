using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessExamples.Core.Services;
using Nancy;

namespace DataAccessExamples.Web
{
    public class DepartmentModule : NancyModule
    {
        public DepartmentModule(IDepartmentService service) : base("department")
        {
            Get["/list"] = parameters => View["List.cshtml", service.ListDepartments()];
        }
    }
}