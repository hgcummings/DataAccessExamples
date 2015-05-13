using AutoMapper;
using DataAccessExamples.Core.Actions;
using DataAccessExamples.Core.Data;
using DataAccessExamples.Core.MappingProfiles;
using DataAccessExamples.Core.Services.Employee;
using FakeItEasy;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Dsl;
using System;
using System.Linq;

namespace DataAccessExamples.Core.Tests
{
    public class FakeOrmTests
    {
        private readonly Fixture fixture = new Fixture();

        [Test]
        public void ListRecentHires_ReturnsEmployeesInLastWeekMostRecentFirst()
        {
            // Arrange
            var recentEmployee = AnEmployee.With(e => e.HireDate, DateTime.Now.AddDays(-6)).Create();
            var newestEmployee = AnEmployee.With(e => e.HireDate, DateTime.Now.AddDays(-1)).Create();
            var olderEmployee = AnEmployee.With(e => e.HireDate, DateTime.Now.AddDays(-8)).Create();
            var fakeEmployees = new StubDbSet<Employee>(recentEmployee, newestEmployee, olderEmployee);

            var fakeEmployeesContext = A.Fake<IEmployeesContext>();
            A.CallTo(() => fakeEmployeesContext.Employees).Returns(fakeEmployees);

            // Act
            var service = new LazyOrmEmployeeService(fakeEmployeesContext);
            var result = service.ListRecentHires();

            // Assert
            Assert.That(result.Employees.ToList(), Is.EqualTo(new[] {newestEmployee, recentEmployee}));
        }

        [Test]
        public void AddEmployee_SavesEmployeeWithDepartment()
        {
            // Arrange
            var fakeEmployeesContext = A.Fake<IEmployeesContext>();
            var fakeEmployees = new StubDbSet<Employee>();
            A.CallTo(() => fakeEmployeesContext.Employees).Returns(fakeEmployees);
            var newEmployee = fixture.Create<AddEmployee>();
            Mapper.Initialize(cfg => { cfg.AddProfile(new EmployeesProfile()); });

            // Act
            var service = new LazyOrmEmployeeService(fakeEmployeesContext);
            service.AddEmployee(newEmployee);

            // Assert
            var actualEmployee = fakeEmployees.Single();
            Assert.That(actualEmployee.FirstName, Is.EqualTo(newEmployee.FirstName));
            Assert.That(
                actualEmployee.DepartmentEmployees.Single().DepartmentCode,
                Is.EqualTo(newEmployee.DepartmentCode));
            A.CallTo(() => fakeEmployeesContext.SaveChanges()).MustHaveHappened();
        }

        private IPostprocessComposer<Employee> AnEmployee
        {
            get { return fixture.Build<Employee>().OmitAutoProperties(); }
        }
    }
}
