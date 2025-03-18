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
    public class AttendancesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public AttendancesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Attendances
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.Attendances.Include(a => a.AbsentReason).Include(a => a.Status);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Attendances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.AbsentReason)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(m => m.AttendanceId == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // GET: Attendances/Create
        public IActionResult Create()
        {
            ViewData["AbsentReasonId"] = new SelectList(_context.AbsentReasons, "Id", "Id");
            ViewData["StatusId"] = new SelectList(_context.AttendanceStatuses, "Id", "Id");
            return View();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttendanceId,EmployeeId,AttendanceDate,AbsentReasonId,StatusId,CheckInTime,CheckOutTime,WorkHours,OverTimeHours,Notes,ApprovedBy,IsApproved")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AbsentReasonId"] = new SelectList(_context.AbsentReasons, "Id", "Id", attendance.AbsentReasonId);
            ViewData["StatusId"] = new SelectList(_context.AttendanceStatuses, "Id", "Id", attendance.StatusId);
            return View(attendance);
        }

        // GET: Attendances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            ViewData["AbsentReasonId"] = new SelectList(_context.AbsentReasons, "Id", "Id", attendance.AbsentReasonId);
            ViewData["StatusId"] = new SelectList(_context.AttendanceStatuses, "Id", "Id", attendance.StatusId);
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AttendanceId,EmployeeId,AttendanceDate,AbsentReasonId,StatusId,CheckInTime,CheckOutTime,WorkHours,OverTimeHours,Notes,ApprovedBy,IsApproved")] Attendance attendance)
        {
            if (id != attendance.AttendanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.AttendanceId))
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
            ViewData["AbsentReasonId"] = new SelectList(_context.AbsentReasons, "Id", "Id", attendance.AbsentReasonId);
            ViewData["StatusId"] = new SelectList(_context.AttendanceStatuses, "Id", "Id", attendance.StatusId);
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.AbsentReason)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(m => m.AttendanceId == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance != null)
            {
                _context.Attendances.Remove(attendance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendances.Any(e => e.AttendanceId == id);
        }
    }
}
