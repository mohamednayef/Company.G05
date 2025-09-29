using AutoMapper;
using Company.G05.BLL.Interfaces;
using Company.G05.DAL.Models;
using Company.G05.PL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Company.G05.PL.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;
    
    // ask CLR create object from EmployeeRepository
    public EmployeeController(
        IEmployeeRepository employeeRepository, 
        IDepartmentRepository departmentRepository,
        IMapper mapper
        )
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }
    
    public IActionResult Index(string? SearchInput)
    {
        IEnumerable<Employee> employees;
        if (SearchInput == null || SearchInput.Length == 0)
        {
            employees = _employeeRepository.GetAll();
        }
        else
        {
            employees = _employeeRepository.GetByName(SearchInput);
        }
        ViewData["Message"] = "Message From ViewData";
        ViewBag.Msg = "Message From ViewBag";
        
        return View(employees);
    }

    public IActionResult Details(int? id, string viewName="Details")
    {
        if(id is null) return BadRequest($"id should not be null");
        
        var employee = _employeeRepository.Get(id ?? 0);
        
        if(employee is null) return NotFound(new { StatusCode = "404", Message = $"Employee with id {id} not found" });
        
        return View(viewName, employee);
    }

    public IActionResult Create()
    {
        ViewData["departments"] = _departmentRepository.GetAll();
        return View();
    }

    [HttpPost]
    // [ValidateAntiForgeryToken]
    public IActionResult Create(CreateEmployeeDto model)
    {
        if (ModelState.IsValid)
        {
            // int count = _employeeRepository.Add(new Employee()
            // {
            //     Name = model.Name,
            //     Age = model.Age,
            //     Email = model.Email,
            //     Address = model.Address,
            //     Phone = model.Phone,
            //     Salary = model.Salary,
            //     IsActive = model.IsActive,
            //     IsDeleted = model.IsDeleted,
            //     HirignDate = model.HirignDate,
            //     CreatedAt = model.CreatedAt,
            //     DepartmentId = model.DepartmentId,
            // });
            var employee = _mapper.Map<Employee>(model);
            int count = _employeeRepository.Add(employee);
            if (count > 0)
            {
                TempData["Message"] = $"Employee {model.Name} created successfully";
                return RedirectToAction(nameof(Index));
            }
        }

        return View();
    }

    public IActionResult Edit(int? id)
    {
        if (id is null) return BadRequest();
        var employee = _employeeRepository.Get(id.Value);
        if (employee is null) return NotFound();
        // var createEmployeeDto = new CreateEmployeeDto()
        // {
        //     Name = employee.Name,
        //     Age = employee.Age,
        //     Email = employee.Email,
        //     Address = employee.Address,
        //     Phone = employee.Phone,
        //     Salary = employee.Salary,
        //     IsActive = employee.IsActive,
        //     IsDeleted = employee.IsDeleted,
        //     HirignDate = employee.HirignDate,
        //     CreatedAt = employee.CreatedAt,
        //     DepartmentId = employee.DepartmentId,
        // };
        var createEmployeeDto = _mapper.Map<CreateEmployeeDto>(employee);
        ViewData["departments"] = _departmentRepository.GetAll();
        return View(createEmployeeDto);
        
        // return Details(id, "Edit");
    }

    [HttpPost]
    public IActionResult Edit([FromRoute]int? id, CreateEmployeeDto model)
    {
        if (ModelState.IsValid)
        {
            var employee = new Employee()
            {
                Id = id.Value,
                Name = model.Name,
                Age = model.Age,
                Email = model.Email,
                Address = model.Address,
                Phone = model.Phone,
                Salary = model.Salary,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                HirignDate = model.HirignDate,
                CreatedAt = model.CreatedAt,
                DepartmentId = model.DepartmentId,
            };
            int count = _employeeRepository.Update(employee);
            if (count > 0)
            {
                TempData["Message"] = $"Employee with id {id} updated successfully";
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
            TempData["Message"] = $"Employee with id {id} deleted successfully";
            return RedirectToAction(nameof(Index));
        }
        
        return View(model);
    }
}