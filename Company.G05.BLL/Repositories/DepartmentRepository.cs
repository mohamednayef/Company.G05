using Company.G05.BLL.Interfaces;
using Company.G05.DAL.Data.Contexts;
using Company.G05.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.G05.BLL.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly CompanyDbContext _context;

    public DepartmentRepository(CompanyDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Department> GetAll()
    {
        return _context.Departments.ToList();
    }

    public Department? Get(int id)
    {
        return _context.Departments.Find(id);
    }

    public int Add(Department model)
    {
        _context.Add(model);
        return _context.SaveChanges();
    }

    public int Update(Department model)
    {
        _context.Update(model);
        return _context.SaveChanges();
    }

    public int Delete(Department model)
    {
        _context.Remove(model);
        return _context.SaveChanges();
    }
}