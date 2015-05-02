using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessExamples.Core.ViewModels
{
    public class DepartmentSummary
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal? AverageSalary { get; set; }
    }
}
