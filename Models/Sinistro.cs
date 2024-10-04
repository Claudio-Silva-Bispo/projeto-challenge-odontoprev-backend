using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace UserApi.Models
{
    public class Sinistro
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id_sinistro { get; set; }

        [BsonElement("id_consulta")]
        public string id_consulta { get; set; } = null!;

        [BsonElement("nome")]
        public string nome { get; set; } = null!;

        [BsonElement("descricao")]
        public string descricao { get; set; } = null!;

        [BsonElement("status_sinistro")]
        public string status_sinistro { get; set; } = null!;

        [BsonElement("valor_sinistro")]
        public float valor_sinistro { get; set; }

        [BsonElement("data_abertura")]
        public DateTime data_abertura { get; set; }

        [BsonIgnore]
        public string Data => ConvertToSaoPauloTime(data_abertura).ToString("yyyy-MM-dd");

        [BsonIgnore]
        public string Hora => ConvertToSaoPauloTime(data_abertura).ToString("HH:mm:ss");

         [BsonElement("data_abertura")]
        public DateTime data_resolucao { get; set; }

        [BsonIgnore]
        public string Data_Resolucao => ConvertToSaoPauloTime(data_resolucao).ToString("yyyy-MM-dd");

        [BsonIgnore]
        public string Hora_Resolucao => ConvertToSaoPauloTime(data_resolucao).ToString("HH:mm:ss");

        [BsonElement("documentacao")]
        public string documentacao { get; set; } = null!;

    
          private DateTime ConvertToSaoPauloTime(DateTime utcDateTime)
        {
            TimeZoneInfo saoPauloTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, saoPauloTimeZone);
        }


    }
}
