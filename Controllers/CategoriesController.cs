using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DbSchool;
using Microsoft.AspNetCore.Http;
using System.IO;
using ClosedXML.Excel;

namespace DbSchool.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly SchoolBDContext _context;

        public CategoriesController(SchoolBDContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }
        public IActionResult IndexExceptionPresentationError()
        {
            return View();
        }

        public IActionResult IndexExceptionWrongData()
        {
            return View();
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            //return View(category);
            return RedirectToAction("Index", "Teachers", new { id = category.CategoryId, name = category.CategoryName });
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            //else if (category.Teachers.Count() > 0) { } else
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel)
        {
            if (ModelState.IsValid)
            {
                if (fileExcel != null)
                {
                    using (var stream = new FileStream(fileExcel.FileName, FileMode.Create))
                    {
                        await fileExcel.CopyToAsync(stream);
                        using (XLWorkbook workBook = new XLWorkbook(stream, XLEventTracking.Disabled))
                        {
                            //перегляд усіх листів (в даному випадку категорій)
                            foreach (IXLWorksheet worksheet in workBook.Worksheets)
                            {
                                //worksheet.Name - назва категорії. Пробуємо знайти в БД, якщо відсутня, то створюємо нову
                                Category newcat;
                                var c = (from ct in _context.Categories
                                         where ct.CategoryName.Contains(worksheet.Name)
                                         select ct).ToList();
                                if (c.Count > 0)
                                {
                                    newcat = c[0];
                                }
                                else
                                {
                                    newcat = new Category();
                                    newcat.CategoryName = worksheet.Name;
                                    //додати в контекст
                                    _context.Categories.Add(newcat);
                                }
                                //перегляд усіх рядків                    
                                foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
                                {
                                    try
                                    {
                                        if (row.Cell(1).Value.ToString() == "" || row.Cell(2).Value.ToString() == "" || row.Cell(3).Value.ToString() == "" ||
                                                row.Cell(4).Value.ToString() == "" || row.Cell(5).Value.ToString() == "" || 
                                                row.Cell(6).Value.ToString() == "" || row.Cell(7).Value.ToString() == "")
                                            throw (new ArgumentException("presentationerror"));
                                        Teacher tch = new Teacher();
                                        tch.TeacherFullName = row.Cell(1).Value.ToString();
                                        tch.MobileNumber = row.Cell(2).Value.ToString();
                                        string kar = row.Cell(3).Value.ToString();
                                        tch.Salary = int.Parse(kar);
                                        tch.Category = newcat;
                                        //у разі наявності класу знайти його, у разі відсутності - додати
                                        if (row.Cell(4).Value.ToString().Length > 0)
                                        {
                                            Class class1;

                                            var cl = (from cs in _context.Classes
                                                     where cs.Name.Contains(row.Cell(4).Value.ToString())
                                                     select cs).Include(cs => cs.Specialization).Include(cs => cs.School).Include(cs=>cs.School.SchoolType).ToList();
                                            if (cl.Count > 0)
                                            {
                                                class1 = cl[0];
                                                /*if (class1.Specialization.SpecializationName != row.Cell(5).Value.ToString() ||
                                                    class1.School.SchoolType.SchoolTypeName != row.Cell(7).Value.ToString() ||
                                                    class1.School.SchoolNumber != row.Cell(6).Value.ToString())
                                                        throw (new ArgumentException("wrongdata"));*/
                                            }
                                            else
                                            {
                                                class1 = new Class();
                                                class1.Name = row.Cell(4).Value.ToString();
                                                //у разі наявності спеціалізації знайти її, у разі відсутності - додати

                                                if (row.Cell(5).Value.ToString().Length > 0)
                                                {
                                                    Specialization spez;

                                                    var ss = (from sp in _context.Specializations
                                                              where sp.SpecializationName.Contains(row.Cell(5).Value.ToString())
                                                              select sp).ToList();
                                                    if (ss.Count > 0)
                                                    {
                                                        spez = ss[0];
                                                    }
                                                    else
                                                    {
                                                        spez = new Specialization();
                                                        spez.SpecializationName = row.Cell(5).Value.ToString();
                                                        //додати в контекст
                                                        _context.Add(spez);
                                                    }

                                                    class1.Specialization = spez;
                                                }

                                                if (row.Cell(6).Value.ToString().Length > 0)
                                                {
                                                    School school;

                                                    var sch = (from sch1 in _context.Schools
                                                              where sch1.SchoolNumber.Contains(row.Cell(6).Value.ToString())
                                                              select sch1).Include(sch1=>sch1.SchoolType).ToList();
                                                    if (sch.Count > 0)
                                                    {
                                                        school = sch[0];
                                                        /*if(school.SchoolType.SchoolTypeName != row.Cell(7).Value.ToString())
                                                            throw (new ArgumentException("wrongdata"));*/
                                                    }
                                                    else
                                                    {
                                                        school = new School();
                                                        school.SchoolNumber = row.Cell(6).Value.ToString();
                                                        if(row.Cell(7).Value.ToString().Length > 0)
                                                        {
                                                            SchoolType sct;
                                                            var scht = (from scht1 in _context.SchoolTypes
                                                                        where scht1.SchoolTypeName.Contains(row.Cell(7).Value.ToString())
                                                                        select scht1).ToList();
                                                            if (scht.Count > 0)
                                                            {
                                                                sct = scht[0];
                                                            }
                                                            else
                                                            {
                                                                sct = new SchoolType();
                                                                sct.SchoolTypeName = row.Cell(7).Value.ToString();
                                                                _context.Add(sct);
                                                            }
                                                            school.SchoolType = sct;
                                                        }                                                   
                                                        _context.Add(school);
                                                    }

                                                    class1.School = school;
                                                }

                                                _context.Add(class1);
                                            }

                                            tch.Class = class1;
                                        }

                                        _context.Teachers.Add(tch);
                                    }

                                    catch (Exception e)
                                    {
                                        if (e.Message == "wrongdata")
                                            return RedirectToAction("IndexExceptionWrongData", "Categories");
                                        if (e.Message == "presentationerror")
                                            return RedirectToAction("IndexExceptionPresentationError", "Categories");
                                    }
                                }
                            }
                        }
                    }
                }

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


        public ActionResult Export()
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var categories = _context.Categories.Include("Teachers").ToList();

                foreach (var catg in categories)
                {
                    var worksheet = workbook.Worksheets.Add(catg.CategoryName);

                    worksheet.Cell("A1").Value = "Повне ім'я";
                    worksheet.Cell("B1").Value = "Мобільний телефон";
                    worksheet.Cell("C1").Value = "Заробітна плата";
                    worksheet.Cell("D1").Value = "Клас";
                    worksheet.Cell("E1").Value = "Спеціалізація";
                    worksheet.Cell("F1").Value = "Школа";
                    worksheet.Cell("G1").Value = "Тип Школи";
                    worksheet.Cell("H1").Value = "Категорія";
                    worksheet.Row(1).Style.Font.Bold = true;
                    var teachers = (from cat in _context.Teachers
                                        where cat.Category.Equals(catg)
                                        select cat).Include(cat => cat.Class).Include(cat => cat.Class.School).Include(cat => cat.Class.School.SchoolType).Include(cat => cat.Class.Specialization).ToList();

                    //нумерація рядків/стовпчиків починається з індекса 1 (не 0)
                    for (int i = 0; i < teachers.Count; i++)
                    {
                        worksheet.Cell(i + 2, 1).Value = teachers[i].TeacherFullName;
                        worksheet.Cell(i + 2, 2).Value = teachers[i].MobileNumber;
                        worksheet.Cell(i + 2, 3).Value = teachers[i].Salary;
                        worksheet.Cell(i + 2, 4).Value = teachers[i].Class.Name;
                        worksheet.Cell(i + 2, 5).Value = teachers[i].Class.Specialization.SpecializationName;
                        worksheet.Cell(i + 2, 6).Value = teachers[i].Class.School.SchoolNumber;
                        worksheet.Cell(i + 2, 7).Value = teachers[i].Class.School.SchoolType.SchoolTypeName;
                        worksheet.Cell(i + 2, 8).Value = teachers[i].Category.CategoryName;

                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"Teachers_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }


    }   
}
