namespace RoadReady.DTO
{
    public class ReservationDTO
    {
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DropoffDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string ReservationStatus { get; set; }
    }
}
