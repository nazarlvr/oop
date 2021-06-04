using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace DbSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly SchoolBDContext _context;

        public ChartsController(SchoolBDContext context)
        {
            _context = context;
        }
        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var categories = _context.Categories.Include(b => b.Teachers).ToList();

            List<object> catTeacher = new List<object>();

            catTeacher.Add(new[] { "Категорія", "Кількість Вчителів" });

            foreach (var c in categories)
            {
                catTeacher.Add(new object[] { c.CategoryName, c.Teachers.Count() });
            }
            return new JsonResult(catTeacher);
        }
        [HttpGet("JsonData1")]
        public JsonResult JsonData1()
        {
            var types = _context.SchoolTypes.Include(b => b.Schools).ToList();

            List<object> catSchool = new List<object>();

            catSchool.Add(new[] { "Тип", "Кількість Шкіл" });

            foreach (var t in types)
            {
                catSchool.Add(new object[] { t.SchoolTypeName, t.Schools.Count() });
            }
            return new JsonResult(catSchool);
        }
    }
}
