using CarRental.Models;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly YourDbContext _context;

        // Constructor to inject the DbContext
        public UserRepository(YourDbContext context)
        {
            _context = context;
        }

        // Get all users
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Reservations)  // Optionally include related reservations
                .Include(u => u.Reviews)  // Optionally include related reviews
                .ToListAsync();
        }

        // Get user by ID
        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.Reservations)
                .Include(u => u.Reviews)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        // Add a new user
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        // Update an existing user
        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        // Delete a user
        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        // Get user by email (useful for login or authentication)
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Reservations)
                .Include(u => u.Reviews)
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
