using RoadReady.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using RoadReady.Repository;
using Microsoft.EntityFrameworkCore;

namespace RoadReady.Repository
{
     public class ReviewRepository : IReviewRepository
    {
        private readonly CarRentalDbContext _context;

        public ReviewRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetByCarIdAsync(int carId)
        {
            return await _context.Reviews
                .Where(r => r.CarId == carId)
                .ToListAsync();
        }

        public async Task<bool> AddAsync(Review review)
        {
            _context.Reviews.Add(review);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Review review)
        {
            _context.Reviews.Update(review);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review == null) return false;

            _context.Reviews.Remove(review);
            return await _context.SaveChangesAsync() > 0;
        }
    }


}
