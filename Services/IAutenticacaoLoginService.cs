using System.Threading.Tasks;

namespace UserApi.Services
{
    public interface IAutenticacaoLoginService
    {
        Task<string?> Autenticar(string email, string senha, string tipoLogin);
    }
}