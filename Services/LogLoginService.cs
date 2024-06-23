using MongoDB.Driver;
using System.Threading.Tasks;
using UserApi.Models;
using Microsoft.Extensions.Options;

namespace UserApi.Services
{
    public class LogLoginService
    {
        private readonly IMongoCollection<LogLogin> _logLoginCollection;

        public LogLoginService(IMongoClient mongoClient, IOptions<UserDatabaseSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _logLoginCollection = database.GetCollection<LogLogin>(settings.Value.LoginCollectionName);
        }

        public async Task LogAsync(string email, string tipoLogin)
        {
            var log = new LogLogin
            {
                Email = email,
                TipoLogin = tipoLogin
            };

            await _logLoginCollection.InsertOneAsync(log);
        }
    }
}
