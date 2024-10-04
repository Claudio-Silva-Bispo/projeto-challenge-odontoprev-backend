using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace UserApi.Models
{
    public class FormularioDetalhado
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id_formulario { get; set; }

        [BsonElement("id_cliente")]
        public string id_cliente { get; set; } = null!;

        [BsonElement("id_estado_civil")]
        public string id_estado_civil { get; set; } = null!;

        [BsonElement("historico_familiar")]
        public string historico_familiar { get; set; } = null!;

        [BsonElement("profissao")]
        public string profissao { get; set; } = null!;

        [BsonElement("data_ultima_atualizacao")]
        public DateTime data_ultima_atualizacao { get; set; }

         [BsonIgnore]
        public string Data => ConvertToSaoPauloTime(data_ultima_atualizacao).ToString("yyyy-MM-dd");

        [BsonIgnore]
        public string Hora => ConvertToSaoPauloTime(data_ultima_atualizacao).ToString("HH:mm:ss");

        private DateTime ConvertToSaoPauloTime(DateTime utcDateTime)
        {
            TimeZoneInfo saoPauloTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, saoPauloTimeZone);
        }

        [BsonElement("renda_mensal")]
        public float renda_mensal { get; set; }

        [BsonElement("historico_medico")]
        public string historico_medico { get; set; } = null!;

        [BsonElement("alergia")]
        public string alergia { get; set; } = null!;

        [BsonElement("condicao_preexistente")]
        public string condicao_preexistente { get; set; } = null!;

        [BsonElement("uso_medicamento")]
        public string uso_medicamento { get; set; } = null!;

        [BsonElement("familiar_com_doencas_dentarias")]
        public string familiar_com_doencas_dentarias { get; set; } = null!;

        [BsonElement("participacao_programa_preventivos")]
        public string participacao_programa_preventivos { get; set; } = null!;

       [BsonElement("contato_emergencial")]
        public string contato_emergencial { get; set; } = null!;

        [BsonElement("pesquisa_satisfacao")]
        public string pesquisa_satisfacao { get; set; } = null!;

        [BsonElement("frequencia_consulta_periodica")]
        public string frequencia_consulta_periodica { get; set; } = null!;

        [BsonElement("sinalizacao_risco")]
        public string sinalizacao_risco { get; set; } = null!;

        [BsonElement("historico_viagem")]
        public string historico_viagem { get; set; } = null!;

        [BsonElement("historico_mudanca_endereco")]
        public string historico_mudanca_endereco { get; set; } = null!;

         [BsonElement("preferencia_contato")]
        public string preferencia_contato { get; set; } = null!;

    }
}
