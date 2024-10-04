using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UserApi.Models;

namespace UserApi.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IMongoCollection<Feedback> _feedbackCollection;

        public FeedbackService(IMongoClient mongoClient, IOptions<UserDatabaseSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _feedbackCollection = database.GetCollection<Feedback>(settings.Value.FeedbackCollectionName);
        }

        public async Task<List<Feedback>> GetAll()
        {
            return await _feedbackCollection.Find(f => true).ToListAsync();
        }

        public async Task<Feedback?> GetById(string id)
        {
            return await _feedbackCollection.Find(f => f.id_feedback == id).FirstOrDefaultAsync();
        }

        public async Task<Feedback> Create(Feedback feedback)
        {
            await _feedbackCollection.InsertOneAsync(feedback);
            return feedback;
        }

        public async Task Update(string id, Feedback feedback)
        {
            await _feedbackCollection.ReplaceOneAsync(f => f.id_feedback == id, feedback);
        }

        public async Task Delete(string id)
        {
            await _feedbackCollection.DeleteOneAsync(f => f.id_feedback == id);
        }
    }
}
