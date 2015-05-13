using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessExamples.Core.Data;
using Nancy.Testing;
using NUnit.Framework;
using Simple.Data;

namespace DataAccessExamples.Web.Tests
{
    public class HeadlessInMemorySimpleDataTests
    {
        [Test]
        public void ListDepartmentsShowsDepartmentsInOrder()
        {
            // Arrange
            var bootstrapper = new Bootstrapper();
            var browser = new Browser(bootstrapper);
            var adapter = new InMemoryAdapter();
            Database.UseMockAdapter(adapter);
            var db = Database.Open();
            var department1 = new Department {Code = "d01", Name = "First Department"};
            var department2 = new Department {Code = "d02", Name = "Second Department"};
            db.Departments.Insert(department2);
            db.Departments.Insert(department1);

            // Act
            var response = browser.Get("/departments/list", with => with.Cookie("Impl", "SimpleData"));

            // Assert
            var departments = response.Body[".departments li"].ToList();
            Assert.That(departments.Count, Is.EqualTo(2));
            Assert.That(departments[0].InnerText.Contains(department1.Name));
        }
    }
}
