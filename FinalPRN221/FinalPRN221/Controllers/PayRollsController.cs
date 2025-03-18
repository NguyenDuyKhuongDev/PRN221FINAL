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
    public class PayRollsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public PayRollsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: PayRolls
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.PayRolls.Include(p => p.Status);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: PayRolls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payRoll = await _context.PayRolls
                .Include(p => p.Status)
                .FirstOrDefaultAsync(m => m.PayrollId == id);
            if (payRoll == null)
            {
                return NotFound();
            }

            return View(payRoll);
        }

        // GET: PayRolls/Create
        public IActionResult Create()
        {
            ViewData["StatusId"] = new SelectList(_context.PayrollStatuses, "Id", "Id");
            return View();
        }

        // POST: PayRolls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PayrollId,EmployeeId,StatusId,BasicSalary,Allowance,Bonus,FineSalary,OverTimePay,PayrollDate,Month,Year,Note,UpdateBy")] PayRoll payRoll)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payRoll);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StatusId"] = new SelectList(_context.PayrollStatuses, "Id", "Id", payRoll.StatusId);
            return View(payRoll);
        }

        // GET: PayRolls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payRoll = await _context.PayRolls.FindAsync(id);
            if (payRoll == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new SelectList(_context.PayrollStatuses, "Id", "Id", payRoll.StatusId);
            return View(payRoll);
        }

        // POST: PayRolls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PayrollId,EmployeeId,StatusId,BasicSalary,Allowance,Bonus,FineSalary,OverTimePay,PayrollDate,Month,Year,Note,UpdateBy")] PayRoll payRoll)
        {
            if (id != payRoll.PayrollId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payRoll);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PayRollExists(payRoll.PayrollId))
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
            ViewData["StatusId"] = new SelectList(_context.PayrollStatuses, "Id", "Id", payRoll.StatusId);
            return View(payRoll);
        }

        // GET: PayRolls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payRoll = await _context.PayRolls
                .Include(p => p.Status)
                .FirstOrDefaultAsync(m => m.PayrollId == id);
            if (payRoll == null)
            {
                return NotFound();
            }

            return View(payRoll);
        }

        // POST: PayRolls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payRoll = await _context.PayRolls.FindAsync(id);
            if (payRoll != null)
            {
                _context.PayRolls.Remove(payRoll);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PayRollExists(int id)
        {
            return _context.PayRolls.Any(e => e.PayrollId == id);
        }
    }
}
