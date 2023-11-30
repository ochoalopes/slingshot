namespace OchoaLopes.SlingShot.Domain.Entities
{
    public class KafkaConfigurationEntity : Entity
    {
        public KafkaConfigurationEntity(Guid id, string bootstrapServers, string topic, 
            string groupId, Guid nodeId) : base(id)
        {
            Id = id;
            BootstrapServers = bootstrapServers;
            Topic = topic;
            GroupId = groupId;
            NodeId = nodeId;
        }

        public string BootstrapServers { get; set; }
        public string Topic { get; set; }
        public string GroupId { get; set; }
        public string? AutoOffsetReset { get; set; }
        public bool EnableAutoCommit { get; set; }
        public string? SecurityProtocol { get; set; }
        public string? SaslMechanism { get; set; }
        public string? SaslUsername { get; set; }
        public string? SaslPassword { get; set; }
        public string? SslCaLocation { get; set; }
        public bool IsEnabled { get; set; } = true;
        public Guid NodeId { get; set; }
        public NodeEntity? Node { get; set; }
    }
}
