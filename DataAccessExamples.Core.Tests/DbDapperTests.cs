using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataAccessExamples.Core.Services.Department;
using NUnit.Framework;

namespace DataAccessExamples.Core.Tests
{
    public class DbDapperTests
    {
        private TestDatabase testDatabase;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            testDatabase = new TestDatabase();
            testDatabase.ExecuteScript(InitialiseSchema);
        }

        [TestFixtureTearDown]
        public void AfterAll()
        {
            testDatabase.Dispose();
        }

        [Test]
        public void ListDepartments_ReturnsDepartmentsByCode()
        {
            // Arrange
            using (var connection = testDatabase.CreateConnection())
            {
                connection.Execute("INSERT INTO Department (Code, Name) Values ('d02', 'Second Department')");
                connection.Execute("INSERT INTO Department (Code, Name) Values ('d01', 'First Department')");
            }

            // Act
            var service = new DapperDepartmentService(testDatabase);
            var result = service.ListDepartments();

            // Assert
            Assert.That(result.Departments.Select(d => d.Name),
                Is.EqualTo(new[] {"First Department", "Second Department"}));
        }

        private const string InitialiseSchema = @"CREATE TABLE Department (
                                            [Code] [nchar](4) NOT NULL,
                                            [Name] [nvarchar](40) NOT NULL)";
    }
}
