using MongoDB.Driver;
using UserApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace UserApi.Services
{
    public class VisitanteAnonimoService
    {
        private readonly IMongoCollection<VisitanteAnonimo> _visitanteAnonimoCollection;

        public VisitanteAnonimoService(IOptions<UserDatabaseSettings> settings, IMongoClient client)
        {
            var databaseSettings = settings.Value;
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _visitanteAnonimoCollection = database.GetCollection<VisitanteAnonimo>(databaseSettings.VisitanteAnonimoCollectionName);
        }

        public async Task Create(VisitanteAnonimo visitanteAnonimo)
        {
            await _visitanteAnonimoCollection.InsertOneAsync(visitanteAnonimo);
        }

        public async Task<List<VisitanteAnonimo>> GetAll()
        {
            return await _visitanteAnonimoCollection.Find(v => true).ToListAsync();
        }

        public async Task<VisitanteAnonimo> GetById(string id)
        {
            return await _visitanteAnonimoCollection.Find(v => v.IdVisitante == id).FirstOrDefaultAsync();
        }

        public async Task Update(string id, VisitanteAnonimo visitanteAnonimo)
        {
            await _visitanteAnonimoCollection.ReplaceOneAsync(v => v.IdVisitante == id, visitanteAnonimo);
        }

        public async Task Delete(string id)
        {
            await _visitanteAnonimoCollection.DeleteOneAsync(v => v.IdVisitante == id);
        }
    }
}
