using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UserApi.Models;

namespace UserApi.Services
{
    public class NotificacoesService : INotificacoesService
    {
        private readonly IMongoCollection<Notificacoes> _notificacoesCollection;

        public NotificacoesService(IMongoClient mongoClient, IOptions<UserDatabaseSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _notificacoesCollection = database.GetCollection<Notificacoes>(settings.Value.NotificacoesCollectionName);
        }

        public async Task<List<Notificacoes>> GetAll()
        {
            return await _notificacoesCollection.Find(n => true).ToListAsync();
        }

        public async Task<Notificacoes?> GetById(string id)
        {
            return await _notificacoesCollection.Find(n => n.id_notificacoes == id).FirstOrDefaultAsync();
        }

        public async Task<Notificacoes> Create(Notificacoes notificacoes)
        {
            await _notificacoesCollection.InsertOneAsync(notificacoes);
            return notificacoes;
        }

        public async Task Update(string id, Notificacoes notificacoes)
        {
            await _notificacoesCollection.ReplaceOneAsync(n => n.id_notificacoes == id, notificacoes);
        }

        public async Task Delete(string id)
        {
            await _notificacoesCollection.DeleteOneAsync(n => n.id_notificacoes == id);
        }
    }
}
