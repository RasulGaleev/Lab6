using Microsoft.AspNetCore.Mvc;
using WebApplication2rrr.Models;
using System.Linq;
using System.Security.Claims;

namespace WebApplication2rrr.Controllers
{
    public class AreaController : Controller
    {
        private readonly MobileContext _dbContext;

        public AreaController(MobileContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var userId = User.FindFirstValue("UserId");

            if (int.TryParse(userId, out int id))
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.UserId == id);
                return View("~/Views/Home/Profile.cshtml", user);
            }

            return View("~/Views/Home/Profile.cshtml");
        }
    }
}
