namespace Company.G05.BLL.Interfaces;

public interface IUnitOfWork : IAsyncDisposable
{
    IDepartmentRepository DepartmentRepository { get; }
    IEmployeeRepository EmployeeRepository { get; }
    Task<int> SaveChangesAsync();
}