using UserApi.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UserApi.Services
{
    public class ContatoService
    {
        private readonly IMongoCollection<Contato> _contatoCollection;

        public ContatoService(IOptions<UserDatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _contatoCollection = database.GetCollection<Contato>(settings.Value.ContatoCollectionName);
        }

        public async Task<List<Contato>> GetContatoAsync() =>
            await _contatoCollection.Find(_ => true).ToListAsync();

        public async Task CreateContatoAsync(Contato newContato)
        {
            await _contatoCollection.InsertOneAsync(newContato);
        }
    }
}
