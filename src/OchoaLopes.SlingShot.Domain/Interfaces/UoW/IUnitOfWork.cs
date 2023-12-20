using Microsoft.EntityFrameworkCore.Storage;

namespace OchoaLopes.SlingShot.Domain.Interfaces.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IDbContextTransaction CreateTransaction();
        Task CommitAsync(IDbContextTransaction dbContextTransaction);
        Task RollbackAsync(IDbContextTransaction dbContextTransaction);
        Task SaveAsync();
    }
}
