using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessExamples.Core.Data;
using DataAccessExamples.Core.Services.Department;
using DataAccessExamples.Core.Services.Employee;
using DataAccessExamples.Core.ViewModels;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Dsl;

namespace DataAccessExamples.Core.Tests
{
    public class DbOrmTest
    {
        private readonly Fixture fixture = new Fixture();
        private TestDatabase testDatabase;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            testDatabase = new TestDatabase();
        }

        [TestFixtureTearDown]
        public void AfterAll()
        {
            testDatabase.Dispose();
        }

        [Test]
        public void ListRecentHires_ReturnsEmployeesInLastWeek()
        {
            var department = new Department { Code = "Code", Name = "Department Name" };
            using (var context = new EmployeesContext(testDatabase.CreateConnection(), true))
            {
                context.Departments.Add(department);
                context.Employees.Add(EmployeeWithDepartmentAndHireDate(department, DateTime.Now.AddDays(-6)));
                context.Employees.Add(EmployeeWithDepartmentAndHireDate(department, DateTime.Now.AddDays(-1)));
                context.Employees.Add(EmployeeWithDepartmentAndHireDate(department, DateTime.Now.AddDays(-8)));
                context.SaveChanges();
            }

            List<Employee> result;
            using (var context = new EmployeesContext(testDatabase.CreateConnection(), true))
            {
                var service = new EagerOrmEmployeeService(context);
                result = service.ListRecentHires().Employees.ToList();
            }

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.All(e => e.HireDate > DateTime.Now.AddDays(-7)));
            Assert.That(result.All(e => e.PrimaryDepartment.Code == department.Code));
        }

        private Employee EmployeeWithDepartmentAndHireDate(Department department, DateTime hireDate)
        {
            Employee employee = fixture.Build<Employee>()
                .With(e => e.HireDate, hireDate)
                .With(e => e.DateOfBirth, DateTime.Now.AddYears(-30))
                .Without(e => e.DepartmentEmployees)
                .Without(e => e.DepartmentManagers)
                .Without(e => e.Positions)
                .Without(e => e.Salaries)
                .Create();

            DepartmentEmployee departmentEmployee = new DepartmentEmployee
            {
                Department = department,
                Employee = employee,
                FromDate = hireDate,
                ToDate = DateTime.Now.AddYears(10)
            };

            employee.DepartmentEmployees.Add(departmentEmployee);
            return employee;
        }
    }
}
