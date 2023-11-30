namespace OchoaLopes.SlingShot.Domain.Entities
{
    public class StorageConfigurationEntity : Entity
    {
        public StorageConfigurationEntity(Guid id, string connectionString, string containerName, 
            string accessKey, string secretKey, Guid nodeId) : base(id)
        {
            Id = id;
            ConnectionString = connectionString;
            ContainerName = containerName;
            AccessKey = accessKey;
            SecretKey = secretKey;
            NodeId = nodeId;
        }

        public string ConnectionString { get; set; }
        public string ContainerName { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string? Region { get; set; }
        public bool UseSSL { get; set; }
        public bool IsEnabled { get; set; } = true;
        public Guid NodeId { get; set; }
        public NodeEntity? Node { get; set; }
    }
}
