using Company.G05.BLL.Interfaces;
using Company.G05.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Company.G05.PL.Controllers;

public class DepartmentController : Controller
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentController(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }
    
    public IActionResult Index()
    {
        var departments = _departmentRepository.GetAll();
        return View(departments);
    }
}