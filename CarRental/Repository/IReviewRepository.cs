using RoadReady.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoadReady.Repository
{

    public interface IReviewRepository
    {
        Task<List<Review>> GetByCarIdAsync(int carId);
        Task<bool> AddAsync(Review review);
        Task<bool> UpdateAsync(Review review);
        Task<bool> DeleteAsync(int reviewId);
    }

}

