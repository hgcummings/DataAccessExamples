using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessExamples.Core.ViewModels
{
    public class EmployeeList
    {
        public IEnumerable<Item> Employees { get; set; }

        public class Item
        {
            public int Number { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime HireDate { get; set; }
        }
    }
}
