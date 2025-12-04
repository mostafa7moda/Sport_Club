using System.Threading.Tasks;

namespace Sport_Club.Interfaces
{
    public interface IUnitOfWork
    {
        ITrainerRepository Trainers { get; }

        Task<int> SaveAsync();
    }
}
