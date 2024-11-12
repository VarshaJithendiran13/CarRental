using System.Collections.Generic;
using System.Threading.Tasks;
using RoadReady.Models;
using RoadReady.Repository;
using RoadReady.DTO;
using RoadReady.Profiles;

namespace RoadReady.Service
{
    public interface ICarService
    {
        Task<List<CarDTO>> GetAllCarsAsync();
        Task<CarDTO> GetCarDetailsAsync(int carId);
        Task<List<CarDTO>> SearchCarsAsync(string make, string model, string location, decimal? maxPrice);
        Task<bool> AddCarAsync(CarDTO carDto);
        Task<bool> UpdateCarAsync(CarDTO carDto);
        Task<bool> DeleteCarAsync(int carId);
    }

}
