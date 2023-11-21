using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace OchoaLopes.SlingShot.Infra.UoW
{
    public abstract class UnitOfWork
    {
        #region Private Properties
        private bool _disposed = false;
        private readonly ILogger<UnitOfWork> _logger;
        private readonly DbContext _context;
        #endregion

        #region Public Methods
        public UnitOfWork(ILogger<UnitOfWork> logger, DbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IDbContextTransaction CreateTransaction()
        {
            _logger.BeginScope("Creating transaction");
            return _context.Database.BeginTransaction();
        }

        public async Task CommitAsync(IDbContextTransaction dbContextTransaction)
        {
            try
            {
                _logger.LogInformation("Commiting transaction");
                await dbContextTransaction.CommitAsync();
            }
            catch
            {
                await RollbackAsync(dbContextTransaction);
                throw;
            }
        }

        public async Task RollbackAsync(IDbContextTransaction dbContextTransaction)
        {
            _logger.LogInformation("Rolling back transaction");
            await dbContextTransaction.RollbackAsync();
        }

        public async Task SaveAsync()
        {
            _logger.LogInformation("Saving changes to database");
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Protected Methods
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
        #endregion
    }
}
