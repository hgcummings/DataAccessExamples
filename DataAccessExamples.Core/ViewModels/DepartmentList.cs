﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessExamples.Core.Data;

namespace DataAccessExamples.Core.ViewModels
{
    public class DepartmentList
    {
        public IEnumerable<Department> Departments { get; set; } 
    }
}