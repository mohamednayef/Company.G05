using Company.G05.DAL.Models;
using Company.G05.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.G05.PL.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;

    public AccountController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }
    
    #region Signup

    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpDto model)
    {
        if (ModelState.IsValid) // server side validation
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user is null)
            {
                user = await _userManager.FindByEmailAsync(model.Email);
                if (user is null)
                {
                    // therefor register
                    user = new AppUser
                    {
                        UserName = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        IsAgree = model.IsAgree
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    
                }
            }
            ModelState.AddModelError("", "Invalid SignUp !!");
        }
        return View();
    }

    #endregion

    #region Login

    

    #endregion

    #region Signout

    

    #endregion
}