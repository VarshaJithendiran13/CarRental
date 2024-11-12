using Microsoft.AspNetCore.Cors.Infrastructure;
using RoadReady.Models;
using RoadReady.Repository;
using RoadReady.DTO;
using RoadReady.Profiles;
using AutoMapper;

namespace RoadReady.Service
{
    public class CarService : ICarService
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;

        public CarService(IMapper mapper, ICarRepository carRepository)
        {
            _mapper = mapper;
            _carRepository = carRepository;
        }

        public async Task<List<CarDTO>> GetAllCarsAsync()
        {
            var cars = await _carRepository.GetAllAsync();
            return _mapper.Map<List<CarDTO>>(cars); // Map cars to DTOs
        }

        public async Task<CarDTO> GetCarDetailsAsync(int carId)
        {
            var car = await _carRepository.GetByIdAsync(carId);
            return _mapper.Map<CarDTO>(car); // Map car to DTO
        }

        public async Task<List<CarDTO>> SearchCarsAsync(string make, string model, string location, decimal? maxPrice)
        {
            var cars = await _carRepository.SearchAsync(make, model, location, maxPrice);
            return _mapper.Map<List<CarDTO>>(cars); // Map searched cars to DTOs
        }

        public async Task<bool> AddCarAsync(CarDTO carDto)
        {
            var car = _mapper.Map<Car>(carDto);
            return await _carRepository.AddAsync(car); // Add car to DB
        }

        public async Task<bool> UpdateCarAsync(CarDTO carDto)
        {
            var car = _mapper.Map<Car>(carDto);
            return await _carRepository.UpdateAsync(car); // Update car in DB
        }

        public async Task<bool> DeleteCarAsync(int carId)
        {
            return await _carRepository.DeleteAsync(carId); // Delete car from DB
        }
    }


}
