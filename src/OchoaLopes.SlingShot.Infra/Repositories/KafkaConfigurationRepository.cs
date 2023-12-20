using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OchoaLopes.SlingShot.Domain.Entities;
using OchoaLopes.SlingShot.Domain.Interfaces.Repositories;

namespace OchoaLopes.SlingShot.Infra.Repositories
{
    public class KafkaConfigurationRepository : Repository<KafkaConfigurationEntity>, IKafkaConfigurationRepository
    {
        public KafkaConfigurationRepository(ILogger<IRepository<KafkaConfigurationEntity>> logger, DbContext context) : base(logger, context)
        {
        }
    }
}
