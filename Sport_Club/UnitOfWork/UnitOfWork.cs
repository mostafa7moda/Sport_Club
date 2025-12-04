using Sport_Club.Data;
using Sport_Club.Interfaces;

namespace Sport_Club.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public ITrainerRepository Trainers { get; }

        public UnitOfWork(AppDbContext context, ITrainerRepository trainerRepository)
        {
            _context = context;
            Trainers = trainerRepository;
        }

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
