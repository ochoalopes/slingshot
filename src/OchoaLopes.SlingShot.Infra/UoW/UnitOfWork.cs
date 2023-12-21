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

        private IDbContextTransaction? _dbContextTransaction;
        #endregion

        #region Public Methods
        public UnitOfWork(ILogger<UnitOfWork> logger, DbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void CreateTransaction()
        {
            _logger.BeginScope("Creating transaction");

            _dbContextTransaction = _context.Database.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            try
            {
                _logger.LogInformation("Commiting transaction");
                await _dbContextTransaction.CommitAsync();
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
        }

        public async Task RollbackAsync()
        {
            _logger.LogInformation("Rolling back transaction");

            await _dbContextTransaction.RollbackAsync();
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
