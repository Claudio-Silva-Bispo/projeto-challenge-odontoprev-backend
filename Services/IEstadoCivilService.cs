using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Models;

namespace UserApi.Services
{
    public interface IEstadoCivilService
    {
        Task<List<EstadoCivil>> GetAll();
        Task<EstadoCivil?> GetById(string id);
        Task<EstadoCivil> Create(EstadoCivil estadoCivil);
        Task Update(string id, EstadoCivil estadoCivil);
        Task Delete(string id);
    }
}
