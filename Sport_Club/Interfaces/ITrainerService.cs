using Sport_Club.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sport_Club.Interfaces
{
    public interface ITrainerService
    {
        Task<TrainerGetDto> CreateAsync(TrainerCreateDto dto);
        Task<IEnumerable<TrainerGetDto>> GetAllAsync();
        Task<TrainerGetDto> GetByIdAsync(int id);
        Task UpdateAsync(int id, TrainerUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
