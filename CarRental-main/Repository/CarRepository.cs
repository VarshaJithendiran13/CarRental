using Microsoft.EntityFrameworkCore;
using RoadReady.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoadReady.Repository
{

    public class CarRepository : ICarRepository
    {
        private readonly CarRentalDbContext _context;

        public CarRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task<List<Car>> GetAllAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car> GetByIdAsync(int carId)
        {
            return await _context.Cars.FindAsync(carId);
        }

        public async Task<List<Car>> SearchAsync(string make, string model, string location, decimal? maxPrice)
        {
            var query = _context.Cars.AsQueryable();

            if (!string.IsNullOrEmpty(make))
                query = query.Where(c => c.Make.Contains(make));
            if (!string.IsNullOrEmpty(model))
                query = query.Where(c => c.Model.Contains(model));
            if (!string.IsNullOrEmpty(location))
                query = query.Where(c => c.Location.Contains(location));
            if (maxPrice.HasValue)
                query = query.Where(c => c.PricePerDay <= maxPrice.Value);

            return await query.ToListAsync();
        }

        public async Task<bool> AddAsync(Car car)
        {
            _context.Cars.Add(car);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Car car)
        {
            _context.Cars.Update(car);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int carId)
        {
            var car = await _context.Cars.FindAsync(carId);
            if (car == null) return false;

            _context.Cars.Remove(car);
            return await _context.SaveChangesAsync() > 0;
        }
    }


}
