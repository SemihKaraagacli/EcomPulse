namespace BusinessLogicLayer.UnitOfWork;

public interface IUnitOfWork
{
    Task<int> CommitAsync();
}
