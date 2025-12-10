using Sport_Club.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sport_Club.Interfaces
{
    public interface ISectionRepository
    {
        Task<IEnumerable<Section>> GetAllAsync();
        Task<Section?> GetByIdAsync(int id);
        Task AddAsync(Section section);
        void Update(Section section);
        void Delete(Section section);
    }
}
