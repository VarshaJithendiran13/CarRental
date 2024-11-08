using AutoMapper;
using RoadReady.DTO;
using RoadReady.Models;
using System.Collections.Generic;
using RoadReady.Repository;
using System.Threading.Tasks;

namespace RoadReady.Service
{
    public class AdminService : IAdminService
    {
        private readonly IMapper _mapper;
        private readonly IAdminRepository _adminRepository;

        public AdminService(IMapper mapper, IAdminRepository adminRepository)
        {
            _mapper = mapper;
            _adminRepository = adminRepository;
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var users = await _adminRepository.GetAllUsersAsync();
            return _mapper.Map<List<UserDTO>>(users); // Map users to DTOs
        }

        public async Task<UserDTO> GetUserByIdAsync(int userId)
        {
            var user = await _adminRepository.GetUserByIdAsync(userId);
            return _mapper.Map<UserDTO>(user); // Map user to DTO
        }

        public async Task<bool> UpdateUserRoleAsync(int userId, string role)
        {
            return await _adminRepository.UpdateUserRoleAsync(userId, role); // Update user role
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            return await _adminRepository.DeleteUserAsync(userId); // Delete user from DB
        }

        //public async Task<AdminReportDTO> GenerateDailyReportAsync()
        //{
        //    var report = await _adminRepository.GenerateDailyReportAsync();
        //    return _mapper.Map<AdminReportDTO>(report); // Generate daily report
        //}

        public async Task<List<AdminReportDTO>> GetReportsAsync()
        {
            var reports = await _adminRepository.GetReportsAsync();
            return _mapper.Map<List<AdminReportDTO>>(reports); // Map reports to DTOs
        }
    }

}
