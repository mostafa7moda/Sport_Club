using Microsoft.EntityFrameworkCore;
using Sport_Club.Data;
using Sport_Club.Interfaces;
using Sport_Club.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sport_Club.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext _context;

        public MemberRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await _context.Members
                .Include(m => m.User)
                .Include(m => m.MemberSections)
                .Include(m => m.TeamMembers)
                .ToListAsync();
        }

        public async Task<Member> GetByIdAsync(int id)
        {
            return await _context.Members
                .Include(m => m.User)
                .Include(m => m.MemberSections)
                .Include(m => m.TeamMembers)
                .FirstOrDefaultAsync(m => m.ID == id);
        }

        public async Task AddAsync(Member member)
        {
            await _context.Members.AddAsync(member);
        }

        public void Update(Member member)
        {
            _context.Members.Update(member);
        }

        public void Delete(Member member)
        {
            _context.Members.Remove(member);
        }
    }
}
