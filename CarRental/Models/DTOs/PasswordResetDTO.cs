namespace CarRental.Models.DTOs
{
    public class PasswordResetDTO
    {
        public int ResetId { get; set; }

        public int UserId { get; set; }

        public string ResetToken { get; set; } = null!;

        public DateTime ExpirationDate { get; set; }

        public bool IsUsed { get; set; }

        // This can be used for returning the associated user's basic information if necessary
        public string UserEmail { get; set; } = null!;
    }
}
