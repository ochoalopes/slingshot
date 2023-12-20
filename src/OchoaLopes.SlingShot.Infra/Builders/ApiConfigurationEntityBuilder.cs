using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OchoaLopes.SlingShot.Domain.Entities;

namespace OchoaLopes.SlingShot.Infra.Builders
{
    public class ApiConfigurationEntityBuilder : IEntityTypeConfiguration<ApiConfigurationEntity>
    {
        public void Configure(EntityTypeBuilder<ApiConfigurationEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.BaseUrl).HasMaxLength(255);
            builder.Property(e => e.ApiKey).HasMaxLength(255);
            builder.Property(e => e.AuthToken).HasMaxLength(255);
            builder.Property(e => e.DefaultHeaders).HasMaxLength(2048);
            builder.Property(e => e.TimeoutInSeconds).HasDefaultValue(30);
            builder.Property(e => e.IsEnabled).HasDefaultValue(true);

            builder.HasOne(e => e.Node).WithMany(e => e.ApiConfigurations).HasForeignKey(e => e.NodeId);

            builder.ToTable("ApiConfigurations");
        }
    }
}
