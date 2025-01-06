using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Data.Static;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers;

public class AccountController : Controller
{
    readonly UserManager<ApplicationUser> _userManager;
    readonly SignInManager<ApplicationUser> _signInManager;
    readonly AppDbContext _context;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    public IActionResult Login()
    {
        return View(new LoginVm());
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVm login)
    {
        if (!ModelState.IsValid) return View(login);

        var user = await _userManager.FindByEmailAsync(login.EmailAdress); //emailin database'de var olup olmadğına baktık

        if (user is not null)
        {
            var passwordCheck = await _userManager.CheckPasswordAsync(user, login.Password);
            if (passwordCheck)
            {
                var result = await _signInManager.PasswordSignInAsync(user, login.Password,false,false);
                if (result.Succeeded) 
                {
                    return RedirectToAction("Index", "Movies"); //login olursa başarılı şekild bu sayfaya gönderir.
                }
                
            }
            TempData["Error"] = "Hatalı şifre."; //eğer başarılı şekilde giriş yapamazsak , bir kere okunduğu zaman kendi kendini yok eder.
            return View(login);
        }

        TempData["Error"] = "Hatalı giriş.";
        return View(login);

    }

    public IActionResult Register() => View(new RegisterVm());

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVm register)
    {
        if (!ModelState.IsValid) return View(register);

        var user = await _userManager.FindByEmailAsync(register.EmailAddress);

        if (user is not null)
        {
            TempData["Error"] = "Bu email adresi kullanılmaktadır.";
            return View(register);
        }

        var newUser = new ApplicationUser()
        {
            Email = register.EmailAddress,
            FullName = register.FullName,
            UserName = register.FullName,
            EmailConfirmed = true
        };

        var response = await _userManager.CreateAsync(newUser, register.Password);

        if (response.Succeeded)
        {

            await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            return View("RegisterCompleted");
        }

        return View(register);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Movies");
    }
}




