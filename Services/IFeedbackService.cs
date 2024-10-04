using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Models;

namespace UserApi.Services
{
    public interface IFeedbackService
    {
        Task<List<Feedback>> GetAll();
        Task<Feedback?> GetById(string id);
        Task<Feedback> Create(Feedback feedback);
        Task Update(string id, Feedback feedback);
        Task Delete(string id);
    }
}
