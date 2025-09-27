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

    public IActionResult Details(int id)
    {
        var department = _departmentRepository.Get(id);
        return View(department);
    }

    public IActionResult Edit(int id)
    {
        var department = _departmentRepository.Get(id);
        return View(department);
    }

    [HttpPost]
    public IActionResult Update(Department model)
    {
        if (ModelState.IsValid)
        {
            _departmentRepository.Update(model);
            return RedirectToAction("Index");
        }
        return View(model);
        // _departmentRepository.Departments.update(model);
        // return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var department = _departmentRepository.Get(id);
        _departmentRepository.Delete(department);
        return RedirectToAction("Index");
    }
}