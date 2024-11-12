using CarRental.DTOs;
using CarRental.Models.DTOs;

namespace CarRental.Models.DTOs
{
    public class PaymentDTO
    {
        public int PaymentId { get; set; }

        public int ReservationId { get; set; }

        public decimal Amount { get; set; }

        public DateOnly PaymentDate { get; set; }

        public string? PaymentMethod { get; set; }

        public string? Status { get; set; }

        // Reservation details could be included if necessary
        public ReservationDTO Reservation { get; set; } = null!;
    }
}
