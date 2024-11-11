using CarRental.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Repository
{
    public interface IPaymentRepository
    {
        Task<Payment> GetPaymentByIdAsync(int paymentId);
        Task AddPaymentAsync(Payment payment);
    }

    public class PaymentRepository : IPaymentRepository
    {
        private readonly YourDbContext _context;

        public PaymentRepository(YourDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> GetPaymentByIdAsync(int paymentId)
        {
            return await _context.Payments.FindAsync(paymentId);
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }
    }
}
