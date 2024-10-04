using MongoDB.Driver;
using UserApi.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserApi.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IMongoCollection<Cliente> _clienteCollection;

        public ClienteService(IMongoClient mongoClient, IOptions<UserDatabaseSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _clienteCollection = database.GetCollection<Cliente>(settings.Value.ClienteCollectionName);
        }

        public async Task<List<Cliente>> GetAll()
        {
            return await _clienteCollection.Find(u => true).ToListAsync();
        }

        public async Task<Cliente?> GetById(string id)
        {
            return await _clienteCollection.Find(u => u.id_cliente == id).FirstOrDefaultAsync();
        }

        public async Task<Cliente> Create(Cliente cliente)
        {
            await _clienteCollection.InsertOneAsync(cliente);
            return cliente;
        }

        public async Task Update(string id, Cliente cliente)
        {
            await _clienteCollection.ReplaceOneAsync(u => u.id_cliente == id, cliente);
        }

        public async Task Delete(string id)
        {
            await _clienteCollection.DeleteOneAsync(u => u.id_cliente == id);
        }
    }
}
