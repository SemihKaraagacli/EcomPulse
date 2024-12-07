using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EcomPulse.Repository.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void CreateAsync(T Entity)
        {
            _dbSet.Add(Entity);
        }

        public void DeleteAsync(T Entity)
        {
            _dbSet.Remove(Entity);
        }

        public async void UpdateAsync(T Entity)
        {
            _dbSet.Update(Entity);
        }

        public async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
    }
}
