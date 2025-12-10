using Microsoft.EntityFrameworkCore;
using Sport_Club.Data;
using Sport_Club.Interfaces;
using Sport_Club.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_Club.Repositories
{
    public class TrainerRepository : ITrainerRepository // Simplified inheritance for clarity, or keep Generic if preferred but implementing specific interface methods is safer for Clean Arch
    {
        private readonly AppDbContext _context;

        public TrainerRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Trainer>> GetAllAsync()
        {
            return await _context.Trainers
                .Include(t => t.User)
                .Include(t => t.Section)
                .ToListAsync();
        }

        public async Task<Trainer?> GetByIdAsync(int id)
        {
            return await _context.Trainers
                .Include(t => t.User)
                .Include(t => t.Section)
                .FirstOrDefaultAsync(t => t.ID == id);
        }

        public async Task<bool> ExistsByUserIdAsync(string userId)
        {
            return await _context.Trainers.AnyAsync(t => t.UserId == userId);
        }

        public async Task AddAsync(Trainer trainer)
        {
            await _context.Trainers.AddAsync(trainer);
        }

        public void Update(Trainer trainer)
        {
            _context.Trainers.Update(trainer);
        }

        public void Delete(Trainer trainer)
        {
            _context.Trainers.Remove(trainer);
        }

        public async Task<IEnumerable<Trainer>> GetTopExperiencedAsync(int years)
        {
            return await _context.Trainers
                .Where(t => t.ExperienceYears >= years)
                .Include(t => t.User)
                .ToListAsync();
        }
    }
}
