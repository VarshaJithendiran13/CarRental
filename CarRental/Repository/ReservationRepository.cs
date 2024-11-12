using CarRental.Models;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly YourDbContext _context;

        public ReservationRepository(YourDbContext context)
        {
            _context = context;
        }

        // Get all reservations
        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            return await _context.Reservations
                .Include(r => r.Car)
                .Include(r => r.User)
                .ToListAsync();
        }

        // Get reservation by ID
        public async Task<Reservation> GetReservationByIdAsync(int reservationId)
        {
            return await _context.Reservations
                .Include(r => r.Car)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.ReservationId == reservationId);
        }

        // Get reservations for a specific user
        public async Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(int userId)
        {
            return await _context.Reservations
                .Where(r => r.UserId == userId)
                .Include(r => r.Car)
                .Include(r => r.User)
                .ToListAsync();
        }

        // Get reservations for a specific car
        public async Task<IEnumerable<Reservation>> GetReservationsByCarIdAsync(int carId)
        {
            return await _context.Reservations
                .Where(r => r.CarId == carId)
                .Include(r => r.Car)
                .Include(r => r.User)
                .ToListAsync();
        }

        // Add a new reservation
        public async Task AddReservationAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        // Update an existing reservation
        public async Task UpdateReservationAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        // Delete a reservation by ID
        public async Task DeleteReservationAsync(int reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
