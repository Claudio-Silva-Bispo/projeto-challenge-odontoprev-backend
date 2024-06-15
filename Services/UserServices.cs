using UserApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace UserApi.Services;

public class UserService
{
    private readonly IMongoCollection<User> _UserCollection;

    public UserService(
        IOptions<UserDatabaseSettings> UserDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            UserDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            UserDatabaseSettings.Value.DatabaseName);

        _UserCollection = mongoDatabase.GetCollection<User>(
            UserDatabaseSettings.Value.UsersCollectionName);
    }

    public async Task<List<User>> GetAsync() =>
        await _UserCollection.Find(_ => true).ToListAsync();

    public async Task<User?> GetAsync(string id) =>
        await _UserCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(User newUser) =>
        await _UserCollection.InsertOneAsync(newUser);

    public async Task UpdateAsync(string id, User updatedUser) =>
        await _UserCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

    public async Task RemoveAsync(string id) =>
        await _UserCollection.DeleteOneAsync(x => x.Id == id);
}