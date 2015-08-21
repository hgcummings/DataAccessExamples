using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using DataAccessExamples.Core.SqlUtils;
using DataAccessExamples.Core.ViewModels;

namespace DataAccessExamples.Core.Services.Department
{
    public class RawAdoDepartmentService : IDepartmentService
    {
        private IDbConnectionFactory connectionFactory;

        public RawAdoDepartmentService(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public DepartmentList ListDepartments()
        {
            using (var connection = connectionFactory.CreateConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT Code, Name FROM Department ORDER BY Department.Code";

                var reader = command.ExecuteReader();

                var departments = new List<DepartmentList.Item>();
                while (reader.Read())
                {
                    departments.Add(new DepartmentList.Item
                    {
                        Code = reader.GetString(0),
                        Name = reader.GetString(1)
                    });
                }
                return new DepartmentList {Departments = departments};
            }
        }

        public DepartmentList ListAverageSalaryPerDepartment()
        {
            throw new NotImplementedException();
        }
    }
}