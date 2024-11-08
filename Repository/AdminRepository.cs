using Microsoft.EntityFrameworkCore;
using RoadReady.Models;

namespace RoadReady.Repository
{

    public class AdminRepository : IAdminRepository
    {
        private readonly CarRentalDbContext _context;

        public AdminRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<bool> UpdateUserRoleAsync(int userId, string role)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.Role = role;
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }

        //public async Task<AdminReport> GenerateDailyReportAsync()
        //{
        //    // Example of generating a report (implement logic as per business requirements)
        //    return new AdminReport { ReportDate = DateTime.Now, TotalUsers = 100, TotalCars = 50 }; // Placeholder logic
        //}

        public async Task<List<AdminReport>> GetReportsAsync()
        {
            return await _context.AdminReports.ToListAsync(); // Placeholder logic
        }
    }
}
