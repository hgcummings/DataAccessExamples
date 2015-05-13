using DataAccessExamples.Core.Data;
using System.Collections.Generic;

namespace DataAccessExamples.Core.ViewModels
{
    public class EmployeeList
    {
        public IEnumerable<Employee> Employees { get; set; }
    }
}
