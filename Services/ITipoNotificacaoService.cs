using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Models;

namespace UserApi.Services
{
    public interface ITipoNotificacaoService
    {
        Task<List<TipoNotificacao>> GetAll();
        Task<TipoNotificacao?> GetById(string id);
        Task<TipoNotificacao> Create(TipoNotificacao tipoNotificacao);
        Task Update(string id, TipoNotificacao tipoNotificacao);
        Task Delete(string id);
    }
}
