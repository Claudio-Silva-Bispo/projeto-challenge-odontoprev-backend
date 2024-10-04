using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Models;

namespace UserApi.Services
{
    public interface IClinicaService
    {
        Task<List<Clinica>> GetAll();
        Task<Clinica?> GetById(string id);
        Task<Clinica> Create(Clinica clinica);
        Task Update(string id, Clinica clinica);
        Task Delete(string id);
    }

}
