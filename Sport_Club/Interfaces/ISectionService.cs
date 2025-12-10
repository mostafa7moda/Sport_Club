using Sport_Club.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sport_Club.Interfaces
{
    public interface ISectionService
    {
        Task<SectionGetDto> CreateAsync(SectionCreateDto dto);
        Task<IEnumerable<SectionGetDto>> GetAllAsync();
        Task<SectionGetDto> GetByIdAsync(int id);
        Task UpdateAsync(int id, SectionUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
