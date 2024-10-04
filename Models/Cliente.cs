using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace UserApi.Models
{
    public class Cliente
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id_cliente { get; set; } = string.Empty;

        [BsonElement("nome")]
        public string nome { get; set; } = string.Empty;

        [BsonElement("sobrenome")]
        public string sobrenome { get; set; } = string.Empty;

        [BsonElement("email")]
        public string email { get; set; } = string.Empty;

        [BsonElement("telefone")]
        public string telefone { get; set; } = string.Empty;

        [BsonElement("endereco")]
        public string endereco { get; set; } = string.Empty;

        [BsonElement("data_nasc")]
        public DateTime data_nasc { get; set; }

        [BsonElement("senha")]
        public string senha { get; set; } = string.Empty;
    }
}
