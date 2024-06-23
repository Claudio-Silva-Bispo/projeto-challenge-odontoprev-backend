using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace UserApi.Models
{
    public class LogLogin
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("tipoLogin")]
        public string TipoLogin { get; set; } = string.Empty;

        [BsonElement("data")]
        public string Data { get; set; } = string.Empty;

        [BsonElement("hora")]
        public string Hora { get; set; } = string.Empty;
        
        private DateTime ConvertToSaoPauloTime(DateTime utcDateTime)
        {
            TimeZoneInfo saoPauloTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, saoPauloTimeZone);
        }

        public LogLogin()
        {
            var saoPauloTime = ConvertToSaoPauloTime(DateTime.UtcNow);
            Data = saoPauloTime.ToString("yyyy-MM-dd");
            Hora = saoPauloTime.ToString("HH:mm:ss");
        }
    }
}
