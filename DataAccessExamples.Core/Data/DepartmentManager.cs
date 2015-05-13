namespace DataAccessExamples.Core.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class DepartmentManager
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(4)]
        public string DepartmentCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeNumber { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public virtual Department Department { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
