using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Database.DAL;
using Database.Models;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Authorization;

namespace Database.Controllers
{
    public class StaffController : Controller
    {
        SignInManager<UserModel>? SignInManager;
        UserManager<UserModel> UserManager;
        private readonly ArchivalContext _context;

        public StaffController(ArchivalContext context)
        {
            _context = context;
        }

        // GET: StaffModels

        public async Task<IActionResult> Index()
        {
            
                return _context.Staff != null ?
                            View(await _context.Staff.ToListAsync()) :
                          Problem("Entity set 'ArchivalContext.Staff'  is null.");
            
        }

        // GET: StaffModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Staff == null)
            {
                return NotFound();
            }

            var staffModel = await _context.Staff
                .FirstOrDefaultAsync(m => m.id == id);
            if (staffModel == null)
            {
                return NotFound();
            }

            return View(staffModel);
        }

        // GET: StaffModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StaffModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,first_name,last_name,email,phone_number")] StaffModel staffModel)
        {
                if (ModelState.IsValid)
                {
                    _context.Add(staffModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(staffModel);
  
        }

        // GET: StaffModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Staff == null)
            {
                return NotFound();
            }

            var staffModel = await _context.Staff.FindAsync(id);
            if (staffModel == null)
            {
                return NotFound();
            }
            return View(staffModel);
        }

        // POST: StaffModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,first_name,last_name,email,phone_number")] StaffModel staffModel)
        {
            if (id != staffModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staffModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffModelExists(staffModel.id))
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
            return View(staffModel);
        }

        // GET: StaffModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Staff == null)
            {
                return NotFound();
            }

            var staffModel = await _context.Staff
                .FirstOrDefaultAsync(m => m.id == id);
            if (staffModel == null)
            {
                return NotFound();
            }

            return View(staffModel);
        }

        // POST: StaffModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Staff == null)
            {
                return Problem("Entity set 'ArchivalContext.Staff'  is null.");
            }
            var staffModel = await _context.Staff.FindAsync(id);
            if (staffModel != null)
            {
                _context.Staff.Remove(staffModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffModelExists(int id)
        {
          return (_context.Staff?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
