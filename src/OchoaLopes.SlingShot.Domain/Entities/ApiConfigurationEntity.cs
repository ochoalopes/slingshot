namespace OchoaLopes.SlingShot.Domain.Entities
{
    public class ApiConfigurationEntity : Entity
    {
        public ApiConfigurationEntity(string baseUrl)
        {
            Id = Guid.NewGuid();
            BaseUrl = baseUrl;
        }

        public string BaseUrl { get; set; }
        public string? ApiKey { get; set; }
        public string? AuthToken { get; set; }
        public string? DefaultHeaders { get; set; }
        public int TimeoutInSeconds { get; set; }
    }
}
