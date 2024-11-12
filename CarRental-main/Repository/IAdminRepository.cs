using RoadReady.Models;
using System.Threading.Tasks;

namespace RoadReady.Repository
{

    public interface IAdminRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task<bool> UpdateUserRoleAsync(int userId, string role);
        Task<bool> DeleteUserAsync(int userId);
        //Task<AdminReport> GenerateDailyReportAsync();
        Task<List<AdminReport>> GetReportsAsync();
    }

}
