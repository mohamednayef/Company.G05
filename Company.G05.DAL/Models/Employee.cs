using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.G05.DAL.Models;

public class Employee : BaseEntity
{
    [Required(ErrorMessage = "Name is required !!")]
    public string Name { get; set; }
    
    [Range(12, 60, ErrorMessage = "Range between 12 and 60")]
    public int? Age { get; set; }
    
    [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid !!")]
    public string Email { get; set; }
    
    // [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}$", ErrorMessage = "Address must be like (123-Street-City-Country")]
    public string Address { get; set; }
    
    [Phone]
    public string Phone { get; set; }
    
    [DataType(DataType.Currency)]
    public decimal Salary { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    
    [DisplayName("Hirign Date")]
    public DateTime HirignDate { get; set; }
    
    [DisplayName("Date Of Creation")]
    public DateTime CreatedAt { get; set; }
    
    public int? DepartmentId { get; set; }
    public Department? Department { get; set; }
}