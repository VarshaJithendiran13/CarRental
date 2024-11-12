using AutoMapper;
using CarRental.Models;
using CarRental.Models.DTOs;
using CarRental.DTOs;// Assuming you have your DTOs defined here

namespace CarRental
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping for User
            CreateMap<User, UserDTO>().ReverseMap();

            // Mapping for Car
            CreateMap<Car, CarDTO>().ReverseMap();

            // Mapping for Reservation
            CreateMap<Reservation, ReservationDTO>().ReverseMap();

            // Mapping for Payment
            CreateMap<Payment, PaymentDTO>().ReverseMap();

            // Mapping for Review
            CreateMap<Review, ReviewDTO>().ReverseMap();
        }
    }
}
