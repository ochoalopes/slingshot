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
    public abstract class ApiConfigurationRepositoryTests
    {
        private Mock<ILogger<IRepository<ApiConfigurationEntity>>> _loggerMock;
        private DbContext _context;
        private IApiConfigurationRepository _sut;

        [SetUp]
        public virtual void Setup()
        {
            var contextOptions = DbContextHelper.CreateNewContextOptions();
            _context = new SlingShotContext(contextOptions);
            _loggerMock = new Mock<ILogger<IRepository<ApiConfigurationEntity>>>();
            _sut = new ApiConfigurationRepository(_loggerMock.Object, _context);
        }

        [TearDown]
        public virtual void TearDown()
        {
            _context.Dispose();
        }

        [TestFixture]
        public class When_Search_ApiConfiguration : ApiConfigurationRepositoryTests
        {
            [SetUp]
            public override void Setup()
            {
                base.Setup();
            }

            [Test]
            public async Task GetApiConfigurationAsync_ReturnsAllEntities()
            {
                // Arrange
                var entity1 = new ApiConfigurationEntity
                (
                    Guid.NewGuid(),
                    "https://localhost:5001",
                    Guid.NewGuid()
                );
                var entity2 = new ApiConfigurationEntity
                (
                    Guid.NewGuid(),
                    "https://localhost:5002",
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
            public async Task GetApiConfigurationAsync_WithFilter_ReturnsFilteredEntities()
            {
                // Arrange
                var entity1 = new ApiConfigurationEntity
                (
                    Guid.NewGuid(),
                    "https://localhost:5001",
                    Guid.NewGuid()
                );
                var entity2 = new ApiConfigurationEntity
                (
                    Guid.NewGuid(),
                    "https://localhost:5002",
                    Guid.NewGuid()
                );
                string baseUrl = "https://localhost:5001";

                await _sut.AddAsync(entity1);
                await _sut.AddAsync(entity2);

                await _context.SaveChangesAsync();

                // Act
                var result = await _sut.Get(x => x.BaseUrl == baseUrl);

                // Assert
                result.Should().HaveCount(1);
            }

            [Test]
            public async Task GetApiConfigurationAsync_WithOrderBy_ReturnsOrderedEntities()
            {
                // Arrange
                var entity1 = new ApiConfigurationEntity
                (
                    Guid.NewGuid(),
                    "https://localhost:5001",
                    Guid.NewGuid()
                );
                var entity2 = new ApiConfigurationEntity
                (
                    Guid.NewGuid(),
                    "https://localhost:5002",
                    Guid.NewGuid()
                );

                await _sut.AddAsync(entity1);
                await _sut.AddAsync(entity2);

                await _context.SaveChangesAsync();

                // Act
                var result = await _sut.Get(orderBy: x => x.OrderBy(y => y.BaseUrl));

                // Assert
                result.Should().HaveCount(2);
                result.First().BaseUrl.Should().Be("https://localhost:5001");
            }

            [Test]
            public async Task GetApiConfigurationAsync_ReturnsEntityById()
            {
                // Arrange
                var entity1 = new ApiConfigurationEntity
                (
                    Guid.NewGuid(),
                    "https://localhost:5001",
                    Guid.NewGuid()
                );
                var entity2 = new ApiConfigurationEntity
                (
                    Guid.NewGuid(),
                    "https://localhost:5002",
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
        public class When_Add_ApiConfiguration : ApiConfigurationRepositoryTests
        {
            [SetUp]
            public override void Setup()
            {
                base.Setup();
            }

            [Test]
            public async Task AddApiConfigurationAsync_SavesToDatabase()
            {
                // Arrange
                var entity = new ApiConfigurationEntity
                (
                    Guid.NewGuid(),
                    "https://localhost:5001",
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
            public void AddApiConfigurationAsync_WithNullEntity_ThrowsValidationException()
            {
                // Arrange
                ApiConfigurationEntity? invalidEntity = null;

                // Act & Assert
                #pragma warning disable CS8604 // Possible null reference argument.
                Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.AddAsync(invalidEntity));
                #pragma warning restore CS8604 // Possible null reference argument.
            }
        }

        [TestFixture]
        public class When_Update_ApiConfiguration : ApiConfigurationRepositoryTests
        {
            [SetUp]
            public override void Setup()
            {
                base.Setup();
            }

            [Test]
            public async Task UpdateApiConfigurationAsync_SavesToDatabase()
            {
                // Arrange
                var entity = new ApiConfigurationEntity
                (
                    Guid.NewGuid(),
                    "https://localhost:5001",
                    Guid.NewGuid()
                );
                string updatedBaseUrl = "https://localhost:5002";

                await _sut.AddAsync(entity);

                await _context.SaveChangesAsync();

                entity.BaseUrl = updatedBaseUrl;

                // Act
                _sut.Update(entity);

                await _context.SaveChangesAsync();

                // Assert
                var result = await _sut.GetByIdAsync(entity.Id);
                result.Should().BeEquivalentTo(entity);
                entity.BaseUrl.Should().Be(updatedBaseUrl);
            }

            [Test]
            public void UpdateApiConfigurationAsync_WithNullEntity_ThrowsValidationException()
            {
                // Arrange
                ApiConfigurationEntity? invalidEntity = null;

                // Act & Assert
                #pragma warning disable CS8604 // Possible null reference argument.
                Assert.Throws<ArgumentNullException>(() => _sut.Update(invalidEntity));
                #pragma warning restore CS8604 // Possible null reference argument.
            }
        }

        [TestFixture]
        public class When_Delete_ApiConfiguration : ApiConfigurationRepositoryTests
        {
            [SetUp]
            public override void Setup()
            {
                base.Setup();
            }

            [Test]
            public async Task DeleteApiConfigurationAsync_RemovesFromDatabase()
            {
                // Arrange
                var entity = new ApiConfigurationEntity
                (
                    Guid.NewGuid(),
                    "https://localhost:5001",
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
            public void DeleteApiConfigurationAsync_WithNullEntity_ThrowsValidationException()
            {
                // Arrange
                ApiConfigurationEntity? invalidEntity = null;

                // Act & Assert
                #pragma warning disable CS8604 // Possible null reference argument.
                Assert.Throws<ArgumentNullException>(() => _sut.Delete(invalidEntity));
                #pragma warning restore CS8604 // Possible null reference argument.
            }
        }
    }
}
