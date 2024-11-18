using Microsoft.AspNetCore.Mvc;
using CarRental.Models.DTOs; // Adjust namespace as needed
using AutoMapper;
using CarRental.Exceptions; // Replace with the namespace where your custom exceptions reside
using CarRental.Models;
using CarRental.Repository;
using Microsoft.AspNetCore.Authorization;

namespace CarRental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenService _jwtTokenService;

        public AuthController(IMapper mapper, IUserRepository userRepository, JwtTokenService jwtTokenService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        // POST: api/Auth/register
        [HttpPost("register")]
        //Authorize(Roles = "Admin")] // Only Admins can register new users
        public async Task<IActionResult> Register([FromBody] UserRegistrationDTO userRegistrationDTO)
        {
            try
            {
                // Map DTO to User entity
                var user = _mapper.Map<User>(userRegistrationDTO);

                // Add user to the repository
                await _userRepository.AddUserAsync(user);

                // Map the created user to a DTO for the response
                var createdUserDTO = _mapper.Map<UserDTO>(user);

                // Return 201 Created with the new user details
                return CreatedAtAction(nameof(Register), new { userId = user.UserId }, createdUserDTO);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DuplicateResourceException ex)
            {
                return Conflict(ex.Message);
            }
            catch (InternalServerException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO request)
        {
            try
            {
                // Validate the user's credentials (use repository or service layer)
                var user = await _userRepository.ValidateUserAsync(request.Email, request.Password);

                if (user == null)
                {
                    return Unauthorized("Invalid credentials.");
                }

                // Generate a JWT token
                var token = _jwtTokenService.GenerateToken(user.Email, user.Role);

                // Return the token in the response DTO
                var response = new UserLoginResponseDTO
                {
                    Token = token
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
