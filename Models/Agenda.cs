using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace UserApi.Models
{
    public class Agenda
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id_agenda { get; set; }

        [BsonElement("id_cliente")]
        public string id_cliente { get; set; } = null!;

        [BsonElement("id_consulta")]
        public string id_consulta { get; set; } = null!;

        [BsonElement("data_consulta")]
        public DateTime data_consulta { get; set; }

        [BsonIgnore]
        public string Data => ConvertToSaoPauloTime(data_consulta).ToString("yyyy-MM-dd");

        [BsonIgnore]
        public string Hora => ConvertToSaoPauloTime(data_consulta).ToString("HH:mm:ss");
        private DateTime ConvertToSaoPauloTime(DateTime utcDateTime)
        {
            TimeZoneInfo saoPauloTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, saoPauloTimeZone);
        }

        [BsonElement("status_consulta")]
        public string status_consulta { get; set; } = null!;

        [BsonElement("observacoes")]
        public string observacoes { get; set; } = null!;

        


    }
}
