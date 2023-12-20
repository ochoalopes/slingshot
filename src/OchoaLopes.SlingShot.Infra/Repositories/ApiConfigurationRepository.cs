using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OchoaLopes.SlingShot.Domain.Entities;
using OchoaLopes.SlingShot.Domain.Interfaces.Repositories;

namespace OchoaLopes.SlingShot.Infra.Repositories
{
    public class ApiConfigurationRepository : Repository<ApiConfigurationEntity>, IApiConfigurationRepository
    {
        public ApiConfigurationRepository(ILogger<IRepository<ApiConfigurationEntity>> logger, DbContext context) : base(logger, context)
        {
        }
    }
}
