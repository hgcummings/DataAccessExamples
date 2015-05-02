using DataAccessExamples.Core.Data;
using DataAccessExamples.Core.ViewModels;

namespace DataAccessExamples.Core.Services
{
    public class LazyOrmDepartmentService : IDepartmentService
    {
        private readonly EmployeesContext context;

        public LazyOrmDepartmentService(EmployeesContext context)
        {
            this.context = context;
        }

        public DepartmentList ListDepartments()
        {
            return new DepartmentList {Departments = context.Departments};
        }
    }
}
