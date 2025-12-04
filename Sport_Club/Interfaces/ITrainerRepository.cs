using Sport_Club.Models;

namespace Sport_Club.Interfaces
{
    public interface ITrainerRepository : IGenericRepository<Trainer>
    {
        Task<IEnumerable<Trainer>> GetTopExperiencedAsync(int years);
    }
}
