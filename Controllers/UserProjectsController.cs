using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WeeklyProgram.Data;
using WeeklyProgram.Models;
using WeeklyProgram.Services;
using WeeklyProgram.ViewModel;

namespace WeeklyProgram.Controllers
{
    public class UserProjectsController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly ImageService _imageService;

        public UserProjectsController(ApplicationContext context, ImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        // GET: UserProjects
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserProjects.ToListAsync());
        }

        // GET: UserProjects/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserProject userProject = await _context.UserProjects.FirstOrDefaultAsync(m => m.Id == id);

            Template template = _context.Templates.Find(userProject!.TemplateId)!;

            ProjectViewModel projectViewModel = new ProjectViewModel()
            {
                TemplateId = (Guid)userProject.TemplateId!,
                Id = userProject.Id,
                ObjectJson = JsonConvert.DeserializeObject<string[]>(userProject.Objectstext!),
                ImageUrl = userProject.ImageUrl,
                Title = template.Title,
                ArrayRow = template.ArrayRow,
                ArrayColmun = template.ArrayColmun,
                Descreption = template.Descreption,
                ProjectName = userProject.ProjectTitle


            };

            if (userProject == null)
            {
                return NotFound();
            }

            return View(projectViewModel);
        }
       



        public IActionResult ProcessImage(Guid? id)
        {
            UserProject? userProject =  _context.UserProjects.Find(id);

            Template template = _context.Templates.Find(userProject!.TemplateId)!;

            ProjectViewModel projectViewModel = new ProjectViewModel()
            {
                TemplateId = (Guid)userProject.TemplateId!,
                Id = userProject.Id,
                ObjectJson = JsonConvert.DeserializeObject<string[]>(userProject.Objectstext!),
                ImageUrl = userProject.ImageUrl,
                Title = template.Title,
                ArrayRow = template.ArrayRow,
                ArrayColmun = template.ArrayColmun,
                Descreption = template.Descreption,
                ProjectName = userProject.ProjectTitle

            };
            string servicePrinter = projectViewModel.TemplateId.ToString().ToUpper();
            if (servicePrinter == "C801EC9A-7286-4FAA-91AF-2833F3DE4935")
            {
                return File(_imageService.ImagePrinter(projectViewModel), "image/png");
            }
            else
            {
                throw new Exception("هذا القالب غير موجود");
            }
            
        }


        public IActionResult TemplateList()
        {
            var templates = _context.Templates.ToList();
            return View(templates);
        }




        // GET: UserProjects/Create
        [HttpGet]
        public IActionResult Create(Guid? id)
        {
            Template template = new Template();
            template = _context.Templates.Find(id)!;


            ProjectViewModel projectViewModel = new ProjectViewModel();

            projectViewModel.ObjectJson = JsonConvert.DeserializeObject<string[]>(template.Objectstext);
            projectViewModel.ArrayColmun = template.ArrayColmun;
            projectViewModel.ArrayRow = template.ArrayRow;
            projectViewModel.ImageUrl = template.ImageUrl;
            projectViewModel.Descreption = template.Descreption;
            projectViewModel.Title = template.Title;



            return View(projectViewModel);
        }

        // POST: UserProjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProjectViewModel projectViewModel)
        {
            UserProject userProject = new UserProject();
            userProject.TemplateId = projectViewModel.Id;
            userProject.CreatedDate = DateTime.Now;
            userProject.ImageUrl = projectViewModel.ImageUrl;
            userProject.ProjectTitle = projectViewModel.ProjectName;
            userProject.UserId = "1";
            userProject.Objectstext = JsonConvert.SerializeObject(projectViewModel.ObjectJson);

            if (projectViewModel is not null)
            {
                userProject.Id = Guid.NewGuid();
                _context.Add(userProject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userProject);
        }






        // GET: UserProjects/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProject = await _context.UserProjects.FindAsync(id);
            if (userProject == null)
            {
                return NotFound();
            }
            return View(userProject);
        }

        // POST: UserProjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,TemplateId,ProjectTitle,Objectstext,ImageUrl,CreatedDate")] UserProject userProject)
        {
            if (id != userProject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProjectExists(userProject.Id))
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
            return View(userProject);
        }

        // GET: UserProjects/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProject = await _context.UserProjects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userProject == null)
            {
                return NotFound();
            }

            return View(userProject);
        }

        // POST: UserProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userProject = await _context.UserProjects.FindAsync(id);
            if (userProject != null)
            {
                _context.UserProjects.Remove(userProject);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserProjectExists(Guid id)
        {
            return _context.UserProjects.Any(e => e.Id == id);
        }
    }
}
