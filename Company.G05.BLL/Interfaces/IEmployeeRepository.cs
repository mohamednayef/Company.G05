using Company.G05.DAL.Models;

namespace Company.G05.BLL.Interfaces;

public interface IEmployeeRepository : IGenericRepository<Employee>
{
    // IEnumerable<Employee> GetAll();
    // Employee? Get(int id);
    // int Add(Employee model);
    // int Update(Employee model);
    // int Delete(Employee model);
    Employee? GetByName(string name);
}