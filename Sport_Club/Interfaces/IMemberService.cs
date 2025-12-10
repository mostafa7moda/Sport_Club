using Sport_Club.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sport_Club.Interfaces
{
    public interface IMemberService
    {
        Task<MemberGetDto> CreateAsync(MemberCreateDto dto);
        Task<IEnumerable<MemberGetDto>> GetAllAsync();
        Task<MemberGetDto> GetByIdAsync(int id);
        Task UpdateAsync(int id, MemberUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
