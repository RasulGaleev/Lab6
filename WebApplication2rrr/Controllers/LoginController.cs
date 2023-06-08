using Microsoft.AspNetCore.Mvc;
using WebApplication2rrr.Models;
using System.Linq;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace WebApplication2rrr.Controllers
{
    public class LoginController : Controller
    {
        private readonly MobileContext _dbContext;

        public LoginController(MobileContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Authentication/Login.cshtml");
        }

        [HttpPost]
        public ActionResult LoginUser(User user)
        {
            var entranceUser = _dbContext.Users.FirstOrDefault(u => u.UserName == user.UserName);

            if (entranceUser != null && BCrypt.Net.BCrypt.Verify(user.Password, entranceUser.Password))
            {
                // Создание утверждений (claims)
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, entranceUser.UserName),
                    new Claim("UserId", entranceUser.UserId.ToString()) // Сохранение UserId как дополнительного утверждения
                };

                // Создание объекта ClaimsIdentity
                var identity = new ClaimsIdentity(claims, "UserAuthentication");

                // Создание объекта ClaimsPrincipal
                var principal = new ClaimsPrincipal(identity);

                // Сохранение аутентификационных данных в HttpContext
                HttpContext.SignInAsync(principal).Wait();

                return RedirectToAction("PersonalArea", "Area"); // Перенаправление на другую страницу после успешной аутентификации
            }
            else if (entranceUser == null || entranceUser.UserName == null)
            {
                ViewBag.ErrorMessage = "Неправильный логин";
                return View("~/Views/Authentication/Login.cshtml", user);
            }
            else
            {
                ViewBag.ErrorMessage = "Неправильный пароль";
                return View("~/Views/Authentication/Login.cshtml", user);

            }
        }


    }
}
