using Company.G05.DAL.Models;
using Company.G05.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.G05.PL.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
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

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is not null)
            {
                bool flag = await _userManager.CheckPasswordAsync(user, model.Password);
                if (flag)
                {
                    // login
                    var resut = await _signInManager.PasswordSignInAsync(user,  model.Password, true, false);
                    if (resut.Succeeded)
                    {
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                    ModelState.AddModelError("", "Invalid Sign In!!");
                    
                }
            }
            ModelState.AddModelError("", "Invalid Email Or Password !!");
            
        }
        return View();
    }
    #endregion

    #region Signout

    

    #endregion
}