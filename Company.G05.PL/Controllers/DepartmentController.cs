using Company.G05.BLL.Interfaces;
using Company.G05.BLL.Repositories;
using Company.G05.DAL.Models;
using Company.G05.PL.Models;
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
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(CreateDepartmentDto model)
    {
        if (ModelState.IsValid)
        {
            var count = _departmentRepository.Add(new Department()
            {
                Code = model.Code,
                Name = model.Name,
                CreatedAt = model.CreatedAt
            });
            if (count > 0)
            {
                return RedirectToAction("Index");
            }
        }
        return View(model);
    }
}