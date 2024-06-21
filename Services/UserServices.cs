using UserApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserApi.Services
{
    public class UserService
    {
        private readonly IMongoCollection<Usuario> _userCollection;
        private readonly IMongoCollection<Cadastro> _cadastroCollection;
        private readonly IMongoCollection<Login> _loginCollection;
        private readonly IMongoCollection<Feedback> _feedbackCollection;

        public UserService(IOptions<UserDatabaseSettings> userDatabaseSettings)
        {
            var client = new MongoClient(userDatabaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(userDatabaseSettings.Value.DatabaseName);

            _userCollection = database.GetCollection<Usuario>(userDatabaseSettings.Value.UsersCollectionName);
            _cadastroCollection = database.GetCollection<Cadastro>(userDatabaseSettings.Value.CadastroCollectionName);
            _loginCollection = database.GetCollection<Login>(userDatabaseSettings.Value.LoginCollectionName);
            _feedbackCollection = database.GetCollection<Feedback>(userDatabaseSettings.Value.FeedbackCollectionName);
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
            newUser.DataHora = ConvertToSaoPauloTime(DateTime.UtcNow);
            await _cadastroCollection.InsertOneAsync(newUser);
        }

        public async Task<Cadastro?> GetCadastroAsync(string email, string senha) =>
            await _cadastroCollection.Find(c => c.Email == email && c.Senha == senha).FirstOrDefaultAsync();

        public async Task RecordLoginAsync(Login login)
        {
            login.LoginDate = ConvertToSaoPauloTime(DateTime.UtcNow);
            await _loginCollection.InsertOneAsync(login);
        }

        public async Task UpdateAsync(string id, Usuario updatedUser) =>
            await _userCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

        public async Task RemoveAsync(string id)
        {
            await _userCollection.DeleteOneAsync(x => x.Id == id);
            await _cadastroCollection.DeleteOneAsync(x => x.Id == id);
        }

        // Método para buscar todos os cadastros
        public async Task<List<Cadastro>> GetCadastrosAsync() =>
            await _cadastroCollection.Find(_ => true).ToListAsync();

        // Método para buscar todos os logins
        public async Task<List<Login>> GetLoginsAsync() =>
            await _loginCollection.Find(_ => true).ToListAsync();

        // Método para buscar todos os feedbacks
        public async Task<List<Feedback>> GetFeedbacksAsync() =>
            await _feedbackCollection.Find(_ => true).ToListAsync();

        // Método para criar um novo feedback
        public async Task CreateFeedbackAsync(Feedback newFeedback)
        {
            newFeedback.DataHora = ConvertToSaoPauloTime(DateTime.UtcNow);
            await _feedbackCollection.InsertOneAsync(newFeedback);
        }

        private DateTime ConvertToSaoPauloTime(DateTime utcDateTime)
        {
            TimeZoneInfo saoPauloTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, saoPauloTimeZone);
        }
    }
}