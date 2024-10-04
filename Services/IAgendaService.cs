using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Models;

namespace UserApi.Services
{
    public interface IAgendaService
    {
        Task<List<Agenda>> GetAll();
        Task<Agenda?> GetById(string id);
        Task<Agenda> Create(Agenda agenda);
        Task Update(string id, Agenda agenda);
        Task Delete(string id);
    }
}
