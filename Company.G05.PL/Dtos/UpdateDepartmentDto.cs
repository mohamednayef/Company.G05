using System.ComponentModel.DataAnnotations;

namespace Company.G05.PL.Models;

public class UpdateDepartmentDto
{
    [Required(ErrorMessage = "Code is required")]
    public string Code { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "CreatedAt is required")]
    public DateTime CreatedAt { get; set; }
}