using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace UserApi.Models
{
    public class Cadastro
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id_cliente { get; set; }

        [BsonElement("nome")]
        public string nome { get; set; } = null!;

        [BsonElement("sobrenome")]
        public string sobrenome { get; set; } = null!;

        [BsonElement("email")]
        [Required]
        [EmailAddress]
        public string email { get; set; } = null!;

        [BsonElement("telefone")]
        [Required]
        [Phone]
        public string telefone { get; set; } = null!;

        [BsonElement("data_nasc")]
        public DateTime data_nasc { get; set; }

        [BsonElement("endereco")]
        public string endereco { get; set; } = null!;

        [BsonElement("senha")]
        [Required]
        public string senha { get; set; } = null!;


        [BsonIgnore]
        public string Data => ConvertToSaoPauloTime(data_nasc).ToString("yyyy-MM-dd");

        [BsonIgnore]
        public string Hora => ConvertToSaoPauloTime(data_nasc).ToString("HH:mm:ss");

        private DateTime ConvertToSaoPauloTime(DateTime utcDateTime)
        {
            TimeZoneInfo saoPauloTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, saoPauloTimeZone);
        }
    }
}
