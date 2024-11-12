using RoadReady.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoadReady.Repository
{
    public interface ICarRepository
    {
        Task<List<Car>> GetAllAsync();
        Task<Car> GetByIdAsync(int carId);
        Task<List<Car>> SearchAsync(string make, string model, string location, decimal? maxPrice);
        Task<bool> AddAsync(Car car);
        Task<bool> UpdateAsync(Car car);
        Task<bool> DeleteAsync(int carId);
    }

}
