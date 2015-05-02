namespace DataAccessExamples.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee
    {
        public Employee()
        {
            dept_emp = new HashSet<DepartmentEmployee>();
            dept_manager = new HashSet<DepartmentManager>();
            salaries = new HashSet<Salary>();
            titles = new HashSet<Title>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int emp_no { get; set; }

        [Column(TypeName = "date")]
        public DateTime birth_date { get; set; }

        [Required]
        [StringLength(14)]
        public string first_name { get; set; }

        [Required]
        [StringLength(16)]
        public string last_name { get; set; }

        [Required]
        [StringLength(1)]
        public string gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime hire_date { get; set; }

        public virtual ICollection<DepartmentEmployee> dept_emp { get; set; }

        public virtual ICollection<DepartmentManager> dept_manager { get; set; }

        public virtual ICollection<Salary> salaries { get; set; }

        public virtual ICollection<Title> titles { get; set; }
    }
}
