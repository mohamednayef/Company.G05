using System.ComponentModel;

namespace Company.G05.PL.Models;

public class CreateEmployeeDto
{
    public string Name { get; set; }
    public int? Age { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public decimal Salary { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime HirignDate { get; set; }
    public DateTime CreatedAt { get; set; }
    
    [DisplayName("Department")]
    public int? DepartmentId { get; set; }
}