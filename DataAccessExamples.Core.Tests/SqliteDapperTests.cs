using Dapper;
using DataAccessExamples.Core.Services.Department;
using NUnit.Framework;
using System.Linq;

namespace DataAccessExamples.Core.Tests
{
    public class SqliteDapperTests
    {
        private SqliteTestDatabase testDatabase;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            testDatabase = new SqliteTestDatabase();
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
