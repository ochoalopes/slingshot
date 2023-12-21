using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
        private Mock<ILogger<IKafkaConfigurationRepository>> _loggerMock;
        private DbContext _context;
        private IKafkaConfigurationRepository _sut;

        [SetUp]
        public virtual void SetUp()
        {
            var contextOptions = DbContextHelper.CreateNewContextOptions();
            _context = new SlingShotContext(contextOptions);
            _loggerMock = new Mock<ILogger<IKafkaConfigurationRepository>>();
            _sut = new KafkaConfigurationRepository(_loggerMock.Object, _context);
        }

        [TearDown]
        public virtual void TearDown()
        {
            _context.Dispose();
        }

        [TestFixture]
        public class When_Search_KafkaConfiguration : KafkaConfigurationRepositoryTests
        {
            [SetUp]
            public override void SetUp()
            {
                base.SetUp();
            }

            [Test]
            public async Task GetAllAsync_ReturnsAllEntities()
            {
                // Arrange
                var entity1 = new KafkaConfigurationEntity
                (
                    Guid.NewGuid(),
                    "localhost:9092",
                    "test-topic",
                    "test-group",
                    Guid.NewGuid()
                );
                var entity2 = new KafkaConfigurationEntity
                (
                    Guid.NewGuid(),
                    "localhost:9093",
                    "test-topic",
                    "test-group",
                    Guid.NewGuid()
                );

                await _sut.AddAsync(entity1);
                await _sut.AddAsync(entity2);

                await _context.SaveChangesAsync();

                // Act
                var result = await _sut.GetAllAsync();

                // Assert
                result.Should().HaveCount(2);
            }

            [Test]
            public async Task Get_WithFilter_ReturnsFilteredEntities()
            {
                // Arrange
                var entity1 = new KafkaConfigurationEntity
                (
                    Guid.NewGuid(),
                    "localhost:9092",
                    "test-topic",
                    "test-group",
                    Guid.NewGuid()
                );
                var entity2 = new KafkaConfigurationEntity
                (
                    Guid.NewGuid(),
                    "localhost:9093",
                    "test-topic",
                    "test-group",
                    Guid.NewGuid()
                );
                string bootstrapServers = "localhost:9092";

                await _sut.AddAsync(entity1);
                await _sut.AddAsync(entity2);

                await _context.SaveChangesAsync();

                // Act
                var result = await _sut.Get(x => x.BootstrapServers == bootstrapServers);

                // Assert
                result.Should().HaveCount(1);
            }

            [Test]
            public async Task Get_WithOrderBy_ReturnsOrderedEntities()
            {
                // Arrange
                var entity1 = new KafkaConfigurationEntity
                (
                    Guid.NewGuid(),
                    "localhost:9092",
                    "test-topic",
                    "test-group",
                    Guid.NewGuid()
                );
                var entity2 = new KafkaConfigurationEntity
                (
                    Guid.NewGuid(),
                    "localhost:9093",
                    "test-topic",
                    "test-group",
                    Guid.NewGuid()
                );

                await _sut.AddAsync(entity1);
                await _sut.AddAsync(entity2);

                await _context.SaveChangesAsync();

                // Act
                var result = await _sut.Get(orderBy: x => x.OrderBy(y => y.BootstrapServers));

                // Assert
                result.Should().HaveCount(2);
                result.First().BootstrapServers.Should().Be("localhost:9092");
            }

            [Test]
            public async Task GetByIdAsync_ReturnsEntityById()
            {
                // Arrange
                var entity1 = new KafkaConfigurationEntity
                (
                    Guid.NewGuid(),
                    "localhost:9092",
                    "test-topic",
                    "test-group",
                    Guid.NewGuid()
                );
                var entity2 = new KafkaConfigurationEntity
                (
                    Guid.NewGuid(),
                    "localhost:9093",
                    "test-topic",
                    "test-group",
                    Guid.NewGuid()
                );

                await _sut.AddAsync(entity1);
                await _sut.AddAsync(entity2);

                await _context.SaveChangesAsync();

                // Act
                var result = await _sut.GetByIdAsync(entity1.Id);

                // Assert
                result.Should().BeEquivalentTo(entity1);
            }
        }

        [TestFixture]
        public class When_Add_KafkaConfiguration : KafkaConfigurationRepositoryTests
        {
            [SetUp]
            public override void SetUp()
            {
                base.SetUp();
            }

            [Test]
            public async Task AddAsync_SavesToDatabase()
            {
                // Arrange
                var entity = new KafkaConfigurationEntity
                (
                    Guid.NewGuid(),
                    "localhost:9092",
                    "test-topic",
                    "test-group",
                    Guid.NewGuid()
                );

                // Act
                await _sut.AddAsync(entity);
                await _context.SaveChangesAsync();

                // Assert
                var result = await _sut.GetByIdAsync(entity.Id);
                result.Should().BeEquivalentTo(entity);
            }

            [Test]
            public void AddAsync_WithNullEntity_ThrowsValidationException()
            {
                // Arrange
                KafkaConfigurationEntity? invalidEntity = null;

                // Act & Assert
                #pragma warning disable CS8604 // Possible null reference argument.
                Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.AddAsync(invalidEntity));
                #pragma warning restore CS8604 // Possible null reference argument.
            }
        }

        [TestFixture]
        public class When_Update_KafkaConfiguration : KafkaConfigurationRepositoryTests
        {
            [SetUp]
            public override void SetUp()
            {
                base.SetUp();
            }

            [Test]
            public async Task Update_SavesToDatabase()
            {
                // Arrange
                var entity = new KafkaConfigurationEntity
                (
                    Guid.NewGuid(),
                    "localhost:9092",
                    "test-topic",
                    "test-group",
                    Guid.NewGuid()
                );
                string updatedBootstrapServers = "localhost:9093";

                await _sut.AddAsync(entity);

                await _context.SaveChangesAsync();

                entity.BootstrapServers = updatedBootstrapServers;

                // Act
                _sut.Update(entity);

                await _context.SaveChangesAsync();

                // Assert
                var result = await _sut.GetByIdAsync(entity.Id);
                result.Should().BeEquivalentTo(entity);
                entity.BootstrapServers.Should().Be(updatedBootstrapServers);
            }

            [Test]
            public void Update_WithNullEntity_ThrowsValidationException()
            {
                // Arrange
                KafkaConfigurationEntity? invalidEntity = null;

                // Act & Assert
                #pragma warning disable CS8604 // Possible null reference argument.
                Assert.Throws<ArgumentNullException>(() => _sut.Update(invalidEntity));
                #pragma warning restore CS8604 // Possible null reference argument.
            }
        }

        [TestFixture]
        public class When_Delete_KafkaConfiguration : KafkaConfigurationRepositoryTests
        {
            [SetUp]
            public override void SetUp()
            {
                base.SetUp();
            }

            [Test]
            public async Task Delete_RemovesFromDatabase()
            {
                // Arrange
                var entity = new KafkaConfigurationEntity
                (
                    Guid.NewGuid(),
                    "localhost:9092",
                    "test-topic",
                    "test-group",
                    Guid.NewGuid()
                );

                await _sut.AddAsync(entity);

                // Act
                _sut.Delete(entity);

                await _context.SaveChangesAsync();

                // Assert
                var result = await _sut.GetByIdAsync(entity.Id);
                result.Should().BeNull();
            }

            [Test]
            public void Delete_WithNullEntity_ThrowsValidationException()
            {
                // Arrange
                KafkaConfigurationEntity? invalidEntity = null;

                // Act & Assert
                #pragma warning disable CS8604 // Possible null reference argument.
                Assert.Throws<ArgumentNullException>(() => _sut.Delete(invalidEntity));
                #pragma warning restore CS8604 // Possible null reference argument.
            }
        }
    }
}
