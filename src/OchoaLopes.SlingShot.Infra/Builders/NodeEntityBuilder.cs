using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OchoaLopes.SlingShot.Domain.Entities;

namespace OchoaLopes.SlingShot.Infra.Builders
{
    public class NodeEntityBuilder : IEntityTypeConfiguration<NodeEntity>
    {
        public void Configure(EntityTypeBuilder<NodeEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).HasMaxLength(255);

            builder.Property(e => e.Description).HasMaxLength(1024);

            builder.Property(e => e.IsEnabled).HasDefaultValue(true);

            builder
                .HasMany(n => n.KafkaConfigurations)
                .WithOne(k => k.Node)
                .HasForeignKey(k => k.NodeId);

            builder
                .HasMany(n => n.ApiConfigurations)
                .WithOne(k => k.Node)
                .HasForeignKey(k => k.NodeId);

            builder
                .HasMany(n => n.StorageConfigurations)
                .WithOne(k => k.Node)
                .HasForeignKey(k => k.NodeId);

            builder.ToTable("Nodes");
        }
    }
}
