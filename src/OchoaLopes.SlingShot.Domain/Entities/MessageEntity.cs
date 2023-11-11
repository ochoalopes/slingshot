using OchoaLopes.SlingShot.Domain.Enums;

namespace OchoaLopes.SlingShot.Domain.Entities
{
    public class MessageEntity : Entity
    {
        public MessageEntity(Guid id, string content, MessageTypeEnum type, DateTime createAt, string origin)
        {
            Id = id;
            Content = content;
            Type = type;
            CreateAt = createAt;
            Origin = origin;
        }

        public string Content { get; set; }
        public MessageTypeEnum Type { get; set; }
        public DateTime CreateAt { get; set; }
        public string Origin { get; set; }
    }
}
