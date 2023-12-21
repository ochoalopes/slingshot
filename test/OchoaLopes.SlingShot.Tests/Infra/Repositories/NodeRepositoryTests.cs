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
    public abstract class NodeRepositoryTests
    {
        private Mock<ILogger<INodeRepository>> _loggerMock;
        private DbContext _context;
        private INodeRepository _sut;

        [SetUp]
        public virtual void SetUp()
        {
            var contextOptions = DbContextHelper.CreateNewContextOptions();
            _context = new SlingShotContext(contextOptions);
            _loggerMock = new Mock<ILogger<INodeRepository>>();
            _sut = new NodeRepository(_loggerMock.Object, _context);
        }

        [TearDown]
        public virtual void TearDown()
        {
            _context.Dispose();
        }

        [TestFixture]
        public class When_Search_Node : NodeRepositoryTests
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
                var entity1 = new NodeEntity
                (
                    Guid.NewGuid(),
                    "Name One"
                );
                var entity2 = new NodeEntity
                (
                    Guid.NewGuid(),
                    "Name Two"
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
            public async Task GetNode_WithFilter_ReturnsFilteredEntities()
            {
                // Arrange
                var entity1 = new NodeEntity
                (
                    Guid.NewGuid(),
                    "Name One"
                );
                var entity2 = new NodeEntity
                (
                    Guid.NewGuid(),
                    "Name Two"
                );

                string name = "Name Two";

                await _sut.AddAsync(entity1);
                await _sut.AddAsync(entity2);

                await _context.SaveChangesAsync();

                // Act
                var result = await _sut.Get(x => x.Name == name);

                // Assert
                result.Should().HaveCount(1);
            }

            [Test]
            public async Task GetNode_WithOrderBy_ReturnsOrderedEntities()
            {
                // Arrange
                var entity1 = new NodeEntity
                (
                    Guid.NewGuid(),
                    "Name One"
                );
                var entity2 = new NodeEntity
                (
                    Guid.NewGuid(),
                    "Name Two"
                );

                await _sut.AddAsync(entity2);
                await _sut.AddAsync(entity1);

                await _context.SaveChangesAsync();

                // Act
                var result = await _sut.Get(orderBy: x => x.OrderBy(y => y.Name));

                // Assert
                result.Should().HaveCount(2);
                result.First().Name.Should().Be("Name One");
            }

            [Test]
            public async Task GetByIdAsync_ReturnsEntityById()
            {
                // Arrange
                var entity1 = new NodeEntity
                (
                    Guid.NewGuid(),
                    "Name One"
                );
                var entity2 = new NodeEntity
                (
                    Guid.NewGuid(),
                    "Name Two"
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
        public class When_Add_Node : NodeRepositoryTests
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
                var entity = new NodeEntity
                (
                    Guid.NewGuid(),
                    "Name One"
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
                NodeEntity? invalidEntity = null;

                // Act & Assert
                #pragma warning disable CS8604 // Possible null reference argument.
                Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.AddAsync(invalidEntity));
                #pragma warning restore CS8604 // Possible null reference argument.
            }
        }

        [TestFixture]
        public class When_Update_Node : NodeRepositoryTests
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
                var entity = new NodeEntity
                (
                    Guid.NewGuid(),
                    "Name One"
                );
                string updatedName = "Name One Updated";

                await _sut.AddAsync(entity);

                await _context.SaveChangesAsync();

                entity.Name = updatedName;

                // Act
                _sut.Update(entity);

                await _context.SaveChangesAsync();

                // Assert
                var result = await _sut.GetByIdAsync(entity.Id);
                result.Should().BeEquivalentTo(entity);
                entity.Name.Should().Be(updatedName);
            }

            [Test]
            public void Update_WithNullEntity_ThrowsValidationException()
            {
                // Arrange
                NodeEntity? invalidEntity = null;

                // Act & Assert
                #pragma warning disable CS8604 // Possible null reference argument.
                Assert.Throws<ArgumentNullException>(() => _sut.Update(invalidEntity));
                #pragma warning restore CS8604 // Possible null reference argument.
            }
        }

        [TestFixture]
        public class When_Delete_Node : NodeRepositoryTests
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
                var entity = new NodeEntity
                (
                    Guid.NewGuid(),
                    "Name One"
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
                NodeEntity? invalidEntity = null;

                // Act & Assert
                #pragma warning disable CS8604 // Possible null reference argument.
                Assert.Throws<ArgumentNullException>(() => _sut.Delete(invalidEntity));
                #pragma warning restore CS8604 // Possible null reference argument.
            }
        }
    }
}
