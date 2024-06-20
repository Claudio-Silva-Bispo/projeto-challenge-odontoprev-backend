using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserApi.Models;

public class Cadastro
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("nome")]
    public string Nome { get; set; } = null!;

    [BsonElement("dataHora")]
    public DateTime DataHora { get; set; }

    [BsonElement("email")]
    public string Email {get; set;} = null!;

    [BsonElement("senha")]
    public string Senha { get; set;} = null!;

}