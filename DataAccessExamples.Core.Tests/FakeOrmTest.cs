using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessExamples.Core.Data;
using DataAccessExamples.Core.Services.Employee;
using FakeItEasy;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Dsl;

namespace DataAccessExamples.Core.Tests
{
    public class FakeOrmTest
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

        private IPostprocessComposer<Employee> AnEmployee
        {
            get { return fixture.Build<Employee>().OmitAutoProperties(); }
        }
    }
}
