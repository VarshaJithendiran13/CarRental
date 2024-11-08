using RoadReady.Models;
using System.Threading.Tasks;

namespace RoadReady.Repository
{
    public interface IPaymentRepository
    {
        Task<bool> AddAsync(Payment payment);
        Task<Payment> GetByReservationIdAsync(int reservationId);
        Task<bool> UpdateStatusAsync(int paymentId, string status);
    }
}
