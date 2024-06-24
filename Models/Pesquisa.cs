using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace UserApi.Models
{
    public class Pesquisa
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("termo")]
        public string Termo { get; set; } = null!; // O conteúdo pesquisado pelo usuário

        [BsonElement("userId")]
        public string? UserId { get; set; } // Opcional, pode ser null para usuários não logados

        [BsonElement("dataHora")]
        public DateTime DataHora { get; set; } = DateTime.UtcNow; // Armazenado em UTC por padrão

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
