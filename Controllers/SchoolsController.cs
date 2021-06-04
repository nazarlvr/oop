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
    public class SchoolsController : Controller
    {
        private readonly SchoolBDContext _context;

        public SchoolsController(SchoolBDContext context)
        {
            _context = context;
        }

        // GET: Schools
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("SchoolTypes", "Index");
            ViewBag.SchoolTypeId = id;
            ViewBag.SchoolTypeName = name;
            var schoolsByType = _context.Schools.Where(s => s.SchoolTypeId == id).Include(s => s.SchoolType);
            
            return View(await schoolsByType.ToListAsync());
        }

        // GET: Schools/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var school = await _context.Schools
                .Include(s => s.SchoolType)
                .FirstOrDefaultAsync(m => m.SchoolId == id);
            if (school == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Classes", new { id = school.SchoolId, name = school.SchoolNumber });
        }

        // GET: Schools/Create
        public IActionResult Create(int schoolTypeId)
        {
            //ViewData["SchoolTypeId"] = new SelectList(_context.SchoolTypes, "SchoolTypeId", "SchoolTypeName");
            ViewBag.SchoolTypeId = schoolTypeId;
            ViewBag.SchoolTypeName = _context.SchoolTypes.Where(s => s.SchoolTypeId == schoolTypeId).FirstOrDefault().SchoolTypeName;
            return View();
        }

        // POST: Schools/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int schoolTypeId, [Bind("SchoolId,SchoolNumber,SchoolTypeId,Info")] School school)
        {
            school.SchoolTypeId = schoolTypeId;
            if (ModelState.IsValid)
            {
                _context.Add(school);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Schools", new { id = schoolTypeId, name = _context.SchoolTypes.Where(s => s.SchoolTypeId == schoolTypeId).FirstOrDefault().SchoolTypeName});
            }
            return RedirectToAction("Index", "Schools", new { id = schoolTypeId, name = _context.SchoolTypes.Where(s => s.SchoolTypeId == schoolTypeId).FirstOrDefault().SchoolTypeName });
            //ViewData["SchoolTypeId"] = new SelectList(_context.SchoolTypes, "SchoolTypeId", "SchoolTypeName", school.SchoolTypeId);
            // return View(school);
        }

        // GET: Schools/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var school = await _context.Schools.FindAsync(id);
            if (school == null)
            {
                return NotFound();
            }
            ViewData["SchoolTypeId"] = new SelectList(_context.SchoolTypes, "SchoolTypeId", "SchoolTypeName", school.SchoolTypeId);
            return View(school);
        }

        // POST: Schools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SchoolId,SchoolNumber,SchoolTypeId,Info")] School school)
        {
            if (id != school.SchoolId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(school);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolExists(school.SchoolId))
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
            ViewData["SchoolTypeId"] = new SelectList(_context.SchoolTypes, "SchoolTypeId", "SchoolTypeName", school.SchoolTypeId);
            return View(school);
        }

        // GET: Schools/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var school = await _context.Schools
                .Include(s => s.SchoolType)
                .FirstOrDefaultAsync(m => m.SchoolId == id);
            if (school == null)
            {
                return NotFound();
            }

            return View(school);
        }

        // POST: Schools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var school = await _context.Schools.FindAsync(id);
            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolExists(int id)
        {
            return _context.Schools.Any(e => e.SchoolId == id);
        }
    }
}
