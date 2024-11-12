using Microsoft.EntityFrameworkCore;
using RoadReady.Models;
using RoadReady.Repository;

namespace RoadReady.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly CarRentalDbContext _context; 

        public UserRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(User user)
        {
            _context.Users.Add(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password); // You should hash the password and compare
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<bool> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ResetPasswordAsync(string email, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return false;

            user.Password = newPassword; // Ideally, hash the new password
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }

}
