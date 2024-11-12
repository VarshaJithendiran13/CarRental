using Microsoft.AspNetCore.Identity;
using RoadReady.Models;
using RoadReady.Repository;
using RoadReady.DTO;
using RoadReady.Profiles;
using AutoMapper;

namespace RoadReady.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository; // Assuming a repository to interact with DB

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterUserAsync(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            return await _userRepository.AddAsync(user); // Add user to DB
        }

        public async Task<User> AuthenticateUserAsync(string email, string password)
        {
            return await _userRepository.AuthenticateAsync(email, password); // Auth logic (e.g., compare hashed password)
        }

        public async Task<UserDTO> GetUserProfileAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return _mapper.Map<UserDTO>(user); // Map user to DTO
        }

        public async Task<bool> UpdateUserProfileAsync(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            return await _userRepository.UpdateAsync(user); // Update user in DB
        }

        public async Task<bool> ResetPasswordAsync(string email, string newPassword)
        {
            return await _userRepository.ResetPasswordAsync(email, newPassword); // Reset password logic
        }
    }


}
