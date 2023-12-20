using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OchoaLopes.SlingShot.Domain.Entities;
using OchoaLopes.SlingShot.Domain.Interfaces.Repositories;

namespace OchoaLopes.SlingShot.Infra.Repositories
{
    public class NodeRepository : Repository<NodeEntity>, INodeRepository
    {
        public NodeRepository(ILogger<IRepository<NodeEntity>> logger, DbContext context) : base(logger, context)
        {
        }
    }
}
