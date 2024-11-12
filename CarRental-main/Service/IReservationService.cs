using System.Collections.Generic;
using System.Threading.Tasks;
using RoadReady.Models;
using RoadReady.Repository;
using RoadReady.DTO;
using RoadReady.Profiles;

namespace RoadReady.Service
{
    public interface IReservationService
    {
        Task<List<ReservationDTO>> GetUserReservationsAsync(int userId);
        Task<ReservationDTO> GetReservationDetailsAsync(int reservationId);
        Task<bool> ReserveCarAsync(ReservationDTO reservationDto);
        Task<bool> UpdateReservationAsync(ReservationDTO reservationDto);
        Task<bool> CancelReservationAsync(int reservationId);
    }

}
