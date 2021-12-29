using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoursesProject.Data;
using CoursesProject.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using CoursesProject.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using static System.Net.Mime.MediaTypeNames;

namespace CoursesProject.Controllers
{
    [Authorize]
    public class TrainersController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _environment;

        public TrainersController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Trainers
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trainers.ToListAsync());
        }

        // GET: Trainers/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var trainer = await _context.Trainers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainer == null)
            {
                return NotFound();
            }

            return View(trainer);
        }

        // GET: Trainers/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trainers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TrainerName,attachment")] TrainerViewModel trainer)
        {
            if (ModelState.IsValid)
            {
                string imgFullName = "~/Images/" + UploadFile(trainer);

                Trainer model = new Trainer
                {
                    TrainerName = trainer.TrainerName,
                    attachment = imgFullName

                };

                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }

        // GET: Trainers/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainers.FindAsync(id);
            ViewBag.Id = trainer.Id;
            if (trainer == null)
            {
                return NotFound();
            }
            return View();
        }

        // POST: Trainers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TrainerName,attachment")] Trainer trainer)
        {
            if (id != trainer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainerExists(trainer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }

        // GET: Trainers/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainer == null)
            {
                return NotFound();
            }

            return View(trainer);
        }

        // POST: Trainers/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            _context.Trainers.Remove(trainer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //public IActionResult DownloadFile(int id)
        //{
        //    Image image1 = Image.FromFile(path);
        //}

        private bool TrainerExists(int id)
        {
            return _context.Trainers.Any(e => e.Id == id);
        }




        public string UploadFile(TrainerViewModel model)
        {
            string newFileName = null;
            string fullPath = null;
            if (model.attachment != null)
            {
                string folderPath = Path.Combine(_environment.WebRootPath, "CVs");

                newFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.attachment.FileName);

                fullPath = Path.Combine(folderPath, newFileName);

                using (var fStream = new FileStream(fullPath, FileMode.Create))
                {
                    model.attachment.CopyTo(fStream);
                }
            }
            return newFileName;
        }


        //public FileResult DownloadFile(string fileName)
        //{
        //    //Build the File Path.
        //    string path = Server.MapPath("~/Files/") + fileName;

        //    //Read the File data into Byte Array.
        //    byte[] bytes = System.IO.File.ReadAllBytes(path);

        //    //Send the File to Download.
        //    return File(bytes, "application/octet-stream", fileName);
        //}


    }
}
