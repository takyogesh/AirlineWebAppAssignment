﻿using AirlineWebApp.customMiddleware;
using AirlineWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirlineWebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly AirLineDbContext _context;
        public AdminController(AirLineDbContext context)
        {
            _context = context;
        }
        // GET: Users
        [CustomAuthorize("Admin")]
        public async Task<IActionResult> Index()
        {
            ViewData["uName"] = HttpContext.Session.GetString("Name");
              return _context.User != null ? 
                          View(await _context.User.Where(u=>u.Status=="Pending").ToListAsync()) :
                          Problem("Entity set User  is null.");
        }
        [CustomAuthorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(string email,string status)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                try
                {
                    var finduser = await _context.User!.FirstOrDefaultAsync(u => u.Email==email);
                    finduser!.Status = status;
                    _context.User!.Update(finduser);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(email))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
        private bool UserExists(string id)
        {
          return (_context.User?.Any(e => e.Email == id)).GetValueOrDefault();
        }
    }
}