using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UserApi.Models;

namespace UserApi.Services
{
    public class TipoNotificacaoService : ITipoNotificacaoService
    {
        private readonly IMongoCollection<TipoNotificacao> _tipoNotificacaoCollection;

        public TipoNotificacaoService(IMongoClient mongoClient, IOptions<UserDatabaseSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _tipoNotificacaoCollection = database.GetCollection<TipoNotificacao>(settings.Value.TipoNotificacaoCollectionName);
        }

        public async Task<List<TipoNotificacao>> GetAll()
        {
            return await _tipoNotificacaoCollection.Find(t => true).ToListAsync();
        }

        public async Task<TipoNotificacao?> GetById(string id)
        {
            return await _tipoNotificacaoCollection.Find(t => t.id_tipo_notificacao == id).FirstOrDefaultAsync();
        }

        public async Task<TipoNotificacao> Create(TipoNotificacao tipoNotificacao)
        {
            await _tipoNotificacaoCollection.InsertOneAsync(tipoNotificacao);
            return tipoNotificacao;
        }

        public async Task Update(string id, TipoNotificacao tipoNotificacao)
        {
            await _tipoNotificacaoCollection.ReplaceOneAsync(t => t.id_tipo_notificacao == id, tipoNotificacao);
        }

        public async Task Delete(string id)
        {
            await _tipoNotificacaoCollection.DeleteOneAsync(t => t.id_tipo_notificacao == id);
        }
    }
}
