using Company.G05.DAL.Models;

namespace Company.G05.BLL.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    IEnumerable<T> GetAll();
    T? Get(int id);
    void Add(T model);
    void Update(T model);
    void Delete(T model);
}