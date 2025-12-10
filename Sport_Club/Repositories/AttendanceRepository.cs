using Microsoft.EntityFrameworkCore;
using Sport_Club.Data;
using Sport_Club.Interfaces;
using Sport_Club.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_Club.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AppDbContext _context;

        public AttendanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Attendance>> GetAllAsync()
        {
            return await _context.Attendances
                .Include(a => a.Member)
                .ThenInclude(m => m.User)
                .Include(a => a.Section)
                .ToListAsync();
        }

        public async Task<Attendance?> GetByIdAsync(int id)
        {
            return await _context.Attendances
                .Include(a => a.Member)
                .ThenInclude(m => m.User)
                .Include(a => a.Section)
                .FirstOrDefaultAsync(a => a.ID == id);
        }

        public async Task<IEnumerable<Attendance>> GetByMemberIdAsync(int memberId)
        {
             return await _context.Attendances
                .Where(a => a.MemberId == memberId)
                .Include(a => a.Section)
                .ToListAsync();
        }

        public async Task<IEnumerable<Attendance>> GetBySectionIdAsync(int sectionId)
        {
             return await _context.Attendances
                .Where(a => a.SectionId == sectionId)
                .Include(a => a.Member)
                    .ThenInclude(m => m.User)
                .ToListAsync();
        }

        public async Task AddAsync(Attendance attendance)
        {
            await _context.Attendances.AddAsync(attendance);
        }

         public void Update(Attendance attendance)
        {
            _context.Attendances.Update(attendance);
        }

        public void Delete(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
        }
    }
}
