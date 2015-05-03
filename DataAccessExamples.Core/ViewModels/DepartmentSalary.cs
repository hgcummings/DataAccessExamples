using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessExamples.Core.ViewModels
{
    public class DepartmentSalary : DepartmentList.Item
    {
        public int AverageSalary { get; set; }
    }
}
