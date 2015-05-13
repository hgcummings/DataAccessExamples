using AutoMapper;
using DataAccessExamples.Core.Actions;
using DataAccessExamples.Core.Data;

namespace DataAccessExamples.Core.MappingProfiles
{
    public class EmployeesProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<AddEmployee, Employee>();
        }
    }
}
