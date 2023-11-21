using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using OchoaLopes.SlingShot.Domain.Entities;
using OchoaLopes.SlingShot.Domain.Interfaces.Repositories;
using OchoaLopes.SlingShot.Infra.Context;
using OchoaLopes.SlingShot.Infra.Repositories;
using OchoaLopes.SlingShot.Tests.Helpers;

namespace OchoaLopes.SlingShot.Tests.Infra.Repositories
{
    [TestFixture]
    public abstract class KafkaConfigurationRepositoryTests
    {
        private Mock<ILogger<IRepository<KafkaConfigurationEntity>>> _loggerMock;
        private IKafkaConfigurationRepository _sut;

        [SetUp]
        public virtual void Setup()
        {
            var contextOptions = DbContextHelper.CreateNewContextOptions();

            _loggerMock = new Mock<ILogger<IRepository<KafkaConfigurationEntity>>>();
            _sut = new KafkaConfigurationRepository(_loggerMock.Object, new SlingShotContext(contextOptions));
        }

        [TestFixture]
        public class When_AddAsync_Is_Called : KafkaConfigurationRepositoryTests
        {
            [SetUp]
            public override void Setup()
            {
                base.Setup();
            }

            [Test]
            public async Task AddKafkaConfigurationAsync_SavesToDatabase()
            {
                // Arrange
                var entity = new KafkaConfigurationEntity
                {
                    Id = Guid.NewGuid(),
                    BootstrapServers = "localhost:9092",
                    Topic = "test-topic",
                    GroupId = "test-group",
                    AutoOffsetReset = "earliest",
                    EnableAutoCommit = true
                };

                // Act
                await _sut.AddAsync(entity);

                // Assert
                var result = await _sut.GetByIdAsync(entity.Id);
                result.Should().BeEquivalentTo(entity);
            }
        }
    }
}
