using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UserApi.Models;

namespace UserApi.Services
{
    public class ClinicaService : IClinicaService
    {
        private readonly IMongoCollection<Clinica> _clinicaCollection;

        public ClinicaService(IMongoClient mongoClient, IOptions<UserDatabaseSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _clinicaCollection = database.GetCollection<Clinica>(settings.Value.ClinicaCollectionName);
        }

        public async Task<List<Clinica>> GetAll()
        {
            return await _clinicaCollection.Find(c => true).ToListAsync();
        }

        public async Task<Clinica?> GetById(string id)
        {
            return await _clinicaCollection.Find(c => c.id_clinica == id).FirstOrDefaultAsync();
        }

        public async Task<Clinica> Create(Clinica clinica)
        {
            await _clinicaCollection.InsertOneAsync(clinica);
            return clinica;
        }

        public async Task Update(string id, Clinica clinica)
        {
            await _clinicaCollection.ReplaceOneAsync(c => c.id_clinica == id, clinica);
        }

        public async Task Delete(string id)
        {
            await _clinicaCollection.DeleteOneAsync(c => c.id_clinica == id);
        }
    }
}
