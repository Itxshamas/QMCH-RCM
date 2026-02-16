using QMCH.Interfaces.Repositories;
using QMCH.Interfaces.Services;
using QMCH.Models;

namespace QMCH.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<IEnumerable<Staff>> GetAllStaffAsync()
        {
            return await _staffRepository.GetAllAsync();
        }

        public async Task<Staff?> GetStaffByIdAsync(int id)
        {
            return await _staffRepository.GetWithDetailsAsync(id);
        }

        public async Task CreateStaffAsync(Staff staff)
        {
            await _staffRepository.AddAsync(staff);
        }

        public async Task UpdateStaffAsync(Staff staff)
        {
            await _staffRepository.UpdateAsync(staff);
        }

        public async Task DeleteStaffAsync(int id)
        {
            await _staffRepository.DeleteAsync(id);
        }
    }
}
