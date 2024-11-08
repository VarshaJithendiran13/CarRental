using RoadReady.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoadReady.Repository
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetByUserIdAsync(int userId);
        Task<Reservation> GetByIdAsync(int reservationId);
        Task<bool> AddAsync(Reservation reservation);
        Task<bool> UpdateAsync(Reservation reservation);
        Task<bool> DeleteAsync(int reservationId);
    }

}
