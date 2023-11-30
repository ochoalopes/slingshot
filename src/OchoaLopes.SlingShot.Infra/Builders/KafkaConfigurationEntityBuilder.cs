using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OchoaLopes.SlingShot.Domain.Entities;

namespace OchoaLopes.SlingShot.Infra.Builders
{
    public class KafkaConfigurationEntityBuilder : IEntityTypeConfiguration<KafkaConfigurationEntity>
    {
        public void Configure(EntityTypeBuilder<KafkaConfigurationEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.BootstrapServers).HasMaxLength(1024);
            builder.Property(e => e.Topic).HasMaxLength(255);
            builder.Property(e => e.GroupId).HasMaxLength(255);
            builder.Property(e => e.AutoOffsetReset).HasMaxLength(16);
            builder.Property(e => e.EnableAutoCommit).HasDefaultValue(false);
            builder.Property(e => e.SecurityProtocol).HasMaxLength(16);
            builder.Property(e => e.SaslMechanism).HasMaxLength(16);
            builder.Property(e => e.SaslUsername).HasMaxLength(255);
            builder.Property(e => e.SaslPassword).HasMaxLength(255);
            builder.Property(e => e.SslCaLocation).HasMaxLength(1024);
            builder.Property(e => e.IsEnabled).HasDefaultValue(true);

            builder.ToTable("KafkaConfigurations");
        }
    }
}
