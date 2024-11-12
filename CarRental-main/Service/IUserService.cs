using System.Collections.Generic;
using System.Threading.Tasks;
using RoadReady.Models;
using RoadReady.Repository;
using RoadReady.DTO;
using RoadReady.Profiles;

namespace RoadReady.Service
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(UserDTO userDto);
        Task<User> AuthenticateUserAsync(string email, string password);
        Task<UserDTO> GetUserProfileAsync(int userId);
        Task<bool> UpdateUserProfileAsync(UserDTO userDto);
        Task<bool> ResetPasswordAsync(string email, string newPassword);
    }

}
