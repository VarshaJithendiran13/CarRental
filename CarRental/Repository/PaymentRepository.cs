using CarRental.Models;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly YourDbContext _context;

        // Constructor that receives the DbContext via Dependency Injection
        public PaymentRepository(YourDbContext context)
        {
            _context = context;
        }

        // Get all payments
        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _context.Payments.ToListAsync();
        }

        // Get payment by ID
        public async Task<Payment> GetPaymentByIdAsync(int paymentId)
        {
            return await _context.Payments
                              .FirstOrDefaultAsync(payment => payment.PaymentId == paymentId);
        }

        // Add new payment
        public async Task AddPaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        // Update existing payment
        public async Task UpdatePaymentAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }

        // Delete payment by ID
        public async Task DeletePaymentAsync(int paymentId)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
