using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Models;

namespace UserApi.Services
{
    public interface IDentistaService
    {
        Task<List<Dentista>> GetAll();
        Task<Dentista?> GetById(string id);
        Task<Dentista> Create(Dentista dentista);
        Task Update(string id, Dentista dentista);
        Task Delete(string id);
    }
}
