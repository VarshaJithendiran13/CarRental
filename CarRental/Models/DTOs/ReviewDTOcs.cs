namespace CarRental.Models.DTOs
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }

        public int UserId { get; set; }

        public int CarId { get; set; }

        public int? Rating { get; set; }

        public string? Comment { get; set; }

        public DateOnly ReviewDate { get; set; }

        // Include User and Car details in the ReviewDTO if needed
        public UserDTO User { get; set; } = null!;
        public CarDTO Car { get; set; } = null!;
    }
}
