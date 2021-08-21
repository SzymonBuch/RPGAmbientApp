using Microsoft.AspNetCore.Mvc;
using RPGAmbientApp.Data;
using RPGAmbientApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGAmbientApp.Controllers
{
    public class EmailController : Controller
    {
        private AppDBContext _appDBContext;

        public EmailController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public IActionResult Index()
        {
            var model = _appDBContext.Email.ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Emails emails)
        {
            if (emails == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(emails);
            }
            _appDBContext.Add(emails);
            _appDBContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var emailSet = _appDBContext.Email.Find(id);
            if (emailSet == null)
            {
                return NotFound();
            }
            return View(emailSet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Emails emails)
        {
            if (emails == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(emails);
            }
            _appDBContext.Update(emails);
            _appDBContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var emailSet = _appDBContext.Email.Find(id);
            if (emailSet == null)
            {
                return NotFound();
            }
            return View(emailSet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Emails emails)
        {
            if (emails == null)
            {
                return NotFound();
            }
            var objectToDelete = _appDBContext.Email.Find(emails.Id);
            _appDBContext.Remove(objectToDelete);
            _appDBContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
