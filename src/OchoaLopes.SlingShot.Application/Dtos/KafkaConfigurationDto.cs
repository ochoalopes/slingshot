using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace OchoaLopes.SlingShot.Application.Dtos
{
    public class KafkaConfigurationDto : BaseDto
    {
        [Required(ErrorMessage = "The bootstrap servers must be informed.")]
        [MaxLength(1024, ErrorMessage = "The bootstrap servers must be less than 1024 characters.")]
        [JsonProperty("bootstrapServers")]
        public string? BootstrapServers { get; set; }

        [Required(ErrorMessage = "The topic must be informed.")]
        [MaxLength(255, ErrorMessage = "The topic must be less than 255 characters.")]
        [JsonProperty("topic")]
        public string? Topic { get; set; }

        [Required(ErrorMessage = "The group id must be informed.")]
        [MaxLength(255, ErrorMessage = "The group id must be less than 255 characters.")]
        [JsonProperty("groupId")]
        public string? GroupId { get; set; }

        [MaxLength(16, ErrorMessage = "The AutoOffsetReset must be less than 16 characters.")]
        [JsonProperty("autoOffsetReset")]
        public string? AutoOffsetReset { get; set; }

        [JsonProperty("enableAutoCommit")]
        public bool EnableAutoCommit { get; set; }

        [MaxLength(16, ErrorMessage = "The SecurityProtocol must be less than 16 characters.")]
        [JsonProperty("securityProtocol")]
        public string? SecurityProtocol { get; set; }

        [MaxLength(16, ErrorMessage = "The SaslMechanism must be less than 16 characters.")]
        [JsonProperty("saslMechanism")]
        public string? SaslMechanism { get; set; }

        [MaxLength(255, ErrorMessage = "The SaslUsername must be less than 255 characters.")]
        [JsonProperty("saslUsername")]
        public string? SaslUsername { get; set; }

        [MaxLength(255, ErrorMessage = "The SaslPassword must be less than 255 characters.")]
        [JsonProperty("saslPassword")]
        public string? SaslPassword { get; set; }

        [MaxLength(1024, ErrorMessage = "The SSL CA location must be less than 1024 characters.")]
        [JsonProperty("sslCaLocation")]
        public string? SslCaLocation { get; set; }
    }
}
