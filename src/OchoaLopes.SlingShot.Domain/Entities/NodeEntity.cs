namespace OchoaLopes.SlingShot.Domain.Entities
{
    public class NodeEntity : Entity
    {
        public NodeEntity(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsEnabled { get; set; } = true;
        public ICollection<ApiConfigurationEntity> ApiConfigurations { get; set; } = new List<ApiConfigurationEntity>();
        public ICollection<KafkaConfigurationEntity> KafkaConfigurations { get; set; } = new List<KafkaConfigurationEntity>();
        public ICollection<StorageConfigurationEntity> StorageConfigurations { get; set; } = new List<StorageConfigurationEntity>();
    }
}
