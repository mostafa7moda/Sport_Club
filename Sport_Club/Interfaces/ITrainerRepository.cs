// ITrainerRepository.cs
using Sport_Club.Models;
using System.Threading.Tasks;

namespace Sport_Club.Interfaces
{
    public interface ITrainerRepository
    {
        Task AddAsync(Trainer trainer);
        // أضف هنا أي طرق أخرى تحتاجها
    }
}

