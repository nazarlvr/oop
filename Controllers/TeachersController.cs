using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DbSchool;

namespace DbSchool.Controllers
{
    public class TeachersController : Controller
    {
        private readonly SchoolBDContext _context;

        public TeachersController(SchoolBDContext context)
        {
            _context = context;
        }

        // GET: Teachers
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Categories", "Index");
            ViewBag.CategoryId = id;
            ViewBag.CategoryName = name;
            var teachersByCategory = _context.Teachers.Where(t => t.CategoryId == id).Include(t => t.Class).Include(q => q.Category);
            return View(await teachersByCategory.ToListAsync());
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .Include(t => t.Category)
                .Include(t => t.Class)
                .FirstOrDefaultAsync(m => m.TeacherId == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teachers/Create
        public IActionResult Create(int categoryId)
        {
            ViewBag.CategoryId = categoryId;
            ViewBag.CategoryName = _context.Categories.Where(t => t.CategoryId == categoryId).FirstOrDefault().CategoryName;
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "Name");
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int categoryId, [Bind("TeacherId,TeacherFullName,CategoryId,ClassId,MobileNumber,Salary")] Teacher teacher)
        {
            teacher.CategoryId = categoryId;
            if (ModelState.IsValid)
            {
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Teachers", new { id = categoryId, name = _context.Categories.Where(c => c.CategoryId == categoryId).FirstOrDefault().CategoryName });
            }
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", teacher.CategoryId);
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "Name", teacher.ClassId);
            //return View(teacher);
            return RedirectToAction("Index", "Teachers", new { id = categoryId, name = _context.Categories.Where(c => c.CategoryId == categoryId).FirstOrDefault().CategoryName });
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", teacher.CategoryId);
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "Name", teacher.ClassId);
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeacherId,TeacherFullName,CategoryId,ClassId,MobileNumber,Salary")] Teacher teacher)
        {
            if (id != teacher.TeacherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.TeacherId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", teacher.CategoryId);
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "Name", teacher.ClassId);
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .Include(t => t.Category)
                .Include(t => t.Class)
                .FirstOrDefaultAsync(m => m.TeacherId == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.TeacherId == id);
        }
    }
}
