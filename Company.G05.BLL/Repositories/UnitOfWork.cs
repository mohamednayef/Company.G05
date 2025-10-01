using Company.G05.BLL.Interfaces;
using Company.G05.DAL.Data.Contexts;

namespace Company.G05.BLL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly CompanyDbContext _context;
    public IDepartmentRepository DepartmentRepository { get; }
    public IEmployeeRepository EmployeeRepository { get; }

    public UnitOfWork(CompanyDbContext context)
    {
        _context = context;
        DepartmentRepository = new DepartmentRepository(_context);
        EmployeeRepository = new EmployeeRepository(_context);
    }
    
    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}