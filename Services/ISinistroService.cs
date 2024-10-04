using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Models;

namespace UserApi.Services
{
    public interface ISinistroService
    {
        Task<List<Sinistro>> GetAll();
        Task<Sinistro?> GetById(string id);
        Task<Sinistro> Create(Sinistro sinistro);
        Task Update(string id, Sinistro sinistro);
        Task Delete(string id);
    }
}
