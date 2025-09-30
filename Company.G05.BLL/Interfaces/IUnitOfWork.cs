namespace Company.G05.BLL.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IDepartmentRepository DepartmentRepository { get; }
    IEmployeeRepository EmployeeRepository { get; }
    int SaveChanges();
}