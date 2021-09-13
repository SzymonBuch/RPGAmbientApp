using Microsoft.AspNetCore.Mvc;
using RPGAmbientApp.Data;
using RPGAmbientApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGAmbientApp.Controllers
{
    public class AudioController : Controller
    {
        private readonly AppDBContext _appDBContext;

        public AudioController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public IActionResult Audio()
        {
            var model = _appDBContext.AudioFiles.ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult SongEditor(int ID)
        {
            var SongEdit = _appDBContext.AudioFiles.Find(ID);
            if(SongEdit == null)
            {
                return NotFound();
            }
            return View(SongEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AudioFile audioFile)
        {
            if (audioFile == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(audioFile);
            }
            _appDBContext.Update(audioFile);
            _appDBContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
