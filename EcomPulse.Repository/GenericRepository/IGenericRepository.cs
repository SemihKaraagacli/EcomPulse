using System.Linq.Expressions;

namespace EcomPulse.Repository.GenericRepository
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetByIdAsync(Guid Id);
        void CreateAsync(T Entity);
        void UpdateAsync(T Entity);
        void DeleteAsync(T Entity);
        Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate);

    }
}
