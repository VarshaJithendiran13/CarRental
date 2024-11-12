namespace RoadReady.DTO
{
    public class CarDTO
    {
        public int CarId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Specifications { get; set; }
        public decimal PricePerDay { get; set; }
        public string Location { get; set; }
        public string ImageURL { get; set; }
        public bool AvailabilityStatus { get; set; }
    }
}
