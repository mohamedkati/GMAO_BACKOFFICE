namespace GMAO.Application.Common.Interfaces.Services
{
    public interface IUnitOfWork
    {
        Task StartTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task<int> SaveAllAsync(CancellationToken cancellationToken = default!);
    }
}
