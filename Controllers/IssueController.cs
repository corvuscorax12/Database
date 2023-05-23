using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Database.DAL;
using Database.Models;
using System.Globalization;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Npgsql;

namespace Database.Controllers
{
    public class IssueController : Controller
    {
        private readonly ArchivalContext _context;

        public IssueController(ArchivalContext context)
        {
            _context = context;
        }

        // GET: IssueModels
        public async Task<IActionResult> Index()
        {
              return _context.Issue != null ? 
                          View(await _context.Issue.ToListAsync()) :
                          Problem("Entity set 'ArchivalContext.Issue'  is null.");
        }

        // GET: IssueModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Issue == null)
            {
                return NotFound();
            }

            var issueModel = await _context.Issue
                .FirstOrDefaultAsync(m => m.id == id);
            if (issueModel == null)
            {
                return NotFound();
            }

            return View(issueModel);
        }

        // GET: IssueModels/Create
        public async Task<IActionResult> Create()
        {
            List<SemesterModel>? semester = await _context.Semester.ToListAsync();

            List<int> semesterId = new List<int>();
            List<string> semesterName = new List<string>();
            int i = 0;
            foreach (var item in semester)
            {

                semesterId.Add(item.id);
                semesterName.Add(item.semester_name);
            }

            ViewBag.Semesterid = semesterId;
            ViewBag.SemesterIdCount = semesterId.Count;
            ViewBag.Semester = semesterName;
            return View();
        }

        // POST: IssueModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,volume,Issue,is_archived,semester")] IssueModel issueModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issueModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(issueModel);
        }

        // GET: IssueModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Issue == null)
            {
                return NotFound();
            }

            var issueModel = await _context.Issue.FindAsync(id);
            if (issueModel == null)
            {
                return NotFound();
            }
            return View(issueModel);
        }

        // POST: IssueModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,publication_date,volume,issue,is_archived,semester")] IssueModel issueModel)
        {
            if (id != issueModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issueModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueModelExists(issueModel.id))
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
            return View(issueModel);
        }

        // GET: IssueModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Issue == null)
            {
                return NotFound();
            }

            var issueModel = await _context.Issue
                .FirstOrDefaultAsync(m => m.id == id);
            if (issueModel == null)
            {
                return NotFound();
            }

            return View(issueModel);
        }

        // POST: IssueModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Issue == null)
            {
                return Problem("Entity set 'ArchivalContext.Issue'  is null.");
            }
            var issueModel = await _context.Issue.FindAsync(id);
            if (issueModel != null)
            {
                _context.Issue.Remove(issueModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueModelExists(int id)
        {
          return (_context.Issue?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
