using System.Collections.Generic;
using System.Threading.Tasks;
using RoadReady.Models;
using RoadReady.Repository;
using RoadReady.DTO;
using RoadReady.Profiles;

namespace RoadReady.Service
{
    public interface IReviewService
    {
        Task<List<ReviewDTO>> GetReviewsByCarIdAsync(int carId);
        Task<bool> AddReviewAsync(ReviewDTO reviewDto);
        Task<bool> UpdateReviewAsync(ReviewDTO reviewDto);
        Task<bool> DeleteReviewAsync(int reviewId);
    }

}
