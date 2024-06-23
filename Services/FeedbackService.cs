using UserApi.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UserApi.Services
{
    public class FeedbackService
    {
        private readonly IMongoCollection<Feedback> _feedbackCollection;

        public FeedbackService(IOptions<UserDatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _feedbackCollection = database.GetCollection<Feedback>(settings.Value.FeedbackCollectionName);
        }

        public async Task<List<Feedback>> GetFeedbacksAsync() =>
            await _feedbackCollection.Find(_ => true).ToListAsync();

        public async Task CreateFeedbackAsync(Feedback newFeedback)
        {
            await _feedbackCollection.InsertOneAsync(newFeedback);
        }
    }
}
