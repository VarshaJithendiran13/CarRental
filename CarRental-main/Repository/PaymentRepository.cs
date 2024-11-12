using Microsoft.EntityFrameworkCore;
using RoadReady.Models;

namespace RoadReady.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly CarRentalDbContext _context;

        public PaymentRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Payment> GetByReservationIdAsync(int reservationId)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(p => p.ReservationId == reservationId);
        }

        public async Task<bool> UpdateStatusAsync(int paymentId, string status)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment == null) return false;

            payment.Status = status;
            _context.Payments.Update(payment);
            return await _context.SaveChangesAsync() > 0;
        }
    }


}
