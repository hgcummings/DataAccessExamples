using DataAccessExamples.Core.Data;
using NUnit.Framework;
using System.Linq;

namespace DataAccessExamples.Core.Tests
{
    public class SqlCeTestDatabaseTest
    {
        private SqlCeTestDatabase testDatabase;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            testDatabase = new SqlCeTestDatabase();
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
    }
}
