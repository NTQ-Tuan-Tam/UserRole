using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RoleUser.Models;
using System.Linq;
using System.Threading.Tasks;
using RoleUser.Controllers;
using Microsoft.AspNetCore.Authentication;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections.Generic;
using System.Security.Claims;
using RoleUser.ViewModel;

namespace RoleUser.Controllers
{
    public class LoginController : Controller
    {
        private readonly EmployeeContext _context;
        public LoginController(EmployeeContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost, ActionName("Login")]
        public async Task<IActionResult> Login(string name, string password)
        {
            {
                var p = _context.UserNames.ToList();
                var userDetail = _context.UserNames.FirstOrDefault(u => u.Name == name && u.Password == password);
                if (userDetail == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    //create claims
                    List<Claim> claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, "CookieAuth"),
                            new Claim(ClaimTypes.Email, name)
                        };

                    // create identity
                    ClaimsIdentity identity = new ClaimsIdentity(claims, "CookieAuth");

                    // create principal
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(principal);

                    return RedirectToAction("Index", "Home");
                }


               
            }
        }
    }
}   
