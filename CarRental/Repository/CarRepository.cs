using CarRental.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Repository
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAvailableCarsAsync();
        Task<Car> GetCarByIdAsync(int carId);
        Task AddCarAsync(Car car);
        Task UpdateCarAsync(Car car);
        Task DeleteCarAsync(int carId);
    }

    public class CarRepository : ICarRepository
    {
        private readonly YourDbContext _context;

        public CarRepository(YourDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Car>> GetAvailableCarsAsync()
        {
            return await _context.Cars
                .Where(c => c.AvailabilityStatus == true)
                .ToListAsync();
        }

        public async Task<Car> GetCarByIdAsync(int carId)
        {
            return await _context.Cars.FindAsync(carId);
        }

        public async Task AddCarAsync(Car car)
        {
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCarAsync(Car car)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(int carId)
        {
            var car = await _context.Cars.FindAsync(carId);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
        }
    }
}

