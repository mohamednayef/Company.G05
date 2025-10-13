using System.ComponentModel.DataAnnotations;

namespace Company.G05.PL.Models;

public class SignUpDto
{
    [Required(ErrorMessage = "username is required")]
    public string UserName { get; set; }
    
    [Required(ErrorMessage = "FirstName is required")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "LastName is required")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }
    
    [Required(ErrorMessage = "Required")]
    public bool IsAgree { get; set; }
}