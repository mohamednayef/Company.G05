using System.Reflection.Metadata;
using AutoMapper;
using Company.G05.BLL.Interfaces;
using Company.G05.DAL.Models;
using Company.G05.PL.Helpers;
using Company.G05.PL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Company.G05.PL.Controllers;

public class EmployeeController : Controller
{
    // private readonly IEmployeeRepository _employeeRepository;
    // private readonly IDepartmentRepository _departmentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    // ask CLR create object from EmployeeRepository
    public EmployeeController(
        // IEmployeeRepository employeeRepository, 
        // IDepartmentRepository departmentRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper
        )
    {
        // _employeeRepository = employeeRepository;
        // _departmentRepository = departmentRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<IActionResult> Index(string? SearchInput)
    {
        IEnumerable<Employee> employees;
        if (SearchInput == null || SearchInput.Length == 0)
        {
            employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
        }
        else
        {
            employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);
        }
        ViewData["Message"] = "Message From ViewData";
        ViewBag.Msg = "Message From ViewBag";
        
        return View(employees);
    }

    public async Task<IActionResult> Details(int? id, string viewName="Details")
    {
        if(id is null) return BadRequest($"id should not be null");
        
        var employee = await _unitOfWork.EmployeeRepository.GetAsync(id ?? 0);
        
        if(employee is null) return NotFound(new { StatusCode = "404", Message = $"Employee with id {id} not found" });
        
        return View(viewName, employee);
    }

    public async Task<IActionResult> Create()
    {
        ViewData["departments"] = await _unitOfWork.DepartmentRepository.GetAllAsync();
        return View();
    }

    [HttpPost]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateEmployeeDto model)
    {
        if (ModelState.IsValid)
        {
            if (model.Image is not null)
            {
                model.ImageName = DocumentSettings.UploadFile(model.Image, "Images");
            }
            var employee = _mapper.Map<Employee>(model);
            await _unitOfWork.EmployeeRepository.AddAsync(employee);
            int count = await _unitOfWork.SaveChangesAsync();
            if (count > 0)
            {
                
                TempData["Message"] = $"Employee {model.Name} created successfully";
                return RedirectToAction(nameof(Index));
            }
        }

        return View();
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return BadRequest();
        var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
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
        ViewData["departments"] = await _unitOfWork.DepartmentRepository.GetAllAsync();
        return View(createEmployeeDto);
        
        // return Details(id, "Edit");
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromRoute]int? id, CreateEmployeeDto model)
    {
        if (ModelState.IsValid)
        {
            if (model.ImageName is not null)
            {
                DocumentSettings.DeleteFile(model.ImageName, "Images");
            }
            
            if (model.Image is not null)
            {
                model.ImageName = DocumentSettings.UploadFile(model.Image, "Images");
            }
            var employee = _mapper.Map<Employee>(model);
            employee.Id = id.Value;
            
            _unitOfWork.EmployeeRepository.Update(employee);
            int count = await _unitOfWork.SaveChangesAsync();
            if (count > 0)
            {
                TempData["Message"] = $"Employee with id {id} updated successfully";
                return RedirectToAction(nameof(Index));
            }
        }
        return View();
    }

    public async Task<IActionResult> Delete(int? id)
    {
        return await Details(id, "Delete");
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromRoute]int id, Employee model)
    {
        _unitOfWork.EmployeeRepository.Delete(model);
        int count = await _unitOfWork.SaveChangesAsync();
        if (count > 0)
        {
            if (model.ImageName is not null)
            {
                DocumentSettings.DeleteFile(model.ImageName, "Images");
            }
            TempData["Message"] = $"Employee with id {id} deleted successfully";
            return RedirectToAction(nameof(Index));
        }
        await _unitOfWork.SaveChangesAsync();
        return View(model);
    }
}