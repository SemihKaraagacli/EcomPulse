
using EcomPulse.Repository;

namespace EcomPulse.Service.UnitOfWork
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public async Task<int> CommitAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
