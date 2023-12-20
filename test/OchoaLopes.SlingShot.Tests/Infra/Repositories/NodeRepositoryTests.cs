using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using OchoaLopes.SlingShot.Domain.Interfaces.Repositories;
using OchoaLopes.SlingShot.Infra.Repositories;

namespace OchoaLopes.SlingShot.Tests.Infra.Repositories
{
    [TestFixture]
    public abstract class NodeRepositoryTests
    {
        private Mock<ILogger<INodeRepository>> _logger;
        private Mock<DbContext> _context;
        private NodeRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _logger = new Mock<ILogger<INodeRepository>>();
            _context = new Mock<DbContext>();
            _repository = new NodeRepository(_logger.Object, _context.Object);
        }
    }
}
