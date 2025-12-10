// UnitOfWork.cs
using Microsoft.EntityFrameworkCore.Storage;
using Sport_Club.Data;
using Sport_Club.Interfaces;
using System.Threading.Tasks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction _transaction;
    public ITrainerRepository Trainers { get; }
    public IMemberRepository Members { get; }

    public UnitOfWork(AppDbContext context, ITrainerRepository trainerRepo, IMemberRepository memberRepo)
    {
        _context = context;
        Trainers = trainerRepo;
        Members = memberRepo;
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
