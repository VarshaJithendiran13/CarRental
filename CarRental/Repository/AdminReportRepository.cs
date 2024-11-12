using CarRental.Models;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Repository
{
    public class AdminReportRepository : IAdminReportRepository
    {
        private readonly YourDbContext _context;

        public AdminReportRepository(YourDbContext context)
        {
            _context = context;
        }

        // Fetch all reports
        public async Task<IEnumerable<AdminReport>> GetAllReportsAsync()
        {
            return await _context.AdminReports.ToListAsync();
        }

        // Fetch a specific report by its ID
        public async Task<AdminReport> GetReportByIdAsync(int reportId)
        {
            return await _context.AdminReports
                .FirstOrDefaultAsync(r => r.ReportId == reportId);
        }

        // Add a new report to the database
        public async Task AddReportAsync(AdminReport report)
        {
            await _context.AdminReports.AddAsync(report);
            await _context.SaveChangesAsync();
        }

        // Update an existing report
        public async Task UpdateReportAsync(AdminReport report)
        {
            _context.AdminReports.Update(report);
            await _context.SaveChangesAsync();
        }

        // Delete a report by ID
        public async Task DeleteReportAsync(int reportId)
        {
            var report = await _context.AdminReports.FindAsync(reportId);
            if (report != null)
            {
                _context.AdminReports.Remove(report);
                await _context.SaveChangesAsync();
            }
        }
    }
}
