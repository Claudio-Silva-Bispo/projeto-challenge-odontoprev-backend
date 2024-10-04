using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace UserApi.Models
{
    public class EstadoCivil
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id_estado_civil { get; set; }

        [BsonElement("descricao")]
        public string descricao { get; set; } = null!;

    }
}
