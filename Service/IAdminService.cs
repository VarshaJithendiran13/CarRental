using System.Collections.Generic;
using System.Threading.Tasks;
using RoadReady.Models;
using RoadReady.Repository;
using RoadReady.DTO;
using RoadReady.Profiles;

namespace RoadReady.Service
{
    public interface IAdminService
    {
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int userId);
        Task<bool> UpdateUserRoleAsync(int userId, string role);
        Task<bool> DeleteUserAsync(int userId);
        //Task<AdminReportDTO> GenerateDailyReportAsync();
        Task<List<AdminReportDTO>> GetReportsAsync();
    }

}
