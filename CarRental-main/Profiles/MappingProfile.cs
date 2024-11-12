using AutoMapper;
using RoadReady.DTO;
using RoadReady.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RoadReady.Profiles
{
    public class MappingProfile : Profile
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

