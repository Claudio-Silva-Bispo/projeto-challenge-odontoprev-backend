using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserApi.Models
{
    public class PersonalizacaoUsuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("userId")]
        public string UserId { get; set; } = null!;

        [BsonElement("backgroundClass")]
        public string BackgroundClass { get; set; } = null!;

        [BsonElement("textClass")]
        public string TextClass { get; set; } = null!;

        [BsonElement("titleClass")]
        public string TitleClass { get; set; } = null!;

        [BsonElement("paragraphClass")]
        public string ParagraphClass { get; set; } = null!;
    }
}
