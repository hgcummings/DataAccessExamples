using DataAccessExamples.Core.Data;
using NUnit.Framework;
using System.Linq;

namespace DataAccessExamples.Core.Tests
{
    public class SqliteTestDatabaseTest
    {
        private SqliteTestDatabase testDatabase;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            testDatabase = new SqliteTestDatabase();

            // TODO: Why is this necessary? The EF context should initialise the DB
            testDatabase.ExecuteScript(InitialiseSchema);
        }

        [TestFixtureTearDown]
        public void AfterAll()
        {
            testDatabase.Dispose();
        }

        [Test]
        public void CanInsertEntity()
        {
            using (var context = new EmployeesContext(testDatabase.CreateConnection(), true))
            {
                var department = new Department
                {
                    Code = "Code",
                    Name = "Department Name"
                };
                context.Departments.Add(department);
                context.SaveChanges();
            }

            using (var context = new EmployeesContext(testDatabase.CreateConnection(), true))
            {
                Assert.AreEqual(1, context.Departments.Count());
            }
        }

        private const string InitialiseSchema = @"CREATE TABLE Department (
                                            [Code] [nchar](4) NOT NULL,
                                            [Name] [nvarchar](40) NOT NULL)";
    }
}
