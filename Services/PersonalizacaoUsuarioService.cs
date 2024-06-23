using MongoDB.Driver;
using UserApi.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserApi.Services
{
    public class PersonalizacaoUsuarioService
    {
        private readonly IMongoCollection<PersonalizacaoUsuario> _personalizacaoCollection;

        public PersonalizacaoUsuarioService(IOptions<UserDatabaseSettings> userDatabaseSettings)
        {
            var client = new MongoClient(userDatabaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(userDatabaseSettings.Value.DatabaseName);
            _personalizacaoCollection = database.GetCollection<PersonalizacaoUsuario>(userDatabaseSettings.Value.PersonalizacaoUsuarioCollectionName);
        }

        public async Task<PersonalizacaoUsuario?> GetByUserIdAsync(string userId) =>
            await _personalizacaoCollection.Find(p => p.UserId == userId).FirstOrDefaultAsync();

        public async Task CreateAsync(PersonalizacaoUsuario personalizacao) =>
            await _personalizacaoCollection.InsertOneAsync(personalizacao);

        public async Task UpdateAsync(string id, PersonalizacaoUsuario personalizacao) =>
            await _personalizacaoCollection.ReplaceOneAsync(p => p.Id == id, personalizacao);

        public async Task RemoveAsync(string id) =>
            await _personalizacaoCollection.DeleteOneAsync(p => p.Id == id);
    }
}
