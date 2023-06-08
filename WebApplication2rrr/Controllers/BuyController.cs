using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebApplication2rrr.Models;

namespace WebApplication2rrr.Controllers
{
    public class BuyController : Controller
    {
        MobileContext db;
        public BuyController(MobileContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Buy(int id)
        {
            Phone phone = db.Phones.Find(id);
            return View("~/Views/Buy/Buy.cshtml", phone);
        }

        [HttpPost]
        public IActionResult Buy(int id, string name, string adress, string phone)
        {
            Phone phoneObj = db.Phones.Find(id);

            var userId = User.FindFirstValue("UserId");

            db.Orders.Add(new Order
            {
                UserId = int.Parse(userId),
                Name = name,
                Address = adress,
                ContactTelephone = phone,
                PhoneId = id
            });

            db.SaveChanges();

            ViewBag.SuccessMessage = "Thank you for your order!";

            return View("~/Views/Buy/Buy.cshtml", phoneObj);
        }
    }
}
