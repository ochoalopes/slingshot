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

        public DbSet<KafkaConfigurationEntity> KafkaConfigurations { get; set; }
        #endregion

        #region Protected Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<KafkaConfigurationEntity>(entity =>
            {
                entity.HasKey(e => e.Id);            
            });
        }
        #endregion
    }
}
