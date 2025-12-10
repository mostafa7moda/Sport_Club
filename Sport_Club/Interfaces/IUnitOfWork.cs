using Sport_Club.Interfaces;


namespace Sport_Club.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITrainerRepository Trainers { get; }
        IMemberRepository Members { get; }
        ISectionRepository Sections { get; }
        ISubscriptionRepository Subscriptions { get; }
        IAttendanceRepository Attendances { get; }

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();

        Task<int> SaveChangesAsync();
    }
}

