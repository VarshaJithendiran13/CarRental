using CarRental.Models;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly YourDbContext _context;

        // Constructor that receives the DbContext via Dependency Injection
        public CarRepository(YourDbContext context)
        {
            _context = context;
        }

        // Get all cars
        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        // Get car by ID
        public async Task<Car> GetCarByIdAsync(int carId)
        {
            return await _context.Cars
                                 .FirstOrDefaultAsync(car => car.CarId == carId);
        }

        // Add new car
        public async Task AddCarAsync(Car car)
        {
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
        }

        // Update existing car
        public async Task UpdateCarAsync(Car car)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
        }

        // Delete car by ID
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
