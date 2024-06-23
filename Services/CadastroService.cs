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
        private readonly IMongoCollection<Usuario> _usuarioCollection;

        public CadastroService(IMongoClient mongoClient, IOptions<UserDatabaseSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _cadastroCollection = database.GetCollection<Cadastro>(settings.Value.CadastroCollectionName);
            _usuarioCollection = database.GetCollection<Usuario>(settings.Value.UsersCollectionName);
        }

        public async Task<List<Cadastro>> GetAll()
        {
            return await _cadastroCollection.Find(c => true).ToListAsync();
        }

        public async Task<Cadastro?> GetById(string id)
        {
            return await _cadastroCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Cadastro> Create(Cadastro cadastro)
        {
            await _cadastroCollection.InsertOneAsync(cadastro);

            var usuario = new Usuario
            {
                Nome = cadastro.Nome,
                Email = cadastro.Email,
                Senha = cadastro.Senha
            };

            await _usuarioCollection.InsertOneAsync(usuario);

            return cadastro;
        }

        public async Task Update(string id, Cadastro cadastro)
        {
            await _cadastroCollection.ReplaceOneAsync(c => c.Id == id, cadastro);

            var usuario = await _usuarioCollection.Find(u => u.Email == cadastro.Email).FirstOrDefaultAsync();
            if (usuario != null)
            {
                usuario.Nome = cadastro.Nome;
                usuario.Senha = cadastro.Senha;
                await _usuarioCollection.ReplaceOneAsync(u => u.Id == usuario.Id, usuario);
            }
        }

        public async Task Delete(string id)
        {
            var cadastro = await _cadastroCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
            if (cadastro != null)
            {
                await _cadastroCollection.DeleteOneAsync(c => c.Id == id);
                await _usuarioCollection.DeleteOneAsync(u => u.Email == cadastro.Email);
            }
        }
    }
}
