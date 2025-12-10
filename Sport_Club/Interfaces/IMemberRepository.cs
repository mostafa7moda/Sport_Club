using Sport_Club.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sport_Club.Interfaces
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetAllAsync();
        Task<Member> GetByIdAsync(int id);
        Task AddAsync(Member member);
        void Update(Member member);
        void Delete(Member member);
    }
}
