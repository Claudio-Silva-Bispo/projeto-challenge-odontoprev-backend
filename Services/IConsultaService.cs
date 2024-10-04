using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Models;

namespace UserApi.Services
{
    public interface IConsultaService
    {
        Task<List<Consulta>> GetAll();
        Task<Consulta?> GetById(string id);
        Task<Consulta> Create(Consulta consulta);
        Task Update(string id, Consulta consulta);
        Task Delete(string id);
    }
}
