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
    public class PupilsController : Controller
    {
        private readonly SchoolBDContext _context;

        public PupilsController(SchoolBDContext context)
        {
            _context = context;
        }

        // GET: Pupils
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Classes", "Index");
            ViewBag.ClassId = id;
            ViewBag.Name = name;
            var pupilsByClass = _context.Pupils.Where(p => p.ClassId == id).Include(p => p.Class);
            return View(await pupilsByClass.ToListAsync());
        }

        // GET: Pupils/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pupil = await _context.Pupils
                .Include(p => p.Class)
                .FirstOrDefaultAsync(m => m.PupilId == id);
            if (pupil == null)
            {
                return NotFound();
            }

            return View(pupil);
        }

        // GET: Pupils/Create
        public IActionResult Create(int classId)
        {
            ViewBag.ClassId = classId;
            ViewBag.Name = _context.Classes.Where(p => p.ClassId == classId).FirstOrDefault().Name;
            //ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "Name");
            return View();
        }

        // POST: Pupils/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int classId, [Bind("PupilId,PupilFullName,ClassId,MobileNumber")] Pupil pupil)
        {
            pupil.ClassId = classId;
            if (ModelState.IsValid)
            {
                _context.Add(pupil);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Pupils", new { id = classId, name = _context.Classes.Where(p=>p.ClassId == classId).FirstOrDefault().Name});
            }
            //ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "Name", pupil.ClassId);
            //return View(pupil);
            return RedirectToAction("Index", "Pupils", new { id = classId, name = _context.Classes.Where(p => p.ClassId == classId).FirstOrDefault().Name });
        }

        // GET: Pupils/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pupil = await _context.Pupils.FindAsync(id);
            if (pupil == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "Name", pupil.ClassId);
            return View(pupil);
        }

        // POST: Pupils/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PupilId,PupilFullName,ClassId,MobileNumber")] Pupil pupil)
        {
            if (id != pupil.PupilId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pupil);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PupilExists(pupil.PupilId))
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
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "Name", pupil.ClassId);
            return View(pupil);
        }

        // GET: Pupils/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pupil = await _context.Pupils
                .Include(p => p.Class)
                .FirstOrDefaultAsync(m => m.PupilId == id);
            if (pupil == null)
            {
                return NotFound();
            }

            return View(pupil);
        }

        // POST: Pupils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pupil = await _context.Pupils.FindAsync(id);
            _context.Pupils.Remove(pupil);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PupilExists(int id)
        {
            return _context.Pupils.Any(e => e.PupilId == id);
        }
    }
}
