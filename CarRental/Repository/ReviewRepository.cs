using CarRental.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Repository
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetReviewsByCarIdAsync(int carId);
        Task AddReviewAsync(Review review);
    }

    public class ReviewRepository : IReviewRepository
    {
        private readonly YourDbContext _context;

        public ReviewRepository(YourDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetReviewsByCarIdAsync(int carId)
        {
            return await _context.Reviews
                .Where(r => r.CarId == carId)
                .ToListAsync();
        }

        public async Task AddReviewAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }
    }
}
