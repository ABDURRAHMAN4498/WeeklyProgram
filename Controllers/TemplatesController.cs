using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WeeklyProgram.Data;
using WeeklyProgram.Models;

namespace WeeklyProgram.Controllers
{
    public class TemplatesController : Controller
    {
        private readonly ApplicationContext _context;

        public TemplatesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Templates
        public async Task<IActionResult> Index()
        {
            var Templates = await _context.Templates.Include(c => c.Category).ToListAsync();
            return View(Templates);
        }

        // GET: Templates/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Templates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (template == null)
            {
                return NotFound();
            }

            return View(template);
        }

        // GET: Templates/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(c => c.ParentCategoryId != null), "Id", "Name");
            return View();
        }

        // POST: Templates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Template template, IFormFile image)
        {
            template.Id = Guid.NewGuid();
            if (image.Length == 0 || image == null)
            {
                throw new Exception("لا يمكن انشاء قالب بدون صورة");
            }
            string uploadsFolder = Path.Combine("wwwroot", "uploads", "templates");
            var uniqueFileName = template.Id.ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            template.ImageUrl = uniqueFileName;
            if (ModelState.IsValid)
            {

                template.ObjectJson = new string[template.ArrayColmun * template.ArrayRow];
                for (int i = 0; i < template.ObjectJson.Length; i++)
                {
                    template.ObjectJson[i] = "";
                }
                template.Objectstext = JsonConvert.SerializeObject(template.ObjectJson);

                _context.Add(template);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(template);
        }

        // GET: Templates/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Templates.FindAsync(id);
            template!.Category = _context.Categories.Find(template.CategoryId);
            template.ObjectJson = JsonConvert.DeserializeObject<string[]>(template.Objectstext!);
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(c => c.ParentCategoryId != null), "Id", "Name", template.CategoryId);
            if (template == null)
            {
                return NotFound();
            }
            return View(template);
        }

        // POST: Templates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [FromForm] Template template, IFormFile? image)
        {

            if (id != template.Id)
            {
                return NotFound();
            }
            string fileName = template.ImageUrl!;
            if (image != null)
            {
                string uploads = Path.Combine("wwwroot", "uploads", "templates");
                fileName = template.ImageUrl!;
                string fullPath = Path.Combine(uploads, fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                fileName = template.Id.ToString() + Path.GetExtension(image.FileName);
                var filePath = Path.Combine(uploads, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
            }

            string[] array = new string[template.ArrayColmun * template.ArrayRow];
            for (int i = 0; i < template.ObjectJson!.Length; i++)
            {
                array[i] = "";
            }
            template.ObjectJson = array;
            template.ImageUrl = fileName;
            if (ModelState.IsValid)
            {
                template.Objectstext = JsonConvert.SerializeObject(template.ObjectJson);
                _context.Update(template);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(template);
        }

        // GET: Templates/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var template = await _context.Templates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (template == null)
            {
                return NotFound();
            }
            return View(template);
        }

        // POST: Templates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var template = await _context.Templates.FindAsync(id);
            if (template != null)
            {
                _context.Templates.Remove(template);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TemplateExists(Guid id)
        {
            return _context.Templates.Any(e => e.Id == id);
        }
    }
}
