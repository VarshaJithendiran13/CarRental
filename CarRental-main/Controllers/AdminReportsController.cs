using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RoadReady.Models;

namespace RoadReady.Controllers
{
    public class AdminReportsController : Controller
    {
        private readonly CarRentalDbContext _context;

        public AdminReportsController(CarRentalDbContext context)
        {
            _context = context;
        }

        // GET: AdminReports
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdminReports.ToListAsync());
        }

        // GET: AdminReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminReport = await _context.AdminReports
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (adminReport == null)
            {
                return NotFound();
            }

            return View(adminReport);
        }

        // GET: AdminReports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReportId,ReportDate,TotalReservations,TotalRevenue,TopCars,MostActiveUser")] AdminReport adminReport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adminReport);
        }

        // GET: AdminReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminReport = await _context.AdminReports.FindAsync(id);
            if (adminReport == null)
            {
                return NotFound();
            }
            return View(adminReport);
        }

        // POST: AdminReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReportId,ReportDate,TotalReservations,TotalRevenue,TopCars,MostActiveUser")] AdminReport adminReport)
        {
            if (id != adminReport.ReportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminReportExists(adminReport.ReportId))
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
            return View(adminReport);
        }

        // GET: AdminReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminReport = await _context.AdminReports
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (adminReport == null)
            {
                return NotFound();
            }

            return View(adminReport);
        }

        // POST: AdminReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adminReport = await _context.AdminReports.FindAsync(id);
            if (adminReport != null)
            {
                _context.AdminReports.Remove(adminReport);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminReportExists(int id)
        {
            return _context.AdminReports.Any(e => e.ReportId == id);
        }
    }
}
