using Sport_Club.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sport_Club.Interfaces
{
    public interface IAttendanceService
    {
        Task<AttendanceGetDto> LogAsync(AttendanceLogDto dto);
        Task<IEnumerable<AttendanceGetDto>> GetAllAsync();
        Task<IEnumerable<AttendanceGetDto>> GetByMemberIdAsync(int memberId);
        Task<IEnumerable<AttendanceGetDto>> GetBySectionIdAsync(int sectionId);
        Task DeleteAsync(int id);
    }
}
