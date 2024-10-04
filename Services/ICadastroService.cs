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

/*

Os métodos definidos na interface são:

Task<List<Cadastro>> GetAll(); - Para obter todos os registros de Cadastro.
Task<Cadastro?> GetById(string id); - Para obter um registro específico de Cadastro pelo id.
Task<Cadastro> Create(Cadastro cadastro); - Para criar um novo registro de Cadastro.
Task Update(string id, Cadastro cadastro); - Para atualizar um registro existente de Cadastro.
Task Delete(string id); - Para deletar um registro de Cadastro pelo id.
*/