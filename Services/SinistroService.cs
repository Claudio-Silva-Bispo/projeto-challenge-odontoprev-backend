using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UserApi.Models;

namespace UserApi.Services
{
    public class SinistroService : ISinistroService
    {
        private readonly IMongoCollection<Sinistro> _sinistroCollection;

        public SinistroService(IMongoClient mongoClient, IOptions<UserDatabaseSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _sinistroCollection = database.GetCollection<Sinistro>(settings.Value.SinistroCollectionName);
        }

        public async Task<List<Sinistro>> GetAll()
        {
            return await _sinistroCollection.Find(s => true).ToListAsync();
        }

        public async Task<Sinistro?> GetById(string id)
        {
            return await _sinistroCollection.Find(s => s.id_sinistro == id).FirstOrDefaultAsync();
        }

        public async Task<Sinistro> Create(Sinistro sinistro)
        {
            await _sinistroCollection.InsertOneAsync(sinistro);
            return sinistro;
        }

        public async Task Update(string id, Sinistro sinistro)
        {
            await _sinistroCollection.ReplaceOneAsync(s => s.id_sinistro == id, sinistro);
        }

        public async Task Delete(string id)
        {
            await _sinistroCollection.DeleteOneAsync(s => s.id_sinistro == id);
        }
    }
}
