using Microsoft.EntityFrameworkCore;
using OchoaLopes.SlingShot.Domain.Entities;
using OchoaLopes.SlingShot.Infra.Builders;

namespace OchoaLopes.SlingShot.Infra.Context
{
    public class SlingShotContext : DbContext
    {
        #region Public Methods
        public SlingShotContext(DbContextOptions<SlingShotContext> options) : base(options)
        {
        }
        #endregion

        #region Public Properties
        public DbSet<KafkaConfigurationEntity> KafkaConfigurations { get; set; }
        public DbSet<ApiConfigurationEntity> ApiConfigurations { get; set; }
        public DbSet<StorageConfigurationEntity> StorageConfigurations { get; set; }
        public DbSet<NodeEntity> Nodes { get; set; }
        #endregion

        #region Protected Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new NodeEntityBuilder());

            modelBuilder.ApplyConfiguration(new KafkaConfigurationEntityBuilder());

            modelBuilder.ApplyConfiguration(new ApiConfigurationEntityBuilder());

            modelBuilder.ApplyConfiguration(new StorageConfigurationEntityBuilder());
        }
        #endregion
    }
}
