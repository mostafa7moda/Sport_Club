using Microsoft.EntityFrameworkCore;
using Sport_Club.Data;
using Sport_Club.Interfaces;
using Sport_Club.Models;

namespace Sport_Club.Repositories
{
    public class TrainerRepository : GenericRepository<Trainer>, ITrainerRepository
    {
        public TrainerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Trainer>> GetTopExperiencedAsync(int years)
        {
            return await _dbSet
                .Where(t => t.ExperienceYears >= years)
                .ToListAsync();
        }
    }
}
