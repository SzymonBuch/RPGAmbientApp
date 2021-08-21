using Microsoft.AspNetCore.Mvc;
using RPGAmbientApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGAmbientApp.Controllers
{
    public class TestController : Controller
    {

        public IActionResult Test()
        {
            var showTime = new Show()
            {
                Title = "Timer",
                Time = new TimeSpan(10, 15, 20)
            };

            return View(showTime);
        }

        [HttpGet]
        public IActionResult FormExample()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FormExample(Emails emails)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            return View(emails);
        }
    }
}
