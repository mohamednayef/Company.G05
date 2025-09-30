using AutoMapper;
using Company.G05.DAL.Models;
using Company.G05.PL.Models;

namespace Company.G05.PL.Mapping;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<CreateEmployeeDto, Employee>()
            .ForMember(D => D.Name, opt => opt.MapFrom(src => $"{ src.Name }"));
        CreateMap<Employee, CreateEmployeeDto>()
            // .ForMember(D => D.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
            ;
    }
}