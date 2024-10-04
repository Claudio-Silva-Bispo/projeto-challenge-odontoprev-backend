using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Models;

namespace UserApi.Services
{
    public interface IFormularioDetalhadoService
    {
        Task<List<FormularioDetalhado>> GetAll();
        Task<FormularioDetalhado?> GetById(string id);
        Task<FormularioDetalhado> Create(FormularioDetalhado formularioDetalhado);
        Task Update(string id, FormularioDetalhado formularioDetalhado);
        Task Delete(string id);
    }
}
