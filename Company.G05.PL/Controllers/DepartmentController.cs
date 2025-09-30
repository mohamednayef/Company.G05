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
    
    public IActionResult Index()
    {
        var departments = _unitOfWork.DepartmentRepository.GetAll();
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
            var department = _mapper.Map<Department>(model);
             _unitOfWork.DepartmentRepository.Add(department);
            int count = _unitOfWork.SaveChanges();
            if (count > 0)
            {
                TempData["Message"] = "Department created successfully";
                return RedirectToAction("Index");
            }
        }
        _unitOfWork.SaveChanges();
        return View(model);
    }

    public IActionResult Details(int? id, string viewName = "Details")
    {
        if (id is null) return BadRequest();
        
        var department = _unitOfWork.DepartmentRepository.Get(id.Value);
        if (department is null) return NotFound( new { StatusCode = "404", Message = $"Department with id: {id.Value} not found" });
        
        return View(viewName, department);
    }

    public IActionResult Edit(int? id)
    {
        if (id is null) return BadRequest();
        
        var department = _unitOfWork.DepartmentRepository.Get(id.Value);
        if (department is null) return NotFound( new { StatusCode = "404", Message = $"Department with id: {id.Value} not found" });
        var departmentDto = _mapper.Map<CreateDepartmentDto>(department);
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
            var department = _mapper.Map<Department>(model);
            _unitOfWork.DepartmentRepository.Update(department);
            int count = _unitOfWork.SaveChanges();
            if (count > 0)
            {
                TempData["Message"] = "Department updated successfully";
                return RedirectToAction("Index");
            }
        }
        
        var departmentDto = _mapper.Map<CreateDepartmentDto>(model);
        _unitOfWork.SaveChanges();
        return View(departmentDto);
    }
    
    public IActionResult Delete(int? id)
    {
        // if (id is null) return BadRequest();
        //
        // var department = UnitOfWork.DepartmentRepository.Get(id.Value);
        // if (department is null) return NotFound( new { StatusCode = "404", Message = $"Department with id: {id.Value} not found" });
        //
        // return View(department);
        
        return Details(id, viewName: "Delete");
    }
    
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var department = _unitOfWork.DepartmentRepository.Get(id);
        _unitOfWork.DepartmentRepository.Delete(department);
        TempData["Message"] = "Department deleted successfully";
        _unitOfWork.SaveChanges();
        return RedirectToAction("Index");
    }
}