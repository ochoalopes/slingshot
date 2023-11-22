using AutoMapper;
using OchoaLopes.SlingShot.Application.Dtos;
using OchoaLopes.SlingShot.Domain.Entities;

namespace OchoaLopes.SlingShot.Application.MappingProfiles
{
    public class KafkaConfigurationProfile : Profile
    {
        public KafkaConfigurationProfile()
        {
            CreateMap<KafkaConfigurationDto, KafkaConfigurationEntity>();
            CreateMap<KafkaConfigurationEntity, KafkaConfigurationDto>();
        }
    }
}
