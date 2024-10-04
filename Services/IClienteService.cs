using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Models;

namespace UserApi.Services
{
    public interface IClienteService
    {
        Task<List<Cliente>> GetAll();
        Task<Cliente?> GetById(string id);
        Task<Cliente> Create(Cliente cliente);
        Task Update(string id, Cliente cliente);
        Task Delete(string id);
    }
}

/*
Os métodos definidos na interface são:

Task<List<Cliente>> GetAll(); - Para obter todos os registros de Cliente.
Task<Cliente?> GetById(string id); - Para obter um registro específico de Cliente pelo id.
Task<Cliente> Create(Cliente cliente); - Para criar um novo registro de Cliente.
Task Update(string id, Cliente cliente); - Para atualizar um registro existente de Cliente.
Task Delete(string id); - Para deletar um registro de Cliente pelo id.
*/