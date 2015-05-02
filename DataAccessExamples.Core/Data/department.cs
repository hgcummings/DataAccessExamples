namespace DataAccessExamples.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<DepartmentEmployee>();
            Managers = new HashSet<DepartmentManager>();
        }

        [Key]
        [StringLength(4)]
        public string dept_no { get; set; }

        [Required]
        [StringLength(40)]
        public string dept_name { get; set; }

        public virtual ICollection<DepartmentEmployee> Employees { get; set; }

        public virtual ICollection<DepartmentManager> Managers { get; set; }
    }
}
