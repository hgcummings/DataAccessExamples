using System.Linq;

namespace DataAccessExamples.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    ///   A current or past member of staff
    /// </summary>
    public partial class Employee
    {
        public Employee()
        {
            DepartmentEmployees = new HashSet<DepartmentEmployee>();
            Salaries = new HashSet<Salary>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        public virtual ICollection<Salary> Salaries { get; set; }

        public Department PrimaryDepartment
        {
            get { return DepartmentEmployees.Where(de => de.ToDate > DateTime.Now).Select(de => de.Department).First(); }
        }
    }
}
