using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication2rrr.Models;

namespace WebApplication2rrr.Controllers
{
    public class RegisterController : Controller
    {
        private readonly MobileContext _dbContext;

        public RegisterController(MobileContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Views/Authentication/Register.cshtml");
        }
        [HttpPost]
        public ActionResult RegisterUser(User user)
        {
            if (ModelState.IsValid)
            {
                // Хэширование пароля
                string hashedPassword = HashPassword(user.Password);

                // Создание нового пользователя
                var newUser = new User
                {
                    UserName = user.UserName,
                    Password = hashedPassword,
                    Name = user.Name,
                    Role = string.IsNullOrEmpty(user.Role) ? "Not_admin" : user.Role,
                    NumberTelephone = user.NumberTelephone,
                    Sex = user.Sex
                };

                // Сохранение пользователя в базе данных
                try
                {
                    _dbContext.Users.Add(newUser);
                    _dbContext.SaveChanges();

                    // Аутентификация пользователя после успешной регистрации
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, newUser.UserName),
                        new Claim("UserId", newUser.UserId.ToString()) // Сохранение UserId как дополнительного утверждения
                    };

                    var identity = new ClaimsIdentity(claims, "UserAuthentication");
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(principal).Wait();

                    return RedirectToAction("PersonalArea", "Area"); // Перенаправление на личный кабинет после успешной регистрации
                }
                catch (Exception ex)
                {
                    // Обработка ошибки
                    Console.WriteLine(ex.Message);
                }
            }

            return View("Register", user);
        }



        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
