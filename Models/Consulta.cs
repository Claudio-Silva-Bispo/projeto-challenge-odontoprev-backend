using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace UserApi.Models
{
    public class Consulta
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id_consulta { get; set; }

        [BsonElement("id_cliente")]
        public string id_cliente { get; set; } = null!;

        [BsonElement("id_clinica")]
        public string id_clinica { get; set; } = null!;

        [BsonElement("id_forma_pegamento")]
        public string id_forma_pegamento { get; set; } = null!;

        [BsonElement("id_tipo_consulta")]
        public string id_tipo_consulta { get; set; } = null!;

        [BsonElement("tipo_servico")]
        public string tipo_servico { get; set; } = null!;

        [BsonElement("data_consulta")]
        public DateTime data_consulta { get; set; }

        [BsonIgnore]
        public string Data => ConvertToSaoPauloTime(data_consulta).ToString("yyyy-MM-dd");

        [BsonIgnore]
        public string Hora => ConvertToSaoPauloTime(data_consulta).ToString("HH:mm:ss");

        [BsonElement("status_consulta")]
        public string status_consulta { get; set; } = null!;

        [BsonElement("observacoes")]
        public string observacoes { get; set; } = null!;

        [BsonElement("sintomas")]
        public string sintomas { get; set; } = null!;
    
        [BsonElement("tratamento_recomendado")]
        public string tratamento_recomendado { get; set; } = null!;

        [BsonElement("custo")]
        public float custo { get; set; }

        [BsonElement("prescricao")]
        public string prescricao { get; set; } = null!;

        [BsonElement("data_retorno")]
        public DateTime data_retorno { get; set; }

        [BsonIgnore]
        public string Data_Retorno => ConvertToSaoPauloTime(data_retorno).ToString("yyyy-MM-dd");

        [BsonIgnore]
        public string Hora_Retorno => ConvertToSaoPauloTime(data_retorno).ToString("HH:mm:ss");

          private DateTime ConvertToSaoPauloTime(DateTime utcDateTime)
        {
            TimeZoneInfo saoPauloTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, saoPauloTimeZone);
        }


    }
}
