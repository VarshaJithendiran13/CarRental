using CarRental.Models;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly YourDbContext _context;

        // Constructor to inject the DbContext
        public ReviewRepository(YourDbContext context)
        {
            _context = context;
        }

        // Get all reviews
        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await _context.Reviews
                .Include(r => r.Car)  // Include related car data
                .Include(r => r.User)  // Include related user data
                .ToListAsync();
        }

        // Get a specific review by ID
        public async Task<Review> GetReviewByIdAsync(int reviewId)
        {
            return await _context.Reviews
                .Include(r => r.Car)  // Include related car data
                .Include(r => r.User)  // Include related user data
                .FirstOrDefaultAsync(r => r.ReviewId == reviewId);
        }

        // Add a new review
        public async Task AddReviewAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        // Update an existing review
        public async Task UpdateReviewAsync(Review review)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }

        // Delete a review by ID
        public async Task DeleteReviewAsync(int reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }
    }
}
