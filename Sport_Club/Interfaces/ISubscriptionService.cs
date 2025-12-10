using Sport_Club.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sport_Club.Interfaces
{
    public interface ISubscriptionService
    {
        Task<SubscriptionGetDto> CreateAsync(SubscriptionCreateDto dto);
        Task<IEnumerable<SubscriptionGetDto>> GetAllAsync();
        Task<SubscriptionGetDto> GetByIdAsync(int id);
        Task UpdateAsync(int id, SubscriptionUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
