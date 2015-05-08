namespace DataAccessExamples.Core.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Salary
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeNumber { get; set; }

        public int Amount { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
