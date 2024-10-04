using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UserApi.Models;

namespace UserApi.Services
{
    public class ConsultaService : IConsultaService
    {
        private readonly IMongoCollection<Consulta> _consultaCollection;

        public ConsultaService(IMongoClient mongoClient, IOptions<UserDatabaseSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _consultaCollection = database.GetCollection<Consulta>(settings.Value.ConsultaCollectionName);
        }

        public async Task<List<Consulta>> GetAll()
        {
            return await _consultaCollection.Find(c => true).ToListAsync();
        }

        public async Task<Consulta?> GetById(string id)
        {
            return await _consultaCollection.Find(c => c.id_consulta == id).FirstOrDefaultAsync();
        }

        public async Task<Consulta> Create(Consulta consulta)
        {
            await _consultaCollection.InsertOneAsync(consulta);
            return consulta;
        }

        public async Task Update(string id, Consulta consulta)
        {
            await _consultaCollection.ReplaceOneAsync(c => c.id_consulta == id, consulta);
        }

        public async Task Delete(string id)
        {
            await _consultaCollection.DeleteOneAsync(c => c.id_consulta == id);
        }
    }
}
