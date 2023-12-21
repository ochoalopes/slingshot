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
    public class StorageConfigurationRepositoryTests
    {
        private Mock<ILogger<IStorageConfigurationRepository>> _loggerMock;
        private DbContext _context;
        private IStorageConfigurationRepository _sut;

        [SetUp]
        public virtual void SetUp()
        {
            var contextOptions = DbContextHelper.CreateNewContextOptions();
            _context = new SlingShotContext(contextOptions);
            _loggerMock = new Mock<ILogger<IStorageConfigurationRepository>>();
            _sut = new StorageConfigurationRepository(_loggerMock.Object, _context);
        }

        [TearDown]
        public virtual void TearDown()
        {
            _context.Dispose();
        }

        [TestFixture]
        public class When_Search_StorageConfiguration : StorageConfigurationRepositoryTests
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
                var entity1 = new StorageConfigurationEntity
                (
                    Guid.NewGuid(),
                    "connectionString1",
                    "containerName1",
                    "accessKey",
                    "secretKey",
                    Guid.NewGuid()
                );
                var entity2 = new StorageConfigurationEntity
                (
                    Guid.NewGuid(),
                    "connectionString2",
                    "containerName2",
                    "accessKey",
                    "secretKey",
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
                var entity1 = new StorageConfigurationEntity
                (
                    Guid.NewGuid(),
                    "connectionString1",
                    "containerName1",
                    "accessKey",
                    "secretKey",
                    Guid.NewGuid()
                );
                var entity2 = new StorageConfigurationEntity
                (
                    Guid.NewGuid(),
                    "connectionString2",
                    "containerName2",
                    "accessKey",
                    "secretKey",
                    Guid.NewGuid()
                );
                string containerName = "containerName1";

                await _sut.AddAsync(entity1);
                await _sut.AddAsync(entity2);

                await _context.SaveChangesAsync();

                // Act
                var result = await _sut.Get(x => x.ContainerName == containerName);

                // Assert
                result.Should().HaveCount(1);
            }

            [Test]
            public async Task Get_WithOrderBy_ReturnsOrderedEntities()
            {
                // Arrange
                var entity1 = new StorageConfigurationEntity
                (
                    Guid.NewGuid(),
                    "connectionString1",
                    "containerName1",
                    "accessKey",
                    "secretKey",
                    Guid.NewGuid()
                );
                var entity2 = new StorageConfigurationEntity
                (
                    Guid.NewGuid(),
                    "connectionString2",
                    "containerName2",
                    "accessKey",
                    "secretKey",
                    Guid.NewGuid()
                );

                await _sut.AddAsync(entity1);
                await _sut.AddAsync(entity2);

                await _context.SaveChangesAsync();

                // Act
                var result = await _sut.Get(orderBy: x => x.OrderBy(y => y.ContainerName));

                // Assert
                result.Should().HaveCount(2);
                result.First().ContainerName.Should().Be("containerName1");
            }

            [Test]
            public async Task GetByIdAsync_ReturnsEntityById()
            {
                // Arrange
                var entity1 = new StorageConfigurationEntity
                (
                    Guid.NewGuid(),
                    "connectionString1",
                    "containerName1",
                    "accessKey",
                    "secretKey",
                    Guid.NewGuid()
                );
                var entity2 = new StorageConfigurationEntity
                (
                    Guid.NewGuid(),
                    "connectionString2",
                    "containerName2",
                    "accessKey",
                    "secretKey",
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
        public class When_Add_StorageConfiguration : StorageConfigurationRepositoryTests
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
                var entity = new StorageConfigurationEntity
                (
                    Guid.NewGuid(),
                    "connectionString1",
                    "containerName1",
                    "accessKey",
                    "secretKey",
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
                StorageConfigurationEntity? invalidEntity = null;

                // Act & Assert
                #pragma warning disable CS8604 // Possible null reference argument.
                Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.AddAsync(invalidEntity));
                #pragma warning restore CS8604 // Possible null reference argument.
            }
        }

        [TestFixture]
        public class When_Update_StorageConfiguration : StorageConfigurationRepositoryTests
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
                var entity = new StorageConfigurationEntity
                (
                    Guid.NewGuid(),
                    "connectionString1",
                    "containerName1",
                    "accessKey",
                    "secretKey",
                    Guid.NewGuid()
                );
                string updatedContainerName = "containerNameUpdated";

                await _sut.AddAsync(entity);

                await _context.SaveChangesAsync();

                entity.ContainerName = updatedContainerName;

                // Act
                _sut.Update(entity);

                await _context.SaveChangesAsync();

                // Assert
                var result = await _sut.GetByIdAsync(entity.Id);
                result.Should().BeEquivalentTo(entity);
                entity.ContainerName.Should().Be(updatedContainerName);
            }

            [Test]
            public void Update_WithNullEntity_ThrowsValidationException()
            {
                // Arrange
                StorageConfigurationEntity? invalidEntity = null;

                // Act & Assert
                #pragma warning disable CS8604 // Possible null reference argument.
                Assert.Throws<ArgumentNullException>(() => _sut.Update(invalidEntity));
                #pragma warning restore CS8604 // Possible null reference argument.
            }
        }

        [TestFixture]
        public class When_Delete_StorageConfiguration : StorageConfigurationRepositoryTests
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
                var entity = new StorageConfigurationEntity
                (
                    Guid.NewGuid(),
                    "connectionString1",
                    "containerName1",
                    "accessKey",
                    "secretKey",
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
                StorageConfigurationEntity? invalidEntity = null;

                // Act & Assert
                #pragma warning disable CS8604 // Possible null reference argument.
                Assert.Throws<ArgumentNullException>(() => _sut.Delete(invalidEntity));
                #pragma warning restore CS8604 // Possible null reference argument.
            }
        }
    }
}
