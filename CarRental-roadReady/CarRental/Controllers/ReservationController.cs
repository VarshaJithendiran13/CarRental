using AutoMapper;
using CarRental.DTOs; // Assuming the DTO is in this namespace
using CarRental.Exceptions;
using CarRental.Models;
using CarRental.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarRental;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Require authentication for all actions
public class ReservationController : ControllerBase
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;
    private readonly JwtTokenService _jwtTokenService;


    public ReservationController(IReservationRepository reservationRepository, IMapper mapper, JwtTokenService jwtTokenService)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
        _jwtTokenService = jwtTokenService;

    }

    // GET: api/Reservations
    [HttpGet]
    [Authorize(Roles = "Admin")] // Allow Admin to get all reservations
    public async Task<IActionResult> GetAllReservations()
    {
        try
        {
            var reservations = await _reservationRepository.GetAllReservationsAsync();
            var reservationDTOs = _mapper.Map<IEnumerable<ReservationDTO>>(reservations); // Map to DTO
            return Ok(reservationDTOs);
        }
        catch (InternalServerException ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    // POST: api/Reservations
    // POST: api/Reservations
    [HttpPost]
    [Authorize(Roles = "User")] // Only Users can create their reservations
    public async Task<IActionResult> AddReservation([FromBody] CreateReservationDTO reservationDTO)
    {
        try
        {
            // Extract userId from the JWT token
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId");
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int loggedInUserId = int.Parse(userIdClaim.Value); // Extract logged-in user's ID

            // Ensure the reservation belongs to the logged-in user
            reservationDTO.UserId = loggedInUserId; // Use the logged-in user's ID for the reservation

            var reservation = _mapper.Map<Reservation>(reservationDTO); // Map from DTO to model
            await _reservationRepository.AddReservationAsync(reservation);
            var createdReservationDTO = _mapper.Map<CreateReservationDTO>(reservation); // Map to DTO for response

            return CreatedAtAction(nameof(GetReservationById), new { reservationId = reservation.ReservationId }, createdReservationDTO);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message); // Return Bad Request if validation fails
        }
        catch (DuplicateResourceException ex)
        {
            return Conflict(ex.Message); // Return Conflict if a duplicate resource is found
        }
        catch (InternalServerException ex)
        {
            return StatusCode(500, ex.Message); // Return internal server error for unexpected issues
        }
    }


    // PUT: api/Reservations
    [HttpPut]
    [Authorize(Roles = "User, Admin")] // Allow Users to update their own reservations and Admin to update any reservation
    public async Task<IActionResult> UpdateReservation([FromBody] ReservationDTO reservationDTO)
    {
        try
        {
            // Extract userId from the JWT token
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId");
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int loggedInUserId = int.Parse(userIdClaim.Value);

            // Fetch the existing reservation to verify its existence
            var existingReservation = await _reservationRepository.GetReservationByIdAsync(reservationDTO.ReservationId);
            if (existingReservation == null)
            {
                return NotFound("Reservation not found.");
            }

            // Allow only the logged-in user or Admin to update
            if (User.IsInRole("User") && existingReservation.UserId != loggedInUserId)
            {
                return Forbid("You can only update your own reservations.");
            }

            // Map updated details from the ReservationDTO to the Reservation model
            var reservation = _mapper.Map<Reservation>(reservationDTO);

            // Save changes to the reservation
            await _reservationRepository.UpdateReservationAsync(reservation);

            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message); // Return Not Found if reservation doesn't exist
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message); // Return Bad Request if validation fails
        }
        catch (InternalServerException ex)
        {
            return StatusCode(500, ex.Message); // Return internal server error for unexpected issues
        }
    }


    // DELETE: api/Reservations/{reservationId}
    [HttpDelete("{reservationId}")]
    [Authorize(Roles = "User, Admin")] // Allow Users to delete their own reservations and Admin to delete any reservation
    public async Task<IActionResult> DeleteReservation(int reservationId)
    {
        try
        {
            // Extract userId from the JWT token
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId");
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int loggedInUserId = int.Parse(userIdClaim.Value);

            // Fetch the existing reservation to verify its existence
            var reservation = await _reservationRepository.GetReservationByIdAsync(reservationId);
            if (reservation == null)
            {
                return NotFound("Reservation not found.");
            }

            // Check if the logged-in user is deleting their own reservation or if they are an admin
            if (User.IsInRole("User") && reservation.UserId != loggedInUserId)
            {
                return Forbid("You can only delete your own reservations.");
            }

            // Delete the reservation
            await _reservationRepository.DeleteReservationAsync(reservationId);

            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InternalServerException ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    //get by reser id
    [HttpGet("{reservationId}")]
    [Authorize(Roles = "User, Admin")] // Allow both User and Admin to get specific reservation details
    public async Task<IActionResult> GetReservationById(int reservationId)
    {
        try
        {
            var reservation = await _reservationRepository.GetReservationByIdAsync(reservationId);
            if (reservation == null)
            {
                throw new NotFoundException("Reservation not found");
            }
            var reservationDTO = _mapper.Map<ReservationDTO>(reservation); // Map to DTO
            return Ok(reservationDTO);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InternalServerException ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


    // GET: api/Reservations/user/{userId}
    [HttpGet("user/reservations")]
    [Authorize(Roles = "User")] // Only Users can access
    public async Task<IActionResult> GetUserReservations()
    {
        try
        {
            // Extract userId from the JWT token
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId");
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);

            // Fetch reservations for the logged-in user
            var reservations = await _reservationRepository.GetReservationsByUserIdAsync(userId);
            if (!reservations.Any())
            {
                throw new NotFoundException("No reservations found for this user.");
            }

            // Map to DTO
            var reservationDTOs = _mapper.Map<IEnumerable<ReservationDTO>>(reservations);
            return Ok(reservationDTOs);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // GET: api/Reservations/car/{carId}
    [HttpGet("car/{carId}")]
    [Authorize(Roles = "Host, Admin")] // Allow both User and Admin to view reservations by car
    public async Task<IActionResult> GetReservationsByCarId(int carId)
    {
        try
        {
            var reservations = await _reservationRepository.GetReservationsByCarIdAsync(carId);
            var reservationDTOs = _mapper.Map<IEnumerable<ReservationDTO>>(reservations); // Map to DTO
            return Ok(reservationDTOs);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InternalServerException ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
