using AutoMapper;
using Company.G05.DAL.Models;
using Company.G05.PL.Models;

namespace Company.G05.PL.Mapping;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<CreateEmployeeDto, Employee>()
            .ForMember(D => D.Name, opt => opt.MapFrom(src => $"{ src.Name } Hello"));
        CreateMap<Employee, CreateEmployeeDto>();
    }
}