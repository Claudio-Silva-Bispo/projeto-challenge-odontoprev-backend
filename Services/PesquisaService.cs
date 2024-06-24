using MongoDB.Driver;
using UserApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace UserApi.Services
{
    public class PesquisaService
    {
        private readonly IMongoCollection<Pesquisa> _pesquisaCollection;

        public PesquisaService(IOptions<UserDatabaseSettings> settings, IMongoClient client)
        {
            var databaseSettings = settings.Value;
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _pesquisaCollection = database.GetCollection<Pesquisa>(databaseSettings.PesquisaCollectionName);
        }

        public async Task Create(Pesquisa pesquisa)
        {
            await _pesquisaCollection.InsertOneAsync(pesquisa);
        }

        public async Task<List<Pesquisa>> GetAll()
        {
            return await _pesquisaCollection.Find(p => true).ToListAsync();
        }

        public async Task<Pesquisa> GetById(string id)
        {
            return await _pesquisaCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Pesquisa>> GetByUserId(string userId)
        {
            return await _pesquisaCollection.Find(p => p.UserId == userId).ToListAsync();
        }
    }
}
