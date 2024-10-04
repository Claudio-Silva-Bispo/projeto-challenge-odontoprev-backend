using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UserApi.Models;

namespace UserApi.Services
{
    public class CadastroService : ICadastroService
    {
        private readonly IMongoCollection<Cadastro> _cadastroCollection;
        private readonly IMongoCollection<Cliente> _clienteCollection;

        public CadastroService(IMongoClient mongoClient, IOptions<UserDatabaseSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _cadastroCollection = database.GetCollection<Cadastro>(settings.Value.CadastroCollectionName);
            _clienteCollection = database.GetCollection<Cliente>(settings.Value.ClienteCollectionName);
        }

        public async Task<List<Cadastro>> GetAll()
        {
            return await _cadastroCollection.Find(c => true).ToListAsync();
        }

        public async Task<Cadastro?> GetById(string id)
        {
            return await _cadastroCollection.Find(c => c.id_cliente == id).FirstOrDefaultAsync();
        }

        public async Task<Cadastro> Create(Cadastro cadastro)
        {
            await _cadastroCollection.InsertOneAsync(cadastro);

            var usuario = new Cliente
            {
                nome = cadastro.nome,
                sobrenome = cadastro.sobrenome,
                email = cadastro.email,
                telefone = cadastro.telefone,
                endereco = cadastro.endereco,
                data_nasc = cadastro.data_nasc,
                senha = cadastro.senha
            };

            await _clienteCollection.InsertOneAsync(usuario);

            return cadastro;
        }

        public async Task Update(string id, Cadastro cadastro)
        {
            await _cadastroCollection.ReplaceOneAsync(c => c.id_cliente == id, cadastro);

            var cliente = await _clienteCollection.Find(u => u.email == cadastro.email).FirstOrDefaultAsync();
            if (cliente != null)
            {
                cliente.nome = cadastro.nome;
                cliente.sobrenome = cadastro.sobrenome;
                cliente.telefone = cadastro.telefone;
                cliente.endereco = cadastro.endereco;
                cliente.data_nasc = cadastro.data_nasc;
                cliente.senha = cadastro.senha;
                await _clienteCollection.ReplaceOneAsync(u => u.id_cliente == cliente.id_cliente, cliente);
            }
        }

        public async Task Delete(string id)
        {
            var cadastro = await _cadastroCollection.Find(c => c.id_cliente == id).FirstOrDefaultAsync();
            if (cadastro != null)
            {
                await _cadastroCollection.DeleteOneAsync(c => c.id_cliente == id);
                await _clienteCollection.DeleteOneAsync(u => u.email == cadastro.email);
            }
        }
    }
}
