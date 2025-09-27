using Company.G05.DAL.Models;

namespace Company.G05.BLL.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    IEnumerable<T> GetAll();
    T? Get(int id);
    int Add(T model);
    int Update(T model);
    int Delete(T model);
}