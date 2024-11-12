using System;

namespace CarRental.DTOs
{
    public class ReservationDTO
    {
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public DateOnly PickupDate { get; set; }
        public DateOnly DropoffDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string? ReservationStatus { get; set; }
    }
}
