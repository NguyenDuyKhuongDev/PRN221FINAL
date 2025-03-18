using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalPRN221.Models;

namespace FinalPRN221.Controllers
{
    public class AttendanceStatusController : Controller
    {
        private readonly ApplicationDBContext _context;

        public AttendanceStatusController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: AttendanceStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.AttendanceStatuses.ToListAsync());
        }

        // GET: AttendanceStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendanceStatus = await _context.AttendanceStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendanceStatus == null)
            {
                return NotFound();
            }

            return View(attendanceStatus);
        }

        // GET: AttendanceStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AttendanceStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] AttendanceStatus attendanceStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendanceStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(attendanceStatus);
        }

        // GET: AttendanceStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendanceStatus = await _context.AttendanceStatuses.FindAsync(id);
            if (attendanceStatus == null)
            {
                return NotFound();
            }
            return View(attendanceStatus);
        }

        // POST: AttendanceStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] AttendanceStatus attendanceStatus)
        {
            if (id != attendanceStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendanceStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceStatusExists(attendanceStatus.Id))
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
            return View(attendanceStatus);
        }

        // GET: AttendanceStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendanceStatus = await _context.AttendanceStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendanceStatus == null)
            {
                return NotFound();
            }

            return View(attendanceStatus);
        }

        // POST: AttendanceStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendanceStatus = await _context.AttendanceStatuses.FindAsync(id);
            if (attendanceStatus != null)
            {
                _context.AttendanceStatuses.Remove(attendanceStatus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceStatusExists(int id)
        {
            return _context.AttendanceStatuses.Any(e => e.Id == id);
        }
    }
}
