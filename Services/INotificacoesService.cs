using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Models;

namespace UserApi.Services
{
    public interface INotificacoesService
    {
        Task<List<Notificacoes>> GetAll();
        Task<Notificacoes?> GetById(string id);
        Task<Notificacoes> Create(Notificacoes notificacoes);
        Task Update(string id, Notificacoes notificacoes);
        Task Delete(string id);
    }
}
