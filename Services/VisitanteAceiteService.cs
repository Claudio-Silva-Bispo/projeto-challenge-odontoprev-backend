using MongoDB.Driver;
using UserApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace UserApi.Services
{
    public class VisitanteAceiteService
    {
        private readonly IMongoCollection<VisitanteAceite> _visitanteAceiteCollection;

        public VisitanteAceiteService(IOptions<UserDatabaseSettings> settings, IMongoClient client)
        {
            var databaseSettings = settings.Value;
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _visitanteAceiteCollection = database.GetCollection<VisitanteAceite>(databaseSettings.VisitanteAceiteCollectionName);
        }

        public async Task Create(VisitanteAceite visitanteAceite)
        {
            await _visitanteAceiteCollection.InsertOneAsync(visitanteAceite);
        }

        public async Task<List<VisitanteAceite>> GetAll()
        {
            return await _visitanteAceiteCollection.Find(v => true).ToListAsync();
        }

        public async Task<VisitanteAceite> GetById(string id)
        {
            return await _visitanteAceiteCollection.Find(v => v.IdVisitante == id).FirstOrDefaultAsync();
        }

        public async Task Update(string id, VisitanteAceite visitanteAceite)
        {
            await _visitanteAceiteCollection.ReplaceOneAsync(v => v.IdVisitante == id, visitanteAceite);
        }

        public async Task Delete(string id)
        {
            await _visitanteAceiteCollection.DeleteOneAsync(v => v.IdVisitante == id);
        }
    }
}
