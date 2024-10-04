using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace UserApi.Models
{
    public class Notificacoes
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id_notificacoes { get; set; }

        [BsonElement("id_cliente")]
        public string id_cliente { get; set; } = null!;

        [BsonElement("id_tipo_notificacao")]
        public string id_tipo_notificacao { get; set; } = null!;

        [BsonElement("mensagem")]
        public string mensagem { get; set; } = null!;

        [BsonElement("data_envio")]
        public DateTime data_envio { get; set; }

        [BsonIgnore]
        public string Data => ConvertToSaoPauloTime(data_envio).ToString("yyyy-MM-dd");

        [BsonIgnore]
        public string Hora => ConvertToSaoPauloTime(data_envio).ToString("HH:mm:ss");

          private DateTime ConvertToSaoPauloTime(DateTime utcDateTime)
        {
            TimeZoneInfo saoPauloTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, saoPauloTimeZone);
        }
    }
}
