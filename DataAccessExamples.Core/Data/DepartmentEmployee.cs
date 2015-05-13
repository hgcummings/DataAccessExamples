namespace DataAccessExamples.Core.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    ///   Represents the tenure of a particular <see cref="Employee"/> under a given <see cref="Department"/>
    /// </summary>
    public partial class DepartmentEmployee
    {
        public DepartmentEmployee()
        {
            ToDate = new DateTime(9999, 1, 1);
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeNumber { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(4)]
        public string DepartmentCode { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public virtual Department Department { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
