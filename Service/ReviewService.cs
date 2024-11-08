using AutoMapper;
using RoadReady.DTO;
using RoadReady.Models;
using RoadReady.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoadReady.Service
{

    public class ReviewService : IReviewService
    {
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository; // Assuming a repository to interact with DB

        public ReviewService(IMapper mapper, IReviewRepository reviewRepository)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
        }

        public async Task<List<ReviewDTO>> GetReviewsByCarIdAsync(int carId)
        {
            var reviews = await _reviewRepository.GetByCarIdAsync(carId);
            return _mapper.Map<List<ReviewDTO>>(reviews); // Map reviews to DTOs
        }

        public async Task<bool> AddReviewAsync(ReviewDTO reviewDto)
        {
            var review = _mapper.Map<Review>(reviewDto);
            return await _reviewRepository.AddAsync(review); // Add review to DB
        }

        public async Task<bool> UpdateReviewAsync(ReviewDTO reviewDto)
        {
            var review = _mapper.Map<Review>(reviewDto);
            return await _reviewRepository.UpdateAsync(review); // Update review in DB
        }

        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            return await _reviewRepository.DeleteAsync(reviewId); // Delete review from DB
        }
    }

}
