using MongoDB.Driver;
using UserApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace UserApi.Services
{
    public class PreferenciaIdiomaService
    {
        private readonly IMongoCollection<PreferenciaIdioma> _preferenciaIdiomaCollection;

        public PreferenciaIdiomaService(IOptions<UserDatabaseSettings> settings, IMongoClient client)
        {
            var databaseSettings = settings.Value;
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _preferenciaIdiomaCollection = database.GetCollection<PreferenciaIdioma>(databaseSettings.PreferenciaIdiomaCollectionName);
        }

        public async Task Create(PreferenciaIdioma preferenciaIdioma)
        {
            await _preferenciaIdiomaCollection.InsertOneAsync(preferenciaIdioma);
        }

        public async Task<List<PreferenciaIdioma>> GetAll()
        {
            return await _preferenciaIdiomaCollection.Find(v => true).ToListAsync();
        }

        public async Task<PreferenciaIdioma> GetById(string id)
        {
            return await _preferenciaIdiomaCollection.Find(v => v.Id == id).FirstOrDefaultAsync();
        }

        public async Task<PreferenciaIdioma> GetByUserId(string userId)
        {
            return await _preferenciaIdiomaCollection.Find(v => v.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task Update(string id, PreferenciaIdioma preferenciaIdioma)
        {
            await _preferenciaIdiomaCollection.ReplaceOneAsync(v => v.Id == id, preferenciaIdioma);
        }

        public async Task Delete(string id)
        {
            await _preferenciaIdiomaCollection.DeleteOneAsync(v => v.Id == id);
        }
    }
}
