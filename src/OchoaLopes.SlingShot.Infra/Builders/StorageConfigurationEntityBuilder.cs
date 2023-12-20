using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OchoaLopes.SlingShot.Domain.Entities;

namespace OchoaLopes.SlingShot.Infra.Builders
{
    public class StorageConfigurationEntityBuilder : IEntityTypeConfiguration<StorageConfigurationEntity>
    {
        public void Configure(EntityTypeBuilder<StorageConfigurationEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.ConnectionString).HasMaxLength(255);
            builder.Property(e => e.ContainerName).HasMaxLength(255);
            builder.Property(e => e.AccessKey).HasMaxLength(255);
            builder.Property(e => e.SecretKey).HasMaxLength(255);
            builder.Property(e => e.Region).HasMaxLength(25);
            builder.Property(e => e.UseSSL).HasDefaultValue(false);
            builder.Property(e => e.IsEnabled).HasDefaultValue(true);

            builder.HasOne(e => e.Node).WithMany(e => e.StorageConfigurations).HasForeignKey(e => e.NodeId);

            builder.ToTable("StorageConfigurations");
        }
    }
}
