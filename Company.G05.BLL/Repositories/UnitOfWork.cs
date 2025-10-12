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
    
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    // public void Dispose()
    // {
    //     _context.Dispose();
    // }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}