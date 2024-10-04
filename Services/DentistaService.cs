using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UserApi.Models;

namespace UserApi.Services
{
    public class DentistaService : IDentistaService
    {
        private readonly IMongoCollection<Dentista> _dentistaCollection;

        public DentistaService(IMongoClient mongoClient, IOptions<UserDatabaseSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _dentistaCollection = database.GetCollection<Dentista>(settings.Value.DentistaCollectionName);
        }

        public async Task<List<Dentista>> GetAll()
        {
            return await _dentistaCollection.Find(d => true).ToListAsync();
        }

        public async Task<Dentista?> GetById(string id)
        {
            return await _dentistaCollection.Find(d => d.id_dentista == id).FirstOrDefaultAsync();
        }

        public async Task<Dentista> Create(Dentista dentista)
        {
            await _dentistaCollection.InsertOneAsync(dentista);
            return dentista;
        }

        public async Task Update(string id, Dentista dentista)
        {
            await _dentistaCollection.ReplaceOneAsync(d => d.id_dentista == id, dentista);
        }

        public async Task Delete(string id)
        {
            await _dentistaCollection.DeleteOneAsync(d => d.id_dentista == id);
        }
    }
}
