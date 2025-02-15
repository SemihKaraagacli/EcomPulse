using System.Linq.Expressions;

namespace DataAccessLayer.GenericRepository;

public interface IGenericRepository<T>
{
    void Create(T Entity);
    void Update(T Entity);
    void Delete(T Entity);
    Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate);

}
