namespace OchoaLopes.SlingShot.Domain.Entities
{
    public class StorageConfigurationEntity : Entity
    {
        public StorageConfigurationEntity(string connectionString, string containerName, string accessKey, string secretKey)
        {
            Id = Guid.NewGuid();
            ConnectionString = connectionString;
            ContainerName = containerName;
            AccessKey = accessKey;
            SecretKey = secretKey;
        }

        public string ConnectionString { get; set; }
        public string ContainerName { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string? Region { get; set; }
        public bool UseSSL { get; set; }
    }
}
