using MongoDB.Driver;
using UserApi.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserApi.Services
{
    public class UsuarioService
    {
        private readonly IMongoCollection<Usuario> _usuarioCollection;

        public UsuarioService(IMongoClient mongoClient, IOptions<UserDatabaseSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _usuarioCollection = database.GetCollection<Usuario>(settings.Value.UsersCollectionName);
        }

        public async Task<List<Usuario>> GetAll()
        {
            return await _usuarioCollection.Find(u => true).ToListAsync();
        }

        public async Task<Usuario?> GetById(string id)
        {
            return await _usuarioCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
        }
    }
}
