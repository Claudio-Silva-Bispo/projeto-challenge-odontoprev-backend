using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Models;

namespace UserApi.Services
{
    public interface ICadastroService
    {
        Task<List<Cadastro>> GetAll();
        Task<Cadastro?> GetById(string id);
        Task<Cadastro> Create(Cadastro cadastro);
        Task Update(string id, Cadastro cadastro);
        Task Delete(string id);
    }
}
