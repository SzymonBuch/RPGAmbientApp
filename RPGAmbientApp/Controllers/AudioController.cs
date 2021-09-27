using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RPGAmbientApp.Constans;
using RPGAmbientApp.Data;
using RPGAmbientApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RPGAmbientApp.Controllers
{
    public class AudioController : Controller
    {
        private readonly AppDBContext _appDBContext;
        private IWebHostEnvironment _webHostEnvironment;

        public AudioController(AppDBContext appDBContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDBContext = appDBContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Audio()
        {
            var model = _appDBContext.AudioFiles.ToList();
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            if (User.IsInRole(Roles.Admin))
            {
                return View();
            }
            return NotFound();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create(AudioFile audioFile)
        {
            if (audioFile == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(audioFile);
            }
            var files = HttpContext.Request.Form.Files;
            string webRootPath = _webHostEnvironment.WebRootPath;

            string uploadImg = webRootPath + Paths.imagePath;
            string uploadAudio = webRootPath + Paths.songFilePath;

            string fileName1 = Guid.NewGuid().ToString();
            string fileName2 = Guid.NewGuid().ToString();

            string extension1 = Path.GetExtension(files[0].FileName);
            string extension2 = Path.GetExtension(files[1].FileName);


            using (var fileStream = new FileStream(Path.Combine(uploadImg, fileName1 + extension1), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
            using (var fileStream = new FileStream(Path.Combine(uploadAudio, fileName2 + extension2), FileMode.Create))
            {
                files[1].CopyTo(fileStream);
            }

            audioFile.ImgPath = fileName1 + extension1;

            audioFile.FilePath = fileName2 + extension2;


            _appDBContext.Add(audioFile);
            _appDBContext.SaveChanges();

            return RedirectToAction(nameof(Create));
        }

        [HttpGet]
        public IActionResult SongEditor(int ID)
        {
            var SongEdit = _appDBContext.AudioFiles.Find(ID);
            if (User.IsInRole(Roles.Admin) && SongEdit != null)
            {
                return View(SongEdit);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SongEditor(AudioFile audioFile)
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

            return RedirectToAction(nameof(Audio));
        }

        [HttpGet]
        public IActionResult Delete(int ID)
        {
            var audioFile = _appDBContext.AudioFiles.Find(ID);
            if (User.IsInRole(Roles.Admin) && audioFile != null)
            {
                return View(audioFile);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(AudioFile audioFile)
        {
            if (audioFile == null)
            {
                return NotFound();
            }
            var obj = _appDBContext.AudioFiles.Find(audioFile.ID);

            string imageRemove = _webHostEnvironment.WebRootPath + Paths.imagePath;
            string audioRemove = _webHostEnvironment.WebRootPath + Paths.songFilePath;

            var imageRemovePath = Path.Combine(imageRemove, obj.ImgPath);
            var audioRemovePath = Path.Combine(audioRemove, obj.FilePath);

            if (System.IO.File.Exists(imageRemovePath))
            {
                System.IO.File.Delete(imageRemovePath);
            }

            if (System.IO.File.Exists(audioRemovePath))
            {
                System.IO.File.Delete(audioRemovePath);
            }

            _appDBContext.Remove(obj);
            _appDBContext.SaveChanges();

            return RedirectToAction(nameof(Audio));
        }
    }
}
