using System.Linq.Expressions;

namespace Sport_Club.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> criteria);
    }
}
