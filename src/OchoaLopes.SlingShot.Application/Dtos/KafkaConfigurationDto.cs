using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace OchoaLopes.SlingShot.Application.Dtos
{
    public class KafkaConfigurationDto : BaseDto
    {
        [Required]
        [MaxLength(255, ErrorMessage = "The Bootstrap Servers must be less than 255 characters.")]
        [JsonProperty("bootstrapServers")]
        public string? BootstrapServers { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "The Topic must be less than 255 characters.")]
        [JsonProperty("topic")]
        public string? Topic { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "The Group Id must be less than 255 characters.")]
        [JsonProperty("groupId")]
        public string? GroupId { get; set; }

        public string? AutoOffsetReset { get; set; }

        public bool EnableAutoCommit { get; set; }

        public string? SecurityProtocol { get; set; }

        public string? SaslMechanism { get; set; }

        public string? SaslUsername { get; set; }

        public string? SaslPassword { get; set; }
    }
}
