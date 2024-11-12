using System.Threading.Tasks;
using RoadReady.Models;
using RoadReady.Repository;
using RoadReady.DTO;
using RoadReady.Profiles;

namespace RoadReady.Service
{
    public interface IPaymentService
    {
        Task<bool> ProcessPaymentAsync(PaymentDTO paymentDto);
        Task<PaymentDTO> GetPaymentDetailsAsync(int reservationId);
        Task<bool> UpdatePaymentStatusAsync(int paymentId, string status);
    }

}
