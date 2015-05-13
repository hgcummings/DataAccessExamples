using System.Data.Common;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DataAccessExamples.Core.Data
{
    using System.Data.Entity;

    public interface IEmployeesContext
    {
        DbSet<Department> Departments { get; set; }
        DbSet<Employee> Employees { get; set; }
        int SaveChanges();
    }

    public partial class EmployeesContext : DbContext, IEmployeesContext
    {
        public EmployeesContext()
        {
        }

        public EmployeesContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
