namespace DataAccessExamples.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Department")]
    public partial class Department
    {
        public Department()
        {
            DepartmentEmployees = new HashSet<DepartmentEmployee>();
            DepartmentManagers = new HashSet<DepartmentManager>();
        }

        [Key]
        [StringLength(4)]
        public string Code { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        public virtual ICollection<DepartmentEmployee> DepartmentEmployees { get; set; }

        public virtual ICollection<DepartmentManager> DepartmentManagers { get; set; }
    }
}
