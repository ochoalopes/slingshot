using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OchoaLopes.SlingShot.Domain.Interfaces.Repositories;

namespace OchoaLopes.SlingShot.Infra.UoW
{
    public class ConfigurationUnitOfWork : UnitOfWork
    {
        #region Public Properties
        public IKafkaConfigurationRepository KafkaConfigurationRepository { get; }
        public IApiConfigurationRepository ApiConfigurationRepository { get; }
        public IStorageConfigurationRepository StorageConfigurationRepository { get; }
        #endregion

        public ConfigurationUnitOfWork(ILogger<ConfigurationUnitOfWork> logger, DbContext context, 
            IKafkaConfigurationRepository kafkaConfigurationRepository, 
            IApiConfigurationRepository apiConfigurationRepository, 
            IStorageConfigurationRepository storageConfigurationRepository) : base(logger, context)
        {
            KafkaConfigurationRepository = kafkaConfigurationRepository;
            ApiConfigurationRepository = apiConfigurationRepository;
            StorageConfigurationRepository = storageConfigurationRepository;
        }
    }
}
