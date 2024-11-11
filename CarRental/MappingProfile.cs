using CarRental.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;

namespace CarRental
{
    public class MappingProfile 
    {
        public MappingProfile()
        {
            // User and UserDTO mapping
            CreateMap<User, UserDTO>().ReverseMap();

            // Car and CarDTO mapping
            CreateMap<Car, CarDTO>().ReverseMap();

            // Reservation and ReservationDTO mapping
            CreateMap<Reservation, ReservationDTO>().ReverseMap();

            // Payment and PaymentDTO mapping
            CreateMap<Payment, PaymentDTO>().ReverseMap();

            // Review and ReviewDTO mapping
            CreateMap<Review, ReviewDTO>().ReverseMap();
        }
    }
}
