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
    public class AbsentReasonsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public AbsentReasonsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: AbsentReasons
        public async Task<IActionResult> Index()
        {
            return View(await _context.AbsentReasons.ToListAsync());
        }

        // GET: AbsentReasons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absentReason = await _context.AbsentReasons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (absentReason == null)
            {
                return NotFound();
            }

            return View(absentReason);
        }

        // GET: AbsentReasons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AbsentReasons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] AbsentReason absentReason)
        {
            if (ModelState.IsValid)
            {
                _context.Add(absentReason);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(absentReason);
        }

        // GET: AbsentReasons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absentReason = await _context.AbsentReasons.FindAsync(id);
            if (absentReason == null)
            {
                return NotFound();
            }
            return View(absentReason);
        }

        // POST: AbsentReasons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] AbsentReason absentReason)
        {
            if (id != absentReason.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(absentReason);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbsentReasonExists(absentReason.Id))
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
            return View(absentReason);
        }

        // GET: AbsentReasons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absentReason = await _context.AbsentReasons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (absentReason == null)
            {
                return NotFound();
            }

            return View(absentReason);
        }

        // POST: AbsentReasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var absentReason = await _context.AbsentReasons.FindAsync(id);
            if (absentReason != null)
            {
                _context.AbsentReasons.Remove(absentReason);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbsentReasonExists(int id)
        {
            return _context.AbsentReasons.Any(e => e.Id == id);
        }
    }
}
