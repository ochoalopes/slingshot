using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OchoaLopes.SlingShot.Domain.Entities;
using OchoaLopes.SlingShot.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace OchoaLopes.SlingShot.Infra.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        #region Properties
        private readonly ILogger<IRepository<TEntity>> _logger;

        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        #endregion

        #region Public Methods
        public Repository(ILogger<IRepository<TEntity>> logger, DbContext context)
        {
            _logger = logger;
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _logger.LogInformation($"Adding {entity.GetType().Name} to database");

            await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _logger.LogInformation($"Updating {entity.GetType().Name} in database");

            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _logger.LogInformation($"Deleting {entity.GetType().Name} from database");

            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }
        #endregion
    }
}
