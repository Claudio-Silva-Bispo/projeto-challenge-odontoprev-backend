using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace UserApi.Models
{
    public class Dentista
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id_dentista { get; set; }

        [BsonElement("nome")]
        public string nome { get; set; } = null!;

        [BsonElement("sobrenome")]
        public string sobrenome { get; set; } = null!;

        [BsonElement("telefone")]
        public string Telefone { get; set; } = null!;

        [BsonElement("id_clinica")]
        public string id_clinica { get; set; } = null!;

        [BsonElement("id_especialidade")]
        public string id_especialidade { get; set; } = null!;
    
        [BsonElement("avalicao")]
        public string avaliacao { get; set; } = null!;

    }
}
