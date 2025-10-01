using Company.G05.BLL.Interfaces;
using Company.G05.DAL.Data.Contexts;
using Company.G05.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.G05.BLL.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly CompanyDbContext _context;
    public GenericRepository(CompanyDbContext context)
    {
        _context = context;
    }
    public IEnumerable<T> GetAll()
    {
        if (typeof(T) == typeof(Employee))
        {
            return (IEnumerable<T>) _context.Employees.Include(E => E.Department).ToList();
        }
        return  _context.Set<T>().ToList();
    }

    public T? Get(int id)
    {
        if (typeof(T) == typeof(Employee))
        {
            return _context.Employees.Include(E => E.Department).FirstOrDefault(E => E.Id == id) as T;
        }
        return  _context.Set<T>().Find(id);
    }

    public void Add(T model)
    {
        _context.Set<T>().Add(model);
    }

    public void Update(T model)
    {
        _context.Set<T>().Update(model);
    }

    public void Delete(T model)
    {
        _context.Set<T>().Remove(model);
    }
}