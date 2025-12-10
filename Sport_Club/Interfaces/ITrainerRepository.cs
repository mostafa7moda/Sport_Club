using Sport_Club.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sport_Club.Interfaces
{
    public interface ITrainerRepository
    {
        Task<IEnumerable<Trainer>> GetAllAsync();
        Task<Trainer?> GetByIdAsync(int id);
        Task<bool> ExistsByUserIdAsync(string userId);
        Task AddAsync(Trainer trainer);
        void Update(Trainer trainer);
        void Delete(Trainer trainer);
        Task<IEnumerable<Trainer>> GetTopExperiencedAsync(int years);
    }
}
