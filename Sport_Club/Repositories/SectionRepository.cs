using Microsoft.EntityFrameworkCore;
using Sport_Club.Data;
using Sport_Club.Interfaces;
using Sport_Club.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sport_Club.Repositories
{
    public class SectionRepository : ISectionRepository
    {
        private readonly AppDbContext _context;

        public SectionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Section>> GetAllAsync()
        {
            return await _context.Sections
                .Include(s => s.Manager)
                .ToListAsync();
        }

        public async Task<Section?> GetByIdAsync(int id)
        {
            return await _context.Sections
                .Include(s => s.Manager)
                .FirstOrDefaultAsync(s => s.ID == id);
        }

        public async Task AddAsync(Section section)
        {
            await _context.Sections.AddAsync(section);
        }

        public void Update(Section section)
        {
            _context.Sections.Update(section);
        }

        public void Delete(Section section)
        {
            _context.Sections.Remove(section);
        }
    }
}
