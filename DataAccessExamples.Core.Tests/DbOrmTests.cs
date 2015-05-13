using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccessExamples.Core.Actions;
using DataAccessExamples.Core.Data;
using DataAccessExamples.Core.MappingProfiles;
using DataAccessExamples.Core.Services.Employee;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace DataAccessExamples.Core.Tests
{
    public class DbOrmTests
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

        [SetUp]
        public void BeforeEach()
        {
            using (var context = CreateContext())
            {
                context.Departments.RemoveRange(context.Departments);
                context.Employees.RemoveRange(context.Employees);
                context.SaveChanges();
            }
        }

        [Test]
        public void ListRecentHires_ReturnsEmployeesInLastWeek()
        {
            // Arrange
            var department = new Department { Code = "Code", Name = "Department Name" };
            using (var context = CreateContext())
            {
                context.Departments.Add(department);
                context.Employees.Add(EmployeeWithDepartmentAndHireDate(department, DateTime.Now.AddDays(-6)));
                context.Employees.Add(EmployeeWithDepartmentAndHireDate(department, DateTime.Now.AddDays(-1)));
                context.Employees.Add(EmployeeWithDepartmentAndHireDate(department, DateTime.Now.AddDays(-8)));
                context.SaveChanges();
            }

            // Act
            List<Employee> result;
            using (var context = CreateContext())
            {
                var service = new EagerOrmEmployeeService(context);
                result = service.ListRecentHires().Employees.ToList();
            }

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.All(e => e.HireDate > DateTime.Now.AddDays(-7)));
            Assert.That(result.All(e => e.PrimaryDepartment.Code == department.Code));
        }

        [Test]
        public void AddEmployee_SavesEmployeeWithDepartment()
        {
            // Arrange
            Mapper.Initialize(cfg => { cfg.AddProfile(new EmployeesProfile()); });
            var department = new Department { Code = "Code", Name = "Department Name" };
            using (var context = CreateContext())
            {
                context.Departments.Add(department);
                context.SaveChanges();
            }

            // Act
            var newEmployee = fixture.Build<AddEmployee>()
                .With(e => e.HireDate, DateTime.Now)
                .With(e => e.DepartmentCode, department.Code)
                .Create();
            using (var context = CreateContext())
            {
                var service = new EagerOrmEmployeeService(context);
                service.AddEmployee(newEmployee);
            }

            // Assert
            using (var context = CreateContext())
            {
                var service = new EagerOrmEmployeeService(context);
                var actualEmployee = service.ListRecentHires().Employees.Single();
                Assert.That(actualEmployee.FirstName, Is.EqualTo(newEmployee.FirstName));
                Assert.That(
                    actualEmployee.PrimaryDepartment.Code,
                    Is.EqualTo(newEmployee.DepartmentCode));
            }
        }

        private EmployeesContext CreateContext()
        {
            return new EmployeesContext(testDatabase.CreateConnection(), true);
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
