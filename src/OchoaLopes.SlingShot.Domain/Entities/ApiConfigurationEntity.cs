namespace OchoaLopes.SlingShot.Domain.Entities
{
    public class ApiConfigurationEntity : Entity
    {
        public ApiConfigurationEntity(Guid id, string baseUrl, Guid nodeId) : base(id)
        {
            Id = id;
            BaseUrl = baseUrl;
            NodeId = nodeId;
        }

        public string BaseUrl { get; set; }
        public string? ApiKey { get; set; }
        public string? AuthToken { get; set; }
        public string? DefaultHeaders { get; set; }
        public int TimeoutInSeconds { get; set; }
        public bool IsEnabled { get; set; } = true;
        public Guid NodeId { get; set; }
        public NodeEntity? Node { get; set; }
    }
}
