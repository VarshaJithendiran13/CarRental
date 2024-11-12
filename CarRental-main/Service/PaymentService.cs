using AutoMapper;
using RoadReady.DTO;
using RoadReady.Models;
using RoadReady.Repository;
using System.Threading.Tasks;

namespace RoadReady.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IMapper mapper, IPaymentRepository paymentRepository)
        {
            _mapper = mapper;
            _paymentRepository = paymentRepository;
        }

        public async Task<bool> ProcessPaymentAsync(PaymentDTO paymentDto)
        {
            var payment = _mapper.Map<Payment>(paymentDto);
            return await _paymentRepository.AddAsync(payment); // Process payment (e.g., create entry in DB)
        }

        public async Task<PaymentDTO> GetPaymentDetailsAsync(int reservationId)
        {
            var payment = await _paymentRepository.GetByReservationIdAsync(reservationId);
            return _mapper.Map<PaymentDTO>(payment); // Map payment to DTO
        }

        public async Task<bool> UpdatePaymentStatusAsync(int paymentId, string status)
        {
            return await _paymentRepository.UpdateStatusAsync(paymentId, status); // Update payment status in DB
        }
    }

}
