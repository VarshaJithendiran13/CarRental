namespace CarRental.Models.DTOs
{
    public class AdminReportDTO
    {
        public int ReportId { get; set; }

        public DateOnly ReportDate { get; set; }

        public int? TotalReservations { get; set; }

        public decimal? TotalRevenue { get; set; }

        public string? TopCars { get; set; }

        public string? MostActiveUser { get; set; }
    }
}
