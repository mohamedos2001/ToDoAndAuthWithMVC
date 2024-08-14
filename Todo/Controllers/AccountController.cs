using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Todo.Context;
using Todo.Models;
using Todo.ViewModels;

namespace Todo.Controllers
{
    public class AccountController : Controller
    {
        private readonly TodoContext context;


        // Request Object TodoContext
        public AccountController(TodoContext todoContext)
        {
            this.context = todoContext;
        }

        public IActionResult Register()
        {
            return View();
        }
        public IActionResult SubmitRegister(User newUser)
        {

            if (ModelState.IsValid)
            {
                context.Users.Add(newUser);
                context.SaveChanges();
                return RedirectToAction("Index", "Todo");
            }
            return View("Register", newUser);
        }


        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> SubmitLogin(LoginCred login)
        {
            if (ModelState.IsValid)
            {
                var user = context.Users.FirstOrDefault(u => (u.Email == login.Email && u.Password == login.Password));
                if (user == null)
                {
                    ModelState.AddModelError("", "Email Or Password Is Wrong");
                    return View("Login", login);
                }
                else
                {
                    // 1 
                    // Magic string
                    var claimsIden = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    claimsIden.AddClaim(new Claim(ClaimTypes.Sid, $"{user.Id.ToString()}"));
                    claimsIden.AddClaim(new Claim(ClaimTypes.Role, user.Role));

                    //2
                    var claimPrin = new ClaimsPrincipal(claimsIden);

                    //3 
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrin, new AuthenticationProperties()
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddHours(8)
                    });
                    return RedirectToAction("Index", "Todo");
                }
            }
            return View("Login", login);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Todo");
        }
    }
}
