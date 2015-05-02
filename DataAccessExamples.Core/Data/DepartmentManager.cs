namespace DataAccessExamples.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DepartmentManager
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(4)]
        public string dept_no { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int emp_no { get; set; }

        [Column(TypeName = "date")]
        public DateTime from_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime to_date { get; set; }

        public virtual Department department { get; set; }

        public virtual Employee employee { get; set; }
    }
}
