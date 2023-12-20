using OchoaLopes.SlingShot.Application.Dtos;
using OchoaLopes.SlingShot.Application.Interfaces;
using OchoaLopes.SlingShot.Domain.Interfaces.UoW;
using System.Linq.Expressions;

namespace OchoaLopes.SlingShot.Application.Services.Configurations
{
    public class KafkaConfigurationDataService : IDataService<KafkaConfigurationDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Task AddAsync(KafkaConfigurationDto dto)
        {
            throw new NotImplementedException();
        }

        public void Delete(KafkaConfigurationDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<KafkaConfigurationDto>> Get(Expression<Func<KafkaConfigurationDto, bool>>? filter = null, Func<IQueryable<KafkaConfigurationDto>, IOrderedQueryable<KafkaConfigurationDto>>? orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<KafkaConfigurationDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<KafkaConfigurationDto?> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(KafkaConfigurationDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
