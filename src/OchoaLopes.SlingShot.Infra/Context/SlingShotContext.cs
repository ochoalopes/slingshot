using Microsoft.EntityFrameworkCore;
using OchoaLopes.SlingShot.Domain.Entities;

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
        #endregion

        #region Protected Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<KafkaConfigurationEntity>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.BootstrapServers).HasMaxLength(255);
                entity.Property(e => e.Topic).HasMaxLength(255);
                entity.Property(e => e.GroupId).HasMaxLength(255);
                entity.Property(e => e.AutoOffsetReset).HasMaxLength(12);
                entity.Property(e => e.EnableAutoCommit).HasDefaultValue(false);
                entity.Property(e => e.SecurityProtocol).HasMaxLength(255);
                entity.Property(e => e.SaslMechanism).HasMaxLength(255);
                entity.Property(e => e.SaslUsername).HasMaxLength(255);
                entity.Property(e => e.SaslPassword).HasMaxLength(255);

                entity.ToTable("KafkaConfigurations");
            });

            modelBuilder.Entity<ApiConfigurationEntity>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.BaseUrl).HasMaxLength(255);
                entity.Property(e => e.ApiKey).HasMaxLength(255);
                entity.Property(e => e.AuthToken).HasMaxLength(255);
                entity.Property(e => e.DefaultHeaders).HasMaxLength(2048);
                entity.Property(e => e.TimeoutInSeconds).HasDefaultValue(30);
            });

            modelBuilder.Entity<StorageConfigurationEntity>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.ConnectionString).HasMaxLength(255);
                entity.Property(e => e.ContainerName).HasMaxLength(255);
                entity.Property(e => e.AccessKey).HasMaxLength(255);
                entity.Property(e => e.SecretKey).HasMaxLength(255);
                entity.Property(e => e.Region).HasMaxLength(25);
                entity.Property(e => e.UseSSL).HasDefaultValue(false);
            });
        }
        #endregion
    }
}
