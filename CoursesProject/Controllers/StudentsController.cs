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

namespace CoursesProject.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Students
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            //var data2 = await _context.Clients.FindAsync();
            //var allData = await _context.OrderViewModels.Include(co => co.Order).ToListAsync();
            //data2.ClientOrders = allData;

            //var AllStudents = await _context.Students.FindAsync();
            //var all = await _context.CourseStudents.Include(cs => cs.Course).ToListAsync();
            //AllStudents.CourseStudents = all;

            var students = await _context.Students
                .Include(s => s.CourseStudents)
                .ThenInclude(cs => cs.Course)
                .ToListAsync();

            var Trainers = await _context.Trainers.ToListAsync();
            ViewBag.Trainers = Trainers;

            //var courses = await _context.Courses
            //    .Include(c => c.CourseStudents)
            //    .ThenInclude(cs => cs.Student)
            //    .ToListAsync();

            return View(students);
            //return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create()
        {
            ViewBag.courses_bag = await _context.Courses.ToListAsync();
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentName,Age")] Student student, string[] selectedCourses)
        {
            //return NoContent();
            if (ModelState.IsValid)
            {
                await _context.AddAsync(student);
 
                await _context.SaveChangesAsync();
 

                for (int i = 0; i < selectedCourses.Count(); i++)
                {

                    CourseStudentViewModel cs = new CourseStudentViewModel
                    {
                        CourseId = Convert.ToInt32(selectedCourses[i]),
                        StudentId = student.Id

                    };
                    await _context.CourseStudents.AddAsync(cs);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentName,Age")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            return View(student);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
