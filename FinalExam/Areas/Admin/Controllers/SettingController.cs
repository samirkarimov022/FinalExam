using FinalExam.Areas.Admin.ViewModels;
using FinalExam.DAL;
using FinalExam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FinalExam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Setting> settings = await _context.Settings.ToListAsync();
            return View(settings);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSettingVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            bool result = await _context.Settings.AnyAsync(s => s.Key.ToLower().Trim() == vm.Key.ToLower().Trim());
            if (result)
            {
                ModelState.AddModelError("Key", "Key already is exists");
                return View(vm);
            }
            Setting setting = new Setting
            {
                Key = vm.Key,
                Value = vm.Value
            };
            await _context.Settings.AddAsync(setting);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Setting? setting = await _context.Settings.FirstOrDefaultAsync(s => s.Id == id);
            if (setting == null) return NotFound();

            UpdateSettingVm update = new UpdateSettingVm
            {
                Key=setting.Key,
                Value = setting.Value
            };
            return View(update);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateSettingVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            Setting? existed = await _context.Settings.FirstOrDefaultAsync(s => s.Id == id);
            if (existed == null) return NotFound();

            existed.Value = vm.Value;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Setting? setting = await _context.Settings.FirstOrDefaultAsync(s => s.Id == id);
            if (setting == null) return NotFound();

            _context.Settings.Remove(setting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
