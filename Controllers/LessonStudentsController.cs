using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppMvc.Data;
using WebAppMvc.Models;

namespace WebAppMvc.Controllers
{
    [Authorize]
    public class LessonStudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LessonStudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LessonStudents
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LessonStudent.Include(l => l.Lesson).Include(l => l.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LessonStudents/Details?LessonId=1&StudentId=1
        public async Task<IActionResult> Details(int? LessonId, int? StudentId)
        {
            if (LessonId == null || StudentId == null)
            {
                return NotFound();
            }

            var lessonStudent = await _context.LessonStudent
                .Include(l => l.Lesson)
                .Include(l => l.Student)
                .FirstOrDefaultAsync(m => m.LessonId == LessonId && m.StudentId == StudentId);
            if (lessonStudent == null)
            {
                return NotFound();
            }

            return View(lessonStudent);
        }

        // GET: LessonStudents/Create
        public IActionResult Create()
        {
            ViewData["LessonId"] = new SelectList(_context.Lesson, "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name");
            return View();
        }

        // POST: LessonStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LessonId,StudentId,Info,CreatedAt,UpdatedAt")] LessonStudent lessonStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lessonStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LessonId"] = new SelectList(_context.Lesson, "Id", "Name", lessonStudent.LessonId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", lessonStudent.StudentId);
            return View(lessonStudent);
        }

        // GET: LessonStudents/Edit?LessonId=1&StudentId=1
        public async Task<IActionResult> Edit(int? LessonId, int? StudentId)
        {
            if (LessonId == null || StudentId == null)
            {
                return NotFound();
            }

            //var lessonStudent = await _context.LessonStudent.FindAsync(LessonId, StudentId);
            var lessonStudent = await _context.LessonStudent
                .Include(l => l.Lesson)
                .Include(l => l.Student)
                .FirstOrDefaultAsync(m => m.LessonId == LessonId && m.StudentId == StudentId);
            if (lessonStudent == null)
            {
                return NotFound();
            }
            ViewData["LessonId"] = new SelectList(_context.Lesson, "Id", "Name", lessonStudent.LessonId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", lessonStudent.StudentId);
            return View(lessonStudent);
        }

        // POST: LessonStudents/Edit?LessonId=1&StudentId=1
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int LessonId, int StudentId, [Bind("LessonId,StudentId,Info,CreatedAt,UpdatedAt")] LessonStudent lessonStudent)
        {
            if (LessonId != lessonStudent.LessonId || StudentId != lessonStudent.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lessonStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LessonStudentExists(lessonStudent.LessonId, lessonStudent.StudentId))
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
            ViewData["LessonId"] = new SelectList(_context.Lesson, "Id", "Name", lessonStudent.LessonId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", lessonStudent.StudentId);
            return View(lessonStudent);
        }

        // GET: LessonStudents/Delete?LessonId=1&StudentId=1
        public async Task<IActionResult> Delete(int? LessonId, int? StudentId)
        {
            if (LessonId == null || StudentId == null)
            {
                return NotFound();
            }

            var lessonStudent = await _context.LessonStudent
                .Include(l => l.Lesson)
                .Include(l => l.Student)
                .FirstOrDefaultAsync(m => m.LessonId == LessonId && m.StudentId == StudentId);
            if (lessonStudent == null)
            {
                return NotFound();
            }

            return View(lessonStudent);
        }

        // POST: LessonStudents/Delete?LessonId=1&StudentId=1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int LessonId, int StudentId)
        {
            var lessonStudent = await _context.LessonStudent.FindAsync(LessonId, StudentId);
            if (lessonStudent != null)
            {
                _context.LessonStudent.Remove(lessonStudent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LessonStudentExists(int LessonId, int StudentId)
        {
            return _context.LessonStudent.Any(e => e.LessonId == LessonId && e.StudentId == StudentId);
        }
    }
}
