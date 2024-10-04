using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace UserApi.Models
{
    public class Feedback
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id_feedback { get; set; }

        [BsonElement("id_cliente")]
        public string id_cliente { get; set; } = string.Empty;

        [BsonElement("id_dentista")]
        public string id_dentista { get; set; } = string.Empty;

        [BsonElement("id_clinica")]
        public string id_clinica { get; set; } = string.Empty;

        [BsonElement("nota")]
        public int Nota { get; set; }

        [BsonElement("comentario")]
        public string comentario { get; set; } = string.Empty;

    }
}
