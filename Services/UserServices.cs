using UserApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace UserApi.Services
{
    public class UserService
    {
        private readonly IMongoCollection<Usuario> _userCollection;
        private readonly IMongoCollection<Cadastro> _cadastroCollection;
        private readonly IMongoCollection<Login> _loginCollection;

        public UserService(IOptions<UserDatabaseSettings> userDatabaseSettings)
        {
            var client = new MongoClient(userDatabaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(userDatabaseSettings.Value.DatabaseName);

            _userCollection = database.GetCollection<Usuario>(userDatabaseSettings.Value.UsersCollectionName);
            _cadastroCollection = database.GetCollection<Cadastro>(userDatabaseSettings.Value.CadastroCollectionName);
            _loginCollection = database.GetCollection<Login>(userDatabaseSettings.Value.LoginCollectionName);
        }

        public async Task<List<Usuario>> GetAsync() =>
            await _userCollection.Find(_ => true).ToListAsync();

        public async Task<Usuario?> GetAsync(string id) =>
            await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Cadastro newUser)
        {
            var usuario = new Usuario
            {
                Nome = newUser.Nome
            };

            await _userCollection.InsertOneAsync(usuario);

            newUser.Id = usuario.Id; // Relaciona o ID do usuário com o cadastro
            newUser.DataHora = DateTime.UtcNow;
            await _cadastroCollection.InsertOneAsync(newUser);
        }

        public async Task<Cadastro?> GetCadastroAsync(string email, string senha) =>
            await _cadastroCollection.Find(c => c.Email == email && c.Senha == senha).FirstOrDefaultAsync();

        public async Task RecordLoginAsync(Login login) =>
            await _loginCollection.InsertOneAsync(login);

        public async Task UpdateAsync(string id, Usuario updatedUser) =>
            await _userCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

        public async Task RemoveAsync(string id)
        {
            await _userCollection.DeleteOneAsync(x => x.Id == id);
            await _cadastroCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
