using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace UserApi.Models
{
    public class TipoNotificacao
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id_tipo_notificacao { get; set; }

        [BsonElement("descricao")]
        public string descricao { get; set; } = null!;

    }
}
