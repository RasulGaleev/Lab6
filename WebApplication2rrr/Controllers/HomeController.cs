using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2rrr.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebApplication2rrr.Controllers
{
    public class HomeController : Controller
    {
        MobileContext db;
        public HomeController(MobileContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Index1()
        {
            return View(db.Phones.ToList());
        }
        [Route("/Home/Index/{UserId}")]
        public IActionResult Index(int? UserId)
        {
            return View(db.Phones.ToList());
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}