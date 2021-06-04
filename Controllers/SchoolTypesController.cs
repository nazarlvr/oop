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
    public class SchoolTypesController : Controller
    {
        private readonly SchoolBDContext _context;

        public SchoolTypesController(SchoolBDContext context)
        {
            _context = context;
        }

        // GET: SchoolTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.SchoolTypes.ToListAsync());
        }

        // GET: SchoolTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolType = await _context.SchoolTypes
                .FirstOrDefaultAsync(m => m.SchoolTypeId == id);
            if (schoolType == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Schools", new { id = schoolType.SchoolTypeId, name = schoolType.SchoolTypeName});
            //return View(schoolType);
        }

        // GET: SchoolTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SchoolTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SchoolTypeId,SchoolTypeName")] SchoolType schoolType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schoolType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(schoolType);
        }

        // GET: SchoolTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolType = await _context.SchoolTypes.FindAsync(id);
            if (schoolType == null)
            {
                return NotFound();
            }
            return View(schoolType);
        }

        // POST: SchoolTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SchoolTypeId,SchoolTypeName")] SchoolType schoolType)
        {
            if (id != schoolType.SchoolTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schoolType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolTypeExists(schoolType.SchoolTypeId))
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
            return View(schoolType);
        }

        // GET: SchoolTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolType = await _context.SchoolTypes
                .FirstOrDefaultAsync(m => m.SchoolTypeId == id);
            if (schoolType == null)
            {
                return NotFound();
            }

            return View(schoolType);
        }

        // POST: SchoolTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schoolType = await _context.SchoolTypes.FindAsync(id);
            _context.SchoolTypes.Remove(schoolType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolTypeExists(int id)
        {
            return _context.SchoolTypes.Any(e => e.SchoolTypeId == id);
        }
    }
}
