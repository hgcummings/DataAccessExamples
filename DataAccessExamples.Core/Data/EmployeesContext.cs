using System.Data.Common;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DataAccessExamples.Core.Data
{
    using System.Data.Entity;

    /// <summary>
    ///   Interface to provide a seam for replacing <see cref="EmployeesContext"/> with a test double
    /// </summary>
    public interface IEmployeesContext
    {
        DbSet<Department> Departments { get; set; }
        DbSet<Employee> Employees { get; set; }
        int SaveChanges();
    }

    /// <summary>
    ///   Entity framework data context for employees data
    /// </summary>
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
