using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace UserApi.Models
{
    public class Clinica
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id_clinica { get; set; }

        [BsonElement("nome")]
        public string Nome { get; set; } = null!;

        [BsonElement("endereco")]
        public string Endereco { get; set; } = null!;

        [BsonElement("telefone")]
        public string Telefone { get; set; } = null!;

        [BsonElement("avaliacao")]
        public string Avaliacao { get; set; } = null!;

        [BsonElement("preco_medio")]
        public float PrecoMedio { get; set; }

        [BsonElement("email")]
        public string Email { get; set; } = null!;
    }
}
