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
            DepartmentEmployees = new HashSet<DepartmentEmployee>();
            DepartmentManagers = new HashSet<DepartmentManager>();
            Salaries = new HashSet<Salary>();
            Positions = new HashSet<Position>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Number { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(14)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(16)]
        public string LastName { get; set; }

        [Required]
        [StringLength(1)]
        public string Gender { get; set; }

        public DateTime HireDate { get; set; }

        public virtual ICollection<DepartmentEmployee> DepartmentEmployees { get; set; }

        public virtual ICollection<DepartmentManager> DepartmentManagers { get; set; }

        public virtual ICollection<Salary> Salaries { get; set; }

        public virtual ICollection<Position> Positions { get; set; }
    }
}
