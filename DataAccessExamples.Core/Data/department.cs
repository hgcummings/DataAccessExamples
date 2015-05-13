namespace DataAccessExamples.Core.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///  Represents a Department within the organisation's structure
    /// </summary>
    public partial class Department
    {
        public Department()
        {
            DepartmentEmployees = new HashSet<DepartmentEmployee>();
        }

        [Key]
        [StringLength(4)]
        public string Code { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        public virtual ICollection<DepartmentEmployee> DepartmentEmployees { get; set; }
    }
}
