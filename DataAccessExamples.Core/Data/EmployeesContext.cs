using System.Data.Common;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DataAccessExamples.Core.Data
{
    using System.Data.Entity;

    public partial class EmployeesContext : DbContext
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
//            modelBuilder.Entity<Department>()
//                .Property(e => e.Code)
//                .IsFixedLength();

//            modelBuilder.Entity<DepartmentEmployee>()
//                .Property(e => e.DepartmentCode)
//                .IsFixedLength();
//
//            modelBuilder.Entity<DepartmentManager>()
//                .Property(e => e.DepartmentCode)
//                .IsFixedLength();
        }
    }
}
