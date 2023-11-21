using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OchoaLopes.SlingShot.Domain.Entities;
using OchoaLopes.SlingShot.Domain.Interfaces.Repositories;

namespace OchoaLopes.SlingShot.Infra.Repositories
{
    public class StorageConfigurationRepository : Repository<StorageConfigurationEntity>, IStorageConfigurationRepository
    {
        public StorageConfigurationRepository(ILogger<IRepository<StorageConfigurationEntity>> logger, DbContext context) : base(logger, context)
        {
        }
    }
}
