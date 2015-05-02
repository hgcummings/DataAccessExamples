namespace DataAccessExamples.Core.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EmployeesContext : DbContext
    {
        public EmployeesContext() : base("name=EmployeesContext")
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .Property(e => e.dept_no)
                .IsFixedLength();

            modelBuilder.Entity<DepartmentEmployee>()
                .Property(e => e.dept_no)
                .IsFixedLength();

            modelBuilder.Entity<DepartmentManager>()
                .Property(e => e.dept_no)
                .IsFixedLength();
        }
    }
}
