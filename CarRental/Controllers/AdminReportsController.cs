using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRental.Models;

namespace CarRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminReportsController : ControllerBase
    {
        private readonly YourDbContext _context;

        public AdminReportsController(YourDbContext context)
        {
            _context = context;
        }

        // GET: api/AdminReports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminReport>>> GetAdminReports()
        {
            return await _context.AdminReports.ToListAsync();
        }

        // GET: api/AdminReports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminReport>> GetAdminReport(int id)
        {
            var adminReport = await _context.AdminReports.FindAsync(id);

            if (adminReport == null)
            {
                return NotFound();
            }

            return adminReport;
        }

        // PUT: api/AdminReports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdminReport(int id, AdminReport adminReport)
        {
            if (id != adminReport.ReportId)
            {
                return BadRequest();
            }

            _context.Entry(adminReport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminReportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AdminReports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdminReport>> PostAdminReport(AdminReport adminReport)
        {
            _context.AdminReports.Add(adminReport);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdminReport", new { id = adminReport.ReportId }, adminReport);
        }

        // DELETE: api/AdminReports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdminReport(int id)
        {
            var adminReport = await _context.AdminReports.FindAsync(id);
            if (adminReport == null)
            {
                return NotFound();
            }

            _context.AdminReports.Remove(adminReport);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminReportExists(int id)
        {
            return _context.AdminReports.Any(e => e.ReportId == id);
        }
    }
}
