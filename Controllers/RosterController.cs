using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Database.DAL;
using Database.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Npgsql;

namespace Database.Controllers
{
    public class RosterController : Controller
    {
        private readonly ArchivalContext _context;

        public RosterController(ArchivalContext context)
        {
            _context = context;
        }


        // GET: Roster
        public  async Task<ActionResult> Index(int itemId)
        {
            List<SemesterModel>? semester =  await _context.Semester.ToListAsync();
            List<int> id = new List<int>();
            List<string> semesterName = new List<string>();
            List<string> selected = new List<string>();
            foreach (var item in semester)
            {
                int iid = item.id;
                if (iid == itemId)
                {
                    selected.Add("selected");
                }
                else
                {
                    selected.Add("");
                }
                semesterName.Add(item.semester_name);
                id.Add(item.id);

            }

            ViewBag.Semesterid = id;
            ViewBag.SemesterIdCount = id.Count;
            ViewBag.Semester = semesterName;
            ViewBag.Selected = selected;

            NpgsqlConnection conn = new NpgsqlConnection(ArchivalContext.conn);
            List<SemesterView> semesterViews = new List<SemesterView>();
            NpgsqlCommand command = new NpgsqlCommand($"Select * from public.viewroster where id = {itemId}  ", conn);
            conn.Open();
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                semesterViews.Add(new()
                {
                    roster_id = reader.GetInt32(0),
                    first_name = (string)reader[1],
                    last_name = (string)reader[2],
                    role_name = (string)reader[3],
                    semester_name = (string)reader[4]
                }
                   );


            }
            return View(semesterViews);


            /*return _context.SemesterRoster != null ? 
                        View(await _context.SemesterRoster.ToListAsync()) :
                        Problem("Entity set 'ArchivalContext.RosterModel'  is null.");
            */
        }

        // GET: Roster/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SemesterRoster == null)
            {
                return NotFound();
            }

            var rosterModel = await _context.SemesterRoster
                .FirstOrDefaultAsync(m => m.roster_id == id);
            if (rosterModel == null)
            {
                return NotFound();
            }

            return View(rosterModel);
        }

        // GET: Roster/Create
        public async Task<IActionResult> Create()
        {
            List<SemesterModel>? semester = await _context.Semester.ToListAsync();
            List<StaffModel>? staff = await _context.Staff.ToListAsync();
            List<RoleModel>? role = await _context.Role.ToListAsync();

            List<int> id = new List<int>();
            List<string> semesterName = new List<string>();
            int i = 0;
            foreach (var item in semester)
            {

                id.Add(item.id);
                semesterName.Add(item.semester_name);
            }

            ViewBag.Semesterid = id;
            ViewBag.SemesterIdCount = id.Count;
            ViewBag.Semester = semesterName;


            List<int> staffId = new List<int>();
            List<string> name = new List<string>();
            foreach (var item in staff)
            {

                staffId.Add(item.id);
                name.Add(string.Format(item.first_name + " " + item.last_name));

            }
            ViewBag.Staffid = staffId;
            ViewBag.StaffIdCount = staffId.Count;
            ViewBag.StaffName = name;

            List<int> RoleId = new List<int>();
            List<string> RoleName = new List<string>();
            foreach (var item in role)
            {

                RoleId.Add(item.role_id);
                RoleName.Add(string.Format(item.role_name));

            }
            ViewBag.RoleId = RoleId;
            ViewBag.RoleIdCount = RoleId.Count;
            ViewBag.RoleName = RoleName;
            return View();
        }

        // POST: Roster/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("roster_id,semester_id,staff_id,role_id")] RosterModel rosterModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rosterModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(rosterModel);
        }
 
        // GET: Roster/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SemesterRoster == null)
            {
                return NotFound();
            }

            var rosterModel = await _context.SemesterRoster.FindAsync(id);
            if (rosterModel == null)
            {
                return NotFound();
            }
            List<SemesterModel>? semester = await _context.Semester.ToListAsync();
            List<StaffModel>? staff = await _context.Staff.ToListAsync();
            List<RoleModel>? role = await _context.Role.ToListAsync();

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


            List<int> staffId = new List<int>();
            List<string> name = new List<string>();
            foreach (var item in staff)
            {

                staffId.Add(item.id);
                name.Add(string.Format(item.first_name + " " + item.last_name));

            }
            ViewBag.StaffId = staffId;
            ViewBag.StaffIdCount = staffId.Count;
            ViewBag.Name = name;

            List<int> RoleId = new List<int>();
            List<string> RoleName = new List<string>();
            foreach (var item in role)
            {

                RoleId.Add(item.role_id);
                RoleName.Add(string.Format(item.role_name));

            }
            ViewBag.RoleId = RoleId;
            ViewBag.RoleIdCount = RoleId.Count;
            ViewBag.RoleName = RoleName;
            return View(rosterModel);
        }

        // POST: Roster/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("roster_id,semester_id,staff_id,role_id")] RosterModel rosterModel)
        {
            if (id != rosterModel.roster_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rosterModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RosterModelExists(rosterModel.roster_id))
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
            return View(rosterModel);
        }

        // GET: Roster/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SemesterRoster == null)
            {
                return NotFound();
            }

            var rosterModel = await _context.SemesterRoster
                .FirstOrDefaultAsync(m => m.roster_id == id);
            if (rosterModel == null)
            {
                return NotFound();
            }

            return View(rosterModel);
        }

        // POST: Roster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SemesterRoster == null)
            {
                return Problem("Entity set 'ArchivalContext.RosterModel'  is null.");
            }
            var rosterModel = await _context.SemesterRoster.FindAsync(id);
            if (rosterModel != null)
            {
                _context.SemesterRoster.Remove(rosterModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RosterModelExists(int id)
        {
          return (_context.SemesterRoster?.Any(e => e.roster_id == id)).GetValueOrDefault();
        }
    }
  
}
