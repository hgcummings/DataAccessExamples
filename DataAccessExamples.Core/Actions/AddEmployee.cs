using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessExamples.Core.Actions
{
    public class AddEmployee
    {
        [Required]
        public DateTime HireDate { get; set; }

        [Required]
        public string DepartmentCode { get; set; }

        [Required]
        [StringLength(14)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(16)]
        public string LastName { get; set; }

        [Required]
        [StringLength(1)]
        public string Gender { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}