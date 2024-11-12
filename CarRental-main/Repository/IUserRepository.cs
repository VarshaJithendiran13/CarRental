using RoadReady.Models;

namespace RoadReady.Repository
{
    public interface IUserRepository
    {
        Task<bool> AddAsync(User user);
        Task<User> AuthenticateAsync(string email, string password);
        Task<User> GetByIdAsync(int userId);
        Task<bool> UpdateAsync(User user);
        Task<bool> ResetPasswordAsync(string email, string newPassword);
    }

}
