using AutoMapper;
using DataAccessExamples.Core.Actions;
using DataAccessExamples.Core.Data;

namespace DataAccessExamples.Core.MappingProfiles
{
    /// <summary>
    ///   AutoMapper configuration for Employee-related models
    /// </summary>
    public class EmployeesProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<AddEmployee, Employee>();
        }
    }
}
