using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UserApi.Models;

namespace UserApi.Services
{
    public class AgendaService : IAgendaService
    {
        private readonly IMongoCollection<Agenda> _agendaCollection;

        public AgendaService(IMongoClient mongoClient, IOptions<UserDatabaseSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _agendaCollection = database.GetCollection<Agenda>(settings.Value.AgendaCollectionName);
        }

        public async Task<List<Agenda>> GetAll()
        {
            return await _agendaCollection.Find(a => true).ToListAsync();
        }

        public async Task<Agenda?> GetById(string id)
        {
            return await _agendaCollection.Find(a => a.id_agenda == id).FirstOrDefaultAsync();
        }

        public async Task<Agenda> Create(Agenda agenda)
        {
            await _agendaCollection.InsertOneAsync(agenda);
            return agenda;
        }

        public async Task Update(string id, Agenda agenda)
        {
            await _agendaCollection.ReplaceOneAsync(a => a.id_agenda == id, agenda);
        }

        public async Task Delete(string id)
        {
            await _agendaCollection.DeleteOneAsync(a => a.id_agenda == id);
        }
    }
}
