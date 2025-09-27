using Company.G05.BLL.Interfaces;
using Company.G05.DAL.Models;
using Company.G05.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.G05.PL.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;
    
    // ask CLR create object from EmployeeRepository
    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    
    public IActionResult Index()
    {
        var employees = _employeeRepository.GetAll();
        return View(employees);
    }

    public IActionResult Details(int? id, string viewName)
    {
        if(id is null) return BadRequest($"id should not be null");
        
        var employee = _employeeRepository.Get(id.Value);
        
        if(employee is null) return NotFound(new { StatusCode = "404", Message = $"Employee with id {id} not found" });
        
        return View(viewName, employee);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    // [ValidateAntiForgeryToken]
    public IActionResult Create(CreateEmployeeDto model)
    {
        if (ModelState.IsValid)
        {
            int count = _employeeRepository.Add(new Employee()
            {
                Name = model.Name,
                Age = model.Age,
                Email = model.Email,
                Address = model.Address,
                Phone = model.Phone,
                Salary = model.Salary,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                HirignDate = model.HirignDate,
                CreatedAt = model.CreatedAt
            });
            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        return View();
    }

    public IActionResult Edit(int? id)
    {
        return Details(id, "Edit");
    }

    [HttpPost]
    public IActionResult Edit(Employee model)
    {
        if (ModelState.IsValid)
        {
            int count = _employeeRepository.Update(model);
            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        return View();
    }

    public IActionResult Delete(int? id)
    {
        return Details(id, "Delete");
    }

    [HttpPost]
    public IActionResult Delete([FromRoute]int id, Employee model)
    {
        int count = _employeeRepository.Delete(model);
        if (count > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        
        return View(model);
    }
}