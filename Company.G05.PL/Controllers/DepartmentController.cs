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
                TempData["Message"] = "Department created successfully";
                return RedirectToAction("Index");
            }
        }
        return View(model);
    }

    public IActionResult Details(int? id, string viewName = "Details")
    {
        if (id is null) return BadRequest();
        
        var department = _departmentRepository.Get(id.Value);
        if (department is null) return NotFound( new { StatusCode = "404", Message = $"Department with id: {id.Value} not found" });
        
        return View(viewName, department);
    }

    public IActionResult Edit(int? id)
    {
        if (id is null) return BadRequest();
        
        var department = _departmentRepository.Get(id.Value);
        if (department is null) return NotFound( new { StatusCode = "404", Message = $"Department with id: {id.Value} not found" });
        var departmentDto = new CreateDepartmentDto()
        {
            Code = department.Code,
            Name = department.Name,
            CreatedAt = department.CreatedAt
        };
        return View(departmentDto);
        
        // return Details(id, viewName: "Edit");
    }

    [HttpPost]
    // [ValidateAntiForgeryToken]
    public IActionResult Edit([FromRoute]int id, CreateDepartmentDto model)
    {
        // return Content("df");
        if (ModelState.IsValid)
        {
            // if (id != model.Id) return BadRequest();
            int count = _departmentRepository.Update(new Department()
            {
                Id = id,
                Code = model.Code,
                Name = model.Name,
                CreatedAt = model.CreatedAt
            });
            if (count > 0)
            {
                TempData["Message"] = "Department updated successfully";
                return RedirectToAction("Index");
            }
        }

        CreateDepartmentDto departmentDto = new CreateDepartmentDto()
        {
            Code = model.Code,
            Name = model.Name,
            CreatedAt = model.CreatedAt
        };
        return View(departmentDto);
    }
    
    public IActionResult Delete(int? id)
    {
        // if (id is null) return BadRequest();
        //
        // var department = _departmentRepository.Get(id.Value);
        // if (department is null) return NotFound( new { StatusCode = "404", Message = $"Department with id: {id.Value} not found" });
        //
        // return View(department);
        
        return Details(id, viewName: "Delete");
    }
    
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var department = _departmentRepository.Get(id);
        _departmentRepository.Delete(department);
        TempData["Message"] = "Department deleted successfully";
        return RedirectToAction("Index");
    }
}