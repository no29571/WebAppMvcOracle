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
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// ViewData設定
        /// </summary>
        private void SetViewData()
        {
            //タイトル
            ViewData["Title"] = "Student";

            //ドロップダウンリスト
            ViewData["DepartmentList"] = new SelectList(_context.Department, "Id", "Name");

            //検索条件の維持
            ViewData["FindName"] = TempData["FindName"];
            ViewData["FindInfo"] = TempData["FindInfo"];
            ViewData["FindDepartment1"] = TempData["FindDepartment1"];
            ViewData["FindDepartment2"] = TempData["FindDepartment2"];
        }

        /// <summary>
        /// 検索条件設定
        /// </summary>
        /// <param name="FindName"></param>
        /// <param name="FindInfo"></param>
        /// <param name="FindDepartment1"></param>
        /// <param name="FindDepartment2"></param>
        /// <returns></returns>
        private IQueryable<Student> GetQuery(string? FindName, string? FindInfo, string? FindDepartment1, string? FindDepartment2)
        {
            //参考：スキャフォールディングのIndexメソッド
            //var applicationDbContext = _context.Student.Include(s => s.Department1).Include(s => s.Department2);
            //return View(await applicationDbContext.ToListAsync());

            IQueryable<Student> query = _context.Student
                .Include(mdl => mdl.Department1)
                .Include(mdl => mdl.Department2)
                .OrderBy(mdl => mdl.Department1Id)
                .ThenBy(mdl => mdl.Id);

            if (FindName != null)
            {
                query = query.Where(mdl => mdl.Name.Contains(FindName));
            }
            if (FindInfo != null)
            {
                query = query.Where(mdl => mdl.Info.Contains(FindInfo));
            }
            if (FindDepartment1 != null)
            {
                query = query.Where(mdl => mdl.Department1Id == FindDepartment1);
            }
            if (FindDepartment2 != null)
            {
                query = query.Where(mdl => mdl.Department2Id == FindDepartment2);
            }
            return query;
        }

        /// <summary>
        /// メニューから遷移
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Init()
        {
            //TempDataをクリア
            TempData.Clear();

            //ViewData設定
            SetViewData();

            //０件で返却
            IQueryable<Student> query = _context.Student
                .OrderBy(mdl => mdl.Department1Id)
                .ThenBy(mdl => mdl.Id)
                .Take(0);
            return View("Index", await query.ToListAsync());
        }

        /// <summary>
        /// 検索
        /// </summary>
        /// <param name="FindName"></param>
        /// <param name="FindInfo"></param>
        /// <param name="FindDepartment1"></param>
        /// <param name="FindDepartment2"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Search(string? FindName, string? FindInfo, string? FindDepartment1, string? FindDepartment2)
        {
            //検索条件の維持
            TempData["FindName"] = FindName;
            TempData["FindInfo"] = FindInfo;
            TempData["FindDepartment1"] = FindDepartment1;
            TempData["FindDepartment2"] = FindDepartment2;

            //ViewData設定
            SetViewData();

            //検索
            IQueryable<Student> query = GetQuery(FindName, FindInfo, FindDepartment1, FindDepartment2);
            return View("Index", await query.ToListAsync());
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            //ViewData設定
            SetViewData();

            //検索
            string? FindName = (string?)TempData["FindName"];
            string? FindInfo = (string?)TempData["FindInfo"];
            string? FindDepartment1 = (string?)TempData["FindDepartment1"];
            string? FindDepartment2 = (string?)TempData["FindDepartment2"];

            IQueryable<Student> query = GetQuery(FindName, FindInfo, FindDepartment1, FindDepartment2);
            return View("Index", await query.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.Department1)
                .Include(s => s.Department2)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["Department1Id"] = new SelectList(_context.Department, "Id", "Name");
            ViewData["Department2Id"] = new SelectList(_context.Department, "Id", "Name");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Info,Department1Id,Department2Id,CreatedAt,UpdatedAt")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Department1Id"] = new SelectList(_context.Department, "Id", "Name", student.Department1Id);
            ViewData["Department2Id"] = new SelectList(_context.Department, "Id", "Name", student.Department2Id);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["Department1Id"] = new SelectList(_context.Department, "Id", "Name", student.Department1Id);
            ViewData["Department2Id"] = new SelectList(_context.Department, "Id", "Name", student.Department2Id);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Info,Department1Id,Department2Id,CreatedAt,UpdatedAt")] Student student)
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
            ViewData["Department1Id"] = new SelectList(_context.Department, "Id", "Name", student.Department1Id);
            ViewData["Department2Id"] = new SelectList(_context.Department, "Id", "Name", student.Department2Id);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.Department1)
                .Include(s => s.Department2)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.Id == id);
        }
    }
}
