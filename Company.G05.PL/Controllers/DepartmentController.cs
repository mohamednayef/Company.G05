using AutoMapper;
using Company.G05.BLL.Interfaces;
using Company.G05.BLL.Repositories;
using Company.G05.DAL.Models;
using Company.G05.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.G05.PL.Controllers;

public class DepartmentController : Controller
{
    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;
    // private readonly IDepartmentRepository _departmentRepository;
    
    public DepartmentController(
        // IDepartmentRepository departmentRepository
        IMapper mapper,
        IUnitOfWork unitOfWork
        )
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        // _departmentRepository = departmentRepository;
    }
    
    public async Task<IActionResult> Index()
    {
        var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
        return View(departments);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateDepartmentDto model)
    {
        if (ModelState.IsValid)
        {
            var department = _mapper.Map<Department>(model);
            await _unitOfWork.DepartmentRepository.AddAsync(department);
            int count = await _unitOfWork.SaveChangesAsync();
            if (count > 0)
            {
                TempData["Message"] = "Department created successfully";
                return RedirectToAction("Index");
            }
        }
        await _unitOfWork.SaveChangesAsync();
        return View(model);
    }

    public async Task<IActionResult> Details(int? id, string viewName = "Details")
    {
        if (id is null) return BadRequest();
        
        var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
        if (department is null) return NotFound( new { StatusCode = "404", Message = $"Department with id: {id.Value} not found" });
        
        return View(viewName, department);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return BadRequest();
        
        var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
        if (department is null) return NotFound( new { StatusCode = "404", Message = $"Department with id: {id.Value} not found" });
        var departmentDto = _mapper.Map<CreateDepartmentDto>(department);
        return View(departmentDto);
        
        // return Details(id, viewName: "Edit");
    }

    [HttpPost]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromRoute]int id, CreateDepartmentDto model)
    {
        // return Content("df");
        if (ModelState.IsValid)
        {
            // if (id != model.Id) return BadRequest();
            var department = _mapper.Map<Department>(model);
            department.Id = id;
            _unitOfWork.DepartmentRepository.Update(department);
            int count = await _unitOfWork.SaveChangesAsync();
            if (count > 0)
            {
                TempData["Message"] = "Department updated successfully";
                return RedirectToAction("Index");
            }
        }
        
        var departmentDto = _mapper.Map<CreateDepartmentDto>(model);
        await _unitOfWork.SaveChangesAsync();
        return View(departmentDto);
    }
    
    public async Task<IActionResult> Delete(int? id)
    {
        // if (id is null) return BadRequest();
        //
        // var department = UnitOfWork.DepartmentRepository.Get(id.Value);
        // if (department is null) return NotFound( new { StatusCode = "404", Message = $"Department with id: {id.Value} not found" });
        //
        // return View(department);
        
        return await Details(id, viewName: "Delete");
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var department = await _unitOfWork.DepartmentRepository.GetAsync(id);
        _unitOfWork.DepartmentRepository.Delete(department);
        TempData["Message"] = "Department deleted successfully";
        await _unitOfWork.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}