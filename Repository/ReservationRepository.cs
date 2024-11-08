using Microsoft.EntityFrameworkCore;
using RoadReady.Models;

namespace RoadReady.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly CarRentalDbContext _context;

        public ReservationRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reservation>> GetByUserIdAsync(int userId)
        {
            return await _context.Reservations
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(int reservationId)
        {
            return await _context.Reservations.FindAsync(reservationId);
        }

        public async Task<bool> AddAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation == null) return false;

            _context.Reservations.Remove(reservation);
            return await _context.SaveChangesAsync() > 0;
        }
    }


}
