using Sport_Club.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sport_Club.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<Subscription>> GetAllAsync();
        Task<Subscription?> GetByIdAsync(int id);
        Task<IEnumerable<Subscription>> GetByMemberSectionIdAsync(int memberSectionId);
        Task AddAsync(Subscription subscription);
        void Update(Subscription subscription);
        void Delete(Subscription subscription);
    }
}
