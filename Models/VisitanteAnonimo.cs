using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace UserApi.Models
{
    public class VisitanteAnonimo
    {
        [BsonId]
        public string IdVisitante { get; set; } = Guid.NewGuid().ToString();

        [BsonElement("pagina_visitada")]
        public string PaginaVisitada { get; set; } = null!;

        [BsonElement("ordem_visita")]
        public string OrdemVisita { get; set; } = null!;

        [BsonElement("tempo_permanencia")]
        public string TempoPermanencia { get; set; } = null!;

        [BsonElement("elementos_clicados")]
        public string ElementosClicados { get; set; } = null!;

        [BsonElement("tipo_navegador")]
        public string TipoNavegador { get; set; } = null!;

        [BsonElement("versao_navegador")]
        public string VersaoNavegador { get; set; } = null!;

        [BsonElement("tipo_dispositivo")]
        public string TipoDispositivo { get; set; } = null!;

        [BsonElement("sistema_operacional")]
        public string SistemaOperacional { get; set; } = null!;

        [BsonElement("data_hora_sessao")]
        public string DataHoraSessao { get; set; } = null!; 
    }

}
