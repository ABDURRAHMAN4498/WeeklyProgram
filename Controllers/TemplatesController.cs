using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WeeklyProgram;
using WeeklyProgram.Data;
using WeeklyProgram.ViewModel;

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
        public async Task<IActionResult> Details(int? id)
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
            List<CategoryViewModel> categoreis = new List<CategoryViewModel>();
            categoreis = _context.Categories
            .Where(c => c.ParentCategoryId != null)
            .Select(c => new CategoryViewModel() 
            {Id= c.Id,Name= c.Name }).ToList();
            ViewData["CategoryId"] = new SelectList(categoreis, "Id", "Name");
            return View();
        }

        // POST: Templates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Template template)
        {
            template.ImageUrl = $"{template.Id}";
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Templates.FindAsync(id);
            template!.Category = _context.Categories.Find(template.CategoryId);
            template.ObjectJson = JsonConvert.DeserializeObject<string[]>(template.Objectstext!);
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(c=>c.ParentCategoryId!=null), "Id", "Name", template.CategoryId);
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
        public async Task<IActionResult> Edit(int id, [FromForm] Template template)
        {
            
            if (id != template.Id)
            {
                return NotFound();
            }
            template.ImageUrl = $"{template.Id}";
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
        public async Task<IActionResult> Delete(int? id)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var template = await _context.Templates.FindAsync(id);
            if (template != null)
            {
                _context.Templates.Remove(template);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TemplateExists(int id)
        {
            return _context.Templates.Any(e => e.Id == id);
        }
    }
}
