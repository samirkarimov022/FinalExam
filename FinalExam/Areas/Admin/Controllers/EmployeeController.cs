
using FinalExam.Areas.Admin.ViewModels;
using FinalExam.DAL;
using FinalExam.Models;
using FinalExam.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalExam.Areas.Admin.Controllers
{
    [Area("Admin")] 
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Employee> employees=await _context.Employees.ToListAsync();
            return View(employees);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeVm vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }
            bool result = await _context.Employees.AnyAsync(e => e.Name.ToLower().Trim() == vm.Name.ToLower().Trim());
            if(result)
            {
                ModelState.AddModelError("Name", "Name already is exists");
                return View(vm);
            }
            if (!vm.Photo.ValidateType())
            {
                ModelState.AddModelError("Photo", "Image type is not valid");
                return View(vm);
            }
            if (!vm.Photo.ValidateSize(10))
            {
                ModelState.AddModelError("Photo", "Image size is not valid");
                return View(vm);
            }
            Employee employee = new Employee
            {
                Name = vm.Name,
                Description = vm.Description,
                Image = await vm.Photo.CreateFileAsync(_env.WebRootPath, "assets","images")
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Employee? employee=await _context.Employees.FirstOrDefaultAsync(e=> e.Id == id);
            if (employee == null) return NotFound();

            UpdateEmployeeVm update = new UpdateEmployeeVm
            {
                Name = employee.Name,            
                Description = employee.Description,
                Image = employee.Image
            };
            return View(update);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,UpdateEmployeeVm vm)
        {
            if(!ModelState.IsValid) return View(vm);
            Employee? existed = await _context.Employees.FirstOrDefaultAsync(e=>e.Id == id);
            if (existed == null) return NotFound();

            bool result = await _context.Employees.AnyAsync(e => e.Name.ToLower().Trim() == vm.Name.ToLower().Trim() && e.Id != id);
            if (result)
            {
                ModelState.AddModelError("Name", "Name already is exists");
                return View(vm);
            }
            if(vm.Photo is not null)
            {
                if (!vm.Photo.ValidateType())
                {
                    ModelState.AddModelError("Photo", "Image type is not valid");
                    return View(vm);
                }
                if (!vm.Photo.ValidateSize(10))
                {
                    ModelState.AddModelError("Photo", "Image size is not valid");
                    return View(vm);
                }
                string newImage = await vm.Photo.CreateFileAsync(_env.WebRootPath, "assets","images");
                existed.Image.DeleteFile(_env.WebRootPath, "assets", "images");
                existed.Image = newImage;
            }
            existed.Name = vm.Name;       
            existed.Description = vm.Description;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Employee? employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null) return NotFound();

            employee.Image.DeleteFile(_env.WebRootPath, "assets","images");
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
