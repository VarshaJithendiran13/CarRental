
        namespace CarRental.Models.DTOs
    {
        public class UserDTO
        {
            public int UserId { get; set; }

            public string FirstName { get; set; } = null!;

            public string LastName { get; set; } = null!;

            public string Email { get; set; } = null!;

            public string? PhoneNumber { get; set; }

            public string? Role { get; set; }
        }
    }



