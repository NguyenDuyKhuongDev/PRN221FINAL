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
    public class PayrollStatusController : Controller
    {
        private readonly ApplicationDBContext _context;

        public PayrollStatusController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: PayrollStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.PayrollStatuses.ToListAsync());
        }

        // GET: PayrollStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payrollStatus = await _context.PayrollStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payrollStatus == null)
            {
                return NotFound();
            }

            return View(payrollStatus);
        }

        // GET: PayrollStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PayrollStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PayrollStatus payrollStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payrollStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(payrollStatus);
        }

        // GET: PayrollStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payrollStatus = await _context.PayrollStatuses.FindAsync(id);
            if (payrollStatus == null)
            {
                return NotFound();
            }
            return View(payrollStatus);
        }

        // POST: PayrollStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PayrollStatus payrollStatus)
        {
            if (id != payrollStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payrollStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PayrollStatusExists(payrollStatus.Id))
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
            return View(payrollStatus);
        }

        // GET: PayrollStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payrollStatus = await _context.PayrollStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payrollStatus == null)
            {
                return NotFound();
            }

            return View(payrollStatus);
        }

        // POST: PayrollStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payrollStatus = await _context.PayrollStatuses.FindAsync(id);
            if (payrollStatus != null)
            {
                _context.PayrollStatuses.Remove(payrollStatus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PayrollStatusExists(int id)
        {
            return _context.PayrollStatuses.Any(e => e.Id == id);
        }
    }
}
