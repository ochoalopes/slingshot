namespace OchoaLopes.SlingShot.Domain.Entities
{
    public class ApiConfigurationEntity : Entity
    {
        public ApiConfigurationEntity(Guid id, string baseUrl) : base(id)
        {
            Id = id;
            BaseUrl = baseUrl;
        }

        public string BaseUrl { get; set; }
        public string? ApiKey { get; set; }
        public string? AuthToken { get; set; }
        public string? DefaultHeaders { get; set; }
        public int TimeoutInSeconds { get; set; }
    }
}
