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

            modelBuilder.Entity<NodeEntity>()
                .HasMany(n => n.KafkaConfigurations)
                .WithOne(k => k.Node)
                .HasForeignKey(k => k.NodeId);

            modelBuilder.Entity<NodeEntity>()
                .HasMany(n => n.ApiConfigurations)
                .WithOne(k => k.Node)
                .HasForeignKey(k => k.NodeId);

            modelBuilder.Entity<NodeEntity>()
                .HasMany(n => n.StorageConfigurations)
                .WithOne(k => k.Node)
                .HasForeignKey(k => k.NodeId);

            modelBuilder.ApplyConfiguration(new KafkaConfigurationEntityBuilder());

            modelBuilder.Entity<ApiConfigurationEntity>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.BaseUrl).HasMaxLength(255);
                entity.Property(e => e.ApiKey).HasMaxLength(255);
                entity.Property(e => e.AuthToken).HasMaxLength(255);
                entity.Property(e => e.DefaultHeaders).HasMaxLength(2048);
                entity.Property(e => e.TimeoutInSeconds).HasDefaultValue(30);
                entity.Property(e => e.IsEnabled).HasDefaultValue(true);

                entity.ToTable("ApiConfigurations");
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
                entity.Property(e => e.IsEnabled).HasDefaultValue(true);

                entity.ToTable("StorageConfigurations");
            });
        }
        #endregion
    }
}
