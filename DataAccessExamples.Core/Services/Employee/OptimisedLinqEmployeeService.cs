﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessExamples.Core.Actions;
using DataAccessExamples.Core.ViewModels;

namespace DataAccessExamples.Core.Services.Employee
{
    public class OptimisedLinqEmployeeService : IEmployeeService
    {
        public void AddEmployee(AddEmployee action)
        {
            throw new NotImplementedException();
        }

        public EmployeeList ListRecentHires()
        {
            throw new NotImplementedException();
        }
    }
}
