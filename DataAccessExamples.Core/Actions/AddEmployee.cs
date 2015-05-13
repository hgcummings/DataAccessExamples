using System;

namespace DataAccessExamples.Core.Actions
{
    public class AddEmployee
    {
        public DateTime HireDate { get; set; }

        public string DepartmentCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}