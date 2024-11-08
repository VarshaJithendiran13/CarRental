using AutoMapper;
using RoadReady.DTO;
using RoadReady.Models;
using RoadReady.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoadReady.Service
{
    public class ReservationService : IReservationService
    {
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IMapper mapper, IReservationRepository reservationRepository)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
        }

        public async Task<List<ReservationDTO>> GetUserReservationsAsync(int userId)
        {
            var reservations = await _reservationRepository.GetByUserIdAsync(userId);
            return _mapper.Map<List<ReservationDTO>>(reservations); // Map reservations to DTOs
        }

        public async Task<ReservationDTO> GetReservationDetailsAsync(int reservationId)
        {
            var reservation = await _reservationRepository.GetByIdAsync(reservationId);
            return _mapper.Map<ReservationDTO>(reservation); // Map reservation to DTO
        }

        public async Task<bool> ReserveCarAsync(ReservationDTO reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);
            return await _reservationRepository.AddAsync(reservation); // Add reservation to DB
        }

        public async Task<bool> UpdateReservationAsync(ReservationDTO reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);
            return await _reservationRepository.UpdateAsync(reservation); // Update reservation in DB
        }

        public async Task<bool> CancelReservationAsync(int reservationId)
        {
            return await _reservationRepository.DeleteAsync(reservationId); // Cancel reservation (delete from DB)
        }
    }

}
