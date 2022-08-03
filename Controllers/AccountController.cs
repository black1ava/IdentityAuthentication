using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using IdentityAuthentication.Models;
using System.Security.Claims;

namespace IdentityAuthentication.Controllers
{
  public class AccountController : Controller
  {
    private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;

    public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
      this.signInManager = signInManager;
      this.userManager = userManager;
    }
    [HttpGet]
    public IActionResult Login()
    {

      if (User.Identity != null && User.Identity.IsAuthenticated)
      {
        return Redirect("/");
      }

      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(SigninModel req)
    {
      IdentityUser user = await userManager.FindByEmailAsync(req.Email);

      if (user == null)
      {
        ModelState.AddModelError(nameof(req.Email), "Invalid Email");
        return View(ModelState);
      }

      bool checkPassword = await userManager.CheckPasswordAsync(user, req.Password);

      if (!checkPassword)
      {
        ModelState.AddModelError(nameof(req.Password), "Invalid Password");
        return View(ModelState);
      }

      Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, req.Password, true, true);

      return Redirect("/");
    }

    [HttpGet]
    public IActionResult Register()
    {
      if (User.Identity != null && User.Identity.IsAuthenticated)
      {
        return Redirect("/");
      }
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel req)
    {

      if (!ModelState.IsValid)
      {
        return View(ModelState);
      }

      IdentityUser user = new IdentityUser
      {
        UserName = req.Username,
        Email = req.Email
      };

      IdentityResult result = await userManager.CreateAsync(user, req.Password);
      await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, req.Email));

      return Redirect("/");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
      await signInManager.SignOutAsync();
      return Redirect("/");
    }
  }
}