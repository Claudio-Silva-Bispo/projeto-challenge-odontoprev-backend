using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UserApi.Models;

namespace UserApi.Services
{
    public class EstadoCivilService : IEstadoCivilService
    {
        private readonly IMongoCollection<EstadoCivil> _estadoCivilCollection;

        public EstadoCivilService(IMongoClient mongoClient, IOptions<UserDatabaseSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _estadoCivilCollection = database.GetCollection<EstadoCivil>(settings.Value.EstadoCivilCollectionName);
        }

        public async Task<List<EstadoCivil>> GetAll()
        {
            return await _estadoCivilCollection.Find(e => true).ToListAsync();
        }

        public async Task<EstadoCivil?> GetById(string id)
        {
            return await _estadoCivilCollection.Find(e => e.id_estado_civil == id).FirstOrDefaultAsync();
        }

        public async Task<EstadoCivil> Create(EstadoCivil estadoCivil)
        {
            await _estadoCivilCollection.InsertOneAsync(estadoCivil);
            return estadoCivil;
        }

        public async Task Update(string id, EstadoCivil estadoCivil)
        {
            await _estadoCivilCollection.ReplaceOneAsync(e => e.id_estado_civil == id, estadoCivil);
        }

        public async Task Delete(string id)
        {
            await _estadoCivilCollection.DeleteOneAsync(e => e.id_estado_civil == id);
        }
    }
}
