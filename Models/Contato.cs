using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace UserApi.Models
{
    public class Contato
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("nome")]
        public string Nome { get; set; } = null!;

        [BsonElement("dataHora")]
        public DateTime DataHora { get; set; }

        [BsonElement("email")]
        public string Email { get; set; } = null!;

        [BsonElement("telefone")]
        public string Telefone { get; set; } = null!;

        [BsonIgnore]
        public string Data => ConvertToSaoPauloTime(DataHora).ToString("yyyy-MM-dd");

        [BsonIgnore]
        public string Hora => ConvertToSaoPauloTime(DataHora).ToString("HH:mm:ss");

        private DateTime ConvertToSaoPauloTime(DateTime utcDateTime)
        {
            TimeZoneInfo saoPauloTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, saoPauloTimeZone);
        }
    }
}
