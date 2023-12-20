using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace OchoaLopes.SlingShot.Application.Dtos
{
    public abstract class BaseDto
    {
        [Required]
        [JsonProperty("id")]
        public string? Id { get; set; }
    }
}
