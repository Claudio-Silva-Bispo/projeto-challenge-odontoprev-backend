using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace UserApi.Models
{
    public class VisitanteAceite
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
        public List<ElementoClicado> ElementosClicados { get; set; } = new List<ElementoClicado>();

        [BsonElement("tipo_navegador")]
        public string TipoNavegador { get; set; } = null!;

        [BsonElement("versao_navegador")]
        public string VersaoNavegador { get; set; } = null!;

        [BsonElement("tipo_dispositivo")]
        public string TipoDispositivo { get; set; } = null!;

        [BsonElement("sistema_operacional")]
        public string SistemaOperacional { get; set; } = null!;

        [BsonElement("data_consentimento")]
        public string DataConsentimento { get; set; } = null!;
    }

    public class ElementoClicado
    {
        [BsonElement("elemento")]
        public string Elemento { get; set; } = null!;

        [BsonElement("posicao_clique")]
        public Posicao PosicaoClique { get; set; } = new Posicao();
    }

    public class Posicao
    {
        [BsonElement("x")]
        public string X { get; set; } = null!;

        [BsonElement("y")]
        public string Y { get; set; } = null!;
    }
}
