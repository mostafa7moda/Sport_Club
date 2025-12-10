using Sport_Club.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sport_Club.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<Attendance>> GetAllAsync();
        Task<Attendance?> GetByIdAsync(int id);
        Task<IEnumerable<Attendance>> GetByMemberIdAsync(int memberId);
        Task<IEnumerable<Attendance>> GetBySectionIdAsync(int sectionId);
        Task AddAsync(Attendance attendance);
        void Delete(Attendance attendance);
        // Usually attendance is not updated, only logged, but for CRUD completeness:
        void Update(Attendance attendance);
    }
}
