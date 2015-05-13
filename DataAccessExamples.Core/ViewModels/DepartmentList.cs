using System.Collections.Generic;

namespace DataAccessExamples.Core.ViewModels
{
    public class DepartmentList
    {
        public IEnumerable<Item> Departments { get; set; }

        public class Item
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }
    }
}
