using AutoMapper;
using Company.G05.DAL.Models;
using Company.G05.PL.Models;

namespace Company.G05.PL.Mapping;

public class DepartmentProflie : Profile
{
    public DepartmentProflie()
    {
        CreateMap<CreateDepartmentDto, Department>();
        CreateMap<Department, CreateDepartmentDto>();
    }
}