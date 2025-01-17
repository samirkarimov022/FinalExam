﻿using FinalExam.DAL;
using FinalExam.Models;
using FinalExamLumia.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalExam.Conrollers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Employee> employees=await _context.Employees.ToListAsync();
            return View(employees);
        }
    }
}
